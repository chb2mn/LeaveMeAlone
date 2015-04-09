using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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

        public Text(string msg = "", Vector2 position = default(Vector2), SpriteFont f = default(SpriteFont), Color c = default(Color))
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
            DirectoryInfo dir = new DirectoryInfo(content.RootDirectory + "/Fonts");
            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            FileInfo[] files = dir.GetFiles("*.xnb");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                fonts[key] = content.Load<SpriteFont>("Fonts/" + key);
            }
        }

        public void Draw(SpriteBatch sb,  Vector2 pos = default(Vector2), Color c = default(Color))
        {
            if (c == default(Color))
            {
                c = this.color;
            }
            if (pos == default(Vector2))
            {
                pos = position;
            }
            sb.DrawString(font, message, pos, c);
        }
        public void Move(Vector2 pos)
        {
            position = pos;
        }
        /*public void draw(SpriteBatch sb, int x, int y)
        {
            //draws a string, params are your font, your message, position, and color
            if (font != null)
            {
                sb.DrawString(font, message, new Vector2(x, y), color);
            }
        }*/
        
    }
}
