using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
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
        public static Rectangle[] buttonLoc = new Rectangle[4];
        private static Text damage_text;
        private static int textx;
        private static int texty;

        private static Text victory_text;
        private static bool victory;

        private static bool menu_change_in_progress = false;
        private static int state_delay_counter = 0;
        private static int state = 0; 
        //0 == Basic
        //1 == Skills
        //2 == Bribe
        //5 == targeting
        //6 == attacking
        //10 == Enemy turn
        public static void Attack(Character caster, Skill skill){

        }

        public static void Target()
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
                        Attack();
                    }
                }    
            }
            if (bossLoc.Contains(selectLocX, selectLocY))
            {
                targeted_enemy = -2;
                any_target = true;

            }
            if (!any_target)
            {
                targeted_enemy = -1;
            }
        }

        public static void CheckVictoryDefeat()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            //Do Background drawing

        }

        public static void Update()
        {
            switch (state)
            {
                case 0:
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
