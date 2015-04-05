using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace LeaveMeAlone
{
    public class Status
    {
        public Texture2D img;
        public int id;
        public String name;
        public int duration;
        public int duration_left;
        //How often does the status affect the character
        public enum Effect_Time { Once, Every, Before, After };

        public Effect_Time effect_time;
        public enum Type { Buff, Debuff };
        public Type type;
        public Affect affect;
        public Affect reverse_affect;
        public delegate void Affect(Character carrier);

        public static Status poison;
        public static Status beserk;
        public static Status defend;
        public static Status specdefend;

        public static Status attackplus;
        public static Status attackminus;
        public static Status defenseplus;
        public static Status defenseminus;
        public static Status specialattackplus;
        public static Status specialattackminus;
        public static Status specialdefenseplus;
        public static Status specialdefenseminus;

        public static Texture2D poison_image;
        public static Texture2D beserk_image;
        public static Texture2D atkplus_image;
        public static Texture2D atkminus_image;
        public static Texture2D defplus_image;
        public static Texture2D defminus_image;
        public static Texture2D specplus_image;
        public static Texture2D specminus_image;
        public static Texture2D specdefplus_image;
        public static Texture2D specdefminus_image;

        public static void LoadContent(ContentManager content)
        {
            
            poison_image = content.Load<Texture2D>("Poison");
            beserk_image = content.Load<Texture2D>("Beserk");
            atkplus_image = content.Load<Texture2D>("AttackPlus");
            atkminus_image = content.Load<Texture2D>("AttackMinus");
            defplus_image = content.Load<Texture2D>("DefPlus");
            defminus_image = content.Load<Texture2D>("DefMinus");
            specplus_image = content.Load<Texture2D>("SpecPlus");
            specminus_image = content.Load<Texture2D>("SpecMinus");
            specdefplus_image = content.Load<Texture2D>("SpecDefPlus");
            specdefminus_image = content.Load<Texture2D>("SpecDefMinus");

            if (poison_image == null)
            {
                Console.WriteLine("Poison didn't load");
            }

            poison = new Status("poison", 3, Effect_Time.After, Type.Debuff, poison_image, Poison);
            beserk = new Status("beserk", 3, Effect_Time.Once, Type.Debuff, beserk_image, Beserk, rev_Beserk);
            defend = new Status("def+", 2, Effect_Time.Once, Type.Buff, defplus_image, DoNothing, ReduceDefense);
            specdefend = new Status("specdef+", 2, Effect_Time.Once, Type.Buff, specdefplus_image, DoNothing, ReduceSDefense);


            attackplus = new Status("atk+", 3, Effect_Time.Once, Type.Buff, atkplus_image, RaiseAttack, ReduceAttack);
            attackminus = new Status("atk-", 3, Effect_Time.Once, Type.Debuff, atkminus_image, ReduceAttack, RaiseAttack);
            defenseplus = new Status("def+", 3, Effect_Time.Once, Type.Buff, defplus_image, RaiseDefense, ReduceDefense);
            defenseminus = new Status("def-", 3, Effect_Time.Once, Type.Debuff, defminus_image, ReduceDefense, RaiseDefense);
            specialattackplus = new Status("spec+", 3, Effect_Time.Once, Type.Buff, specplus_image, RaiseSAttack, ReduceSAttack);
            specialattackminus = new Status("spec-", 3, Effect_Time.Once, Type.Debuff, specminus_image, ReduceSAttack, RaiseSAttack);
            specialdefenseplus = new Status("specdef+", 3, Effect_Time.Once, Type.Buff, specdefplus_image, RaiseSDefense, ReduceSDefense);
            specialdefenseminus = new Status("specdef-", 3, Effect_Time.Once, Type.Debuff, specdefminus_image, ReduceSDefense, RaiseSDefense);


        }

        public Status (String _name, int _duration, Effect_Time _effect_time, Type _type, Texture2D _img, Affect _Effect, Affect _Reverse = null)
        {
            name = _name;
            duration = _duration;
            duration_left = duration;
            img = _img;
            effect_time = _effect_time;
            type = _type;
            this.affect = _Effect;
            this.reverse_affect = _Reverse;
        }

        public override String ToString()
        {
            return this.name;
        }
        //>>>>>>>>>>>>>>>>>>>>accessor<<<<<<<<<<<<<<<<<<<//


        //>>>>>>>>>>>>>>>>>>>>methods<<<<<<<<<<<<<<<<<<<<//
        private static void DoNothing(Character carrier)
        {
            //This method is used for statuses that activate after a number of turns 
            return;
        }
        private static void Poison(Character carrier)
        {
            int damage = (int)(carrier.max_health * .1);
            carrier.health -= damage;
            carrier.damage_text.changeMessage((-damage).ToString());
        }
        private static void Beserk(Character carrier)
        {
            carrier.attack *= 2;
        }
        private static void rev_Beserk(Character carrier)
        {
            carrier.attack /= 2;
        }

        
        private static void RaiseAttack(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.attack += reduction;
        }
        private static void ReduceAttack(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.attack -= reduction;
            if (carrier.attack <= 0)
            {
                carrier.attack = 0;
            }
        }
        private static void RaiseDefense(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            Console.WriteLine(reduction);
            carrier.defense += reduction;
        }
        private static void ReduceDefense(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.defense -= reduction;
            if (carrier.defense <= 0)
            {
                carrier.defense = 0;
            }
        }
        private static void RaiseSAttack(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.special_attack += reduction;
        }
        private static void ReduceSAttack(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.special_attack -= reduction;
            if (carrier.special_attack <= 0)
            {
                carrier.special_attack = 0;
            }
        }
        private static void RaiseSDefense(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.special_defense += reduction;
        }
        private static void ReduceSDefense(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.special_defense -= reduction;
            if (carrier.special_defense <= 0)
            {
                carrier.special_defense = 0;
            }
        }
    }
}
