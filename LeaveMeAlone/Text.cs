using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LeaveMeAlone
{
    class Text
    {
        protected SpriteFont font;
        private string message;

        public Text(string msg)
        {
            message = msg;
        }

        public void changeMessage(string msg) 
        {
            message = msg;
        }

        public void loadContent(ContentManager content)
        {
            //loads font
            font = content.Load<SpriteFont>("Arial");
        }

        public void draw(SpriteBatch sb, int x, int y)
        {
            //draws a string, params are your font, your message, position, and color
            sb.DrawString(font, message, new Vector2(x, y), Color.Black);
        }
        
    }
}
