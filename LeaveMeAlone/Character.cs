using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using LeaveMeAlone;

namespace LeaveMeAlone
{
    public class Character
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
        public int level;
        public int manaRechargeRate;

        public Texture2D sprite;
        public int spriteIndex;
        public int animationStart;
        public int animationEnd;
        public int animationNext;
        public Type charType;

        public enum Type{Ranger, Mage, Knight, Brute, Mastermind, Operative};

        public const int MAX_SKILLS = 6;

        public Character(int _max_health, int _attack, int _special_attack, int _defense, int _special_defense, int _energy, int _level, int _manaRechargeRate, Texture2D _sprite)
        {
            id = character_counter++;
            max_health = _max_health;
            attack = _attack;
            special_attack = _special_attack;
            defense = _defense;
            special_defense = _special_defense;
            energy = _energy;
            level = _level;
            manaRechargeRate = _manaRechargeRate;
            sprite = _sprite; 
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
        public void update() {
        
        }
        public void draw(SpriteBatch Spritebatch){

        }
        public void levelUp(){

        }
        public void cast(Skill skill, Character target = null)
        {
            skill.runnable(this, target);
        }

        public static void BasicAttack(Character caster, Character target=null)
        {
            int damage = Skill.damage(caster, target, 0, 1, 1000);
            target.health -= damage;
        }
    }
}
