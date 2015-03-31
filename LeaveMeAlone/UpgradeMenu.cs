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
    public class UpgradeMenu
    {
        private static Texture2D menuBackground;
        private static Button next;
        private static MouseState currentMouseState, lastMouseState;


        public static void loadContent(ContentManager content)
        {
            menuBackground = content.Load<Texture2D>("DummyHero");
            next = new Button(content.Load<Texture2D>("next"), 250, 250, 113, 32);
        }

        public static LeaveMeAlone.GameState Update(GameTime g)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {

                if (next.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    return LeaveMeAlone.GameState.Battle;
                }
            }
            return LeaveMeAlone.GameState.Upgrade;
        }

        public static void Draw(SpriteBatch sb)
        {
            sb.Draw(menuBackground, new Rectangle(0, 0, 800, 600), Color.Black);
            BattleManager.boss.sPosition = new Vector2( 50, 50);
            BattleManager.boss.Draw(sb, Color.White);

            next.Draw(sb);
        }
    }
}
