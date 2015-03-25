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
        public int id;
        public List<Skill> skills;
        public List<Skill> selected_skills;
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
        public void cast(Skill skill, BattleManager bm, Character target = null)
        {
            skill.runnable(bm, this, target);
        }
    }
}
