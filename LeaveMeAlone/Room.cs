using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace LeaveMeAlone
{
    public class Room
    {
        public int id;
        public String name;
        public int cost;
        public int level;
        public int type;
        public String description;
        public delegate void Run(List<Character> heroes);
        public Run activate;
        public Texture2D img;

        public Room(string _name, int _gold_cost, int _level, int _type, string _description, Room.Run _run, Texture2D _img)
        {
            this.name = _name;
            this.cost = _gold_cost;
            this.level = _level;
            this.type = _type;
            this.description = _description;
            this.activate = _run;
            this.img = _img;

        }
    }


}
