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
        public Vector2 position;
        public string message;

        public static Color DEFAULT_COLOR = Color.Black;

        public Text(SpriteFont f = default(SpriteFont), Color c = default(Color), string msg = "", Vector2 position = default(Vector2))
        {
            message = msg;
            if(f == default(SpriteFont))
            {
                f = fonts["Arial-12"];
            }
            font = f;
            if(c == default(Color))
            {
                c = DEFAULT_COLOR;
            }
            color = c;
            if(position == default(Vector2))
            {
                position = new Vector2(0, 0);
            }
            this.position = position;
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
