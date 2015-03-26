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
        private static Random RNG = new Random();
        public static Character CreateHero(ContentManager Content, int type)
        {
            Character c;
            switch (type)
            {
                case 0:
                    //Knight
                    return new Character(25, 25, 5, 25, 5, 5, 1, 1, Content.Load<Texture2D>("Knight"));
                case 1:
                    //Mage;
                    return new Character(25, 5, 25, 5, 25, 15, 1, 1, Content.Load<Texture2D>("BlackMage"));
                case 2:
                    //Ranger
                    return new Character(25, 10, 10, 10, 10, 35, 1, 1, Content.Load<Texture2D>("Archer"));
            }
            return new Character(25, 5, 5, 5, 5, 5, 1, 1, Content.Load<Texture2D>("DummyHero"));

            // Character c = new Character(Character.Type.Ranger, 1);
            // return c;

        }

        public static List<Character> CreateParty(ContentManager Content)
        {
            List<Character> new_party = new List<Character>();
            int partysize = RNG.Next(20); //Roll a d20
            if (partysize == 0) //0 means 1 hero
            {
                //Just one buff hero
                
                int type = RNG.Next(3);
                new_party.Add(CreateHero(Content, type));
                BattleManager.hero_hp[0].changeMessage(new_party[0].max_health.ToString() + "/" + new_party[0].max_health.ToString());

            }
            else if (partysize <= 5) //1-5 means 2 heroes
            {
                //A pair of heroes
                for (int i = 0; i < 2; i++)
                {
                    int type = RNG.Next(3);
                    new_party.Add(CreateHero(Content, type));
                    BattleManager.hero_hp[i].changeMessage(new_party[i].max_health.ToString() + "/" + new_party[i].max_health.ToString());

                }
            }
            else if (partysize <= 11) // 6-11 means 3 heroes
            {
                for (int i = 0; i < 3; i++)
                {
                    int type = RNG.Next(3);
                    new_party.Add(CreateHero(Content, type));
                    BattleManager.hero_hp[i].changeMessage(new_party[i].max_health.ToString() + "/" + new_party[i].max_health.ToString());

                }
                //A classic trio of heroes
            }
            else // 12-19 means 4 heroes
            {
                //4 heroes
                for (int i = 0; i < 4; i++)
                {
                    int type = RNG.Next(3);
                    new_party.Add(CreateHero(Content, type));
                    BattleManager.hero_hp[i].changeMessage(new_party[i].max_health.ToString() + "/" + new_party[i].max_health.ToString());
                }
            }
            //Changing the boss's health and energy text fields 
            //--This could/should probably move--
            BattleManager.boss_hp.changeMessage(BattleManager.boss.health.ToString() + "/" + BattleManager.boss.max_health.ToString());
            BattleManager.boss_energy.changeMessage(BattleManager.boss.energy.ToString() + "/" + BattleManager.boss.energy.ToString());
            return new_party;
        }
        protected static void Update(GameTime gameTime)
        {

        }
        protected static void Draw(SpriteBatch spritebatch, GameTime gametime)

        {

        }
    }
}
