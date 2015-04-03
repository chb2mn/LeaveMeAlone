using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace LeaveMeAlone
{
    public class LairManager
    {
        public static int TowerLevel;
        public static int MaxLevel;
        public static List<Room> LairRooms;
        private static Texture2D lairBkgd;
        private static Button skillsBtn, nextwaveBtn, constructionBtn;
        private static MouseState currentMouseState, lastMouseState;
        //Took out the parameters on these next two functions as 
        //they are likely going to want to hit every room in the lair?
        public static void loadContent(ContentManager content)
        {
            skillsBtn = new Button(content.Load<Texture2D>("skillsBtn"), 300, 200, 200, 75);
            nextwaveBtn = new Button(content.Load<Texture2D>("nextwaveBtn"), 300, 275, 200, 75);
            constructionBtn = new Button(content.Load<Texture2D>("constructionBtn"), 300, 350, 200, 75);
            lairBkgd = content.Load<Texture2D>("lairBkgd");
        }
        public static void LairAttack()
        {

        }
        public static void AdvanceLevel()
        {

        }
        public static LeaveMeAlone.GameState Update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {
                if (nextwaveBtn.Intersects(currentMouseState.X, currentMouseState.Y))
                {

                }
            }

            return LeaveMeAlone.GameState.Lair;
        }
        public static void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(lairBkgd, new Rectangle(-1000, -600, 2308, 1200), Color.White);
            nextwaveBtn.Draw(Spritebatch);
            skillsBtn.Draw(Spritebatch);
            constructionBtn.Draw(Spritebatch);
        }
    }
}
