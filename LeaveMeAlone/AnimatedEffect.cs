using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace LeaveMeAlone
{
    public class AnimatedEffect
    {
        #region Fields

        public Texture2D sTexture;
        public Vector2 sPosition;
        public Vector2 sDestination;
        protected bool facingRight;
        private int offset = 0;
        protected float aWidth;
        protected float aHeight;
        protected Rectangle[] sRectangles;
        protected int frameIndex;
        private double timeElapsed;
        private double timeToUpdate;
        private int startFrame;
        private int endFrame;
        public int width;
        public int height;
        private int xVel, yVel;
        public static Texture2D magefire, cure, defend, poison_dagger, poison_dagger2, portal_punch, flamethrower, tophat;
        public enum EffectType{magefire, cure, defend, poison_dagger, portal_punch, flamethrower, tophat};
        public EffectType effectType;

        public double FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        #endregion
        public AnimatedEffect(Vector2 position, Vector2 destination, EffectType effect, bool isFacingRight)
        {
            sPosition = position;
            sDestination = destination;
            effectType = effect;
            facingRight = isFacingRight;
            if (effectType == EffectType.magefire){
                sTexture = magefire;
                AddAnimation(1);
            }
            if (effectType == EffectType.cure)
            {
                sTexture = cure;
                AddAnimation(28);
                startFrame = 0;
                endFrame = 27;
            }
            if (effectType == EffectType.defend)
            {
                sTexture = defend;
                AddAnimation(39);
                startFrame = 0;
                endFrame = 38;
            }
            if (effectType == EffectType.poison_dagger)
            {
                if (sPosition.Y > 300)
                {
                    sTexture = poison_dagger2;
                }
                else
                {
                    sTexture = poison_dagger;
                }
                AddAnimation(1);
            }
            if (effectType == EffectType.portal_punch)
            {
                sTexture = portal_punch;
                AddAnimation(1);
            }
            if (effectType == EffectType.flamethrower)
            {
                sTexture = flamethrower;
                AddAnimation(1);
            }
            if (effectType == EffectType.tophat)
            {
                sTexture = tophat;
                AddAnimation(1);
            }
        }

        public static void LoadContent(ContentManager content)
        {
            magefire = content.Load<Texture2D>("magefire");
            cure = content.Load<Texture2D>("sigil");
            defend = content.Load<Texture2D>("shield");
            poison_dagger = content.Load<Texture2D>("poisondagger");
            poison_dagger2 = content.Load<Texture2D>("poisondagger2");
            portal_punch = content.Load<Texture2D>("portalpunch");
            flamethrower = content.Load<Texture2D>("magefire2");
            tophat = content.Load<Texture2D>("tophat");
        }

        public void AddAnimation(int frames)
        {
            //Calculates the width of each frame
            width = (int)Math.Floor((double)(sTexture.Width / frames));
            aWidth = width + 25;
            aHeight = sTexture.Height;
            height = sTexture.Height;
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
            if (effectType == EffectType.magefire || effectType == EffectType.poison_dagger || effectType == EffectType.portal_punch || effectType == EffectType.flamethrower || effectType == EffectType.tophat)
            {
                if (xVel == 0)
                {
                    xVel = (int)((sDestination.X - sPosition.X)/35);
                }
                if (yVel == 0)
                {
                    yVel = (int)((sDestination.Y - sPosition.Y)/35);
                }
                sPosition.X += xVel;
                sPosition.Y += yVel;
            }
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
                    if (effectType == EffectType.cure || effectType == EffectType.defend)
                    {
                        endFrame = 0;
                    }
                    frameIndex = startFrame;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (facingRight)
            {
                Vector2 oPosition = new Vector2(sPosition.X + 5, sPosition.Y);
                spriteBatch.Draw(sTexture, sPosition, sRectangles[frameIndex], color);
            }
            else
            {
                spriteBatch.Draw(sTexture, sPosition, sRectangles[frameIndex], color);
            }
        }
    }
}
