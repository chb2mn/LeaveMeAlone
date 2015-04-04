#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Linq;
using System.Text;
#endregion

namespace LeaveMeAlone
{
    public class BattleManager
    {

        public static Text boss_hp;
        public static Text boss_energy;

        public static List<Character> heroes = new List<Character>(4);
        public static List<Text> hero_hp = new List<Text>(4);
        public static List<Rectangle> heroLoc = new List<Rectangle>();

        public static Dictionary<Character.Knowledge, bool> Knowledge = new Dictionary<Character.Knowledge,bool>();


        public static Character boss;
        public static Rectangle bossLoc;

        private static Button[] basic_buttons = new Button[4];
        private static Button[] skill_buttons = new Button[6];
        private static Texture2D buttonLocPic;
        private static Texture2D bkgd;
        private static Texture2D targeter;
        private static Text target_text;

        private static Text victory_text;
        private static bool victory;
        private static bool defeat;
        private static Text defeat_text;

        private static Button next_button;
        private static Button back_button;

        //debug string
        private static Text message; 

        private static bool left_click = false;
        private static bool right_click = false;
        private static int animation_counter = 30;


        private static int enemy_attack_delay = 30;
        private static int enemy_turn = 0;
        private enum State { Basic, Skills, Bribe, Target, Attack, Endgame, EnemyTurn }
        private static State state;


        //TODO figure out if this stuff needs to be numbers or can be Characters
        private static int hovered_enemy = -1;
        private static int targeted_enemy = -1;
        private static Skill selected_skill;

       
        //------------Bribe Stuff------------//
        private static Button[] bribe_amounts = new Button[4];
        private static Button total_amount;
        private static Button my_amount;
        private static int bribe_gold;


        public static void LoadContent(ContentManager Content)
        {


            bkgd = Content.Load<Texture2D>("skyscraperBkgd");
            int button_basex = 100;
            int button_basey = 350;

            //remove all knowledge that the enemy heroes have
            Knowledge.Clear();

            buttonLocPic = Content.Load<Texture2D>("buttonbase");
            targeter = Content.Load<Texture2D>("Target");
            target_text = new Text("Select Target");

            basic_buttons[0] = new Button(buttonLocPic, button_basex, button_basey, 250, 50);
            basic_buttons[1] = new Button(buttonLocPic, button_basex + 300, button_basey, 250, 50);
            basic_buttons[2] = new Button(buttonLocPic, button_basex, button_basey + 60, 250, 50);
            basic_buttons[3] = new Button(buttonLocPic, button_basex + 300, button_basey + 60, 250, 50);

            skill_buttons[0] = new Button(buttonLocPic, button_basex - 75, button_basey, 200, 50);
            skill_buttons[1] = new Button(buttonLocPic, button_basex - 75, button_basey + 60, 200, 50);
            skill_buttons[2] = new Button(buttonLocPic, button_basex + 140, button_basey, 200, 50);
            skill_buttons[3] = new Button(buttonLocPic, button_basex + 140, button_basey + 60, 200, 50);
            skill_buttons[4] = new Button(buttonLocPic, button_basex + 350, button_basey, 200, 50);
            skill_buttons[5] = new Button(buttonLocPic, button_basex + 350, button_basey + 60, 200, 50);

            basic_buttons[0].UpdateText("Attack");
            basic_buttons[1].UpdateText("Skills");
            basic_buttons[2].UpdateText("Defend");
            basic_buttons[3].UpdateText("Bribe");


            bossLoc = new Rectangle(650, 120, 100, 100); 
            boss_hp = new Text("");
            boss_energy = new Text("");

            for (int i = 0; i < 4; i++)
            {
                Text hptext = new Text("");
                hero_hp.Add(hptext);
            }


            back_button = new Button(Content.Load<Texture2D>("Back"), 675, 410, 113, 51);

            victory_text = new Text("Victory!\nWe will survive another day!");
            defeat_text = new Text("Defeat\nYour friends will be so embarrased with you");

            next_button = new Button(Content.Load<Texture2D>("Next"), 325, 100, 113, 32);

            //---Bribe Stuff---//
            bribe_gold = 0;
            bribe_amounts[0] = new Button(buttonLocPic, button_basex, button_basey, 250, 50);
            bribe_amounts[1] = new Button(buttonLocPic, button_basex + 300, button_basey, 250, 50);
            bribe_amounts[2] = new Button(buttonLocPic, button_basex, button_basey + 60, 250, 50);
            bribe_amounts[3] = new Button(buttonLocPic, button_basex + 300, button_basey + 60, 250, 50);
            for (int i = 0; i < 4; i += 1)
            {
                bribe_amounts[i].UpdateText((Math.Pow(10,i+1)).ToString());
            }

            total_amount = new Button(buttonLocPic, button_basex + 300, button_basey - 60, 200, 50);
            my_amount = new Button(buttonLocPic, button_basex + 50, button_basey - 60, 200, 50);

        }

        public static void Init(Character.Type t)
        {
            message = new Text(Text.fonts["Arial-12"], Color.Black, "");
            boss.sPosition = new Vector2(bossLoc.X - 20, bossLoc.Y + 20);
            victory = false;
            defeat = false;
            state = State.Basic;
            boss.health = boss.max_health;
            boss.energy = boss.max_energy;
            boss_hp.changeMessage(boss.health.ToString() + "/" + boss.max_health.ToString());
            boss_energy.changeMessage(boss.energy.ToString() + "/" + boss.max_energy.ToString());
            NewMenu(0);

            total_amount.UpdateText("How Much?: 0");
            my_amount.UpdateText("My Total: " + Resources.gold.ToString());
        }

        public static void Apply_Status(Character affected, Status.Effect_Time effect_time)
        {
            //iterating through the list backwards allows us to properly remove them from the list (it auto-concatenates after every removal)
            for (int i = affected.statuses.Count() - 1; i >= 0; i--)
            {
                Status status = affected.statuses[i];
                //If the effect is a one time, increment the counter and move on
                if (effect_time == Status.Effect_Time.Once)
                {
                    status.duration_left--;
                    if (status.duration_left == 0)
                    {
                        //reverse the affect and remove the status
                        status.reverse_affect(affected);
                        affected.statuses.Remove(status);
                    }
                }
                //If the effect is not one time, do the effect and increment counter
                else if (effect_time == status.effect_time)
                {
                    status.affect(affected);
                    //Whenever the status is triggered, check if the status should be removed
                    if (status.duration_left-- == 0)
                    {
                        affected.statuses.Remove(status);
                    }
                }

            }
        }


        public static void Attack(Character caster)
        {
            //targeted_enemy is our target
            //selected_skill is our skill

            //Initiate animation
            caster.attackAnimation();

            //Check if targeted_enemy is within the party size
            if (targeted_enemy >= heroes.Count())
            {
                state = State.Target;
                return;
            }

            if (targeted_enemy >= 0)
            {
                //if hero is dead, ignore
                if (heroes[targeted_enemy] == null)
                {
                    state = State.Target;
                    return;
                }
                Apply_Status(caster, Status.Effect_Time.Before);
                caster.cast(selected_skill, heroes[targeted_enemy]);
            }

            

            else if (targeted_enemy == -1)
            {
                Apply_Status(caster, Status.Effect_Time.Before);
                caster.cast(selected_skill);
            }
            //For enemy turns
            else if (targeted_enemy == -2)
            {
                Apply_Status(caster, Status.Effect_Time.Before);
                heroes[enemy_turn].cast(selected_skill, boss);
            }
            //apply affects for after the attack
            Apply_Status(caster, Status.Effect_Time.After);

            //check the duration remaining on once effects
            Apply_Status(caster, Status.Effect_Time.Once);

            //Do damage and send state to enemy turn
            //Update texts
            for (int i = 0; i < heroes.Count(); i++)
            {
                if (heroes[i] == null) { continue; }
                hero_hp[i].changeMessage(heroes[i].health.ToString() + "/" + heroes[i].max_health.ToString());
            }
            boss_hp.changeMessage(boss.health.ToString() + "/" + boss.max_health.ToString());
            boss_energy.changeMessage(boss.energy.ToString() + "/" + boss.max_energy.ToString());

            //update the state to pass the turn to enemies
            state = State.EnemyTurn;
            //Check after the Boss goes
            CheckVictoryDefeat();
        }

        /*
         * Targetting menu, it will return the int of the target or -1 if no target
         */
        public static int Target()
        {
            int selectLocX = Mouse.GetState().X;
            int selectLocY = Mouse.GetState().Y;

            bool any_target = false;

            for (int i = 0; i < heroes.Count(); i++)
            {
                if (heroLoc[i].Contains(selectLocX, selectLocY) && heroes[i] != null)
                {
                    hovered_enemy = i;
                    any_target = true;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        return hovered_enemy;
                    }
                }
            }
            if (bossLoc.Contains(selectLocX, selectLocY))
            {
                hovered_enemy = -2;
                any_target = true;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    return hovered_enemy;
                }

            }
            if (!any_target)
            {
                Console.WriteLine("None found");
                hovered_enemy = -1;
            }
            //return no target if no target has been selected
            return -1;
        }
        public static void CheckVictoryDefeat()
        {
            victory = true;
            defeat = false;
            Character hero;
            for (int i = 0; i < heroes.Count; i++)
            {
                hero = heroes[i];
                if (hero == null)
                {
                    continue;
                }
                if (hero.health > 0)
                {
                    victory = false;
                }
                else
                {
                    Console.WriteLine("Removing Enemy: " + i + " At health: " + hero.health);
                    Resources.gold += heroes[i].gold;
                    Resources.exp += heroes[i].exp;
                    heroes[i] = null;

                    //Reward the boss
                }
            }
            if (boss.health <= 0)
            {
                defeat = true;
            }
            if (victory || defeat)
            {
                state = State.Endgame;
            }
        }

        private static void NewMenu(int menu)
        {
            left_click = true;
            if (victory)
            {
                return;
            }
            switch (menu)
            {
                case 0:
                    state = 0;
                    break;
                case 1:
                    //Skills Menu
                    state = State.Skills;
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            skill_buttons[i].UpdateText(boss.selected_skills[i].name);
                        }
                        catch
                        {
                            skill_buttons[i].UpdateText("NONE");
                        }
                    }
                    break;
                case 2:
                    //Bribe Menu
                    state = State.Bribe;
                    break;
                default:
                    //When will we need this?
                    {
                        state = State.Basic;
                    }
                    break;
            }
        }

        public static LeaveMeAlone.GameState Update(GameTime gametime)
        {
            //If the mouse is released we can continue taking new input
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                left_click = false;
            }
            if (Mouse.GetState().RightButton == ButtonState.Released)
            {
                right_click = false;
            }
            switch (state)
            {
                case State.Basic:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !left_click)
                    {
                        int selectLocX = Mouse.GetState().X;
                        int selectLocY = Mouse.GetState().Y;
                        if (basic_buttons[1].Intersects(selectLocX, selectLocY))
                        {
                            //Go toSkill menu
                            NewMenu(1);
                        }
                        else if (basic_buttons[3].Intersects(selectLocX, selectLocY))
                        {
                            //Go to Bribe menu
                            NewMenu(2);
                        }
                        else if (basic_buttons[0].Intersects(selectLocX, selectLocY))
                        {
                            //TODO: need a way to select basic attack
                            selected_skill = boss.basic_attack;

                            state = State.Target;
                        }
                        else if (basic_buttons[2].Intersects(selectLocX, selectLocY))
                        {
                            //TODO: need a way to select taunt
                            selected_skill = boss.defend;
                            targeted_enemy = -1; //Don't need this
                            state = State.Attack;
                        }
                    }
                    break;
                case State.Skills:
                    //Skill Selection
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !left_click)
                    {
                        int selectLocX = Mouse.GetState().X;
                        int selectLocY = Mouse.GetState().Y;

                        message.changeMessage(selectLocX + ", " + selectLocY);
                        for (int i = 0; i < 6; i++)
                        {
                            if (skill_buttons[i].Intersects(selectLocX, selectLocY))
                            {
                                try
                                {
                                    selected_skill = boss.selected_skills[i];
                                    if (selected_skill.target == Skill.Target.Single)
                                    {
                                        state = State.Target;
                                    }
                                    else
                                    {
                                        state = State.Attack;
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                        if (back_button.Intersects(selectLocX, selectLocY))
                        {
                            NewMenu(0);
                            state = 0;
                        }

                    }
                    break;
                case State.Bribe:
                    //Bribe Stuff
                    
                    if (Mouse.GetState().RightButton == ButtonState.Pressed && !right_click)
                    {
                        int selectLocX = Mouse.GetState().X;
                        int selectLocY = Mouse.GetState().Y;
                        for (int i = 0; i < 4; i++)
                        {
                            if (bribe_amounts[i].Intersects(selectLocX, selectLocY))
                            {
                                bribe_gold -= (int)Math.Pow(10, i + 1);
                                total_amount.UpdateText("How Much?: " + bribe_gold.ToString());
                                right_click = true;
                            }
                        }
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !left_click)
                    {
                        int selectLocX = Mouse.GetState().X;
                        int selectLocY = Mouse.GetState().Y;
                        for (int i = 0; i < 4; i++)
                        {
                            if (bribe_amounts[i].Intersects(selectLocX, selectLocY))
                            {
                                bribe_gold += (int) Math.Pow(10, i+1);
                                total_amount.UpdateText("How Much?: " + bribe_gold.ToString());
                                left_click = true;
                            }
                        }
                        if (back_button.Intersects(selectLocX, selectLocY))
                        {
                            NewMenu(0);
                            state = 0;
                            bribe_gold = 0;
                            total_amount.UpdateText("How Much?: 0");
                        }
                    }
                        //Send bribe target at enemy
                    targeted_enemy = Target();
                    if (targeted_enemy >= 0)
                    {
                        if (heroes[targeted_enemy].gold <= bribe_gold && Resources.gold >= bribe_gold)
                        {
                            //remove hero
                            heroes[targeted_enemy] = null;
                            Resources.gold -= bribe_gold;
                            my_amount.UpdateText("My Total: " + Resources.gold);
                            CheckVictoryDefeat();
                        }
                        else
                        {
                            NewMenu(0);
                            state = 0;
                            hovered_enemy = -1;

                        }
                        bribe_gold = 0;
                        total_amount.UpdateText("How Much?: 0");
                    }
                    break;
                case State.Target:
                    //Targetting

                    //highlighted needs to be separate from targeted enemy because target ensure that we have clicked on something
                    targeted_enemy = Target();
                    if (targeted_enemy != -1)
                    {

                        state = State.Attack;
                        hovered_enemy = -1;
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        int selectLocX = Mouse.GetState().X;
                        int selectLocY = Mouse.GetState().Y;

                        if (back_button.Intersects(selectLocX, selectLocY))
                        {
                            NewMenu(0);
                        }
                    }

                    break;
                case State.Attack:
                    //Attacking
                    Attack(boss);
                    break;
                case State.Endgame:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !left_click)
                    {
                        int selectLocX = Mouse.GetState().X;
                        int selectLocY = Mouse.GetState().Y;
                        if (next_button.Intersects(selectLocX, selectLocY))
                        {
                            if (victory)
                            {
                                //Do next battle
                                //Go to next (Upgrade) menu
                                PartyManager.PartyNum++;
                                MainMenu.init();
                                victory = false;
                                BattleManager.Init(boss.charType);
                                return LeaveMeAlone.GameState.Lair;
                            }
                            else if (defeat)
                            {
                                //Restart battle
                                MainMenu.init();
                                return LeaveMeAlone.GameState.Main;

                            }
                        }
                    }
                    break;
                case State.EnemyTurn:
                    //Enemy Turn
                    //Wait to allow the user to see what's happening
                    if (enemy_attack_delay > 0)
                    {
                        enemy_attack_delay--;
                        break;
                    }
                    if (enemy_turn >= heroes.Count())
                    {
                        state = State.Basic;
                        NewMenu(0);
                        enemy_turn = 0;
                        targeted_enemy = -1;
                        CheckVictoryDefeat();
                        break;
                    }


                    Character enemy = heroes[enemy_turn];
                    if (enemy == null)
                    {
                        enemy_turn++;
                        break;
                    }
                    enemy_attack_delay = 31;

                    //AI occurs
                    targeted_enemy = -2;
                    selected_skill = enemy.basic_attack;
                    Attack(enemy);

                    enemy_turn++;
                    //Check if end of enemy turn;
                    if (enemy_turn >= heroes.Count())
                    {
                        state = State.Basic;
                        NewMenu(0);
                        enemy_turn = 0;
                        targeted_enemy = -1;
                    }
                    //Check after each Enemy
                    CheckVictoryDefeat();
                    break;
            }

            for (int i = 0; i < heroes.Count(); i++)
            {
                if (heroes[i] == null) { continue; }
                heroes[i].Update(gametime);
            }
            boss.Update(gametime);
            boss_hp.changeMessage(BattleManager.boss.health.ToString() + "/" + BattleManager.boss.max_health.ToString());
            boss_energy.changeMessage(BattleManager.boss.energy.ToString() + "/" + BattleManager.boss.energy.ToString());
            return LeaveMeAlone.GameState.Battle;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            //Do Background drawing

            spriteBatch.Draw(bkgd, new Rectangle(-100, -25, 1000, 543), Color.White);
            //Draw Heroes
            Console.WriteLine("State: " + state.ToString() + " Hovered Enemy: "+hovered_enemy);
            for (int i = 0; i < heroes.Count(); i++)
            {
                try
                {
                    if (i == hovered_enemy)
                    {
                        heroes[i].Draw(spriteBatch, Color.Violet);
                        if (state == State.Target || state == State.Bribe)
                        {
                            target_text.draw(spriteBatch, 200, 180);
                            spriteBatch.Draw(targeter, new Vector2(heroLoc[i].Location.X + 45, heroLoc[i].Location.Y), Color.Red);
                        }
                    }
                    else
                    {
                        heroes[i].Draw(spriteBatch, Color.White);
                        if (state == State.Target || state == State.Bribe)
                        {
                            target_text.draw(spriteBatch, 200, 180);
                            spriteBatch.Draw(targeter, new Vector2(heroLoc[i].Location.X + 45, heroLoc[i].Location.Y), Color.Black);
                        }
                    }
                    hero_hp[i].draw(spriteBatch, heroLoc[i].Location.X, heroLoc[i].Location.Y + 30);

                    if (!heroes[i].damage_text.message.Equals(""))
                    {
                        if (animation_counter-- >= 0)
                        {
                            heroes[i].damage_text.draw(spriteBatch, heroLoc[i].Location.X, heroLoc[i].Location.Y - 20 + animation_counter / 3);
                        }
                        else
                        {
                            animation_counter = 50;
                            heroes[i].damage_text.changeMessage("");
                        }
                    }
                    else
                    {
                        //Console.WriteLine("This is what is there: " + heroes[i].damage_text.message);
                    }
                }
                catch (NullReferenceException)
                {
                    //dead/KO animation
                }

            }

            boss.Draw(spriteBatch, Color.White);
            boss_hp.draw(spriteBatch, bossLoc.Location.X, bossLoc.Location.Y + 100);
            boss_energy.draw(spriteBatch, bossLoc.Location.X, bossLoc.Location.Y + 120);
            if (!boss.damage_text.message.Equals(""))
            {
                if (animation_counter-- >= 0)
                {
                    boss.damage_text.draw(spriteBatch, bossLoc.Location.X, bossLoc.Location.Y - 20 + animation_counter / 3);
                }
                else
                {
                    animation_counter = 25;
                    boss.damage_text.changeMessage("");
                }
            }

            //Check if we have victory
            if (victory)
            {
                victory_text.draw(spriteBatch, 300, 50);
                next_button.Draw(spriteBatch);
                return;
            }
            else if (defeat)
            {
                defeat_text.draw(spriteBatch, 300, 50);
                next_button.Draw(spriteBatch);
                return;
            }

            //Draw Buttons
            if (state == State.Basic)
            {
                for (int i = 0; i < 4; i++)
                {
                    basic_buttons[i].Draw(spriteBatch);
                }


            }
            else if (state == State.Skills)
            {
                for (int i = 0; i < 6; i++)
                {
                    skill_buttons[i].Draw(spriteBatch);
                }
            }

            else if (state == State.Bribe)
            {
                for (int i = 0; i < 4; i++)
                {
                    bribe_amounts[i].Draw(spriteBatch);
                }
                total_amount.Draw(spriteBatch);
                my_amount.Draw(spriteBatch);
            }

            if (state == State.Skills || state == State.Bribe || state == State.Target)
            {
                back_button.Draw(spriteBatch);
            }


            message.draw(spriteBatch, 0, 0);
        }

        public static void bossDefaultPosition()
        {
            boss.sPosition = new Vector2(640, 140);
        }
        public static void setHeroesPosition()
        {
            for (int i = 0; i < heroes.Count(); i++)
            {
                heroes[i].sPosition = new Vector2(heroLoc[i].X + 20, heroLoc[i].Y);
            }
        }
    }
}
