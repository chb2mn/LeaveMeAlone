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

        public int health;
        public int attack;
        public int special_attack;
        public int defence;
        public int special_defense;
        public int energy;
        public int level;
        public int manaRechargeRate;

        public Texture2D sprite;
        public int spriteIndex;
        public int animationStart;
        public int animationEnd;
        public int animationNext;

        public void update() {
        
        }
        public void draw(SpriteBatch Spritebatch){

        }
        public void levelUp(){

        }

    }
}
