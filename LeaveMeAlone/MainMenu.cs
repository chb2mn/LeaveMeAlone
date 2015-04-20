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
        private static Button newGame;
        private static Button loadGame;
        private static Button quit;
        private static Texture2D bruteTitle;
        private static Texture2D mastermindTitle;
        private static Texture2D operativeTitle;
        private static Text bruteText;
        private static Text mastermindText;
        private static Text operativeText;
        private static MouseState currentMouseState, lastMouseState;
        public enum MenuState { main, opening, boss };
        public static MenuState menu_state;
        private static int opening_timer = 0;
        public static int line_number = 0;
        private static Texture2D opening_scene;
        public static List<Text> opening_monologue = new List<Text>();
        public static Button next_intro;
        public static Button skip_intro;
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
            menu_state = MenuState.main;
            isNewGame = defeat;
            brute = new MenuBoss(Character.Type.Brute, new Vector2(LeaveMeAlone.WindowX / 2 - 400, LeaveMeAlone.WindowY - 400));
            mastermind = new MenuBoss(Character.Type.Mastermind, new Vector2(LeaveMeAlone.WindowX / 2 - 100, LeaveMeAlone.WindowY - 400));
            operative = new MenuBoss(Character.Type.Operative, new Vector2(LeaveMeAlone.WindowX / 2 + 200, LeaveMeAlone.WindowY - 400));
            LeaveMeAlone.Main_Song_Instance.Play();

            brute.idle();
            mastermind.idle();
            operative.idle();
        }
        public static void loadContent(ContentManager content)
        {
            menuBackground = content.Load<Texture2D>("DummyHero");
            titleCard = content.Load<Texture2D>("TitleCard");
            newGame = new Button(content.Load<Texture2D>("NewGame"), LeaveMeAlone.WindowX / 2 - 100, 275, 200, 75);
            //newGame.selected = true;
            loadGame = new Button(content.Load<Texture2D>("LoadGame"), LeaveMeAlone.WindowX / 2 - 150, 200, 300, 75);
            next_intro = new Button(content.Load<Texture2D>("Next"), LeaveMeAlone.WindowX - 113, LeaveMeAlone.WindowY - 96, 113, 32);
            skip_intro = new Button(content.Load<Texture2D>("Buttons/Skip"), LeaveMeAlone.WindowX - 113, LeaveMeAlone.WindowY - 32, 113, 32);
            opening_scene = content.Load<Texture2D>("OpeningLandscape");
            //loadGame.selected = true;
            quit = new Button(content.Load<Texture2D>("Quit"), LeaveMeAlone.WindowX / 2 - 100, 350, 200, 75);

            bruteTitle = content.Load<Texture2D>("bruteTitle");
            bruteText = new Text("High Physical Stats\nLow Special Stats", f: Text.fonts["RetroComputer-12"], c: Color.White);
            mastermindTitle = content.Load<Texture2D>("mastermindTitle");
            mastermindText = new Text("High Special Stats\nLow Physical Stats", f: Text.fonts["RetroComputer-12"], c: Color.White);
            operativeTitle = content.Load<Texture2D>("operativeTitle");
            operativeText = new Text("Balanced Stats\nHigh Energy and Good skills", f: Text.fonts["RetroComputer-12"], c: Color.White);


            SpriteFont story_font = Text.fonts["RetroComputer-18"];
            opening_monologue.Add(new Text("When I first began my reign as a boss monster\n\n      Everything was so simple.", new Vector2(200,200), story_font));
            opening_monologue.Add(new Text("My knees limber...\n\n\n\n\n\n\n                                  The heroes...\n\n\n\n                                                           They were so scared", new Vector2(100,100), story_font));
            opening_monologue.Add(new Text("But like all things, I've aged...", new Vector2(450, 600), story_font));
            opening_monologue.Add(new Text("It's time to retire, but the heroes...\n\n\n           Well... Grudges never die.", new Vector2(250, 300), story_font));
            opening_monologue.Add(new Text("I need something to keep those heroes away\n\n         A statement piece...", new Vector2(350, 100), story_font));
            opening_monologue.Add(new Text("Something to get these heroes to", new Vector2(250, 250), story_font));

        }
        public static LeaveMeAlone.GameState Update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (menu_state == MenuState.main)
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (newGame.Intersects(currentMouseState.X, currentMouseState.Y))
                    {
                        menu_state = MenuState.opening;
                    }
                    else if (loadGame.Intersects(currentMouseState.X, currentMouseState.Y) && !isNewGame)
                    {
                        LeaveMeAlone.Main_Song_Instance.Stop();
                        LeaveMeAlone.Menu_Song_Instance.Play();
                        return LeaveMeAlone.GameState.Lair;
                    }
                    else if (quit.Intersects(currentMouseState.X, currentMouseState.Y))
                    {
                        return LeaveMeAlone.GameState.Quit;
                    }
                }
            }
            else if (menu_state == MenuState.opening)
            {
                if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (next_intro.Intersects(currentMouseState.X, currentMouseState.Y)) 
                    {
                        line_number++;
                        opening_timer = 0;
                    }
                    if (skip_intro.Intersects(currentMouseState.X, currentMouseState.Y))
                    {
                        line_number = 1 + opening_monologue.Count();
                    }
                }
                if (line_number == 1 + opening_monologue.Count())
                {
                    menu_state = MenuState.boss;
                    
                }
                else if (opening_timer > 300)
                {
                    if(line_number != opening_monologue.Count())
                    { 
                    opening_timer = 0;
                    }
                    line_number++;
                }
                else
                {
                    opening_timer++;
                }
            }
            else
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
                        

                        BattleManager.heroes = PartyManager.CreateParty();
                        
                        LeaveMeAlone.Main_Song_Instance.Stop();

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
            if (menu_state == MenuState.main)
            {
                Spritebatch.Draw(menuBackground, new Rectangle(0, 0, LeaveMeAlone.WindowX, LeaveMeAlone.WindowY), Color.Black);
                Spritebatch.Draw(titleCard, new Rectangle(LeaveMeAlone.WindowX / 2 - 200, 0, 400, 200), Color.White);
                newGame.Draw(Spritebatch);
                if (!isNewGame)
                {
                    loadGame.Draw(Spritebatch);
                }
                quit.Draw(Spritebatch);
            }
            else if (menu_state == MenuState.opening)
            {
                Spritebatch.Draw(opening_scene, new Rectangle(-((line_number*300)+opening_timer)/9, 0, (int)(LeaveMeAlone.WindowX*1.5), LeaveMeAlone.WindowY), Color.White);
                
                if (line_number < opening_monologue.Count())
                {
                    opening_monologue[line_number].Draw(Spritebatch);
                }
                else
                {
                    if(opening_timer == 359)
                    {
                        Console.WriteLine("it's time");
                    }
                    Spritebatch.Draw(menuBackground, new Rectangle(0, 0, LeaveMeAlone.WindowX, LeaveMeAlone.WindowY), new Color(Color.Black, (int)(Math.Sqrt((double)opening_timer/300)*255)));
                    Spritebatch.Draw(titleCard, new Rectangle(LeaveMeAlone.WindowX / 2 - 200, 0, 400, 200), Color.White);
                }
                skip_intro.Draw(Spritebatch);
                next_intro.Draw(Spritebatch);
            }
            else
            {
                Spritebatch.Draw(menuBackground, new Rectangle(0, 0, LeaveMeAlone.WindowX, LeaveMeAlone.WindowY), Color.Black);
                Spritebatch.Draw(titleCard, new Rectangle(LeaveMeAlone.WindowX/2 - 200, 0, 400, 200), Color.White);
                brute.Draw(Spritebatch, Color.White);
                mastermind.Draw(Spritebatch, Color.White);
                operative.Draw(Spritebatch, Color.White);
                if (bruteHover)
                {
                    Spritebatch.Draw(bruteTitle, new Rectangle(LeaveMeAlone.WindowX / 2 - 370, LeaveMeAlone.WindowY - 250, 150, 50), Color.White);
                    bruteText.Draw(Spritebatch, pos: new Vector2(LeaveMeAlone.WindowX / 2 - 370, LeaveMeAlone.WindowY - 200));
                }
                if (mastermindHover)
                {
                    Spritebatch.Draw(mastermindTitle, new Rectangle(LeaveMeAlone.WindowX / 2 - 100, LeaveMeAlone.WindowY - 250, 225, 50), Color.White);
                                        mastermindText.Draw(Spritebatch, pos: new Vector2(LeaveMeAlone.WindowX / 2 - 100, LeaveMeAlone.WindowY - 200));

                }
                if (operativeHover)
                {
                    Spritebatch.Draw(operativeTitle, new Rectangle(LeaveMeAlone.WindowX / 2 + 220, LeaveMeAlone.WindowY - 250, 200, 50), Color.White);
                    operativeText.Draw(Spritebatch, pos: new Vector2(LeaveMeAlone.WindowX / 2 +220, LeaveMeAlone.WindowY - 200));

                }
            }
        }
    }
}

