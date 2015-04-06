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
        //The text that will display the damage done
        public Text damage_text;
        public Text debug_text;

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

        public Character(int _max_health, int _attack, int _special_attack, int _defense, int _special_defense, int _max_energy, int _level, int _manaRechargeRate, int _gold, int _exp, Text _damage_text)
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
            damage_text = _damage_text;
            debug_text = new Text("atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense);
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
            ;
        }
        private void initMage()
        {
            ;
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
                    spriteBatch.Draw(status.img, new Vector2(sPosition.X + 20*i, sPosition.Y), Color.White);
                    i++;
                }
                debug_text.changeMessage("atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense);
                debug_text.draw(spriteBatch, (int)oPosition.X + 100, (int)oPosition.Y);

            }
            else
            {
                spriteBatch.Draw(sTexture, sPosition, sRectangles[frameIndex], color);
                int i = 0;
                foreach (Status status in this.statuses)
                {
                    spriteBatch.Draw(status.img, new Vector2(sPosition.X + 20*i, sPosition.Y), Color.White);
                    i++;
                }
                debug_text.changeMessage("atk: " + attack + " def: " + defense + "satk: " + special_attack + " sdef: " + special_defense);
                debug_text.draw(spriteBatch, (int)sPosition.X - 100, (int)sPosition.Y);


            }
        }

        public Skill Think()
        {
            // Get the health remaining if normalized to 100
            // Essentially, this is a percentage times 100
            Random random = new Random();

            int thought = random.Next(100);

            int normal_health = this.health*100/this.max_health;
            if (normal_health < 20)
            {
                //I need health!
                if (thought > 20)
                {
                    //cure
                }
            }

            //We will use our battlemanager.Knowledge to detemine what we know about boss
            //If the key exists we know whether it does or doesn't work
            //If it doesn't exist, fuck it and try anything
            if (this.energy > 10)
            {
                //I can use abilities!

                //Consider exploiting a weakness?
                if (BattleManager.Knowledge.ContainsKey(Knowledge.Weak_Def))
                {
                    //If we know there is a weakness and we are smart enough to make the move
                    if (BattleManager.Knowledge[Knowledge.Weak_Def] && thought > 100-(40+5*level))
                    {
                        //Use strong physical skill
                    }
                    thought = random.Next(100);
                }
                else if (BattleManager.Knowledge.ContainsKey(Knowledge.Weak_SDef) && thought > 100 - (40 + 5 * level))
                {
                    if (BattleManager.Knowledge[Knowledge.Weak_SDef])
                    {
                        //Use strong special skill
                    }
                    thought = random.Next(100);

                }
                else if (BattleManager.Knowledge.ContainsKey(Knowledge.Str_Atk) && thought > 100 - (40 + 5 * level))
                {
                    if (BattleManager.Knowledge[Knowledge.Str_Atk])
                    {
                        //Use status effect lowering skill
                    }
                    thought = random.Next(100);

                }
                else if (BattleManager.Knowledge.ContainsKey(Knowledge.Str_SAtk) && thought > 100 - (40 + 5 * level))
                {
                    if (BattleManager.Knowledge[Knowledge.Str_SAtk])
                    {
                        //Use status effect lowering skill
                    }
                    thought = random.Next(100);
                }
                //Let's take another thought
                thought = random.Next(100);

                //Consider adding a status effect?
                if (!BattleManager.boss.statuses.Contains(Status.check_poison) //If the boss doesn't have poison
                    && (BattleManager.boss.health*100)/BattleManager.boss.max_health > 75 //and it has high health
                    && thought > 100 - (40 + 5 * level)) // and the Enemy has a good thought
                {
                    //Use status inflicting skill
                }
            }

            
            return basic_attack;
        }
    }
}
