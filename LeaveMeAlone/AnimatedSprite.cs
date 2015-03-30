using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace LeaveMeAlone
{
    public abstract class AnimatedSprite
    {
        #region Fields

        protected Texture2D sTexture;
        public Vector2 sPosition;
        protected bool facingRight;
        protected float aWidth;
        protected float aHeight;
        protected Rectangle[] sRectangles;
        protected int frameIndex;
        private double timeElapsed;
        private double timeToUpdate;
        private int startFrame;
        private int endFrame;
        public int idleStartFrame;
        public int idleEndFrame;
        public int walkStartFrame;
        public int walkEndFrame;
        private string currentState;
        private string lastState;

        public double FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        #endregion
        public AnimatedSprite()
        {
            sPosition = new Vector2();
        }

        public void AddAnimation(int frames)
        {
            //Calculates the width of each frame
            int width = (int)Math.Floor((double)(sTexture.Width / frames));
            aWidth = width + 25;
            aHeight = sTexture.Height;
            //Creates an array of rectangles which will be used when playing an animation
            sRectangles = new Rectangle[frames];

            //Fills up the array of rectangles
            for (int i = 0; i < frames; i++)
            {
                sRectangles[i] = new Rectangle(i * width, 0, width, sTexture.Height);
            }
        }

        public void FrameUpdate(GameTime gameTime)
        {

            //Adds time that has elapsed since our last draw
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            //We need to change our image if our timeElapsed is greater than our timeToUpdate(calculated by our framerate)
            if (timeElapsed > timeToUpdate)
            {
                //Resets the timer in a way, so that we keep our desired FPS
                timeElapsed -= timeToUpdate;

                //Adds one to our frameIndex
                if (frameIndex < endFrame)
                {
                    frameIndex++;
                }
                else //Restarts the animation
                {
                    frameIndex = startFrame;
                }
            }
        }
        public void idle()
        {
            currentState = "idle";
            if (currentState != lastState)
            {
                startFrame = idleStartFrame;
                endFrame = idleEndFrame;
                frameIndex = startFrame;
                FramesPerSecond = 1;
            }
            lastState = currentState;
        }
        public void walk()
        {
            currentState = "walk";
            if (currentState != lastState)
            {
                startFrame = walkStartFrame;
                endFrame = walkEndFrame;
                frameIndex = startFrame;
                FramesPerSecond = 10;
            }
            lastState = currentState;
        }
        /*
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (facingRight)
            {
                Vector2 oPosition = new Vector2(sPosition.X + 5, sPosition.Y);
                spriteBatch.Draw(sTexture, oPosition, sRectangles[frameIndex], color, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(sTexture, sPosition, sRectangles[frameIndex], color);
            }
        }
         * */
    }
}
