using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaveMeAlone
{
    class PartyManager
    {
        public static int PartyNum;
        public static int ArmyNum;
        public static Character CreateHero()
        {
            Character c = new Character(Character.Type.Ranger, 1);
            return c;
        }
        protected static void Update(GameTime gameTime)
        {

        }
        protected static  void Draw(SpriteBatch spritebatch, GameTime gametime)
        {

        }
    }
}
