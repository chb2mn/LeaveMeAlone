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
        public int power;
        public int duration;
        public int duration_left;
        //How often does the status affect the character
        public enum Effect_Time { Once, Every, Before, After };
        public Effect_Time effect_time;
        public enum Type { Buff, Debuff, Other };
        public Type type;
        public Affect affect;
        public Affect reverse_affect;

        public delegate void Affect(Character carrier);

        public static Status check_poison;
        public static Status check_beserk;
        public static Status check_defend;
        public static Status check_specdefend;
        public static Status check_abom;
        public static Status check_stun;
        public static Status check_haste;

        public static Status check_attackplus;
        public static Status check_attackminus;
        public static Status check_defenseplus;
        public static Status check_defenseminus;
        public static Status check_specialattackplus;
        public static Status check_specialattackminus;
        public static Status check_specialdefenseplus;
        public static Status check_specialdefenseminus;

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
        public static Texture2D target_status_image;
        public static Texture2D no_image;
        public static Texture2D stun_image;
        public static Texture2D haste_image;

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
            no_image = content.Load<Texture2D>("Blank");
            target_status_image = content.Load<Texture2D>("Target_Status");
            stun_image = content.Load<Texture2D>("Stun_Image");
            haste_image = content.Load<Texture2D>("Haste_Image");

            if (poison_image == null)
            {
                Console.WriteLine("Poison didn't load");
            }

            /*
             * These are used to check if a status is present in person
             */
            check_poison = new Status("poison", 3, 0, Effect_Time.After, Type.Debuff, poison_image, Poison);
            check_beserk = new Status("beserk", 3, 0, Effect_Time.Once, Type.Debuff, beserk_image, Beserk, rev_Beserk);
            check_defend = new Status("defend", 2, 0, Effect_Time.Once, Type.Buff, defplus_image, DoNothing, ReduceDefense);
            check_specdefend = new Status("specdefend", 2, 0, Effect_Time.Once, Type.Buff, specdefplus_image, DoNothing, ReduceSDefense);
            check_abom = new Status("abom", 3, 0, Effect_Time.Once, Type.Other, null, DoNothing, null);
            check_stun = new Status("stun", 3, 0, Effect_Time.Once, Type.Debuff, stun_image, DoNothing, DoNothing);
            check_haste = new Status("haste", 3, 0, Effect_Time.Once, Type.Debuff, haste_image, DoNothing, DoNothing);

            check_attackplus = new Status("atk+", 3, 0, Effect_Time.Once, Type.Buff, atkplus_image, RaiseAttack, ReduceAttack);
            check_attackminus = new Status("atk-", 3, 0, Effect_Time.Once, Type.Debuff, atkminus_image, ReduceAttack, RaiseAttack);
            check_defenseplus = new Status("def+", 3, 0, Effect_Time.Once, Type.Buff, defplus_image, RaiseDefense, ReduceDefense);
            check_defenseminus = new Status("def-", 3, 0, Effect_Time.Once, Type.Debuff, defminus_image, ReduceDefense, RaiseDefense);
            check_specialattackplus = new Status("spec+", 3, 0, Effect_Time.Once, Type.Buff, specplus_image, RaiseSAttack, ReduceSAttack);
            check_specialattackminus = new Status("spec-", 3, 0, Effect_Time.Once, Type.Debuff, specminus_image, ReduceSAttack, RaiseSAttack);
            check_specialdefenseplus = new Status("specdef+", 3, 0, Effect_Time.Once, Type.Buff, specdefplus_image, RaiseSDefense, ReduceSDefense);
            check_specialdefenseminus = new Status("specdef-", 3, 0, Effect_Time.Once, Type.Debuff, specdefminus_image, ReduceSDefense, RaiseSDefense);
            

        }

        public Status (String _name, int _duration, int _power, Effect_Time _effect_time, Type _type, Texture2D _img, Affect _Effect, Affect _Reverse = null)
        {
            name = _name;
            power = _power;
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

        public override bool Equals(object obj)
        {
            Status candidate;
            try
            {
                candidate = (Status)obj;
            }
            catch
            {
                return false;
            }
            return this.name.Equals(candidate.name);
        }
        //>>>>>>>>>>>>>>>>>>>>accessor<<<<<<<<<<<<<<<<<<<//


        //>>>>>>>>>>>>>>>>>>>>methods<<<<<<<<<<<<<<<<<<<<//
        public static void DoNothing(Character carrier)
        {
            //This method is used for statuses that activate after a number of turns 
            return;
        }
        public static void Poison(Character carrier)
        {
            int damage = (int)(carrier.max_health * .1);
            carrier.health -= damage;
            carrier.damage_text.changeMessage((-damage).ToString());
        }
        public static void Beserk(Character carrier)
        {
            carrier.attack *= 2;
        }
        public static void rev_Beserk(Character carrier)
        {
            carrier.attack /= 2;
        }

        public static void rev_Abom(Character carrier)
        {
            int temp = carrier.attack;
            carrier.attack = carrier.special_attack;
            carrier.special_attack = temp;
        }
        
        public void rev_Igor(Character carrier)
        {
            int damage = Skill.damage(this.power, carrier.special_defense, carrier.level, 120);
            carrier.health -= damage;
            carrier.damage_text.changeMessage((-damage).ToString());
        }

        public void Stun(Character carrier)
        {
            //This isn't really needed
        }



        /*
         * Basic Status Changes
         */
        public static void RaiseAttack(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.attack += reduction;
        }
        public static void ReduceAttack(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.attack -= reduction;
            if (carrier.attack <= 0)
            {
                carrier.attack = 0;
            }
        }
        public static void RaiseDefense(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            Console.WriteLine(reduction);
            carrier.defense += reduction;
        }
        public static void ReduceDefense(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.defense -= reduction;
            if (carrier.defense <= 0)
            {
                carrier.defense = 0;
            }
        }
        public static void RaiseSAttack(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.special_attack += reduction;
        }
        public static void ReduceSAttack(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.special_attack -= reduction;
            if (carrier.special_attack <= 0)
            {
                carrier.special_attack = 0;
            }
        }
        public static void RaiseSDefense(Character carrier)
        {
            int reduction = (int)(5 * (1 + carrier.level / 3));
            carrier.special_defense += reduction;
        }
        public static void ReduceSDefense(Character carrier)
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
