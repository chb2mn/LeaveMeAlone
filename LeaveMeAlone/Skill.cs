using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaveMeAlone
{
    class Skill
    {
        public static int total;
        public int id;
        public string name;
        public int energy;
        public int cost;
        public int level;
        public int cooldown;
        public int type;
        public string description;
        public Delegate run;
        public Skill(string name, int energy, int cost, int level, int cooldown, int type, string description, Delegate run)
        {
            this.name = name;
            this.energy = energy;
            this.cost = cost;
            this.level = level;
            this.cooldown = cooldown;
            this.type = type;
            this.description = description;
            this.run = run;
        }
    }
}
