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
        private static bool isNewGame;
        private static Button continueGame;
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
        private static MenuBoss current;
        


        public static void init(bool defeat = true)
        {
            currentMouseState = Mouse.GetState();
            mainMenuOpen = true;
            bossMenuOpen = false;
            isNewGame = defeat;
            brute = new MenuBoss(Character.Type.Brute, new Vector2(LeaveMeAlone.WindowX / 2 - 400, LeaveMeAlone.WindowY - 400));
            mastermind = new MenuBoss(Character.Type.Mastermind, new Vector2(LeaveMeAlone.WindowX / 2 - 100, LeaveMeAlone.WindowY - 400));
            operative = new MenuBoss(Character.Type.Operative, new Vector2(LeaveMeAlone.WindowX / 2 + 200, LeaveMeAlone.WindowY - 400));
            

            brute.idle();
            mastermind.idle();
            operative.idle();
        }
        public static void loadContent(ContentManager content)
        {
            menuBackground = content.Load<Texture2D>("DummyHero");
            titleCard = content.Load<Texture2D>("TitleCard");
            continueGame = new Button(content.Load<Texture2D>("ContinueGame"), LeaveMeAlone.WindowX / 2 - 100, 200, 200, 75);
            newGame = new Button(content.Load<Texture2D>("NewGame"), LeaveMeAlone.WindowX / 2 - 100, 200, 200, 75);
            loadGame = new Button(content.Load<Texture2D>("LoadGame"), LeaveMeAlone.WindowX / 2 - 100, 275, 200, 75);
            quit = new Button(content.Load<Texture2D>("Quit"), LeaveMeAlone.WindowX / 2 - 100, 350, 200, 75);

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
                    if (newGame.Intersects(currentMouseState.X, currentMouseState.Y) && isNewGame)
                    {
                        mainMenuOpen = false;
                        bossMenuOpen = true;
                    }
                    else if (newGame.Intersects(currentMouseState.X, currentMouseState.Y) && !isNewGame)
                    {
                        return LeaveMeAlone.GameState.Lair;
                    }
                    else if (quit.Intersects(currentMouseState.X, currentMouseState.Y))
                    {
                        return LeaveMeAlone.GameState.Quit;
                    }
                }
            }
            if (bossMenuOpen)
            {
                if (brute.Contains(new Vector2(currentMouseState.X, currentMouseState.Y)))
                {
                    bruteHover = true;
                    mastermindHover = false;
                    operativeHover = false;
                    brute.walk();
                    mastermind.idle();
                    operative.idle();
                    current = brute;
                }
                else if (mastermind.Contains(new Vector2(currentMouseState.X, currentMouseState.Y)))
                {
                    bruteHover = false;
                    mastermindHover = true;
                    operativeHover = false;
                    brute.idle();
                    mastermind.walk();
                    operative.idle();
                    current = mastermind;
                }
                else if (operative.Contains(new Vector2(currentMouseState.X, currentMouseState.Y)))
                {
                    bruteHover = false;
                    mastermindHover = false;
                    operativeHover = true;
                    brute.idle();
                    mastermind.idle();
                    operative.walk();
                    current = operative;
                }
                else
                {
                    current = null;
                    bruteHover = false;
                    mastermindHover = false;
                    operativeHover = false;
                    brute.idle();
                    mastermind.idle();
                    operative.idle();
                }
                brute.Update(gameTime);
                mastermind.Update(gameTime);
                operative.Update(gameTime);
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released && canFinish)
                {

                    Vector2 mousePos = new Vector2(currentMouseState.X, currentMouseState.Y);
                    if (operative.Contains(mousePos) || brute.Contains(mousePos) || mastermind.Contains(mousePos))
                    {
                        BattleManager.boss = new Character(current.bossType, 1, new Vector2(BattleManager.bossLoc.X, BattleManager.bossLoc.Y));
                        /*if (current.bossType == Character.Type.Brute)
                        {

                            BattleManager.boss = new Character(100, 50, 10, 50, 10, 25, 1, 1, 100, 0);
                            BattleManager.boss.charType = current.bossType;
                        }
                        else if (current.bossType == Character.Type.Mastermind)
                        {
                            BattleManager.boss = new Character(100, 10, 50, 10, 50, 35, 1, 1, 100, 0);
                            BattleManager.boss.charType = current.bossType;
                        }
                        else if (current.bossType == Character.Type.Operative)
                        {
                            BattleManager.boss = new Character(100, 25, 25, 30, 30, 50, 10, 1, 100, 0);
                            Resources.exp = 20001;
                            BattleManager.boss.charType = current.bossType;
                            Resources.gold = 50000;

                        }
                    */
                        //BattleManager.boss.Init();

                        BattleManager.heroes = PartyManager.CreateParty();
                        /*
                        foreach (Character hero in BattleManager.heroes)
                        {
                            hero.Init();
                            //required because the UpgradeMenu needs some info

                        }*/
                        //BattleManager.Init();

                        //}
                        //BattleManager.Init();

                        UpgradeMenu.Init(current);
                        LairManager.Init();
                        //return LeaveMeAlone.GameState.Upgrade;

                        return LeaveMeAlone.GameState.Lair;
                    }
                }
                canFinish = true;
            }
            return LeaveMeAlone.GameState.Main;
        }
        public static void Draw(SpriteBatch Spritebatch)
        {
            if (mainMenuOpen)
            {
                Spritebatch.Draw(menuBackground, new Rectangle(0, 0, LeaveMeAlone.WindowX, LeaveMeAlone.WindowY), Color.Black);
                Spritebatch.Draw(titleCard, new Rectangle(LeaveMeAlone.WindowX / 2 - 200, 0, 400, 200), Color.White);
                if (isNewGame)
                {
                    newGame.Draw(Spritebatch);
                }
                else
                {
                    continueGame.Draw(Spritebatch);
                }
                loadGame.Draw(Spritebatch);
                quit.Draw(Spritebatch);
            }
            if (bossMenuOpen)
            {
                Spritebatch.Draw(menuBackground, new Rectangle(0, 0, LeaveMeAlone.WindowX, LeaveMeAlone.WindowY), Color.Black);
                Spritebatch.Draw(titleCard, new Rectangle(LeaveMeAlone.WindowX/2 - 200, 0, 400, 200), Color.White);
                brute.Draw(Spritebatch, Color.White);
                mastermind.Draw(Spritebatch, Color.White);
                operative.Draw(Spritebatch, Color.White);
                if (bruteHover)
                {
                    Spritebatch.Draw(bruteTitle, new Rectangle(LeaveMeAlone.WindowX / 2 - 370, LeaveMeAlone.WindowY - 250, 150, 50), Color.White);
                }
                if (mastermindHover)
                {
                    Spritebatch.Draw(mastermindTitle, new Rectangle(LeaveMeAlone.WindowX / 2 - 100, LeaveMeAlone.WindowY - 250, 225, 50), Color.White);
                }
                if (operativeHover)
                {
                    Spritebatch.Draw(operativeTitle, new Rectangle(LeaveMeAlone.WindowX / 2 + 220, LeaveMeAlone.WindowY - 250, 200, 50), Color.White);
                }
            }
        }
    }
}
