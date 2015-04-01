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
    class MainMenu
    {
        private static Texture2D menuBackground;
        private static Texture2D titleCard;
        private static Button newGame;
        private static Button loadGame;
        private static Button quit;
        private static Texture2D bruteTitle;
        private static Texture2D mastermindTitle;
        private static Texture2D operativeTitle;
        private static MouseState currentMouseState, lastMouseState;
        public static bool mainMenuOpen;
        public static bool bossMenuOpen;
        public static bool bruteHover;
        public static bool mastermindHover;
        public static bool operativeHover;
        public static bool canFinish = false;
        public static MenuBoss brute;
        public static MenuBoss mastermind;
        public static MenuBoss operative;
        


        public static void init()
        {
            currentMouseState = Mouse.GetState();
            mainMenuOpen = true;
            bossMenuOpen = false;
            brute = new MenuBoss(Character.Type.Brute, new Vector2(75, 200));
            mastermind = new MenuBoss(Character.Type.Mastermind, new Vector2(275, 200));
            operative = new MenuBoss(Character.Type.Operative, new Vector2(475, 200));
            

            brute.idle();
            mastermind.idle();
            operative.idle();
        }
        public static void loadContent(ContentManager content)
        {
            menuBackground = content.Load<Texture2D>("DummyHero");
            titleCard = content.Load<Texture2D>("TitleCard");
            newGame = new Button(content.Load<Texture2D>("NewGame"), 300, 200, 200, 75);
            loadGame = new Button(content.Load<Texture2D>("LoadGame"), 300, 275, 200, 75);
            quit = new Button(content.Load<Texture2D>("Quit"), 300, 350, 200, 75);

            bruteTitle = content.Load<Texture2D>("bruteTitle");
            mastermindTitle = content.Load<Texture2D>("mastermindTitle");
            operativeTitle = content.Load<Texture2D>("operativeTitle");


        }
        public static LeaveMeAlone.GameState Update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (mainMenuOpen)
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (newGame.Intersects(currentMouseState.X, currentMouseState.Y))
                    {
                        mainMenuOpen = false;
                        bossMenuOpen = true;
                    }
                    else if (quit.Intersects(currentMouseState.X, currentMouseState.Y))
                    {
                        return LeaveMeAlone.GameState.Quit;
                    }
                }
            }
            if (bossMenuOpen)
            {
                if ((100 < currentMouseState.X) && (currentMouseState.X < 250))
                {
                    bruteHover = true;
                    mastermindHover = false;
                    operativeHover = false;
                    brute.walk();
                    mastermind.idle();
                    operative.idle();
                }
                if ((325 < currentMouseState.X) && (currentMouseState.X < 425))
                {
                    bruteHover = false;
                    mastermindHover = true;
                    operativeHover = false;
                    brute.idle();
                    mastermind.walk();
                    operative.idle();
                }
                if ((500 < currentMouseState.X) && (currentMouseState.X < 600))
                {
                    bruteHover = false;
                    mastermindHover = false;
                    operativeHover = true;
                    brute.idle();
                    mastermind.idle();
                    operative.walk();
                }
                brute.Update(gameTime);
                mastermind.Update(gameTime);
                operative.Update(gameTime);
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released && canFinish)
                {
                    bossMenuOpen = false;
                    Console.WriteLine("Here we go!");
                    BattleManager.heroes = PartyManager.CreateParty();
                    foreach (Character hero in BattleManager.heroes)
                    {
                        hero.Init();
                    }
                    BattleManager.Init();
                    return LeaveMeAlone.GameState.Upgrade;
                }
                canFinish = true;
            }
            return LeaveMeAlone.GameState.Main;
        }
        public static void Draw(SpriteBatch Spritebatch)
        {
            if (mainMenuOpen)
            {
                Spritebatch.Draw(menuBackground, new Rectangle(0, 0, 800, 600), Color.Black);
                Spritebatch.Draw(titleCard, new Rectangle(200, 0, 400, 200), Color.White);
                newGame.Draw(Spritebatch);
                loadGame.Draw(Spritebatch);
                quit.Draw(Spritebatch);
            }
            if (bossMenuOpen)
            {
                Spritebatch.Draw(menuBackground, new Rectangle(0, 0, 800, 600), Color.Black);
                Spritebatch.Draw(titleCard, new Rectangle(200, 0, 400, 200), Color.White);
                brute.Draw(Spritebatch, Color.White);
                mastermind.Draw(Spritebatch, Color.White);
                operative.Draw(Spritebatch, Color.White);
                if (bruteHover)
                {
                    Spritebatch.Draw(bruteTitle, new Rectangle(110, 330, 150, 50), Color.White);
                }
                if (mastermindHover)
                {
                    Spritebatch.Draw(mastermindTitle, new Rectangle(285, 330, 225, 50), Color.White);
                }
                if (operativeHover)
                {
                    Spritebatch.Draw(operativeTitle, new Rectangle(485, 330, 200, 50), Color.White);
                }
            }
        }
    }
}
