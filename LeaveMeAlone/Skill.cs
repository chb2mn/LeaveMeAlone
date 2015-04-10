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
        public Status inflicts;
        public string description;
        public Target target;
        public Run runnable;

        public enum Target { Self, Single, All }
        public enum Attack { Attack,  SpecialAttack }
        public enum Defense { Defense, SpecialDefense }
        public delegate void Run(Character caster, Character target=null);

        // example way to make a skill
        // Skill s = new Skill("test", 1, 100, 1, 0, Target.Self, 0, "My first skill", new Skill.Run(function_name));
        public Skill(string name, int energy, int cost, int level, int cooldown, Target t, int type, string description, Skill.Run run, Status _inflicts = null)
        {
            this.id = total;
            total++;
            this.name = name;
            this.energy = energy;
            this.cost = cost;
            this.level = level;
            this.cooldown = cooldown;
            this.target = t;
            this.type = type;
            this.description = description;
            this.runnable = run;
            this.inflicts = _inflicts;
        }
        
        public static int damage(Character caster, Character target, Attack type_attack, Defense type_defense, int power, double modifier=1)
        {
            //figure out what stats we are using
            int attack;
            if(type_attack==Attack.Attack)
            {
                //Console.WriteLine("using attack");

                attack = caster.attack;
            }
            else
            {
                //Console.WriteLine("using sp_attack");

                attack = caster.special_attack;
            }
            int defense;
            if(type_defense==Defense.Defense)
            {
                //Console.WriteLine("using defense");
                defense = target.defense;
            }
            else
            {
                //Console.WriteLine("using sp_def");
                defense = target.special_defense;
            }
            //from .85 to 1.0
            //modifier *= (100 -(LeaveMeAlone.random.Next(16))) / 100;
            int val = (int)(((2.0 * (double)caster.level + 10.0)/250.0 * ((double)attack/(double)defense)*(double)power+2.0) * (double)modifier);
            Console.WriteLine("calculate damage: {0}", val);
            return val;
        }

        public static int damage(int attack, int defense, int level, int power, int modifier = 1)
        {
            //from .85 to 1.0
            //modifier *= (100 -(LeaveMeAlone.random.Next(16))) / 100;
            int val = (int)(((2.0 * (double)level + 10.0) / 250.0 * ((double)attack / (double)defense) * (double)power + 2.0) * (double)modifier);
            return val;
        }
        public class TargetRequiredException: Exception
        {
            public TargetRequiredException()
            { }
        }
        public override string ToString()
        {
            return String.Format("Skill {0}", name);
        }
    }
}