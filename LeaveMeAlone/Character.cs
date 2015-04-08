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

        private static Texture2D hood2;
        private static Texture2D hood1;
        private static Texture2D mastermind70;

        public enum Type{Ranger, Mage, Knight, Brute, Mastermind, Operative};

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

            debug_text = new Text("atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense, new Vector2(sPosition.Y - 100, sPosition.Y));

        }

        public Character(Type t, int level)
        {
            this.level = level;
            this.charType = t;

            switch(t)
            {
                case Type.Ranger:
                    initRanger();
                    break;
                case Type.Mage:
                    initMage();
                    break;
                default:
                    ;
                    break;
            }
            debug_text = new Text("atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense);



        }
        public void addSkill(Skill s)
        {
            skills.Add(s);
            if(selected_skills.Count < MAX_SKILLS)
            {
                selected_skills.Add(s);
            }
        }
        private void initRanger()
        {
            cure = SkillTree.cure;
        }
        private void initMage()
        {
            cure = SkillTree.cure;
        }
        private void initBrute()
        {
            ;
        }

        public static void load_content(ContentManager content)
        {
            hood2 = content.Load<Texture2D>("hood2");
            hood1 = content.Load<Texture2D>("hood1");
            mastermind70 = content.Load<Texture2D>("mastermind70");
        }
        public void Init()
        {
            idleStartFrame = 0;
            idleEndFrame = 1;
            walkStartFrame = 3;
            walkEndFrame = 11;
            facingRight = false;
            if (charType == Type.Mage)
            {
                sTexture = hood2;
                facingRight = true;
            }
            if (charType == Type.Ranger)
            {
                sTexture = hood2;
                facingRight = true;
            }
            if (charType == Type.Knight)
            {
                sTexture = hood1;
                facingRight = true;
            }
            if (charType == Type.Brute)
            {
                sTexture = mastermind70;
            }
            if (charType == Type.Mastermind)
            {
                sTexture = mastermind70;
            }
            if (charType == Type.Operative)
            {
                sTexture = mastermind70;
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
            if (facingRight)
            {
                Vector2 oPosition = new Vector2(sPosition.X + 5, sPosition.Y);
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
                debug_text.changeMessage("atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense);
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
                debug_text.changeMessage("atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense);
                debug_text.Draw(spriteBatch, sPosition);


            }
        }

        public Skill Think()
        {
            // Get the health remaining if normalized to 100
            // Essentially, this is a percentage times 100
            Random random = new Random();
            Skill selected_skill = basic_attack;
            bool str_used = true;
            int thought = random.Next(100);

            //They don't help each other
            int normal_health = this.health*100/this.max_health;
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
                Console.WriteLine("normal_health: "+normal_health);
                //I can use abilities!
                //This is basically health percentage
                if (normal_health < 50)
                {
                    //I need health!
                    thought = random.Next(100);
                    Console.WriteLine("thought: " + thought);

                    if (thought > 20)
                    {
                        if (cure != null)
                        {
                            selected_skill = cure;//cure
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
                        && (BattleManager.boss.health * 100) / BattleManager.boss.max_health > 75 //and it has high health
                        && thought > 100 - (40 + 5 * level)) // and the Enemy has a good thought
                    {
                        //Use status inflicting skill
                        selected_skill = status;
                    }
                }
            }
            else
            {
                if (normal_health < 20)
                {
                    thought = random.Next(100);
                    //I need health!
                    if (thought > 20)
                    {
                        selected_skill = defend;//cure
                    }
                }
            }
            if (str_used)
            {
                expected_damage = (int)(((2.0 * (double)level + 10.0) / 250.0 * ((double)attack / (double)BattleManager.boss.defense)));
            }
            else
            {
                expected_damage = (int)(((2.0 * (double)level + 10.0) / 250.0 * ((double)special_attack / (double)BattleManager.boss.special_defense)));
            }
            Console.WriteLine("selected_skill: " + selected_skill + " expected damage: " + expected_damage);
            return selected_skill;
        }
    }
}
