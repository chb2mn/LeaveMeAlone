#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace LeaveMeAlone
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D boss;
        Rectangle bossLoc;
        Texture2D[] buttons = new Texture2D[4];
        Texture2D[] main_buttons = new Texture2D[4];
        Texture2D[] magic_buttons = new Texture2D[4];
        Texture2D[] special_buttons = new Texture2D[4];
        Texture2D back;
        Rectangle backLoc;
        Rectangle[] buttonLoc = new Rectangle[4]; 
        Texture2D[] heroes = new Texture2D[3];
        Rectangle[] heroLoc = new Rectangle[3];

        Text damage_text;
        int textx;
        int texty;

        Text victory_text;
        bool victory;

        public Game1()
            : base() 
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        int battle_menu_state; //0 == main, 1 == magic, 2 == special, 3 == bribe 

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            this.Window.Title = "Leave Me Alone";
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //font = Content.Load<SpriteFont>("coure.fon");
            // TODO: use this.Content to load your game content here
            
            boss = Content.Load<Texture2D>("Machamp_Boss");
            bossLoc = new Rectangle(650,120,100,100);
            int button_basex = 100;
            int button_basey = 350;

            buttonLoc[0] = new Rectangle(button_basex, button_basey, 250, 50);
            buttonLoc[1] = new Rectangle(button_basex + 300, button_basey, 250, 50);
            buttonLoc[2] = new Rectangle(button_basex, button_basey +60, 250, 50);
            buttonLoc[3] = new Rectangle(button_basex + 300, button_basey + 60, 250, 50);

            heroes[0] = Content.Load<Texture2D>("DummyHero");
            heroes[1] = Content.Load<Texture2D>("DummyHero");
            heroes[2] = Content.Load<Texture2D>("DummyHero");

            int hero_basex = 50;
            int hero_basey = 150;
            heroLoc[0] = new Rectangle(hero_basex, hero_basey, 50, 50);
            heroLoc[1] = new Rectangle(hero_basex, hero_basey - 75, 50, 50);
            heroLoc[2] = new Rectangle(hero_basex, hero_basey + 75, 50, 50);

            buttons[0] = Content.Load<Texture2D>("Attack");
            buttons[1] = Content.Load<Texture2D>("Magic");
            buttons[2] = Content.Load<Texture2D>("Special");
            buttons[3] = Content.Load<Texture2D>("Bribe");
            main_buttons[0] = Content.Load<Texture2D>("Attack");
            main_buttons[1] = Content.Load<Texture2D>("Magic");
            main_buttons[2] = Content.Load<Texture2D>("Special");
            main_buttons[3] = Content.Load<Texture2D>("Bribe");
            magic_buttons[0] = Content.Load<Texture2D>("Fire");
            magic_buttons[1] = Content.Load<Texture2D>("Ice");
            magic_buttons[2] = Content.Load<Texture2D>("Gust");
            magic_buttons[3] = Content.Load<Texture2D>("Earth");
            special_buttons[0] = Content.Load<Texture2D>("Steal");
            special_buttons[1] = Content.Load<Texture2D>("Charge");
            special_buttons[2] = Content.Load<Texture2D>("QuickStrike");
            special_buttons[3] = Content.Load<Texture2D>("Swipe");

            back = Content.Load<Texture2D>("back");
            backLoc = new Rectangle(675, 410, 113, 51);

            victory = false;
            victory_text = new Text("Victory!!");
            victory_text.loadContent(Content);

            damage_text = new Text("0");
            textx = 120;
            texty = 175;
            damage_text.loadContent(Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected void NewMenu (int menu) {
            if (victory)
            {
                return;
            }
            switch (menu) {
                case 0:
                    for (int i = 0; i < 4; i++)
                    {
                        battle_menu_state = 0;
                        buttons[i] = main_buttons[i];
                    }
                    break;
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        battle_menu_state = 1;
                        buttons[i] = magic_buttons[i];
                    }
                    break;
                case 2:
                    for (int i = 0; i < 4; i++)
                    {
                        battle_menu_state = 2;
                        buttons[i] = special_buttons[i];
                    }
                    break;
                case 3:
                    //bribe
                    {
                        battle_menu_state = 3;
                    }
                    break;
            }
        }
        /*
         * Things to change are here
         */
        int hero1hp = 50;
        int hero2hp = 50;
        int hero3hp = 50;

        int state_delay_counter = 0;
        int state = 0; //non-attack
        int damage;

        protected void Attack()
        {
            if (victory)
            {
                return;
            }
            state = 1; //attack
            Random myrandom = new Random();
            damage = (10 + myrandom.Next(0, 5));
            textx = 120;
            texty = 175;
            if (hero1hp > 0)
            {

                hero1hp -= damage;
                if (hero1hp <= 0)
                {
                    heroes[0] = null;
                }
            }
            else if (hero2hp > 0) {
                texty -= 75;
                hero2hp -= damage;
                if (hero2hp <= 0)
                {
                    heroes[1] = null;
                }
            }
            else if (hero3hp > 0)
            {
                texty += 75;
                hero3hp -= damage;
                if (hero3hp <= 0)
                {
                    heroes[2] = null;
                    victory = true;
                }
            }
            damage_text.changeMessage(damage.ToString());

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (state == 1)
            {
                state_delay_counter++;
                if (state_delay_counter == 15)
                {
                    state = 0; //Non-attack
                    state_delay_counter = 0;
                }
            }

            // TODO: Add your update logic here
            int selectLocX = Mouse.GetState().X;
            int selectLocY = Mouse.GetState().Y;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && state == 0 && !victory) {
                //Search through the buttons to see if any were hit
                for (int i = 0; i < 4; i++) {
                     if (buttonLoc[i].Contains(selectLocX,selectLocY)) {
                         state = 2; //menu change
                        switch (i) {
                            case 0:
                                Attack();
                                break;
                            case 1:
                                if (battle_menu_state == 0)
                                {
                                    NewMenu(1); //Magic
                                }
                                else
                                {
                                    Attack();
                                }
                                break;
                            case 2:
                                if (battle_menu_state == 0)
                                {
                                    NewMenu(2); //Special
                                }
                                else
                                {
                                    Attack();
                                }
                                break;
                            case 3:
                                if (battle_menu_state != 0)
                                {
                                    Attack();//Bribe -- Not Implemented
                                }
                                break;
                        }
                        break;
                    }
                }
                if (backLoc.Contains(selectLocX, selectLocY))
                {
                    NewMenu(0);
                }
                

            }
            if (Mouse.GetState().LeftButton == ButtonState.Released && state == 2)
            {
                state = 0;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(boss, bossLoc, Color.White);
            spriteBatch.Draw(buttons[0], buttonLoc[0], Color.White);
            spriteBatch.Draw(buttons[1], buttonLoc[1], Color.White);
            spriteBatch.Draw(buttons[2], buttonLoc[2], Color.White);
            spriteBatch.Draw(buttons[3], buttonLoc[3], Color.White);
            if (battle_menu_state > 0) {

               spriteBatch.Draw(back, backLoc, Color.White);
            }
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    spriteBatch.Draw(heroes[i], heroLoc[i], Color.White);

                }
                catch
                {

                }
            }


            if (state == 1)
            {
                damage_text.draw(spriteBatch, textx, texty);
                //spriteBatch.DrawString(font, String.Format("dmg: {0}", damage), new Vector2(200, 50), Color.White);
            }

            if (victory)
            {
                victory_text.draw(spriteBatch, 200, 200);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
