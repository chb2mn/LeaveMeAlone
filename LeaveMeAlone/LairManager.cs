using System;
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
        public static Text InfoText;
        public static int texttimer;
        public static Vector2 towerPosition;
        public static List<Room> LairRooms;
        private static Texture2D lairBkgd, lairLobby, bossRoom, spikeRoom, unconstructed_room;
        private static Button skillsBtn, nextwaveBtn, constructionBtn;
        private static MouseState currentMouseState, lastMouseState;
        public static Room UnconstructedRoom;
        public static List<UpgradeMenu.ButtonRoom> boughtRooms = new List<UpgradeMenu.ButtonRoom>();
        public static UpgradeMenu.ButtonRoom selectedRoomSwapButton { get; set; }
        public static  int sideOffset = 75;
        public static  int sideScaling = 85;
        public static  int topOffset = 150;
        public static int topScaling = 80;
        public static bool selected_flag = false;
        //Took out the parameters on these next two functions as 
        //they are likely going to want to hit every room in the lair?
        public static void loadContent(ContentManager content)
        {
            selectedRoomSwapButton = new UpgradeMenu.ButtonRoom();
            EndOfGame = false;
            TowerLevel = 0;
            MaxLevel = 3;
            InfoText = new Text("", new Vector2(LeaveMeAlone.WindowX - 350, LeaveMeAlone.WindowY - 200), Text.fonts["6809Chargen-24"], Color.Black);
            towerPosition = new Vector2(0, 0);
            LairRooms = new List<Room>();
            skillsBtn = new Button(content.Load<Texture2D>("skillsBtn"), LeaveMeAlone.WindowX - 200, 100, 200, 75);
            constructionBtn = new Button(content.Load<Texture2D>("constructionBtn"), LeaveMeAlone.WindowX - 200, 175, 200, 75);
            nextwaveBtn = new Button(content.Load<Texture2D>("nextwaveBtn"), LeaveMeAlone.BackgroundRect.X, LeaveMeAlone.BackgroundRect.Height - 75, 200, 75);
            lairBkgd = content.Load<Texture2D>("lairBkgd");
            lairLobby = content.Load<Texture2D>("lairLobby");
            bossRoom = content.Load<Texture2D>("bossRoom");
            spikeRoom = content.Load<Texture2D>("spikeRoom");
            unconstructed_room = content.Load<Texture2D>("unconstructed_room");
            UnconstructedRoom = new Room("Unconstructed Room", 0, 0, 0, "A new blank space to construct a room.", null, unconstructed_room); 

        }

        public static void Init()
        {
            //play the music
            LeaveMeAlone.Battle_Song_Instance.Stop();
            LeaveMeAlone.Menu_Song_Instance.Play();
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
                        
                    }
                }
                //next wave
                if (nextwaveBtn.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    if (EndOfGame)
                    {
                        
                        InfoText.changeMessage("PEASANTS!\nOUT OF THE WAY!\nHE'S OURS");
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
                        else
                        {

                            if (PartyManager.popParty())
                            {
                                BattleManager.Init();

                                List<Character> FinalParty = new List<Character>();
                                FinalParty.Add(new Character(Character.Type.Knight, 14, new Vector2(sideOffset + sideScaling, topOffset)));
                                FinalParty.Add(new Character(Character.Type.Ranger, 14, new Vector2(sideOffset, topOffset + topScaling * 1)));
                                FinalParty.Add(new Character(Character.Type.Mage, 14, new Vector2(sideOffset + sideScaling, topOffset + topScaling * 2)));
                                FinalParty.Add(new Character(Character.Type.Mage, 14, new Vector2(sideOffset, topOffset + topScaling * 3)));
                                PartyManager.partyQueue.Add(FinalParty);
                               
                                return LeaveMeAlone.GameState.Battle;
                            }
                            else
                            {
                                PartyManager.partyQueue.Add(null);
                                return LeaveMeAlone.GameState.Lair;
                            }

                        }

                    }
                    else
                    {
                        for (int i = 0; i < TowerLevel; i++)
                        {
                            LairAttack(LairRooms[i], PartyManager.partyQueue[i]);
                        }
                        Random random = new Random();
                        int makeParty = random.Next(100) % 4;
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
                            InfoText.changeMessage("No party!\n      Take a breather.");
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
                    return LeaveMeAlone.GameState.Lair;
                }
                //checks if a room button was selected
                selected_flag = false;
                foreach (UpgradeMenu.ButtonRoom r in boughtRooms)
                {
                    if (r.b.Intersects(currentMouseState.X, currentMouseState.Y))
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
            int count = 0;
            foreach (UpgradeMenu.ButtonRoom r in boughtRooms)
            {
                r.Draw(Spritebatch, r.used);
                count++;
            }
            nextwaveBtn.Draw(Spritebatch);
            skillsBtn.Draw(Spritebatch);
            constructionBtn.Draw(Spritebatch);
            InfoText.Draw(Spritebatch);

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
