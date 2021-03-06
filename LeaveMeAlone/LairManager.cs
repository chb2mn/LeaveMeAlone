﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;

namespace LeaveMeAlone
{
    public class LairManager
    {
        public static bool EndOfGame;
        private static bool one_last_party = true;
        public static int TowerLevel;
        public static int MaxLevel;

        public static Text TutorialText;
        public enum TutorialState { Skill, Build1, Build2, Build3, SendWave, None};
        public static TutorialState tutorial_state;
        public static Texture2D highlighter;
        public static Rectangle highlighter_rect;

        public static Text InfoText;
        public static int texttimer;
        public static Vector2 towerPosition;
        public static List<Room> LairRooms;
        private static Texture2D lairBkgd, lairLobby, bossRoom, unconstructed_room;
        private static Button skillsBtn, constructionBtn, mainmenuBtn, grindwaveBtn;
        public static Button nextwaveBtn;
        private static MouseState currentMouseState, lastMouseState;
        public static Room UnconstructedRoom;
        public static List<UpgradeMenu.ButtonRoom> boughtRooms = new List<UpgradeMenu.ButtonRoom>();
        public static UpgradeMenu.ButtonRoom selectedRoomSwapButton { get; set; }
        public static  int sideOffset = 75;
        public static  int sideScaling = 85;
        public static  int topOffset = 150;
        public static int topScaling = 80;
        public static bool selected_flag = false;
        public static bool BossBattle = false;
        //Took out the parameters on these next two functions as 
        //they are likely going to want to hit every room in the lair?
        public static void loadContent(ContentManager content)
        {
            selectedRoomSwapButton = new UpgradeMenu.ButtonRoom();
            EndOfGame = false;
            TowerLevel = 0;
            MaxLevel = 3;
            TutorialText = new Text("", new Vector2(200, 25), Text.fonts["6809Chargen-24"], Color.DarkGreen);
            highlighter = content.Load<Texture2D>("Highlight");
            highlighter_rect = new Rectangle();
            InfoText = new Text("", new Vector2(LeaveMeAlone.WindowX - 350, LeaveMeAlone.WindowY - 200), Text.fonts["6809Chargen-24"], Color.Black);
            towerPosition = new Vector2(0, 0);
            LairRooms = new List<Room>();
            skillsBtn = new Button(content.Load<Texture2D>("skillsBtn"), LeaveMeAlone.WindowX - 200, 100, 200, 75);
            constructionBtn = new Button(content.Load<Texture2D>("constructionBtn"), LeaveMeAlone.WindowX - 200, 180, 200, 75);
            nextwaveBtn = new Button(content.Load<Texture2D>("nextwaveBtn"), LeaveMeAlone.BackgroundRect.X, LeaveMeAlone.BackgroundRect.Height - 75, 200, 75);
            grindwaveBtn = new Button(content.Load<Texture2D>("Buttons/GrindBtn"), LeaveMeAlone.BackgroundRect.X, LeaveMeAlone.BackgroundRect.Height - 75, 200, 75);
            mainmenuBtn = new Button(content.Load<Texture2D>("Buttons/MainMenu"), LeaveMeAlone.WindowX - 300, 0, 300, 80);
            lairBkgd = content.Load<Texture2D>("lairBkgd");
            lairLobby = content.Load<Texture2D>("lairLobby");
            bossRoom = content.Load<Texture2D>("bossRoom");
            unconstructed_room = content.Load<Texture2D>("unconstructed_room");
            UnconstructedRoom = new Room("Unconstructed Room", 0, 0, 0, "A new blank space to construct a room.", null, unconstructed_room); 

            //Testing rooms
            //UpgradeMenu.boughtRooms.Add(SkillTree.papparazzi);


            /*
             * This can be used to debug rooms
             * 
             *             int index = 0;

            foreach (Room r in UpgradeMenu.boughtRooms)
            {
                
                index++;
                //Console.WriteLine(r.name);
            }
             */

        }

        public static void Init()
        {
            //play the music
            LeaveMeAlone.Battle_Song_Instance.Stop();
            LeaveMeAlone.Menu_Song_Instance.Play();
            tutorial_state = TutorialState.Skill;
            if (EndOfGame)
            {
                nextwaveBtn.rectangle.X = LeaveMeAlone.BackgroundRect.X;
                nextwaveBtn.rectangle.Y = LeaveMeAlone.BackgroundRect.Height - 200;
            }
        }

        public static void HandleTutorial()
        {
            Rectangle target_rect = new Rectangle();
            switch (tutorial_state)
            {
                case TutorialState.Build1:
                    TutorialText.changeMessage("Now let's build that room\nwe just bought!\nFirst build a level to our tower");
                    target_rect = constructionBtn.rectangle;
                    break;
                case TutorialState.Build2:
                    TutorialText.changeMessage("Now select the room we want to add to our tower");
                    try
                    {
                        target_rect = boughtRooms[0].b.rectangle;
                    }
                    catch
                    {
                        TutorialText.changeMessage("You haven't bought a room yet!");
                        target_rect = skillsBtn.rectangle;
                    }
                    break;
                case TutorialState.Build3:
                    TutorialText.changeMessage("Finally, select the level of our tower\nfor whichwe want to add that tower");
                    target_rect = new Rectangle((int)(towerPosition.X + LeaveMeAlone.WindowX / 3), (int)(towerPosition.Y + LeaveMeAlone.WindowY - 100 - 100 * (1)), 400, 100);
                    break;
                case TutorialState.SendWave:
                    TutorialText.changeMessage("Now each time we press the NEXT WAVE\nand the heroes will come to attack!");
                    target_rect = nextwaveBtn.rectangle;
                    break;
                case TutorialState.None:
                    TutorialText.changeMessage("");
                    //I know this is bad coding practice
                    target_rect.X = 2000;
                    target_rect.Y = 2000;
                    break;
            }
            highlighter_rect.X = target_rect.X - 15;
            highlighter_rect.Y = target_rect.Y - 15;
            highlighter_rect.Width = target_rect.Width + 30;
            highlighter_rect.Height = target_rect.Height + 30;
        }
        public static void LairAttack(Room room, List<Character> party)
        {
            if (party != null && room.activate != null)
            {
                room.activate(party);
            }
        }
        public static void AdvanceLevel()
        {

        }
        public static LeaveMeAlone.GameState Update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (BattleManager.boss.level < 2)
            {
                HandleTutorial();
            }

            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < TowerLevel; i++)
                {
                    var rectpos = new Rectangle((int)(towerPosition.X + LeaveMeAlone.WindowX / 3), (int)(towerPosition.Y + LeaveMeAlone.WindowY - 100 - 100 * (i + 1)), 400, 100);
                    if (selectedRoomSwapButton.r != null && LairRooms.Contains(selectedRoomSwapButton.r) == false && rectpos.Contains(currentMouseState.X, currentMouseState.Y))
                    {
                        foreach (UpgradeMenu.ButtonRoom oldroom in boughtRooms)
                        {
                            if (oldroom.r == LairRooms[i])
                            {
                                oldroom.used = false;
                            }
                        }
                        LairRooms[i] = selectedRoomSwapButton.r;
                        selectedRoomSwapButton.used = true;
                        tutorial_state = TutorialState.SendWave;
                        
                    }
                }
                if ((EndOfGame && grindwaveBtn.Intersects(currentMouseState.X, currentMouseState.Y)))
                {
                    BattleManager.Init();
                    List<Character> newParty = PartyManager.CreateParty();
                    BattleManager.heroes = newParty;
                    return LeaveMeAlone.GameState.Battle;
                }
                //next wave
                if (nextwaveBtn.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    BossBattle = false;
                    if (EndOfGame)
                    {
                        
                        InfoText.changeMessage("PEASANTS!\nOUT OF THE WAY!\nHE'S OURS");
                        //create the last party
                        if (one_last_party)
                        {
                            for (int i = 0; i <= PartyManager.partyQueue.Count(); i++)
                            {
                                PartyManager.partyQueue.Add(null);
                                PartyManager.popParty();
                            }
                            List<Character> FinalParty = new List<Character>();

                            FinalParty.Add(new Character(Character.Type.Knight, 14, new Vector2(sideOffset + sideScaling, topOffset)));
                            FinalParty.Add(new Character(Character.Type.Ranger, 14, new Vector2(sideOffset, topOffset + topScaling * 1)));
                            FinalParty.Add(new Character(Character.Type.Mage, 14, new Vector2(sideOffset + sideScaling, topOffset + topScaling * 2)));
                            FinalParty.Add(new Character(Character.Type.Mage, 14, new Vector2(sideOffset, topOffset + topScaling * 3)));
                            PartyManager.partyQueue.Add(FinalParty);
                            PartyManager.popParty();
                            one_last_party = false;

                        }
                        //push nothing else
                        else
                        {

                            if (PartyManager.popParty())
                            {
                                BattleManager.Init();
                                //Re-Add the party in case the player loses
                                List<Character> FinalParty = new List<Character>();
                                FinalParty.Add(new Character(Character.Type.Knight, 14, new Vector2(sideOffset + sideScaling, topOffset)));
                                FinalParty.Add(new Character(Character.Type.Ranger, 14, new Vector2(sideOffset, topOffset + topScaling * 1)));
                                FinalParty.Add(new Character(Character.Type.Mage, 14, new Vector2(sideOffset + sideScaling, topOffset + topScaling * 2)));
                                FinalParty.Add(new Character(Character.Type.Mage, 14, new Vector2(sideOffset, topOffset + topScaling * 3)));
                                PartyManager.partyQueue.Add(FinalParty);
                                BossBattle = true;
                                return LeaveMeAlone.GameState.Battle;
                            }
                            else
                            {
                                PartyManager.partyQueue.Add(null);
                                return LeaveMeAlone.GameState.Lair;
                            }

                        }

                    }
                        //Normally push a party
                    else
                    {
                        for (int i = 0; i < TowerLevel; i++)
                        {
                            LairAttack(LairRooms[i], PartyManager.partyQueue[i]);
                        }
                        Random random = new Random();
                        int makeParty = 0;//random.Next(100) % 4;
                        if (makeParty != 1)
                        {
                            List<Character> newParty = PartyManager.CreateParty();
                            PartyManager.partyQueue.Add(newParty);
                        }
                        else
                        {
                            PartyManager.partyQueue.Add(null);
                        }
                        if (PartyManager.popParty())
                        {
                            BattleManager.Init();
                            InfoText.changeMessage("");
                            return LeaveMeAlone.GameState.Battle;
                        }
                        else
                        {
                            //Should have some sort of interface on screen
                            InfoText.changeMessage("Here they come\n      I can feel them.");
                            return LeaveMeAlone.GameState.Lair;
                        }
                    }
                }
                if (skillsBtn.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    return LeaveMeAlone.GameState.Upgrade;
                }
                if (constructionBtn.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    if (TowerLevel < MaxLevel)
                    {
                        TowerLevel++;
                        LairRooms.Add(UnconstructedRoom);
                        PartyManager.partyQueue.Add(null);
                        for (int j = PartyManager.partyQueue.Count() - 1; j > 0; j--)
                        {
                            PartyManager.partyQueue[j] = PartyManager.partyQueue[j - 1];
                        }
                        PartyManager.partyQueue[0] = null;
                    }
                    else
                    {
                        InfoText.changeMessage("Max Lair Height");
                        texttimer = 600;
                    }
                    tutorial_state = TutorialState.Build2;
                    return LeaveMeAlone.GameState.Lair;
                }
                if (mainmenuBtn.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    MainMenu.init(false);
                    LeaveMeAlone.Menu_Song_Instance.Stop();
                    //UpgradeMenu.boughtRooms.Clear();
                    return LeaveMeAlone.GameState.Main;
                }
                //checks if a room button was selected
                selected_flag = false;
                foreach (UpgradeMenu.ButtonRoom r in boughtRooms)
                {
                    if (r.b.Intersects(currentMouseState.X, currentMouseState.Y) && !r.used)
                    {
                        Console.WriteLine(r.b.text);
                        //already have a selected thing;
                        if (selectedRoomSwapButton.b != null)
                        {
                            selectedRoomSwapButton.b.selected = false;
                        }
                        selectedRoomSwapButton = r;
                        r.b.selected = true;
                        selected_flag = true;
                        tutorial_state = TutorialState.Build3;
                    }
                }
                //otherwise we unselect any previously selected button
                if (selected_flag == false && selectedRoomSwapButton.b != null)
                {
                    selectedRoomSwapButton.b.selected = false;
                    selectedRoomSwapButton = new UpgradeMenu.ButtonRoom();
                }
            }

            return LeaveMeAlone.GameState.Lair;
        }
        public static void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(lairBkgd, new Rectangle(-500, -200, 2308, 1200), Color.White);
            Spritebatch.Draw(lairLobby, new Rectangle((int)(towerPosition.X + LeaveMeAlone.WindowX / 3), (int)(towerPosition.Y + LeaveMeAlone.WindowY - 100), 400, 100), Color.White);
            Spritebatch.Draw(bossRoom, new Rectangle((int)(towerPosition.X + LeaveMeAlone.WindowX / 3), (int)(towerPosition.Y + LeaveMeAlone.WindowY - 100 - 100 * (TowerLevel + 1)), 400, 100), Color.White);

            
            
            for (int i = 0; i < TowerLevel; i++)
            {
                Spritebatch.Draw(LairRooms[i].img, new Rectangle((int)(towerPosition.X + LeaveMeAlone.WindowX / 3), (int)(towerPosition.Y + LeaveMeAlone.WindowY - 100 - 100 * (i + 1)), 400, 100), Color.White);
                if (selected_flag)
                {
                    Spritebatch.Draw(BattleManager.targeter, new Rectangle((int)(towerPosition.X + LeaveMeAlone.WindowX / 3) + 150, (int)(towerPosition.Y + LeaveMeAlone.WindowY - 100 - 100 * (i + 1)), 100, 100), Color.Red);
                }
            }
            for (int j = 0; j < TowerLevel + 1; j++)
            {
                //This is where is breaks in the final battle
                if (PartyManager.partyQueue[j] != null)
                {
                    
                    Character.Type placeholder = Character.Type.LairHero;
                    Vector2 characterPos = new Vector2((int)(towerPosition.X + LeaveMeAlone.WindowX / 3), (int)(towerPosition.Y + LeaveMeAlone.WindowY + 20 - 100 * (TowerLevel - j + 1)));
                    Character newChar = new Character(placeholder, 1, characterPos);
                                        //newChar.Init();
                    newChar.Draw(Spritebatch, Color.White);
                }
            }
            BattleManager.boss.Draw(Spritebatch, Color.White, true);
            int count = 0;
            foreach (UpgradeMenu.ButtonRoom r in boughtRooms)
            {
                r.Draw(Spritebatch, r.used);
                count++;
            }
            nextwaveBtn.Draw(Spritebatch);
            if (EndOfGame)
            {
                grindwaveBtn.Draw(Spritebatch);
            }
            skillsBtn.Draw(Spritebatch);
            constructionBtn.Draw(Spritebatch);
            mainmenuBtn.Draw(Spritebatch);
            InfoText.Draw(Spritebatch);

            if (TutorialText.message != "" && BattleManager.boss.level < 2)
            {
                 TutorialText.Draw(Spritebatch);
                 Spritebatch.Draw(highlighter, highlighter_rect, new Color(Color.LimeGreen, 120));
            }
        }

        internal static void addRoom(UpgradeMenu.ButtonRoom buttonRoom)
        {
            var cbutton = new Button(buttonRoom.r.img, 30, 50 + 60 * boughtRooms.Count, 100, 50);
            cbutton.text.font = Text.fonts["RetroComputer-12"];
            cbutton.text.position = new Vector2(140, 50 + 10 + 60 * boughtRooms.Count);
            var croom = new Room(buttonRoom.r.name, buttonRoom.r.cost, buttonRoom.r.level, buttonRoom.r.type, buttonRoom.r.description, buttonRoom.r.activate, buttonRoom.r.img);
            var copy = new UpgradeMenu.ButtonRoom(cbutton, croom);
            boughtRooms.Add(copy);
        }


    }
}
