using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeaveMeAlone;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LeaveMeAlone
{

    public class Button
    {    
        public Texture2D sprite;
        public Rectangle rectangle;
        public Text text;
        public Button(Texture2D pic, Rectangle rec, Text text)
        {
            this.sprite = pic;
            this.rectangle = rec;
            this.text = text;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(sprite, rectangle, Color.White);
            text.draw(sb, rectangle.X+10, rectangle.Y+10);
        }
        public Boolean Intersects(int x, int y)
        {
            if(rectangle.Contains(x,y))
            {
                return true;
            }
            return false;
        }
        public void UpdateText(string update)
        {
            text.changeMessage(update);
        }
    }
}
