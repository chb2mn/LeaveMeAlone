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

        public int character_counter = 0;

        public Character(int _max_health, int _attack, int _special_attack, int _defence, int _special_defence, int _energy, int _level, int _manaRechargeRate, Texture2D _sprite)
        {
            id = character_counter++;
            max_health = _max_health;
            attack = _attack;
            special_attack = _special_attack;
            defence = _defence;
            special_defense = _special_defence;
            energy = _energy;
            level = _level;
            manaRechargeRate = _manaRechargeRate;
            sprite = _sprite; 
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
