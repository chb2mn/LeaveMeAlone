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

        //TODO figure out how to make this less about the boss
        public static Character Boss;
        public static List<Character> heroes = new List<Character>();
        public static List<Rectangle> heroLoc = new List<Rectangle>();

        public static Character boss;
        public static Rectangle bossLoc;
        public static Texture2D back;
        public static Rectangle backLoc;


        //TODO change to class
        private static Button[] buttons = new Button[4];
        private static Texture2D buttonLocPic;
        private static Rectangle[] buttonLoc = new Rectangle[4];
        private static Text[] button_text = new Text[4];
        
        private static Text damage_text;
        //this is awful
        private static int textx;
        private static int texty;

        private static Text victory_text;
        private static bool victory;
        private static bool defeat;

        //debug string
        private static Text message = new Text("");

        private static bool menu_change_in_progress = false;
        private static int animation_counter = 0;
        //TODO Change state to an enum for easier understanding
        private static int state = 0;
        //TODO figure out if this stuff needs to be numbers or can be Characters
        private static int hovered_enemy = -1;
        private static int target = -1;
        private static Skill selected_skill;

        
        //0 == Basic Menu
        //1 == Skills Menu
        //2 == Bribe Menu
        //5 == targeting
        //6 == attacking
        //10 == Enemy turn
        public static void Init(ContentManager Content)
        {
            int button_basex = 100;
            int button_basey = 350;

            buttonLoc[0] = new Rectangle(button_basex, button_basey, 250, 50);
            buttonLoc[1] = new Rectangle(button_basex + 300, button_basey, 250, 50);
            buttonLoc[2] = new Rectangle(button_basex, button_basey + 60, 250, 50);
            buttonLoc[3] = new Rectangle(button_basex + 300, button_basey + 60, 250, 50);
            textx = button_basex + 60;
            texty = button_basey;

            buttonLocPic = Content.Load<Texture2D>("buttonbase");
            button_text[0] = new Text("Attack");
            button_text[1] = new Text("Skills");
            button_text[2] = new Text("Defend");
            button_text[3] = new Text("Bribe");

            bossLoc = new Rectangle(650, 120, 100, 100);

            int hero_basex = 50;
            int hero_basey = 150;
            heroLoc.Add(new Rectangle(hero_basex, hero_basey, 50, 50));
            heroLoc.Add(new Rectangle(hero_basex, hero_basey - 60, 50, 50));
            heroLoc.Add(new Rectangle(hero_basex, hero_basey + 60, 50, 50));
            heroLoc.Add(new Rectangle(hero_basex, hero_basey + 120, 50, 50));

            back = Content.Load<Texture2D>("back");
            backLoc = new Rectangle(675, 410, 113, 51);
            Skill s = new Skill("Basic attack", 0, 0, 1, 0, Skill.Target.Single, 0, "Basic attack", Character.BasicAttack);
            boss.addSkill(s);

        }
        public static void Attack(Character caster, Skill skill)
        {

            caster.cast(skill, heroes[target]);
            message.changeMessage(""+(heroes[target].health));
            

            //Do damage and send state to enemy turn
            //Do enemy turn here?
        }

        /*
         * Targetting menu, it will return the int of the target or -1 if no target
         */
        public static int Target()
        {
            int selectLocX = Mouse.GetState().X;
            int selectLocY = Mouse.GetState().Y;

            bool any_target = false;

            for (int i = 0; i < 3; i++)
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
                    state = 1;
                    for (int i = 0; i < 4; i++)
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
                    state = 2;
                    break;
                default:
                    //When will we need this?
                    {
                        state = 0;
                    }
                    break;
            }
        }

        public static void Update(GameTime gametime)
        {
            //If the mouse is released we can continue taking new input
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                menu_change_in_progress = false;
            }
            switch (state)
            {
                case 0:
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
                            selected_skill = boss.skills[0];
                            state = 5;
                        }
                        else if (buttonLoc[2].Contains(selectLocX, selectLocY))
                        {
                            //TODO: need a way to select taunt
                            selected_skill = boss.skills[1];
                            target = -2;
                            state = 6;
                        }
                    }
                    break;
                case 1:
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
                                state = 5;
                            }
                        }
                        if (backLoc.Contains(selectLocX, selectLocY))
                        {
                                    NewMenu(0);
                                    state = 0;
                        }
                            
                    }
                    break;
                case 2:
                    //Bribe Stuff
                    
                    break;
                case 5:
                    //Targetting

                    //highlighted needs to be separate from targeted enemy because target ensure that we have clicked on something
                    target = Target();
                    if (target != -1)
                    {
                        Console.WriteLine("targeting: " + target);
                        state = 6;
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
                case 6:
                    //Attacking
                    Attack(boss, selected_skill);
                    state = 0;
                    NewMenu(0);
                    break;
                case 10:
                    //Enemy Turn
                    break;
            }
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
                }
                catch
                {
                    //dead/KO animation
                }
                spriteBatch.Draw(boss.sprite, bossLoc, Color.White);
                //status too
            }
            //Draw Buttons
            if (state < 2)
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

            if (state > 0 && state < 5)
            {
                spriteBatch.Draw(back, backLoc, Color.White);
            }


            message.draw(spriteBatch, 0, 0);
        }
    }
}
