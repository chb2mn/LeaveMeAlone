using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeaveMeAlone;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace LeaveMeAlone
{

    public class Button
    {    
        public Texture2D sprite;
        public Rectangle rectangle;
        public Rectangle selectRectangle;
        public Text text;
        public static Texture2D buttonPic;
        public static Texture2D greenbackground;
        public static Texture2D redbackground;
        public static Texture2D bluebackground;
        public static Texture2D graybackground;
        public bool selected = false;
        public bool notBought = false;
        public Button(Texture2D pic, int x, int y, int width, int height)
        {
            this.sprite = pic;
            this.rectangle= new Rectangle(x, y, width, height);
            this.selectRectangle= new Rectangle(x-5, y-5, width+10, height+10);
            this.text = new Text(position:new Vector2(x+10, y+10));
        }
        public static void LoadContent(ContentManager c)
        {
            buttonPic = c.Load<Texture2D>("buttonbase");
            greenbackground = c.Load<Texture2D>("green");
            redbackground = c.Load<Texture2D>("red");
            bluebackground = c.Load<Texture2D>("blue");
            graybackground = c.Load<Texture2D>("gray");
        }
        public void Draw(SpriteBatch sb, bool faded = false)
        {
            if(selected)
            {
                sb.Draw(greenbackground, selectRectangle, Color.White);
            }
            if (faded)
            {
                sb.Draw(sprite, rectangle, Color.DimGray);
            }
            else
            {
                sb.Draw(sprite, rectangle, Color.White);
            }
            text.Draw(sb);
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
