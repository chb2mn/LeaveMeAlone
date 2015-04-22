using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace LeaveMeAlone
{
    class PartyManager
    {
        public static int PartyNum;
        public static int ArmyNum;
        public static List<List<Character>> partyQueue;
        private static Random RNG = new Random();
        //private static Text[] damage_texts = new Text[4];

        public static void Init()
        {
            partyQueue = new List<List<Character>>();
            partyQueue.Add(null);
            for (int x = 0; x < 4; x++)
            {
                int xpos = LairManager.sideOffset + LairManager.sideScaling * ((x + 1) % 2) + 60;
                int ypos = LairManager.topOffset + LairManager.topScaling * x;
                BattleManager.heroLoc.Add(new Rectangle(xpos, ypos, 80, 140));
            }
        }

        public static Character CreateHero(int type, int lvl, Vector2 pos)
        {

            //BattleManager.heroLoc.Add(new Rectangle(hero_basex, hero_basey, 100, 60));
            Character c;
            switch (type)
            {
                case 0:
                    //Knight
                    //c = new Character(50, 25, 5, 500, 0, 5, 1, 1, 100, 100);
                    //c.charType = Character.Type.Knight;
                    c = new Character(Character.Type.Knight, lvl, pos);
                    return c;
                case 1:
                    //Mage;
                    //c =  new Character(25, 5, 25, 5, 25, 15, 1, 1, 100, 100);
                    //c.charType = Character.Type.Mage;
                    c = new Character(Character.Type.Mage, lvl, pos);

                    return c;
                case 2:
                    //Ranger
                    //c = new Character(25, 10, 10, 10, 10, 35, 1, 1, 100, 100);
                    //c.charType = Character.Type.Ranger;
                    c = new Character(Character.Type.Ranger, lvl, pos);

                    return c;
            }
            return new Character(25, 5, 5, 5, 5, 5, 1, 1, 100, 100);
        }

        public static List<Character> CreateParty()
        {
            List<Character> new_party = new List<Character>();
            int partysize = RNG.Next(20); //Roll a d20
            int num = 0;
            if (partysize == 0) //0 means 1 hero
            {
                num = 1;
            }
            else if (partysize <= 5) //1-5 means 2 heroes
            {
                num = 2;
            }
            else if (partysize <= 11) // 6-11 means 3 heroes
            {
                num = 3;
            }
            else // 12-19 means 4 heroes
            {
                num = 4;
            }

            for(int x = 0; x < num; x++)
            {
                int type = RNG.Next(3);
                // want to make a party with a single enemy more difficult than a single enemy in a party of 4
                int level = BattleManager.boss.level + (4 - num); //LeaveMeAlone.random.Next(3) +
                if (BattleManager.boss.level > 4)
                {
                    level += LeaveMeAlone.random.Next(3);
                }
                int xpos = LairManager.sideOffset + LairManager.sideScaling * ((x + 1)% 2);
                int ypos = LairManager.topOffset + LairManager.topScaling * x;
               
                new_party.Add(CreateHero(type, level, new Vector2(xpos, ypos)));
            }
            return new_party;
        }
        public static bool popParty()
        {
            List<Character> topParty = partyQueue[0];
            for (int i = 0; i < partyQueue.Count(); i++)
            {
                try
                {
                    partyQueue[i] = partyQueue[i + 1];
                }
                catch (Exception)
                {
                    partyQueue.RemoveAt(partyQueue.Count() - 1);
                }
            }
            if (topParty != null)
            {
                BattleManager.heroes = topParty;
                
                return true;
            }
            else
            {
                return false;
            }
        }
        protected static void Update(GameTime gameTime)
        {

        }
        protected static void Draw(SpriteBatch spritebatch, GameTime gametime)

        {

        }
    }
}
