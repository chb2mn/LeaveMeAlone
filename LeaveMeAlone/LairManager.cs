using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private static Texture2D lairBkgd, lairLobby, bossRoom, spikeRoom;
        private static Button skillsBtn, nextwaveBtn, constructionBtn;
        private static MouseState currentMouseState, lastMouseState;
        //Took out the parameters on these next two functions as 
        //they are likely going to want to hit every room in the lair?
        public static void loadContent(ContentManager content)
        {
            TowerLevel = 0;
            towerPosition = new Vector2(0, 0);
            skillsBtn = new Button(content.Load<Texture2D>("skillsBtn"), 600, 100, 200, 75);
            nextwaveBtn = new Button(content.Load<Texture2D>("nextwaveBtn"), -50, 350, 200, 75);
            constructionBtn = new Button(content.Load<Texture2D>("constructionBtn"), 600, 175, 200, 75);
            lairBkgd = content.Load<Texture2D>("lairBkgd");
            lairLobby = content.Load<Texture2D>("lairLobby");
            bossRoom = content.Load<Texture2D>("bossRoom");
            spikeRoom = content.Load<Texture2D>("spikeRoom2");
            
        }
        public static void LairAttack()
        {

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
                if (nextwaveBtn.Intersects(currentMouseState.X, currentMouseState.Y))
                {
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
                        return LeaveMeAlone.GameState.Battle;
                    }
                    else
                    {
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
                    TowerLevel++;
                    PartyManager.partyQueue.Add(null);
                    for (int j = PartyManager.partyQueue.Count() - 1; j > 0; j--)
                    {
                        PartyManager.partyQueue[j] = PartyManager.partyQueue[j - 1];
                    }
                    PartyManager.partyQueue[0] = null;
                    return LeaveMeAlone.GameState.Lair;
                }
            }

            return LeaveMeAlone.GameState.Lair;
        }
        public static void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(lairBkgd, new Rectangle(-1000, -600, 2308, 1200), Color.White);
            Spritebatch.Draw(lairLobby, new Rectangle((int)(towerPosition.X+200), (int)(towerPosition.X+400), 400, 100), Color.White);
            Spritebatch.Draw(bossRoom, new Rectangle((int)(towerPosition.X + 200), (int)(towerPosition.X + 400 - 100*(TowerLevel+1)), 400, 100), Color.White);
            for (int i = 0; i < TowerLevel; i++)
            {
                Spritebatch.Draw(spikeRoom, new Rectangle((int)(towerPosition.X + 200), (int)(towerPosition.X + 400 - 100 * (i + 1)), 400, 100), Color.White);
            }
            for (int j = 0; j < TowerLevel + 1; j++)
            {
                
                if (PartyManager.partyQueue[j] != null)
                {
                    Character.Type placeholder = Character.Type.Ranger;
                    Character newChar = new Character(placeholder, 1);
                    newChar.sPosition = new Vector2((int)(towerPosition.X + 200), (int)(towerPosition.Y + 500 - 100 * (TowerLevel - j + 1)));
                    newChar.Init();
                    newChar.Draw(Spritebatch, Color.White);
                }
            }
            nextwaveBtn.Draw(Spritebatch);
            skillsBtn.Draw(Spritebatch);
            constructionBtn.Draw(Spritebatch);
        }
    }
}
