using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;

namespace LeaveMeAlone
{
    class MenuBoss : AnimatedSprite
    {
        private String bossType;
        public MenuBoss(String type)
        {
            bossType = type;
        }
        public void LoadContent(ContentManager content)
        {
            idleStartFrame = 0;
            idleEndFrame = 1;
            walkStartFrame = 3;
            walkEndFrame = 11;
            if (bossType == "brute")
            {
                sTexture = content.Load<Texture2D>("bruteMenu");
                sPosition = new Vector2(75, 200);
                AddAnimation(12);
                facingRight = true;
            }
            if (bossType == "mastermind")
            {
                sTexture = content.Load<Texture2D>("bruteMenu");
                sPosition = new Vector2(275, 200);
                AddAnimation(12);
                facingRight = true;
            }
            if (bossType == "operative")
            {
                sTexture = content.Load<Texture2D>("bruteMenu");
                sPosition = new Vector2(475, 200);
                AddAnimation(12);
                facingRight = true;
            }
        }
        public void Update(GameTime gameTime)
        {
            FrameUpdate(gameTime);
        }
    }
}
