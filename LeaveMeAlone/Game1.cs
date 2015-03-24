#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;
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

        Character boss_char = new Character();
        Text damage_text;
        int textx;
        int texty;

        Text victory_text;
        bool victory;

        bool menu_change_in_progress = false;
        int state_delay_counter = 0;
        int state = 0; 
        // 0 = non-attack
        // 1 = targetting
        // 2 = attack
        // 3 = animate

        int targeted_enemy = -1;

        public Game1()
            : base() 
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        int battle_menu_state; 
        /* 0 == main
         * 1 == magic,
         * 2 == special,
         * 3 == bribe,
         * 4 == remove all (targeting)
         */

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            this.Window.Title = "Leave Me Alone";

            //Skill s = new Skill("test", 1, 100, 1, 0, 0, "My first skill", new Skill.Run(test));
            //s.runnable(boss_char);
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
                    battle_menu_state = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        
                        buttons[i] = main_buttons[i];
                    }
                    break;
                case 1:
                    battle_menu_state = 1;
                    for (int i = 0; i < 4; i++)
                    {
                        
                        buttons[i] = magic_buttons[i];
                    }
                    break;
                case 2:
                    battle_menu_state = 2;
                    for (int i = 0; i < 4; i++)
                    {
                        buttons[i] = special_buttons[i];
                    }
                    break;
                case 3:
                    //Bribe
                    {
                        battle_menu_state = 3;
                    }
                    break;
                case 4:
                    //Remove buttons
                    {
                        battle_menu_state = 4;
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
        int damage;
        /*
         * end things to change 
         */

        /* 
         * this is called for any attack that requires a target 
         */
        protected void Target()
        {
            int selectLocX = Mouse.GetState().X;
            int selectLocY = Mouse.GetState().Y;

            bool any_target = false;

            for (int i = 0; i < 3; i++)
            {
                if (heroLoc[i].Contains(selectLocX, selectLocY))
                {
                    targeted_enemy = i;
                    any_target = true;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        Attack();
                    }
                }
            }
            if (!any_target)
            {
                targeted_enemy = -1;
            }
        }

        /*
         * this is called to do damage to an enemy
         */
        protected void Attack()
        {
            if (victory)
            {
                return;
            }
            //Check if state is in attack mode
            Random myrandom = new Random();
            damage = (10 + myrandom.Next(0, 5));
            textx = 120;
            texty = 175;
            switch (targeted_enemy)
            {
                case (0):
                    {

                        hero1hp -= damage;
                        if (hero1hp <= 0)
                        {
                            heroes[0] = null;
                        }
                    }
                    break;
                case (1):
                    {
                        texty -= 75;
                        hero2hp -= damage;
                        if (hero2hp <= 0)
                        {
                            heroes[1] = null;
                        }
                    }
                    break;
                case (2):
                    {
                        texty += 75;
                        hero3hp -= damage;
                        if (hero3hp <= 0)
                        {
                            heroes[2] = null;
                        }
                    }
                    break;
            }
            if (hero1hp < 0 && hero2hp < 0 && hero3hp < 0)
            {
                victory = true;
            }
            damage_text.changeMessage(damage.ToString());
            state = 2;
        }
        //Unifnished, needs to actually animate
        //Animate will reset the state to 0
        protected void Animate()
        {
            state_delay_counter++;
            if (state_delay_counter == 15)
            {
                state = 0; //Non-attack
                battle_menu_state = 0;
                state_delay_counter = 0;
                targeted_enemy = -1;
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //If we're attacking, call animate (This will eventually reset state)
            if (state == 2)
            {
                Animate();
            }
            // Check if we need to return to the main menu
            int selectLocX = Mouse.GetState().X;
            int selectLocY = Mouse.GetState().Y;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && backLoc.Contains(selectLocX, selectLocY))
            {
                state = 0;
                NewMenu(0);
            }
            //if we need to target, go do that
            else if (state == 1)
            {
                Target();
                return;
            }
            //Check if the menu needs changing
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && state == 0 && !menu_change_in_progress  && !victory) {
                //Search through the buttons to see if any were hit
                for (int i = 0; i < 4; i++) {
                     if (buttonLoc[i].Contains(selectLocX,selectLocY)) {
                        menu_change_in_progress = true; //menu change
                        switch (i) {
                            case 0:
                                state = 1; //set state to targeting
                                battle_menu_state = 4;
                                break;
                            case 1:
                                if (battle_menu_state == 0)
                                {
                                    NewMenu(1); //Magic
                                }
                                else
                                {
                                    state = 1; //set state to targeting
                                    battle_menu_state = 4;

                                }
                                break;
                            case 2:
                                if (battle_menu_state == 0)
                                {
                                    NewMenu(2); //Special
                                }
                                else
                                {
                                    state = 1; //set state to targeting
                                    battle_menu_state = 4;
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
                
                

            }
            if (Mouse.GetState().LeftButton == ButtonState.Released && menu_change_in_progress)
            {
                menu_change_in_progress =false;
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
            if (battle_menu_state < 3)
            {
                spriteBatch.Draw(buttons[0], buttonLoc[0], Color.White);
                spriteBatch.Draw(buttons[1], buttonLoc[1], Color.White);
                spriteBatch.Draw(buttons[2], buttonLoc[2], Color.White);
                spriteBatch.Draw(buttons[3], buttonLoc[3], Color.White);
            }
            if (battle_menu_state > 0) {

               spriteBatch.Draw(back, backLoc, Color.White);
            }
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    if (i == targeted_enemy)
                    {
                        spriteBatch.Draw(heroes[i], heroLoc[i], Color.Violet);
                    }
                    else
                    {
                        spriteBatch.Draw(heroes[i], heroLoc[i], Color.White);
                    }
                }
                catch
                {

                }
            }


            if (state == 2)
            {
                damage_text.draw(spriteBatch, textx, texty);
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
