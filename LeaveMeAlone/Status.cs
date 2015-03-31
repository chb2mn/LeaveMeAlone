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
        public static Texture2D poison_image;
        public static Texture2D beserk_image;

        public static void LoadContent(ContentManager content)
        {
            
            poison_image = content.Load<Texture2D>("Poison");
            beserk_image = content.Load<Texture2D>("Beserk");

            if (poison_image == null)
            {
                Console.WriteLine("Poison didn't load");
            }
            poison = new Status("poison", 3, Effect_Time.After, Type.Debuff, poison_image, Poison);
            beserk = new Status("beserk", 3, Effect_Time.Once, Type.Debuff, beserk_image, Beserk, rev_Beserk);
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

        //>>>>>>>>>>>>>>>>>>>>accessor<<<<<<<<<<<<<<<<<<<//


        //>>>>>>>>>>>>>>>>>>>>methods<<<<<<<<<<<<<<<<<<<<//
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
    }
}
