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
        public static Text[] damage_texts = new Text[4];
        private static Texture2D knight_sprite;
        private static Texture2D mage_sprite;
        private static Texture2D ranger_sprite;


        public static void Init(ContentManager Content)
        {
            for (int i = 0; i < 4; i++)
            {
                damage_texts[i] = new Text("");
            }
            knight_sprite = Content.Load<Texture2D>("Knight");
            mage_sprite = Content.Load<Texture2D>("BlackMage");
            ranger_sprite = Content.Load<Texture2D>("Archer");


        }
        public static Character CreateHero(int type, Text damage_text)
        {
            Character c;
            switch (type)
            {
                case 0:
                    //Knight
                    return new Character(25, 25, 5, 25, 5, 5, 1, 1, knight_sprite, damage_text);
                case 1:
                    //Mage;
                    return new Character(25, 5, 25, 5, 25, 15, 1, 1, mage_sprite, damage_text);
                case 2:
                    //Ranger
                    return new Character(25, 10, 10, 10, 10, 35, 1, 1, ranger_sprite, damage_text);
            }
            return new Character(5, 5, 5, 5, 5, 5, 1, 1, knight_sprite, damage_text);

            // Character c = new Character(Character.Type.Ranger, 1);
            // return c;

        }

        public static List<Character> CreateParty()
        {
            Console.WriteLine("Creating Party");
            List<Character> new_party = new List<Character>();
            int partysize = RNG.Next(20); //Roll a d20
            if (partysize == 0) //0 means 1 hero
            {
                //Just one buff hero
                
                int type = RNG.Next(3);
                new_party.Add(CreateHero(type, damage_texts[0]));
                BattleManager.hero_hp[0].changeMessage(new_party[0].max_health.ToString() + "/" + new_party[0].max_health.ToString());

            }
            else if (partysize <= 5) //1-5 means 2 heroes
            {
                //A pair of heroes
                for (int i = 0; i < 2; i++)
                {
                    int type = RNG.Next(3);
                    new_party.Add(CreateHero(type, damage_texts[i]));
                    BattleManager.hero_hp[i].changeMessage(new_party[i].max_health.ToString() + "/" + new_party[i].max_health.ToString());

                }
            }
            else if (partysize <= 11) // 6-11 means 3 heroes
            {
                for (int i = 0; i < 3; i++)
                {
                    int type = RNG.Next(3);
                    new_party.Add(CreateHero(type, damage_texts[i]));
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
                    new_party.Add(CreateHero(type, damage_texts[i]));
                    BattleManager.hero_hp[i].changeMessage(new_party[i].max_health.ToString() + "/" + new_party[i].max_health.ToString());
                }
            }
            //Changing the boss's health and energy text fields 
            //--This could/should probably move--
            BattleManager.boss.health = BattleManager.boss.max_health;
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
