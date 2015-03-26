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
    class MainMenu
    {
        private Texture2D menuBackground;
        private Texture2D titleCard;
        private Texture2D newGame;
        private Texture2D loadGame;
        private Texture2D quit;
        private Texture2D bruteTitle;
        private Texture2D mastermindTitle;
        private Texture2D operativeTitle;
        private MouseState currentMouseState, lastMouseState;
        public bool mainMenuOpen;
        public bool bossMenuOpen;
        public bool bruteHover;
        public bool mastermindHover;
        public bool operativeHover;
        public bool canFinish = false;
        public MenuBoss brute;
        public MenuBoss mastermind;
        public MenuBoss operative;
        public MainMenu()
        {
            init();
        }
        public void init()
        {
            currentMouseState = Mouse.GetState();
            mainMenuOpen = true;
            bossMenuOpen = false;
        }
        public void loadContent(ContentManager content)
        {
            menuBackground = content.Load<Texture2D>("DummyHero");
            titleCard = content.Load<Texture2D>("TitleCard");
            newGame = content.Load<Texture2D>("NewGame");
            loadGame = content.Load<Texture2D>("LoadGame");
            quit = content.Load<Texture2D>("Quit");

            bruteTitle = content.Load<Texture2D>("bruteTitle");
            mastermindTitle = content.Load<Texture2D>("mastermindTitle");
            operativeTitle = content.Load<Texture2D>("operativeTitle");

            brute = new MenuBoss("brute");
            mastermind = new MenuBoss("mastermind");
            operative = new MenuBoss("operative");
            brute.LoadContent(content);
            mastermind.LoadContent(content);
            operative.LoadContent(content);
            brute.idle();
            mastermind.idle();
            operative.idle();
        }
        public void Update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (mainMenuOpen)
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    if ((300 < currentMouseState.X) && (currentMouseState.X < 500))
                    {
                        mainMenuOpen = false;
                        bossMenuOpen = true;
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
                }
                canFinish = true;
            }
        }
        public void Draw(SpriteBatch Spritebatch)
        {
            if (mainMenuOpen)
            {
                Spritebatch.Draw(menuBackground, new Rectangle(0, 0, 800, 600), Color.Black);
                Spritebatch.Draw(titleCard, new Rectangle(200, 0, 400, 200), Color.White);
                Spritebatch.Draw(newGame, new Rectangle(300, 200, 200, 75), Color.White);
                Spritebatch.Draw(loadGame, new Rectangle(300, 275, 200, 75), Color.White);
                Spritebatch.Draw(quit, new Rectangle(300, 350, 200, 75), Color.White);
            }
            if (bossMenuOpen)
            {
                Spritebatch.Draw(menuBackground, new Rectangle(0, 0, 800, 600), Color.Black);
                Spritebatch.Draw(titleCard, new Rectangle(200, 0, 400, 200), Color.White);
                brute.Draw(Spritebatch);
                mastermind.Draw(Spritebatch);
                operative.Draw(Spritebatch);
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
