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
        public static MenuBoss selectedBoss;
        public static void Init(MenuBoss boss)
        {
            SkillTree.Init();
            selectedBoss = boss;
            selectedBoss.MoveTo(new Vector2( 0, 0));
            selectedBoss.idle();
        }

        public static void loadContent(ContentManager content)
        {
            menuBackground = content.Load<Texture2D>("DummyHero");
            next = new Button(content.Load<Texture2D>("next"), 250, 250, 113, 32);
        }

        public static LeaveMeAlone.GameState Update(GameTime g)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            selectedBoss.Update(g);
            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {

                if (next.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    BattleManager.bossDefaultPosition();
                    return LeaveMeAlone.GameState.Lair;
                }
            }
            return LeaveMeAlone.GameState.Upgrade;
        }

        public static void Draw(SpriteBatch sb)
        {

            if (LeaveMeAlone.gamestate == LeaveMeAlone.GameState.Upgrade)
            {
                sb.Draw(menuBackground, new Rectangle(0, 0, 800, 600), Color.Black);
                selectedBoss.Draw(sb, Color.White);
                SkillTree.skilltrees[selectedBoss.bossType].Draw(sb);
                next.Draw(sb);
            }
        }
    }
}
