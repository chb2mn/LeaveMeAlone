using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
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
        public List<Status> statuses;

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
        public Skill basic_attack;
        public Text damage_text;
        //public Skill defend;

        public Texture2D sprite;
        public int spriteIndex;
        public int animationStart;
        public int animationEnd;
        public int animationNext;
        public Type charType;

        public enum Type{Ranger, Mage, Knight, Brute, Mastermind, Operative};

        public const int MAX_SKILLS = 6;

        public Character(int _max_health, int _attack, int _special_attack, int _defense, int _special_defense, int _max_energy, int _level, int _manaRechargeRate, Text _damage_text)
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
            basic_attack = SkillTree.basic_attack;
            damage_text = _damage_text;
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
        public void loadContent(ContentManager content)
        {
            idleStartFrame = 0;
            idleEndFrame = 1;
            walkStartFrame = 3;
            walkEndFrame = 11;
            facingRight = false;
            if (charType == Type.Mage)
            {
                sTexture = content.Load<Texture2D>("hood2");
                facingRight = true;
            }
            if (charType == Type.Ranger)
            {
                sTexture = content.Load<Texture2D>("hood2");
                facingRight = true;
            }
            if (charType == Type.Knight)
            {
                sTexture = content.Load<Texture2D>("hood1");
                facingRight = true;
            }
            if (charType == Type.Brute)
            {
                sTexture = content.Load<Texture2D>("mastermind70");
            }
            if (charType == Type.Mastermind)
            {
                sTexture = content.Load<Texture2D>("mastermind70");
            }
            if (charType == Type.Operative)
            {
                sTexture = content.Load<Texture2D>("mastermind70");
            }
            idle();
            AddAnimation(12);
        }

        public void Update(GameTime gameTime) {
            FrameUpdate(gameTime);
        }
        public void levelUp(){

        }
        public void cast(Skill skill, Character target = null)
        {
            skill.runnable(this, target);
        }

        
    }
}
