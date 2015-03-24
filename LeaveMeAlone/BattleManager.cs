using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LeaveMeAlone
{
    class BattleManager
    {
        public static List<Character> heroes;
        public static List<Rectangle> heroLoc;
        public static Character boss;
        public static Rectangle bossLoc;
        public static Texture2D back;
        public static Rectangle backLoc;

        private static Texture2D buttonLocPic;
        private static Rectangle[] buttonLoc = new Rectangle[4];
        private static Text damage_text;
        private static int textx;
        private static int texty;

        private static Text victory_text;
        private static bool victory;
        private static bool defeat;

        private static bool menu_change_in_progress = false;
        private static int state_delay_counter = 0;
        private static int state = 0;
        private static int targeted_enemy = -1;
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

            buttonLocPic = Content.Load<Texture2D>("buttonbase");
            bossLoc = new Rectangle(650, 120, 100, 100);


            int hero_basex = 50;
            int hero_basey = 150;
            heroLoc[0] = new Rectangle(hero_basex, hero_basey, 50, 50);
            heroLoc[1] = new Rectangle(hero_basex, hero_basey - 60, 50, 50);
            heroLoc[2] = new Rectangle(hero_basex, hero_basey + 60, 50, 50);
            heroLoc[3] = new Rectangle(hero_basex, hero_basey + 120, 50, 50);

        }
        public static void Attack(Character caster, Skill skill)
        {
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
                    targeted_enemy = i;
                    any_target = true;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        return targeted_enemy;
                    }
                }    
            }
            if (bossLoc.Contains(selectLocX, selectLocY))
            {
                targeted_enemy = -2;
                any_target = true;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    return targeted_enemy;
                }

            }
            if (!any_target)
            {
                targeted_enemy = -1;
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

        public static void Draw(SpriteBatch spriteBatch)
        {
            //Do Background drawing
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    if (i == targeted_enemy)
                    {
                        spriteBatch.Draw(heroes[i].sprite, heroLoc[i], Color.Violet);
                    }
                    else
                    {
                        spriteBatch.Draw(heroes[i].sprite, heroLoc[i], Color.White);
                    }
                }
                catch
                {
                    //dead animation
                }
            }
        }

        public static void Update()
        {
            switch (state)
            {
                case 0:
                    foreach (Rectangle button in buttonLoc)
                    {

                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 5:
                    Target();
                    break;
                case 6:
                    break;
                case 10:
                    break;
            }
        }
    }
}
