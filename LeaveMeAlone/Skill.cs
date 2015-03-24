using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaveMeAlone
{
    public class Skill
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
        public Run runnable;
        public delegate void Run(Character caster);

        // example way to make a skill
        // Skill s = new Skill("test", 1, 100, 1, 0, 0, "My first skill", new Skill.Run(function_name));
        public Skill(string name, int energy, int cost, int level, int cooldown, int type, string description, Skill.Run run)
        {
            this.name = name;
            this.energy = energy;
            this.cost = cost;
            this.level = level;
            this.cooldown = cooldown;
            this.type = type;
            this.description = description;
            this.runnable = run;
        }
    }
}
