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

        public static void loadContent(ContentManager content)
        {
            menuBackground = content.Load<Texture2D>("DummyHero");
        }

        public static Game1.GameState Update()
        {
            return Game1.GameState.Battle;
        }

        public static void Draw(SpriteBatch sb)
        {
            sb.Draw(menuBackground, new Rectangle(0, 0, 800, 600), Color.Black);
            BattleManager.boss.sPosition = new Vector2( 50, 50);
            BattleManager.boss.Draw(sb, Color.White);
        }
    }
}
