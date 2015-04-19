﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaveMeAlone
{
    public class Resources
    {
        public static int gold;
        public static int exp;

        public static void Init()
        {
            gold = 1000;
            exp = 0;
        }
        public static int NextLevel(int level)
        {
            if (level > 12)
            {
                return 24500;
            }
            else if (level > 11)
            {
                return 20000;
            }
            else if (level > 10)
            {
                return 16000;
            }
            else if (level > 9)
            {
                return 12500;
            }
            else if (level > 8)
            {
                return 10000;
            }
            else if (level > 7)
            {
                return 8000;
            }
            else if (level > 6)
            {
                return 6000;
            }
            else if (level > 5)
            {
                return 4500;
            }
            else if (level > 4)
            {
                return 3200;
            }
            else if (level > 3)
            {
                return 2200;
            }
            else if (level > 2)
            {
                return 1700;
            }
            else if (level > 1)
            {
                return 1000;
            }
            else if (level > 0)
            {
                return 500;
            }
            return 0;
        }
        public static int get_level(int experience)
        {
            if (experience >= 24500)
            {
                return 14;
            }
            else if (experience >= 20000 ) 
            {
                return 13;
            }
            else if (experience >= 16000)
            {
                return 12;
            }
            else if (experience >= 12500)
            {
                return 11;
            }
            else if (experience >= 10000)
            {
                return 10;
            }
            else if (experience >= 8000 ) 
            {
                return 9;
            }
            else if (experience >= 6000)
            {
                return 8;
            }
            else if (experience >= 4500)
            {
                return 7;
            }
            else if (experience >= 3200)
            {
                return 6;
            }
            else if (experience >= 2200)
            {
                return 5;
            }
            else if (experience >= 1700)
            {
                return 4;
            }
            else if (experience >= 1000)
            {
                return 3;
            }
            else if (experience >= 500)
            {
                return 2;
            }
            return 1;
        }
    }
}
