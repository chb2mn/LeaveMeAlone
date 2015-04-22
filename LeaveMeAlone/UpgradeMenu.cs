using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;

namespace LeaveMeAlone
{
    public class UpgradeMenu
    {
        private static Texture2D menuBackground;
        public static Texture2D nothing_img { get; set; }

        public static Texture2D empty_exp;
        public static Rectangle full_exp;
        public static Text exp_text;
        public static Texture2D green;

        private static Button next;
        private static MouseState currentMouseState, lastMouseState;
        public static MenuBoss selectedBoss;
        public static List<Skill> boughtSkills = new List<Skill>();
        public static List<Room> boughtRooms = new List<Room>();

        public static Text TutorialText;
        public enum TutorialState { BuySkill, BuyRoom, Back, None };
        public static TutorialState tutorial_state;
        public static Rectangle highlighter_rect;

        public static SkillTree skilltree;
        public static Dictionary<string, Text> texts = new Dictionary<string, Text>();

        public static Vector2 baseSkillButtonPos = new Vector2(400, 100);
        public static Vector2 baseRoomButtonPos = new Vector2(400, 550);
        public static Vector2 baseSelectedSkillButtonPos = new Vector2(30, 250);

        public static bool left_click = false;
        public static bool right_click = false;

        public static Skill skill_from_skilltree_to_swap;

        public static ButtonRoom[] AvailableRooms = new ButtonRoom[2];
        public static ButtonSkill[] SelectedSkills = new ButtonSkill[6];
        //public static ;
        public static ButtonSkill selectedSkillSwapButton;
        public static Room Nothing;

        public static Text hovertext;
        public static Texture2D hovertextbackground;
        public static Vector2 hovertextpos;
        public static Rectangle hovertextRect;

        public static SoundEffectInstance buyingSound;
        public static SoundEffectInstance swappingSound;

        public static Vector2 measurements;

        public static Text statsTitle;
        public static Rectangle statsTitleRect;
        public static Text statsText;
        public static Rectangle statsTextRect;

        public class ButtonSkill
        {
            public Button b;
            public Skill s;
            public void Draw(SpriteBatch s)
            {
                    b.Draw(s);
            }
            public void UpdateSkill(Skill s)
            {
                b.text.changeMessage(s.name);
                this.s = s;
            }
            public void DeleteRoom()
            {
                s = default(Skill);
                b.UpdateText("NONE");
            }
        }
        public class ButtonRoom
        {
            public ButtonRoom Copy()
            {
                var b = new ButtonRoom(this.b, this.r);
                b.drawable = false;
                return b;
            }
            public Button b;
            public Room r;
            public bool used = false;
            private bool drawable;
            public ButtonRoom(Button b, Room r)
            {
                this.b = b;
                this.r = r;
                this.b.text.changeMessage(r.name);
                drawable = true;
            }
            public ButtonRoom()
            {

            }
            public void Draw(SpriteBatch s, bool faded = false)
            {
                if(drawable)
                {
                    b.Draw(s, faded);
                }
            }
            public void Draw(SpriteBatch s, Rectangle r)
            {
                s.Draw(b.sprite, r, Color.White);
            }
            public void UpdateRoom(Room newRoom)
            {
                b.text.changeMessage(newRoom.name);
                b.sprite = newRoom.img;
                r.img = newRoom.img;
                drawable = true;
            }
            public void DeleteRoom()
            {
                r = default(Room);
                drawable = false;
            }
        }
        
        public static Room makeNothing()
        {
            return new Room("", 0, -1, 0, "Nothing!", null, nothing_img);
        }
        public static void zeroAvailableRooms()
        {
            AvailableRooms[0] = new ButtonRoom(new Button(nothing_img, (int)baseRoomButtonPos.X, (int)baseRoomButtonPos.Y, 200, 50), makeNothing());
            AvailableRooms[1] = new ButtonRoom(new Button(nothing_img, (int)baseRoomButtonPos.X + 300, (int)baseRoomButtonPos.Y, 200, 50), makeNothing());
        }
        public static void HandleTutorial()
        {
            Rectangle target_rect = new Rectangle();
            switch (tutorial_state)
            {
                case TutorialState.BuySkill:
                    TutorialText.changeMessage("To fight heroes we need to buy a basic skill");
                    target_rect = new Rectangle((int)baseSkillButtonPos.X - 10, (int)baseSkillButtonPos.Y - 10, 220, 70);
                    break;
                case TutorialState.BuyRoom:
                    TutorialText.changeMessage("And let's get a room to go along with it");
                    target_rect = new Rectangle((int)baseRoomButtonPos.X - 10, (int)baseRoomButtonPos.Y - 10, 220, 70);

                    break;
                case TutorialState.Back:
                    TutorialText.changeMessage("Now let's go and check out our Lair");
                    target_rect = new Rectangle(next.rectangle.X-10, next.rectangle.Y-10, next.rectangle.Width + 20, next.rectangle.Height + 20);
                    break;
                case TutorialState.None:
                    TutorialText.changeMessage("");
                    break;
            }
            highlighter_rect = target_rect;
        }

        public static void Init(MenuBoss boss)
        {
            SkillTree.Init(boss.bossType);

            
            tutorial_state = TutorialState.BuySkill;

            selectedBoss = boss;
            skilltree = SkillTree.skilltrees[selectedBoss.bossType];
          
            selectedBoss.MoveTo(new Vector2(0, 0));
            selectedBoss.idle();
            Console.WriteLine("" + Text.fonts.Keys.ToString());
            Console.Out.Flush();
            var test = Text.fonts;

            zeroAvailableRooms();
            UpgradeMenu.rerollRooms();
            
            selectedSkillSwapButton = new ButtonSkill();
            for(int x = 0; x < SelectedSkills.Length; x++)
            {
                SelectedSkills[x] = new ButtonSkill();
                SelectedSkills[x].b = new Button(Button.buttonPic, (int)baseSelectedSkillButtonPos.X, (int)baseSelectedSkillButtonPos.Y + 60 * x, 200, 50);
                SelectedSkills[x].b.text.font = Text.fonts["6809Chargen-12"];
                SelectedSkills[x].b.UpdateText("NONE");
            }

            hovertextpos = new Vector2(30, 150);
            hovertext = new Text("", new Vector2(hovertextpos.X + 10, hovertextpos.Y + 10), Text.fonts["RetroComputer-12"]);
            hovertextRect = new Rectangle((int)hovertextpos.X, (int)hovertextpos.Y, 500, 600);
            //250 x 150
            hovertextbackground = BattleManager.hovertextbackground;

            Vector2 statspos = new Vector2(30, boss.sTexture.Height + 10);
            statsTitle = new Text("Stats", statspos, Text.fonts["RetroComputer-18"]);
            statsTitleRect = new Rectangle((int)statspos.X - 10, (int)statspos.Y - 5, (int)statsTitle.getMeasurements(300).X + 10, (int)statsTitle.font.MeasureString(statsTitle.message).Y + 15);
            statsText = new Text(BattleManager.boss.StatsToString(), new Vector2(statspos.X, statspos.Y - 70), Text.fonts["RetroComputer-12"]);;
            statsTextRect = new Rectangle((int)statsText.position.X, (int)statsText.position.Y - 100, (int)statsText.getMeasurements(300).X + 10, (int)statsText.getMeasurements(300).Y + 10);
            statsText.changeMessage("");

            texts["gold"] =             new Text("Gold: " + Resources.gold, new Vector2(150, 0), Text.fonts["6809Chargen-24"], Color.White);
            texts["level"] =            new Text("Level: " + BattleManager.boss.level, new Vector2(150, 50), Text.fonts["6809Chargen-24"], Color.White);
            texts["selectedskills"] =   new Text("Selected Skills",         new Vector2(baseSelectedSkillButtonPos.X, baseSelectedSkillButtonPos.Y - Text.fonts["6809Chargen-24"].MeasureString("Selected Skills").Y - 10), Text.fonts["6809Chargen-24"], Color.White);
            texts["skilltext"] =        new Text("Skills",                  new Vector2(baseSkillButtonPos.X + 100, baseSkillButtonPos.Y - 50), Text.fonts["6809Chargen-24"], Color.White);
            texts["roomtext"] =         new Text("Rooms",                   new Vector2(baseRoomButtonPos.X, baseRoomButtonPos.Y - 50), Text.fonts["6809Chargen-24"], Color.White);
        }
        public static void rerollRooms()
        {
            //find out why room except on list doesn't work
            List<Room> validRooms = new List<Room>();

            //get all keys to room_tiers
            List<int> keys = skilltree.room_tiers.Keys.ToList();
            //sort to get from lowest to highest
            keys.Sort();
            int boss_level = BattleManager.boss.level;

            //go though all ints representing levels in the 
            for (int x = 0; x < keys.Count; x++)
            {
                int key = keys[x];
                //if we are at a higher level than we should be
                if(key > BattleManager.boss.level)
                {
                    break;
                }
                List<Room> rooms = skilltree.room_tiers[key];
                validRooms.AddRange(rooms);
            }
            //get rooms not already bought
            List<Room> roomlist = new List<Room>();
            foreach(Room r in validRooms)
            {
                if(boughtRooms.Contains(r) == false)
                {
                    roomlist.Add(r);
                }
            }
            validRooms = roomlist;

            //clear the old rooms
            //zeroAvailableRooms();
            int numToPick = 2;
            if (validRooms.Count < 2)
            {
                numToPick = validRooms.Count;
            }
            int index =0;
            for (index = 0; index < numToPick; index++)
            {
                Room r = validRooms[LeaveMeAlone.random.Next(validRooms.Count)];
                validRooms.Remove(r);
                var roombutton = AvailableRooms[index];
                AvailableRooms[index] = new ButtonRoom(new Button(r.img, (int)baseRoomButtonPos.X + 300 * index, (int)baseRoomButtonPos.Y, 200, 50), r);
                AvailableRooms[index].b.text.font = Text.fonts["RetroComputer-12"];
                AvailableRooms[index].b.text.position = new Vector2(roombutton.b.rectangle.X, roombutton.b.rectangle.Y + roombutton.b.rectangle.Height + 5);
                AvailableRooms[index].b.text.color = Color.White;
            }
            for (; index < 2; index++)
            {
                AvailableRooms[index] = new ButtonRoom(new Button(nothing_img, (int)baseRoomButtonPos.X + 300 * index, (int)baseRoomButtonPos.Y, 200, 50), makeNothing());
            }
        }
        public static void loadContent(ContentManager content)
        {
            menuBackground = content.Load<Texture2D>("DummyHero");

            green = content.Load<Texture2D>("green");
            empty_exp = content.Load<Texture2D>("Exp");
            full_exp = new Rectangle(150,100, 0, 40);
            exp_text = new Text(0+"/"+500, new Vector2(155, 100), f: Text.fonts["Arial-24"], c: Color.Azure);

            next = new Button(content.Load<Texture2D>("next"), LeaveMeAlone.BackgroundRect.Width-180, LeaveMeAlone.BackgroundRect.Height-110, 113, 32);
            nothing_img = content.Load<Texture2D>("nothing");
            TutorialText = new Text("", new Vector2(375, 0), Text.fonts["RetroComputer-18"], Color.White);

            buyingSound = content.Load<SoundEffect>("Sounds/cash-register").CreateInstance();
            buyingSound.Volume = .2f;
            swappingSound = content.Load<SoundEffect>("Sounds/swap_sound").CreateInstance();
        }
        public static bool leftClicked()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed && !left_click;
        }
        public static bool rightClicked()
        {
            return Mouse.GetState().RightButton == ButtonState.Pressed && !right_click;
        }
        public static LeaveMeAlone.GameState Update(GameTime g)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            int xpos = currentMouseState.X;
            int ypos = currentMouseState.Y;

            texts["gold"].changeMessage("Gold: " + Resources.gold);
            texts["level"].changeMessage("Level: " + BattleManager.boss.level);
            
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                left_click = false;
            }
            if (Mouse.GetState().RightButton == ButtonState.Released)
            {
                right_click = false;
            }

            if (BattleManager.boss.level < 2)
            {
                HandleTutorial();
            }

            foreach(Skill s in BattleManager.boss.skills)
            {
                skilltree.SkillButtons[s].text.color = Color.Black;
            }
            //gets things that haven't been bought and colors them
            foreach (Skill s in skilltree.SkillButtons.Keys.Except(BattleManager.boss.skills))
            {
                if (s.cost > Resources.gold || BattleManager.boss.level < s.level)
                {
                    skilltree.SkillButtons[s].text.color = Color.Red;
                }
                else
                {
                    skilltree.SkillButtons[s].text.color = Color.Blue;
                }
            }

            bool hovered = false;
            foreach (Skill s in skilltree.SkillButtons.Keys)
            {
                
                if (skilltree.SkillButtons[s].Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    hovertext.changeMessage(s.name + ":\n" + s.description);
                    hovered = true;
                }
            }
            //Hover over Stats button
            if(statsTitleRect.Contains(currentMouseState.X, currentMouseState.Y))
            {
                statsText.changeMessage(BattleManager.boss.StatsToString());
                Vector2 statmeasurements = statsText.getMeasurements(300);
                statsText.position = new Vector2(statsTitle.position.X, statsTitleRect.Y + statsTitleRect.Height + 10);
                Console.WriteLine("statspos" + statsText.position.ToString());
                statsTextRect = new Rectangle((int)statsText.position.X - 10, (int)statsText.position.Y - 10, 300, (int)statmeasurements.Y + 10);
            }
            else
            {
                statsText.changeMessage("");
            }

            //perfect place for polymophism Chris
            hovered = false;
            foreach (Skill s in skilltree.SkillButtons.Keys)
            {

                if (skilltree.SkillButtons[s].Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(s.name + ":");
                    if(BattleManager.boss.skills.Contains(s))
                    {
                        sb.Append(" (bought)");
                    }
                    else
                    {
                        sb.Append(" (Cost: " + s.cost + ")");
                    }
                    sb.Append("\n" + s.description);
                    hovertext.changeMessage(sb.ToString());
                    hovered = true;
                }
            }
            foreach (ButtonRoom r in AvailableRooms)
            {
                if(r.b.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(r.r.name + ":");
                    if (boughtRooms.Contains(r.r))
                    {
                        sb.Append(" (bought)");
                    }
                    else
                    {
                        sb.Append(" (Cost: " + r.r.cost + ")");
                    }
                    sb.Append("\n" + r.r.description);
                    hovertext.changeMessage(sb.ToString());
                    hovered = true;
                }
            }
            if(hovered == false)
            {
                hovertext.changeMessage("");
            }
            selectedBoss.Update(g);
            //if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            if(leftClicked())
            {
                //check if a room was clicked on
                for (int x = 0; x < AvailableRooms.Length; x++)
                {
                    if(BattleManager.boss.selected_rooms.Contains(AvailableRooms[x].r) == false && AvailableRooms[x].b.Intersects(currentMouseState.X, currentMouseState.Y) && AvailableRooms[x].r.level != -1)
                    {
                        if(AvailableRooms[x].r.cost < Resources.gold)
                        {
                            BattleManager.boss.selected_rooms.Add(AvailableRooms[x].r);
                            LairManager.addRoom(AvailableRooms[x]);
                            boughtRooms.Add(AvailableRooms[x].r);
                            Resources.gold -= AvailableRooms[x].r.cost;
                            tutorial_state = TutorialState.Back;
                            buyingSound.Play();
                        }
                    }
                }
                //check the next button
                if (next.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    BattleManager.bossDefaultPosition();
                    LairManager.tutorial_state = LairManager.TutorialState.Build1;
                    return LeaveMeAlone.GameState.Lair;
                }
                //check if we clicked on a skill
                foreach (Skill s in skilltree.SkillButtons.Keys)
                {
                    if (skilltree.SkillButtons[s].Intersects(currentMouseState.X, currentMouseState.Y))
                    {
                        Console.WriteLine("Got one!");
                        if (BattleManager.boss.skills.Contains(s) == false && BattleManager.boss.level >= s.level)
                        {
                            //if you have enough money, buy it
                            if(s.cost < Resources.gold)
                            {
                                BattleManager.boss.addSkill(s);
                                buyingSound.Play();
                                tutorial_state = TutorialState.BuyRoom;
                                Resources.gold -= s.cost;
                                if (s == SkillTree.final_skill[BattleManager.boss.charType])
                                {
                                    LairManager.EndOfGame = true;
                                    LairManager.nextwaveBtn.rectangle.X = LeaveMeAlone.BackgroundRect.X;
                                    LairManager.nextwaveBtn.rectangle.Y = LeaveMeAlone.BackgroundRect.Height - 200;
                                }
                                //Console.WriteLine(BattleManager.boss.skills.Count);
                            }
                        }
                        else
                        {
                            //When a skill isn't selected to swap, it should be set to default(Skill)
                            if (selectedSkillSwapButton.s != default(Skill) && BattleManager.boss.skills.Contains(s) == true && BattleManager.boss.selected_skills.Contains(s) == false)
                            {
                                int index = BattleManager.boss.selected_skills.IndexOf(selectedSkillSwapButton.s);
                                BattleManager.boss.selected_skills[index] = s;
                                swappingSound.Play();
                                Console.WriteLine("swapped");
                            }
                        }
                        //Console.WriteLine(s+" pressed");
                    }
                }
                updateSelectedSkills();
                bool flag = false;
                for (int x = 0; x < SelectedSkills.Length; x++)
                {
                    var buttonStuff = SelectedSkills[x];

                    if (buttonStuff.b.Intersects(currentMouseState.X, currentMouseState.Y) && buttonStuff.s != null)
                    {
                        //already have a selected thing;
                        if(selectedSkillSwapButton.b != null)
                        {
                            selectedSkillSwapButton.b.selected = false;
                        }
                        flag = true;
                        selectedSkillSwapButton = buttonStuff;
                        selectedSkillSwapButton.b.selected = true;
                        //Console.WriteLine("selected " + buttonStuff.s.name);
                    }
                }
                if (flag == false)
                {
                    if (selectedSkillSwapButton.b != null)
                    {
                        selectedSkillSwapButton.b.selected = false;
                    }
                    selectedSkillSwapButton = new ButtonSkill();;

                    Console.WriteLine("unselected");
                }
            }
            
            measurements = hovertext.getMeasurements(hovertextRect.Width - 15);
            if (xpos + hovertextRect.Width > LeaveMeAlone.WindowX -20)
            {
                hovertextRect.X = LeaveMeAlone.WindowX - hovertextRect.Width - 20;
            }
            else
            {
                hovertextRect.X = currentMouseState.X + 10;
            }
            if (ypos + measurements.Y > LeaveMeAlone.WindowY - 20)
            {
                hovertextRect.Y = LeaveMeAlone.WindowY - (int)measurements.Y - 20;
            }
            else
            {
                hovertextRect.Y = currentMouseState.Y + 10;
            }
            return LeaveMeAlone.GameState.Upgrade;
        }
        public static void updateSelectedSkills()
        {
            int count = 0;
            foreach(Skill s in BattleManager.boss.selected_skills)
            {
                SelectedSkills[count].UpdateSkill(s);
                count++;
            }
        }
        public static void Draw(SpriteBatch sb)
        {
            sb.Draw(menuBackground, LeaveMeAlone.BackgroundRect, Color.Black);
            selectedBoss.Draw(sb, Color.White);

            if (BattleManager.boss.level < 2) 
            {
                TutorialText.Draw(sb);
            }

            sb.Draw(green, full_exp, Color.Green);
            sb.Draw(empty_exp, new Vector2(150, 100), Color.White);
            exp_text.Draw(sb, c: Color.Azure);

            foreach (Button button in skilltree.SkillButtons.Values)
            {
                if (button.text.color == Color.Red)
                {
                    button.Draw(sb, true);
                }
                else
                {
                    button.Draw(sb);
                }
            }
            foreach (Text t in texts.Values)
            {
                t.Draw(sb);
                //Console.WriteLine(t.message);
            }
            for(int x = 0; x < SelectedSkills.Length; x++)
            {
                if(SelectedSkills[x].b.text.message == "NONE")
                {
                    SelectedSkills[x].b.Draw(sb, true);
                }
                else
                {
                SelectedSkills[x].Draw(sb);
                }
            }
            for (int x = 0; x < AvailableRooms.Length; x++)
            {
                if (BattleManager.boss.selected_rooms.Contains(AvailableRooms[x].r) == false && AvailableRooms[x].r.level != -1)
                {
                    if(AvailableRooms[x].r.cost > Resources.gold)
                    {
                        sb.Draw(Button.redbackground, AvailableRooms[x].b.selectRectangle, Color.White);
                        AvailableRooms[x].b.Draw(sb);
                        sb.Draw(Button.redbackground, AvailableRooms[x].b.selectRectangle, Color.White * 0.2f);
                    }
                    else
                    {
                        sb.Draw(Button.bluebackground, AvailableRooms[x].b.selectRectangle, Color.White);
                        AvailableRooms[x].b.Draw(sb);
                        sb.Draw(Button.bluebackground, AvailableRooms[x].b.selectRectangle, Color.White * 0.2f);
                        //sb.Draw(Button.redbackground, AvailableRooms[x].b.selectRectangle, Color.White*0.5f);
                    }
                }
                else
                {
                    sb.Draw(Button.graybackground, AvailableRooms[x].b.selectRectangle, Color.White);
                    AvailableRooms[x].b.Draw(sb);
                }
                
            }

            if (hovertext.message != "")
            {
                
                Rectangle scaledHoverTextRect = new Rectangle(hovertextRect.X, hovertextRect.Y, hovertextRect.Width, (int)measurements.Y);

                hovertext.position = new Vector2(hovertextRect.X + 10, hovertextRect.Y + 10);
                sb.Draw(hovertextbackground, scaledHoverTextRect, Color.White);
                hovertext.Draw(sb, maxLineWidth: hovertextRect.Width - 15);
            }

            sb.Draw(hovertextbackground, statsTitleRect, Color.White);
            statsTitle.Draw(sb);
            if(statsText.message != "")
            {
                sb.Draw(hovertextbackground, statsTextRect, Color.White);
                statsText.Draw(sb);
            }

            next.Draw(sb);

            if (BattleManager.boss.level < 2)
            {
                sb.Draw(green, highlighter_rect, new Color(Color.LimeGreen, 40)); 
            }
        }


    }
}
