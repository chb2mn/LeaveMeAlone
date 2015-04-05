using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

namespace LeaveMeAlone
{
    public class MenuBoss : AnimatedSprite
    {
        public Character.Type bossType;
        private Rectangle bounding;
        public static Dictionary<Character.Type, Texture2D> textures = new Dictionary<Character.Type,Texture2D>();
        public MenuBoss(Character.Type type, Vector2 pos)
        {
            bossType = type;
            sPosition = pos;
            sTexture = textures[bossType];
            //funky stuff accounting for sprites
            bounding = new Rectangle((int)sPosition.X+50, (int)sPosition.Y, 125, sTexture.Height);
            idleStartFrame = 0;
            idleEndFrame = 1;
            walkStartFrame = 3;
            walkEndFrame = 11;
            AddAnimation(12);
            facingRight = true;
        }
        public bool Contains(Vector2 v)
        {
            if(bounding.Contains((int)v.X, (int)v.Y))
            {
                return true;
            }
            return false;
        }
        public void MoveTo(Vector2 v)
        {
            bounding.X = (int)v.X;
            bounding.Y = (int)v.Y;
            sPosition = v;
        }
        public static void LoadContent(ContentManager content)
        {
            textures[Character.Type.Brute] = content.Load<Texture2D>("bruteMenu");
            textures[Character.Type.Operative] = content.Load<Texture2D>("bruteMenu");
            textures[Character.Type.Mastermind] = content.Load<Texture2D>("bruteMenu");
        }

        public void Update(GameTime gameTime)
        {
            FrameUpdate(gameTime);
        }
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
    }
}
