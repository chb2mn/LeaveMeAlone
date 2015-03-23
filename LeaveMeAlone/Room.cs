using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace LeaveMeAlone
{
    class Room
    {
        public int id;
        public String name;
        public int cost;
        public int level;
        public Delegate run;
        public Texture2D img;
    }
}
