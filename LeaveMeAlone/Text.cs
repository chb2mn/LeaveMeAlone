using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LeaveMeAlone
{
    public class Text
    {
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string,SpriteFont>();
        public SpriteFont font;
        public Color color;
        public string message;

        public static Color DEFAULT_COLOR = Color.Black;

        public Text(SpriteFont f, Color c, string msg="")
        {
            message = msg;
            font = f;
            color = c;
        }
        public Text(string msg = "")
        {
            message = msg;
            font = fonts["Arial-12"];
            color = DEFAULT_COLOR;
        }

        public void changeMessage(string msg) 
        {
            message = msg;
        }

        public static void loadContent(ContentManager content)
        {
            //loads font
            fonts["Arial-12"] = content.Load<SpriteFont>("Fonts/Arial-12");
            fonts["Arial-24"] = content.Load<SpriteFont>("Fonts/Arial-24");
            fonts["BattleMenuText-12"] = content.Load<SpriteFont>("Fonts/BattleMenuText-12");
            fonts["RetroComputer-12"] = content.Load<SpriteFont>("Fonts/RetroComputer-12");
        }

        public void draw(SpriteBatch sb, int x, int y)
        {
            //draws a string, params are your font, your message, position, and color
            if (font != null)
            {
                sb.DrawString(font, message, new Vector2(x, y), color);
            }
        }
        
    }
}
