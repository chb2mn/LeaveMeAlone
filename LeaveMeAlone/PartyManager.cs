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
        }

        public static Character CreateHero(int type)
        {
            Character c;
            switch (type)
            {
                case 0:
                    //Knight
                    c = new Character(50, 25, 5, 500, 0, 5, 1, 1, 100, 100);
                    c.charType = Character.Type.Knight;
                    return c;
                case 1:
                    //Mage;
                    c =  new Character(25, 5, 25, 5, 25, 15, 1, 1, 100, 100);
                    c.charType = Character.Type.Mage;
                    return c;
                case 2:
                    //Ranger
                    c = new Character(25, 10, 10, 10, 10, 35, 1, 1, 100, 100);
                    c.charType = Character.Type.Ranger;
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
                new_party.Add(CreateHero(type));
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
                for (int i = 0; i < BattleManager.heroes.Count; i++)
                {
                    if (BattleManager.heroes[i] != null)
                    {
                        BattleManager.heroes[i].Init();

                    }
                }
                int hero_basex = 50;
                int hero_basey = 150;
                BattleManager.heroLoc.Add(new Rectangle(hero_basex, hero_basey - 60, 100, 60));
                BattleManager.heroLoc.Add(new Rectangle(hero_basex, hero_basey, 100, 60));
                BattleManager.heroLoc.Add(new Rectangle(hero_basex, hero_basey + 60, 100, 60));
                BattleManager.heroLoc.Add(new Rectangle(hero_basex, hero_basey + 120, 100, 60));
                BattleManager.setHeroesPosition();
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
