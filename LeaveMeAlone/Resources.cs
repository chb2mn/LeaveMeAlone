using System;
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

        public static int get_level(int experience)
        {
            if (experience > 20000)
            {
                return 10;
            }
            else if (experience > 16000 ) 
            {
                return 9;
            }
            else if (experience > 12000)
            {
                return 8;
            }
            else if (experience > 9000)
            {
                return 7;
            }
            else if (experience > 6400)
            {
                return 6;
            }
            else if (experience > 4400)
            {
                return 5;
            }
            else if (experience > 3400)
            {
                return 4;
            }
            else if (experience > 2000)
            {
                return 3;
            }
            else if (experience > 1000)
            {
                return 2;
            }
            return 1;
        }
    }
}
