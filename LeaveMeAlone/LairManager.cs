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
    public class LairManager
    {
        public static int TowerLevel;
        public static int MaxLevel;
        public static Vector2 towerPosition;
        public static List<Room> LairRooms;
        private static Texture2D lairBkgd, lairLobby, bossRoom, spikeRoom, unconstructed_room;
        private static Button skillsBtn, nextwaveBtn, constructionBtn;
        private static MouseState currentMouseState, lastMouseState;
        public static Room UnconstructedRoom;
        public static List<UpgradeMenu.ButtonRoom> boughtRooms = new List<UpgradeMenu.ButtonRoom>();
        //Took out the parameters on these next two functions as 
        //they are likely going to want to hit every room in the lair?
        public static void loadContent(ContentManager content)
        {
            TowerLevel = 0;
            MaxLevel = 3;
            towerPosition = new Vector2(0, 0);
            LairRooms = new List<Room>();
            skillsBtn = new Button(content.Load<Texture2D>("skillsBtn"), LeaveMeAlone.WindowX-200, 100, 200, 75);
            constructionBtn = new Button(content.Load<Texture2D>("constructionBtn"), LeaveMeAlone.WindowX - 200, 175, 200, 75);
            nextwaveBtn = new Button(content.Load<Texture2D>("nextwaveBtn"), -50, LeaveMeAlone.WindowY - 200, 200, 75);
            lairBkgd = content.Load<Texture2D>("lairBkgd");
            lairLobby = content.Load<Texture2D>("lairLobby");
            bossRoom = content.Load<Texture2D>("bossRoom");
            spikeRoom = content.Load<Texture2D>("spikeRoom2");
            unconstructed_room = content.Load<Texture2D>("unconstructed_room");
            UnconstructedRoom = new Room("Unconstructed Room", 0, 0, 0, "A new blank space to construct a room.", null, unconstructed_room);
            
        }
        public static void LairAttack(Room room, List<Character> party)
        {
            if (party != null)
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
                //next wave
                if (nextwaveBtn.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    for (int i = 0; i < TowerLevel; i++)
                    {
                        LairAttack(LairRooms[i], PartyManager.partyQueue[i]);
                    }
                    Random random = new Random();
                    int makeParty = random.Next(100) % 2;
                    if (makeParty == 1)
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
                        return LeaveMeAlone.GameState.Battle;
                    }
                    else
                    {
                        //Should have some sort of interface on screen
                        Console.Write("No party! Take a breather.");
                        return LeaveMeAlone.GameState.Lair;
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
                        Console.WriteLine("MaxLevel Reached");
                    }
                    return LeaveMeAlone.GameState.Lair;
                }
            }

            return LeaveMeAlone.GameState.Lair;
        }
        public static void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(lairBkgd, new Rectangle(0, 0, 2308, 1200), Color.White);
            Spritebatch.Draw(lairLobby, new Rectangle((int)(towerPosition.X+LeaveMeAlone.WindowX/3), (int)(towerPosition.Y+LeaveMeAlone.WindowY-100), 400, 100), Color.White);
            Spritebatch.Draw(bossRoom, new Rectangle((int)(towerPosition.X + LeaveMeAlone.WindowX / 3), (int)(towerPosition.Y + LeaveMeAlone.WindowY - 100 - 100 * (TowerLevel + 1)), 400, 100), Color.White);
            for (int i = 0; i < TowerLevel; i++)
            {
                Spritebatch.Draw(LairRooms[i].img, new Rectangle((int)(towerPosition.X + LeaveMeAlone.WindowX / 3), (int)(towerPosition.Y + LeaveMeAlone.WindowY - 100 - 100 * (i + 1)), 400, 100), Color.White);
            }
            for (int j = 0; j < TowerLevel + 1; j++)
            {
                
                if (PartyManager.partyQueue[j] != null)
                {
                    Character.Type placeholder = Character.Type.LairHero;
                    Character newChar = new Character(placeholder, 1);
                    newChar.sPosition = new Vector2((int)(towerPosition.X + LeaveMeAlone.WindowX / 3), (int)(towerPosition.Y + LeaveMeAlone.WindowY + 20 - 100 * (TowerLevel - j + 1)));
                    newChar.Init();
                    newChar.Draw(Spritebatch, Color.White);
                }
            }
            int count = 0;
            foreach(UpgradeMenu.ButtonRoom r in boughtRooms)
            {
                r.Draw(Spritebatch, new Rectangle(30, 300 + 100 * count, 200, 75));
                count ++;
            }
            nextwaveBtn.Draw(Spritebatch);
            skillsBtn.Draw(Spritebatch);
            constructionBtn.Draw(Spritebatch);
        }
    }
}
