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

        public enum Knowledge { Weak_Def, Weak_SDef, Str_Atk, Str_SAtk };

        //>>Stats<<
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
        public int crit_chance;
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
        public List<Text> damage_text_queue = new List<Text>();
        public Text damage_text;
        public int damage_counter;
        public Text debug_text;
        public Text lvl_text;
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
        public static Texture2D Dead;

        public enum Type { Ranger, Mage, Knight, Brute, Mastermind, Operative, LairHero };

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

            crit_chance = 0;
            lvl_text = new Text("Lvl: " + level.ToString(), new Vector2(sPosition.X + 45, sPosition.Y), c: Color.CadetBlue);
            /*
            for (int i = 0; i < 4; i++)
            {
                damage_text_queue.Add(new Text(position: new Vector2(sPosition.X, sPosition.Y - 20)));
            }
            */
            damage_text = new Text(position: new Vector2(sPosition.X, sPosition.Y - 20));
            damage_counter = 100;

            debug_text = new Text("atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense, new Vector2(sPosition.Y - 100, sPosition.Y));

        }

        public Character(Type t, int level, Vector2 pos)
        {
            sPosition = pos;
            this.level = level;
            this.charType = t;
            basic_attack = SkillTree.basic_attack;
            defend = SkillTree.defend;
            crit_chance = 0;
            switch (t)
            {
                case Type.Ranger:
                    initRanger();
                    defend = SkillTree.enemy_defend;

                    break;
                case Type.Mage:
                    initMage();
                    defend = SkillTree.enemy_defend;

                    break;
                case Type.Knight:
                    initKnight();
                    defend = SkillTree.enemy_defend;

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

            lvl_text = new Text("Lvl: " + level.ToString(), new Vector2(sPosition.X + 45, sPosition.Y), c: Color.CadetBlue);
            /*
            for (int i = 0; i < 4; i++)
            {
                damage_text_queue.Add(new Text(position: new Vector2(sPosition.X, sPosition.Y - 20)));
            }
            */
            damage_text = new Text(position: new Vector2(sPosition.X, sPosition.Y - 20), f: Text.fonts["Arial-24"]);
            damage_counter = 100;

            debug_text = new Text("atk: " + attack + " def: " + defense + "\nsatk: " + special_attack + " sdef: " + special_defense, new Vector2(sPosition.Y - 100, sPosition.Y), Text.fonts["RetroComputer-12"]);



        }
        public void addSkill(Skill s)
        {
            skills.Add(s);
            if (selected_skills.Count < MAX_SKILLS)
            {
                selected_skills.Add(s);
            }
        }


        //>>>>>>>>>>>>>>>>>>Class Stats<<<<<<<<<<<<<<<<<<
        private void initBrute()
        {
            max_health = 100;
            health = max_health;
            attack = 50;
            special_attack = 20;
            defense = 50;
            special_defense = 10;
            max_energy = 25;
            manaRechargeRate = 1;
            energy = max_energy;
            gold = 100;
            exp = 0;
            //level = 1;
            crit_chance = 10;
            /*
            Resources.gold = 200000;
            Resources.exp = 10001;
            for (int i = 0; i < level; i++)
            {
                levelUp();
            }
             */
        }
        private void initMastermind()
        {
            max_health = 100;
            health = max_health;
            special_attack = 50;
            attack = 20;
            special_attack = 50;
            defense = 20;
            special_defense = 50;
            max_energy = 35;
            manaRechargeRate = 1;
            energy = max_energy;
            gold = 100;
            exp = 0;
            crit_chance = 10;
            /*
            Resources.gold = 200000;
            Resources.exp = 10000;
            level = 10;
            for (int i = 0; i < level; i++)
            {
                levelUp();
            }
             * */
        }
        private void initOperative()
        {
            //level = 10;
            max_health = 100;
            health = max_health;
            attack = 34;
            special_attack = 34;
            defense = 30;
            special_defense = 30;
            max_energy = 50;
            manaRechargeRate = 1;
            energy = max_energy;
            gold = 100;
            exp = 0;
            crit_chance = 20;
            /*
            Resources.gold = 200000;
            Resources.exp = 10000;
            level = 10;
            for (int i = 0; i < level; i++)
            {
                levelUp();
            }
             * */
             
        }
        //>>>>>>>>>>>>>>>>>Hero Stats<<<<<<<<<<<<<<<<<<<
        private void initRanger()
        {
            max_health = 25 + 5 * (level - 1);
            health = max_health;
            attack = 10 + 4 * (level - 1);
            special_attack = 10 + 4 * (level - 1);
            defense = 7 + 3 * (level - 1);
            special_defense = 7 + 3 * (level - 1);
            max_energy = 35 + 4 * (level - 1);
            energy = max_energy;
            manaRechargeRate = (int)(1 + .2 * (double)(level - 1));
            gold = 100 + 20 * (level - 1);
            exp = 100 + 100 * (level - 1);
            cure = SkillTree.cure;
            strong_attack = SkillTree.bash;
            status = SkillTree.poison_dagger;
            esuna = SkillTree.panacea;
        }
        private void initMage()
        {
            max_health = 25 + 5 * (level - 1);
            health = max_health;
            attack = 5 + 2 * (level - 1);
            special_attack = 25 + 5 * (level - 1);
            defense = 5 + 1 * (level - 1);
            special_defense = 15 + 4 * (level - 1);
            max_energy = 15 + 3 * (level - 1);
            energy = max_energy;
            manaRechargeRate = 1 + (int)(1 + .4 * (double)level);
            gold = 100 + 20 * (level - 1);
            exp = 100 + 100 * (level - 1);
            cure = SkillTree.cure;
            basic_attack = SkillTree.magefire;
        }
        private void initKnight()
        {
            max_health = 50 + 10 * (level - 1);
            health = max_health;
            attack = 25 + 5 * (level - 1);
            special_attack = 5 + 2 * (level - 1);
            defense = 15 + 9 * (level - 1);
            special_defense = 5 + 1 * (level - 1);
            max_energy = 5 + 2 * (level - 1);
            energy = max_energy;
            manaRechargeRate = 1 + (int)(1 + .3 * (double)(level - 1));
            gold = 100 + 20 * (level - 1);
            exp = 100 + 100 * (level - 1);
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
            Dead = content.Load<Texture2D>("CharacterSprites/Dead");
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

        public void PushDamage(String damage)
        {
            Text new_text = new Text(msg: damage, position: new Vector2(sPosition.X, sPosition.Y - 20), f: Text.fonts["Arial-24"]);
            damage_text_queue.Add(new_text);
        }
        public Text PopDamage()
        {
            Text ret_text = damage_text_queue[0];
            damage_text_queue.RemoveAt(0);
            return ret_text;
        }

        public void levelUp()
        {
            Random rng = new Random();
            int var;
            if (charType == Type.Brute)
            {
                var = rng.Next(100);
                this.max_health += 40;
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
                this.attack += 4;
                if (var >= 50)
                {
                    this.attack += 2;
                }

                var = rng.Next(100);
                this.defense += 4;
                if (var >= 50)
                {
                    this.defense += 2;
                }

                var = rng.Next(100);
                this.special_attack += 2;
                if (var >= 50)
                {
                    this.special_attack += 1;
                }

                var = rng.Next(100);
                this.special_defense += 2;
                if (var >= 50)
                {
                    this.special_defense += 1;
                }
            }
            else if (charType == Type.Mastermind)
            {
                var = rng.Next(100);
                this.max_health += 40;
                if (var >= 50)
                {
                    this.max_health += 15;
                }

                var = rng.Next(100);
                this.max_energy += 8;
                if (var >= 50)
                {
                    this.max_energy += 8;
                }

                var = rng.Next(100);
                this.special_attack += 4;
                if (var >= 50)
                {
                    this.special_attack += 2;
                }

                var = rng.Next(100);
                this.special_defense += 4;
                if (var >= 50)
                {
                    this.special_defense += 2;
                }

                var = rng.Next(100);
                this.attack += 2;
                if (var >= 50)
                {
                    this.attack += 1;
                }

                var = rng.Next(100);
                this.defense += 2;
                if (var >= 50)
                {
                    this.defense += 1;
                }
            }
            else if (charType == Type.Operative)
            {
                var = rng.Next(100);
                this.max_health += 40;
                if (var >= 50)
                {
                    this.max_health += 10;
                }

                var = rng.Next(100);
                this.max_energy += 10;
                if (var >= 50)
                {
                    this.max_energy += 5;
                }

                var = rng.Next(100);
                this.special_attack += 3;
                if (var >= 50)
                {
                    this.special_attack += 2;
                }

                var = rng.Next(100);
                this.special_defense += 3;
                if (var >= 50)
                {
                    this.special_defense += 2;
                }

                var = rng.Next(100);
                this.attack += 3;
                if (var >= 50)
                {
                    this.attack += 2;
                }

                var = rng.Next(100);
                this.defense += 3;
                if (var >= 50)
                {
                    this.defense += 2;
                }
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

            if (statuses.Contains(Status.check_confused))
            {
                Character new_target = null;
                int heroes_alive = BattleManager.heroes.Count();
                while (new_target == null)
                {
                    int rand = LeaveMeAlone.random.Next(heroes_alive + 1);
                    //If this number is equal, then we target the heroes
                    if (rand == heroes_alive)
                    {
                        new_target = BattleManager.boss;
                    }
                    else
                    {
                        new_target = BattleManager.heroes[rand];
                    }
                }
                target = new_target;
            }

            skill.runnable(this, target);
            if (skill.sound != null)
            {
                skill.sound.Play();
            }
        }

        public void Update(GameTime gameTime)
        {

            FrameUpdate(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
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
                    else if (status.img == null)
                    {
                        continue;
                    }
                    spriteBatch.Draw(status.img, new Rectangle((int)(sPosition.X + 20 * i), (int)sPosition.Y, 30, 30), Color.White);
                    i++;
                }
                lvl_text.Draw(spriteBatch);

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
                        spriteBatch.Draw(status.img, new Rectangle((int)(sPosition.X + 20 * i), (int)sPosition.Y, 30, 30), Color.White);
                    }
                    i++;
                }
                lvl_text.Draw(spriteBatch);

                //Vector2 oPosition = new Vector2(sPosition.X - 50, sPosition.Y);
                debug_text.Draw(spriteBatch, sPosition);
            }

            if (!damage_text.message.Equals(""))
            {
                if (damage_counter-- >= 0)
                {
                    damage_text.Draw(spriteBatch, new Vector2((int)sPosition.X + 25, (int)sPosition.Y - 20 + damage_counter / 3), Color.AntiqueWhite);
                }
                else
                {
                    damage_counter = 100;

                    if (damage_text_queue.Count() > 0)
                    {
                        damage_text = PopDamage();
                    }

                    else
                    {
                        damage_text.changeMessage("");
                    }
                }
            }
            else if (damage_text_queue.Count() > 0)
            {
                damage_text = PopDamage();
            }
        }

        public void Learn(int real_damage, Character.Knowledge idea)
        {
            //if it was effective of an attack
            if (real_damage >= 1.1 * (double)expected_damage)
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

            foreach (KeyValuePair<Character.Knowledge, bool> kvp in BattleManager.Knowledge)
            {
                Console.WriteLine("Knowledge = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            //They don't help each other
            int[] normal_health = new int[4];
            for (int i = 0; i < BattleManager.heroes.Count(); i++)
            {
                if (BattleManager.heroes[i] != null)
                {
                    normal_health[i] = BattleManager.heroes[i].health * 100 / BattleManager.heroes[i].max_health;
                }
                else
                {
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
                Console.WriteLine("I can use abilities!");
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
                            if (cure != null && this.energy >= 20)
                            {
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

                    if (BattleManager.Knowledge[Knowledge.Weak_Def] && thought > 100 - (40 + 5 * level))
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
                if (health * 100 / max_health < 40)
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
                else if (energy >= 5)
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
                }

            }

            if (selected_skill == null)
            {
                selected_skill = basic_attack;
            }
            //average the boss' defense plus or minus a third
            int boss_defense = (int)(LeaveMeAlone.random.Next(80, 120) * (double)((BattleManager.boss.defense + BattleManager.boss.special_defense) / 2) / LeaveMeAlone.random.Next(80, 120));

            if (str_used)
            {
                expected_damage = (int)(((2.0 * (double)level + 10.0) / 250.0 * (2 + (double)attack / boss_defense) * 100));
            }
            else
            {
                expected_damage = (int)(((2.0 * (double)level + 10.0) / 250.0 * (2 + (double)special_attack / boss_defense) * 100));
            }
            Console.WriteLine("Expected: " + expected_damage);
            damage_text.changeMessage(selected_skill.name);
            //Console.WriteLine("selected_skill: " + selected_skill.name + " expected damage: " + expected_damage);
            return new KeyValuePair<Skill, int>(selected_skill, my_target);
        }

        internal string StatsToString()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append("health: "+this.max_health);
            sb.Append("\n");
            sb.Append("energy: "+this.max_energy);
            sb.Append("\n");
            sb.Append("attack: "+this.attack);
            sb.Append("\n");
            sb.Append("defense: "+this.defense);
            sb.Append("\n");
            sb.Append("special attack: "+this.special_attack);
            sb.Append("\n");
            sb.Append("special defense: "+this.special_defense);


            return sb.ToString();
        }
    }
}
