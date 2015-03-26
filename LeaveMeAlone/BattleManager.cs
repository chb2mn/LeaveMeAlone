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

        public static List<Character> heroes = new List<Character>();
        public static List<Text> hero_hp = new List<Text>();
        public static List<Rectangle> heroLoc = new List<Rectangle>();

        public static Character boss;
        public static Rectangle bossLoc;
        public static Texture2D back;
        public static Rectangle backLoc;


        //TODO change to class
        private static Button[] buttons = new Button[4];
        private static Texture2D buttonLocPic;
        private static Rectangle[] buttonLoc = new Rectangle[4];
        private static Rectangle[] skillLoc = new Rectangle[6];
        private static Text[] button_text = new Text[6];
        private static Text damage_text;
        //this is awful
        private static int textx;
        private static int texty;

        private static Text victory_text;
        private static bool victory;
        private static bool defeat;
        private static Text defeat_text;
        private static Texture2D next_button;
        private static Rectangle nextRect;

        //debug string
        private static Text message = new Text("");

        private static bool menu_change_in_progress = false;
        private static int animation_counter = 0;

        
        private static int enemy_attack_delay = 120;
        private static int enemy_turn = 0;
        private enum State { Basic, Skills, Bribe, Target, Attack, Endgame, EnemyTurn }
        private static State state;


        //TODO figure out if this stuff needs to be numbers or can be Characters
        private static int hovered_enemy = -1;
        private static int targeted_enemy = -1;
        private static int current_enemy = 0;
        private static Skill selected_skill;
       
        public static void Init(ContentManager Content)
        {
            int button_basex = 100;
            int button_basey = 350;

            buttonLoc[0] = new Rectangle(button_basex, button_basey, 250, 50);
            buttonLoc[1] = new Rectangle(button_basex + 300, button_basey, 250, 50);
            buttonLoc[2] = new Rectangle(button_basex, button_basey + 60, 250, 50);
            buttonLoc[3] = new Rectangle(button_basex + 300, button_basey + 60, 250, 50);
            skillLoc[0] = new Rectangle(button_basex - 75, button_basey, 200, 50);
            skillLoc[1] = new Rectangle(button_basex - 75, button_basey + 60, 200, 50);
            skillLoc[2] = new Rectangle(button_basex + 140, button_basey, 200, 50);
            skillLoc[3] = new Rectangle(button_basex + 140, button_basey + 60, 200, 50);
            skillLoc[4] = new Rectangle(button_basex + 350, button_basey, 200, 50);
            skillLoc[5] = new Rectangle(button_basex + 350, button_basey + 60, 200, 50);

            textx = button_basex + 60;
            texty = button_basey;


            buttonLocPic = Content.Load<Texture2D>("buttonbase");
            button_text[0] = new Text("Attack");
            button_text[1] = new Text("Skills");
            button_text[2] = new Text("Defend");
            button_text[3] = new Text("Bribe");
            button_text[4] = new Text("");
            button_text[5] = new Text("");


            bossLoc = new Rectangle(650, 120, 100, 100);
            boss_hp = new Text("");
            boss_energy = new Text("");


            int hero_basex = 50;
            int hero_basey = 150;
            heroLoc.Add(new Rectangle(hero_basex, hero_basey, 50, 50));
            heroLoc.Add(new Rectangle(hero_basex, hero_basey - 60, 50, 50));
            heroLoc.Add(new Rectangle(hero_basex, hero_basey + 60, 50, 50));
            heroLoc.Add(new Rectangle(hero_basex, hero_basey + 120, 50, 50));
            for (int i = 0; i < 4; i++)
            {
                    Text hptext = new Text("");
                    hero_hp.Add(hptext);
            }


            back = Content.Load<Texture2D>("back");
            backLoc = new Rectangle(675, 410, 113, 51);

            Skill s = new Skill("Basic attack", 0, 0, 1, 0, Skill.Target.Single, 0, "Basic attack", Character.BasicAttack);
            boss.addSkill(s);

            victory_text = new Text("Victory!\nWe will survive another day!");
            defeat_text = new Text("Defeat\nYour friends will be so embarrased with you");
            next_button = Content.Load<Texture2D>("Next");
            nextRect = new Rectangle(325, 300, 113, 32);
        }
        public static void Attack(Character caster)
        {
            //message.changeMessage(""+(heroes[target].health));

            //targeted_enemy is our target
            //selected_skill is our skill
            if (targeted_enemy >= 0)
            {
                
                caster.cast(selected_skill, heroes[targeted_enemy]);
            }
            else if (targeted_enemy == -1)
            {
                caster.cast(selected_skill);
            }
            //For enemy turns
            else if (targeted_enemy == -2)
            {
                heroes[current_enemy].cast(selected_skill, boss);
            }
            //Do damage and send state to enemy turn
            //Update texts
            for (int i = 0; i < heroes.Count(); i++)
            {
                hero_hp[i].changeMessage(heroes[i].health.ToString() + "/" + heroes[i].max_health.ToString());
            }
            boss_hp.changeMessage(boss.health.ToString() + "/" + boss.max_health.ToString());
            boss_hp.changeMessage(boss.energy.ToString() + "/" + boss.energy.ToString());


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

            for (int i = 0; i < 4; i++)
            {
                if (heroLoc[i].Contains(selectLocX, selectLocY))
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
                hovered_enemy = -1;
            }
            //return no target if no target has been selected
            return -1;
        }
        public static void CheckVictoryDefeat()
        {
            victory = true;
            defeat = false;
            foreach (Character hero in heroes)
            {
                if (hero.health > 0)
                {
                    victory = false;
                }
            }
            if (boss.health <= 0)
            {
                defeat = true;
            }
        }

        private static void NewMenu(int menu)
        {
            menu_change_in_progress = true;
            if (victory)
            {
                return;
            }
            switch (menu)
            {
                case 0:
                    state = 0;
                    button_text[0].changeMessage("Attack");
                    button_text[1].changeMessage("Skills");
                    button_text[2].changeMessage("Defend");
                    button_text[3].changeMessage("Bribe");
                    break;
                case 1:
                    //Skills Menu
                    state = State.Skills;
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            button_text[i].changeMessage(boss.selected_skills[i].name);
                        }
                        catch
                        {
                            button_text[i].changeMessage("NONE");
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

        public static Game1.GameState Update(GameTime gametime)

        {
            //If the mouse is released we can continue taking new input
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                menu_change_in_progress = false;
            }
            switch (state)
            {
                case State.Basic:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !menu_change_in_progress)
                    {
                        int selectLocX = Mouse.GetState().X;
                        int selectLocY = Mouse.GetState().Y;
                        if (buttonLoc[1].Contains(selectLocX, selectLocY))
                        {
                            //Go toSkill menu
                            NewMenu(1);
                        }
                        else if (buttonLoc[3].Contains(selectLocX, selectLocY))
                        {
                            //Go to Bribe menu
                            NewMenu(2);
                        }
                        else if (buttonLoc[0].Contains(selectLocX, selectLocY))
                        {
                            //TODO: need a way to select basic attack
                            //selected_skill = Skill.Attack;
                            
                            state = State.Target;
                        }
                        else if (buttonLoc[2].Contains(selectLocX, selectLocY))
                        {
                            //TODO: need a way to select taunt
                            selected_skill = boss.skills[1];
                            targeted_enemy = -2; //Don't need this
                            state = State.Attack;
                        }
                    }
                    break;
                case State.Skills:
                    //Skill Selection
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !menu_change_in_progress)
                    {
                        int selectLocX = Mouse.GetState().X;
                        int selectLocY = Mouse.GetState().Y;

                        message.changeMessage(selectLocX + ", " + selectLocY);
                        for (int i = 0; i < 4; i++)
                        {
                            if (buttonLoc[i].Contains(selectLocX, selectLocY))
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
                        }
                        if (backLoc.Contains(selectLocX, selectLocY))
                        {
                                    NewMenu(0);
                                    state = 0;
                        }
                            
                    }
                    break;
                case State.Bribe:
                    //Bribe Stuff
                    
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

                        if (backLoc.Contains(selectLocX, selectLocY))
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
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed && !menu_change_in_progress)
                    {
                        int selectLocX = Mouse.GetState().X;
                        int selectLocY = Mouse.GetState().Y;
                        if (nextRect.Contains(selectLocX, selectLocY))
                        {
                            if (victory) {
                                //Do next battle
                                //Go to next (Upgrade) menu
                                PartyManager.PartyNum++;
                                return Game1.GameState.Upgrade;
                            }
                            else if (defeat)
                            {
                                //Restart battle

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

                    enemy_attack_delay = 120;
                    
                    Character enemy = heroes[enemy_turn];
                    //AI occurs
                    targeted_enemy = -2;
                    //selected_skill = enemy.Attack;
                    Attack(enemy);

                    enemy_turn++;
                    //Check if end of enemy turn;
                    if (enemy_turn >= heroes.Count()){
                        state = 0;
                        enemy_turn = 0;
                    }
                    //Check after each Enemy
                    CheckVictoryDefeat();
                    break;
            }
            return Game1.GameState.Battle;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            //Do Background drawing
            //Draw Heroes
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (i == hovered_enemy)
                    {
                        spriteBatch.Draw(heroes[i].sprite, heroLoc[i], Color.Violet);
                        //status too
                    }
                    else
                    {
                        
                        spriteBatch.Draw(heroes[i].sprite, heroLoc[i], Color.White);
                        //status too
                    }
                    hero_hp[i].draw(spriteBatch, heroLoc[i].Location.X + 50, heroLoc[i].Location.Y + 30);
                }
                catch
                {
                    //dead/KO animation
                }
                spriteBatch.Draw(boss.sprite, bossLoc, Color.White);
                boss_hp.draw(spriteBatch, bossLoc.Location.X, bossLoc.Location.Y+100);
                boss_energy.draw(spriteBatch, bossLoc.Location.X, bossLoc.Location.Y + 120);

                //status too
            }

            //Check if we have victory
            if (victory)
            {
                victory_text.draw(spriteBatch, 300, 250);
                spriteBatch.Draw(next_button, nextRect, Color.White);
                return;
            }
            else if (defeat)
            {
                defeat_text.draw(spriteBatch, 300, 250);
                spriteBatch.Draw(next_button, nextRect, Color.White);
                return;
            }

            //Draw Buttons
            if (state == State.Basic)
            {
                for (int i = 0; i < 4; i++)
                {
                    spriteBatch.Draw(buttonLocPic, buttonLoc[i], Color.White);
                }
                button_text[0].draw(spriteBatch, textx, texty);
                button_text[1].draw(spriteBatch, textx + 300, texty);
                button_text[2].draw(spriteBatch, textx, texty+60);
                button_text[3].draw(spriteBatch, textx + 300, texty+60);


            }
            if (state == State.Skills)
            {
                for (int i = 0; i < 6; i++)
                {
                    spriteBatch.Draw(buttonLocPic, skillLoc[i], Color.White);
                }
                button_text[0].draw(spriteBatch, textx - 75, texty);
                button_text[1].draw(spriteBatch, textx - 75, texty + 60);
                button_text[2].draw(spriteBatch, textx + 140, texty);
                button_text[3].draw(spriteBatch, textx + 140, texty + 60 );
                button_text[4].draw(spriteBatch, textx + 350, texty);
                button_text[5].draw(spriteBatch, textx + 350, texty + 60);
            }

            if (state == State.Skills || state == State.Bribe || state == State.Target)
            {
                spriteBatch.Draw(back, backLoc, Color.White);
            }


            message.draw(spriteBatch, 0, 0);
        }
    }
}
