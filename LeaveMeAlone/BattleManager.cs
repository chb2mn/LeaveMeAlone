using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LeaveMeAlone
{
    class BattleManager
    {
        public static List<Character> heroes;
        public static Character boss;
        public static Rectangle bossLoc;
        public static Texture2D back;
        public static Rectangle backLoc;
        public static Rectangle[] buttonLoc = new Rectangle[4];
        private static Text damage_text;
        private static int textx;
        private static int texty;

        private static Text victory_text;
        bool victory;

        bool menu_change_in_progress = false;
        int state_delay_counter = 0;
        int state = 0; 


        public static void Attack(Character caster, Skill skill){

        }

        public static void Target(Character caster)
        {

        }

        public static void CheckVictoryDefeat()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {

        }

        public static void Update()
        {

        }
    }
}
