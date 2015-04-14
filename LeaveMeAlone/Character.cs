using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

namespace LeaveMeAlone
{
    public class Character : AnimatedSprite
    {
        //TODO make static sprites and instance currentsprite
        public int id;
        public static int character_counter = 0;
        public List<Skill> skills = new List<Skill>();
        public List<Skill> selected_skills = new List<Skill>();
        public const int MAX_ROOMS = 6;
        public List<Room> rooms = new List<Room>();
        public List<Room> selected_rooms = new List<Room>();
        public List<Status> statuses = new List<Status>();

        public enum Knowledge {Weak_Def, Weak_SDef, Str_Atk, Str_SAtk};

        public int max_health;
        public int health;
        public int attack;
        public int special_attack;
        public int defense;
        public int special_defense;
        public int energy;
        public int max_energy;
        public int level;
        public int manaRechargeRate;
        //Rewards
        public int gold;
        public int exp;
        //The basic skills everybody has
        public Skill basic_attack;
        public Skill defend;
        //Generic Skill fills for the Heroes
        private Skill strong_attack = null;
        private Skill strong_special = null;
        private Skill cure = null;
        private Skill esuna = null;
        private Skill boon = null;
        private Skill status = null;

        //The text that will display the damage done
        public Text damage_text;
        public Text debug_text;
        //This is how much damage is expected from any attack done
        public int expected_damage;

        public Texture2D sprite;
        public int spriteIndex;
        public int animationStart;
        public int animationEnd;
        public int animationNext;
        public Type charType;

        private static Texture2D Mage140;
        private static Texture2D Ranger140, Knight140;
        private static Texture2D mastermind140, operative140, brute140, lairHero70;

        public enum Type{Ranger, Mage, Knight, Brute, Mastermind, Operative, LairHero};

        public const int MAX_SKILLS = 6;

        public Character(int _max_health, int _attack, int _special_attack, int _defense, int _special_defense, int _max_energy, int _level, int _manaRechargeRate, int _gold, int _exp)
        {
            id = character_counter++;
            max_health = _max_health;
            health = max_health;
            attack = _attack;
            special_attack = _special_attack;
            defense = _defense;
            special_defense = _special_defense;
            max_energy = _max_energy;
            energy = max_energy;
            level = _level;
            manaRechargeRate = _manaRechargeRate;
            gold = _gold;
            exp = _exp;
            basic_attack = SkillTree.basic_attack;
            defend = SkillTree.defend;

            damage_text = new Text(position:new Vector2(sPosition.X, sPosition.Y-20));

            debug_text = new HoverText(this,"atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense, new Vector2(sPosition.Y - 100, sPosition.Y));

        }

        public Character(Type t, int level, Vector2 pos)
        {
            sPosition = pos;
            this.level = level;
            this.charType = t;
            basic_attack = SkillTree.basic_attack;
            defend = SkillTree.defend;
            switch(t)
            {
                case Type.Ranger:
                    initRanger();
                    break;
                case Type.Mage:
                    initMage();
                    break;
                case Type.Knight:
                    initKnight();
                    break;
                case Type.Brute:
                    initBrute();
                    break;
                case Type.Mastermind:
                    initMastermind();
                    break;
                case Type.Operative:
                    initOperative();
                    break;
                default:
                    ;
                    break;
            }
            Init();
            

            damage_text = new Text(position: new Vector2(sPosition.X, sPosition.Y - 20));

            debug_text = new HoverText(this, "atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense, new Vector2(sPosition.Y - 100, sPosition.Y), Text.fonts["RetroComputer-12"]);



        }
        public void addSkill(Skill s)
        {
            skills.Add(s);
            if(selected_skills.Count < MAX_SKILLS)
            {
                selected_skills.Add(s);
            }
        }
        private void initBrute()
        {
            max_health = 100;
            health = max_health;
            attack = 50;
            special_attack = 10;
            defense = 50;
            special_defense = 10;
            max_energy = 25;
            manaRechargeRate = 1;
            energy = max_energy;
            gold = 100;
            exp = 0;
        }
        private void initMastermind()
        {
            max_health = 100;
            health = max_health;
            attack = 10;
            special_attack = 50;
            defense = 10;
            special_defense = 50;
            max_energy = 35;
            manaRechargeRate = 1;
            energy = max_energy;
            gold = 100;
            exp = 0;
            //new Character(100, 10, 50, 10, 50, 35, 1, 1, 100, 0);
        }
        private void initOperative()
        {
            max_health = 100;
            health = max_health;
            attack = 50;
            special_attack = 10;
            defense = 50;
            special_defense = 10;
            max_energy = 25;
            manaRechargeRate = 1;
            energy = max_energy;
            gold = 100;
            exp = 0;
        }
        private void initRanger()
        {
            max_health = 25 + 5 * level-1;
            health = max_health;
            attack = 10 + 5 * level - 1;
            special_attack = 10 + 5 * level - 1;
            defense = 7 + 5 * level - 1;
            special_defense = 7 + 5 * level - 1;
            max_energy = 35 + 4 * level - 1;
            energy = max_energy;
            manaRechargeRate = (int)(1 + .2 * (double)level - 1);
            gold = 100 + 20 * level - 1;
            exp = 100 + 40 * level - 1;
            cure = SkillTree.cure;
            strong_attack = SkillTree.bash;
            status = SkillTree.poison_dagger;
            esuna = SkillTree.panacea;
        }
        private void initMage()
        {
            max_health = 25 + 5 * level - 1;
            health = max_health;
            attack = 5 + 5 * level - 1;
            special_attack = 25 + 5 * level - 1;
            defense = 5 + 5 * level - 1;
            special_defense = 15 + 5 * level - 1;
            max_energy = 15 + 7 * level - 1;
            energy = max_energy;
            manaRechargeRate = 1 + (int)(1 + .4 * (double)level);
            gold = 100 + 20 * level - 1;
            exp = 100 + 40 * level - 1;
            cure = SkillTree.cure;
            basic_attack = SkillTree.magefire;
        }
        private void initKnight()
        {
            max_health = 50 + 10 * level - 1;
            health = max_health;
            attack = 25 + 5 * level - 1;
            special_attack = 5 + 5 * level - 1;
            defense = 15 + 10 * level - 1;
            special_defense = 5 + 2 * level - 1;
            max_energy = 5 + 3 * level - 1;
            energy = max_energy;
            manaRechargeRate = 1 + (int)(1 + .3 * (double)level - 1);
            gold = 100 + 20 * level - 1;
            exp = 100 + 40 * level - 1;
            strong_attack = SkillTree.bash;
        }

        public static void load_content(ContentManager content)
        {
            Mage140 = content.Load<Texture2D>("Mage140");
            Ranger140 = content.Load<Texture2D>("Ranger140");
            Knight140 = content.Load<Texture2D>("Knight140");
            brute140 = content.Load<Texture2D>("bruteMenu");
            mastermind140 = content.Load<Texture2D>("mastermindMenu");
            operative140 = content.Load<Texture2D>("operativeMenu");
            lairHero70 = content.Load<Texture2D>("LairHero70");
        }
        private void Init()
        {
            idleStartFrame = 0;
            idleEndFrame = 1;
            walkStartFrame = 3;
            walkEndFrame = 11;
            facingRight = false;
            if (charType == Type.Mage)
            {
                sTexture = Mage140;
                facingRight = true;
            }
            if (charType == Type.Ranger)
            {
                sTexture = Ranger140;
                facingRight = true;
            }
            if (charType == Type.Knight)
            {
                sTexture = Knight140;
                facingRight = true;
            }
            if (charType == Type.LairHero)
            {
                sTexture = lairHero70;
                facingRight = true;
            }
            if (charType == Type.Brute)
            {
                sTexture = brute140;
            }
            if (charType == Type.Mastermind)
            {
                sTexture = mastermind140;
            }
            if (charType == Type.Operative)
            {
                sTexture = operative140;
            }
            idle();
            AddAnimation(12);
        }

        public void levelUp()
        {
            Random rng = new Random();
            int var;
            if (charType == Type.Brute)
            {
                var = rng.Next(100);
                this.max_health += 25;
                if (var >= 50)
                {
                    this.max_health += 25;
                }

                var = rng.Next(100);
                this.max_energy += 5;
                if (var >= 50)
                {
                    this.max_energy += 5;
                }

                var = rng.Next(100);
                this.attack += 2;
                if (var >= 50)
                {
                    this.attack += 2;
                }

                var = rng.Next(100);
                this.defense += 2;
                if (var >= 50)
                {
                    this.defense += 2;
                }

                var = rng.Next(100);
                this.special_attack += 2;
                if (var >= 50)
                {
                    this.special_attack += 2;
                }

                var = rng.Next(100);
                this.special_defense += 2;
                if (var >= 50)
                {
                    this.special_defense += 2;
                }
            }
            else if (charType == Type.Mastermind)
            {

            }
            else if (charType == Type.Operative)
            {

            }
        }
        public void cast(Skill skill, Character target = null)
        {
            if (skill.energy > this.energy)
            {
                Console.WriteLine("Character doesn't have enough energy");

                return;
            }
            this.energy -= skill.energy;
            skill.runnable(this, target);
        }

        public void Update(GameTime gameTime) {
            
            FrameUpdate(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            debug_text.changeMessage("atk: " + attack + " def: " + defense + "\nsatk: " + special_attack + " sdef: " + special_defense);
            if (facingRight)
            {
                Vector2 oPosition = new Vector2(sPosition.X + 5, sPosition.Y);
                if (charType == Character.Type.Knight)
                {
                    oPosition = new Vector2(sPosition.X + 5, sPosition.Y - 50);
                }
                spriteBatch.Draw(sTexture, oPosition, sRectangles[frameIndex], color, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                int i = 0;
                foreach (Status status in this.statuses)
                {
                    if (status == null) 
                    {
                        continue;
                    }
                    spriteBatch.Draw(status.img, new Vector2(sPosition.X + 20*i, sPosition.Y), Color.White);
                    i++;
                }
                
                debug_text.Draw(spriteBatch, oPosition);

            }
            else
            {
                spriteBatch.Draw(sTexture, sPosition, sRectangles[frameIndex], color);
                int i = 0;
                foreach (Status status in this.statuses)
                {
                    if (status == null)
                    {
                        continue;
                    }
                    if (status.img != null)
                    {
                        spriteBatch.Draw(status.img, new Vector2(sPosition.X + 20 * i, sPosition.Y), Color.White);
                    }
                    i++;
                }
                //Vector2 oPosition = new Vector2(sPosition.X - 50, sPosition.Y);
                debug_text.Draw(spriteBatch, sPosition);


            }
        }

        public void Learn(int real_damage, Character.Knowledge idea)
        {
            //if it was effective of an attack
            if (real_damage >= 1.1*(double)expected_damage)
            {
                BattleManager.Knowledge[idea] = true;
            }
            else if (real_damage <= .9 * (double)expected_damage)
            {
                BattleManager.Knowledge[idea] = false;
            }
        }

        public KeyValuePair<Skill, int> Think()
        {
            // Get the health remaining if normalized to 100
            // Essentially, this is a percentage times 100
            Random random = new Random();
            int my_target = -2; //-2 is boss, -1 is nobody, 0-3 is heroes
            Skill selected_skill = null;
            bool str_used = true;
            int thought = random.Next(100);

            foreach ( KeyValuePair<Character.Knowledge, bool> kvp in BattleManager.Knowledge)
            {
                Console.WriteLine("Knowledge = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            //They don't help each other
            int[] normal_health = new int[4];
            for(int i = 0; i < BattleManager.heroes.Count(); i++)
            {
                if (BattleManager.heroes[i] != null)
                {
                    normal_health[i] = BattleManager.heroes[i].health*100/BattleManager.heroes[i].max_health;
                }
                else{
                    normal_health[i] = 100000;
                }
            }
            bool has_defect = false;

            foreach (Status stat in statuses)
            {
                if (stat.type == Status.Type.Debuff)
                {
                    has_defect = true;
                }
            }

            //We will use our battlemanager.Knowledge to detemine what we know about boss
            //If the key exists we know whether it does or doesn't work
            //If it doesn't exist, fuck it and try anything
            if (this.energy >= 10)
            {
                //Console.WriteLine("normal_health: "+normal_health);
                //I can use abilities!
                //This is basically health percentage
                for (int i = 0; i < BattleManager.heroes.Count(); i++)
                {
                    if (normal_health[i] < 50)
                    {
                        //I need health!
                        thought = random.Next(100);
                        Console.WriteLine("thought: " + thought);

                        if (thought > 20)
                        {
                            if (cure != null)
                            {
                                Console.WriteLine("Cure Selected");
                                selected_skill = cure;//cure
                                my_target = i;
                            }
                        }
                    }
                }
                if (has_defect)
                {
                    thought = random.Next(100);

                    if (thought > 40)
                    {
                        if (esuna != null)
                        {
                            selected_skill = esuna;
                        }
                    }
                }

                thought = random.Next(100);
                //Consider exploiting a weakness?
                if (BattleManager.Knowledge.ContainsKey(Knowledge.Weak_Def))
                {
                    //If we know there is a weakness and we are smart enough to make the move
                    thought = random.Next(100);

                    if (BattleManager.Knowledge[Knowledge.Weak_Def] && thought > 100-(40+5*level))
                    {
                        //Use strong physical skill
                        if (strong_attack != null)
                        {
                            selected_skill = strong_attack;
                            my_target = -2;
                        }
                    }
                }
                if (BattleManager.Knowledge.ContainsKey(Knowledge.Weak_SDef))
                {
                    thought = random.Next(100);
                    if (BattleManager.Knowledge[Knowledge.Weak_SDef] && thought > 100 - (40 + 5 * level))
                    {
                        //Use strong special skill
                        if (strong_special != null)
                        {
                            selected_skill = strong_special;
                            str_used = false;
                            my_target = -2;
                        }

                    }

                }
                //Let's take another thought
                thought = random.Next(100);

                //Consider adding a status effect?
                //How do I check if status is there?
                if (status != null)
                {
                    if (!BattleManager.boss.statuses.Contains(status.inflicts) //If the boss doesn't have the status effect that my status does
                        && (BattleManager.boss.health * 100) / BattleManager.boss.max_health > 50 //and it has high health
                        && thought > 100 - (40 + 5 * level)) // and the Enemy has a good thought
                    {
                        //Use status inflicting skill
                        selected_skill = status;
                        my_target = -2;
                    }
                }
            }
            else
            {
                if (health*100/max_health < 40)
                {
                    thought = random.Next(100);
                    //I need health!
                    if (thought > 20)
                    {
                        selected_skill = defend;//cure
                    }
                }
            }

            thought = random.Next(100);
            //if we still haven't picked a skill, pick a random attack
            if (selected_skill == null)
            {
                if (thought < 66)
                {
                    //most of the time we want to attack
                    selected_skill = basic_attack;
                }
                else if (thought > 90)
                {
                    //maybe we want to defend
                    selected_skill = defend;
                }
                else
                {
                    if (strong_attack != null)
                    {
                        selected_skill = strong_attack;
                    }
                    else if (strong_special != null)
                    {
                        selected_skill = strong_special;
                        str_used = false;
                    }
                    else if (status != null)
                    {
                        selected_skill = status;
                    }
                    else
                    {
                        selected_skill = basic_attack;
                    }
                }
            }

            //average the boss' defense plus or minus a third
            int boss_defense = (int) (LeaveMeAlone.random.Next(80, 120) * (double)((BattleManager.boss.defense + BattleManager.boss.special_defense) / 2) / LeaveMeAlone.random.Next(80,120));

            if (str_used)
            {
                expected_damage = (int)(((2.0 * (double)level + 10.0) / 250.0 * (2+(double)attack / boss_defense) * 100));
            }
            else
            {
                expected_damage = (int)(((2.0 * (double)level + 10.0) / 250.0 * (2+(double)special_attack / boss_defense) * 100));
            }
            Console.WriteLine("Expected: " + expected_damage);
            Console.WriteLine("Data: {0}, {1}, {2}", level, attack, boss_defense);
            damage_text.changeMessage(selected_skill.name);
            //Console.WriteLine("selected_skill: " + selected_skill.name + " expected damage: " + expected_damage);
            return new KeyValuePair<Skill, int>(selected_skill, my_target);
        }
    }
}
