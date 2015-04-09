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
    public class UpgradeMenu
    {
        private static Texture2D menuBackground;
        private static Button next;
        private static MouseState currentMouseState, lastMouseState;
        public static MenuBoss selectedBoss;
        public static List<Skill> boughtSkills = new List<Skill>();
        public static List<Room> boughtRooms = new List<Room>();

        public static SkillTree skilltree;
        public static Dictionary<string, Text> texts = new Dictionary<string, Text>();

        public static Vector2 baseSkillButtonPos = new Vector2(500, 50);
        public static Vector2 baseRoomButtonPos = new Vector2(1200, 50);
        public static Vector2 baseSelectedSkillButtonPos = new Vector2(20, 100);
        
        public static Text goldText;
        public static Text skillText;
        public static Text RoomText;
        public static Text selectedSkillsText;

        public static Skill skill_from_skilltree_to_swap;

        public static ButtonRoom[] AvailableRooms = new ButtonRoom[2];
        public static ButtonSkill[] SelectedSkills = new ButtonSkill[6];
        //public static ;
        public static ButtonSkill selectedSkillSwapButton;

        public struct ButtonSkill
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
        public struct ButtonRoom
        {
            public Button b;
            public Room r;
            private bool drawable;
            public void Draw(SpriteBatch s)
            {
                if(drawable)
                {
                    b.Draw(s);
                }
            }
            public void UpdateRoom(Room r)
            {
                b.text.changeMessage(r.name);
                this.r = r;
                drawable = true;
            }
            public void DeleteRoom()
            {
                r = default(Room);
                drawable = false;
            }
        }
        

        public static void Init(MenuBoss boss)
        {
            SkillTree.Init();
            selectedBoss = boss;
            skilltree = SkillTree.skilltrees[selectedBoss.bossType];
            selectedBoss.MoveTo(new Vector2(0, 0));
            selectedBoss.idle();
            Console.WriteLine("" + Text.fonts.Keys.ToString());
            Console.Out.Flush();
            var test = Text.fonts;

            AvailableRooms[0] = new ButtonRoom();
            AvailableRooms[1] = new ButtonRoom();
            AvailableRooms[0].b = new Button(SkillTree.buttonPic, (int)baseRoomButtonPos.X, (int)baseRoomButtonPos.Y, 100, 100);
            AvailableRooms[1].b = new Button(SkillTree.buttonPic, (int)baseRoomButtonPos.X + 75, (int)baseRoomButtonPos.Y, 100, 100);

            for(int x = 0; x < SelectedSkills.Length; x++)
            {
                SelectedSkills[x] = new ButtonSkill();
                SelectedSkills[x].b = new Button(SkillTree.buttonPic, 30, 450 + 75*x, 200, 50);
                SelectedSkills[x].b.UpdateText("NONE");
            }


            texts["gold"] = new Text("Gold: " + Resources.gold, new Vector2(30, 200), Text.fonts["6809Chargen-32"], Color.White);
            texts["selectedskills"] = new Text("Selected Skills", new Vector2(30, 400), Text.fonts["6809Chargen-32"], Color.White);
            texts["skilltext"] = new Text("Skills", new Vector2(SkillTree.baseSkillButtonPos.X, SkillTree.baseSkillButtonPos.Y - 50), Text.fonts["6809Chargen-32"], Color.White);
            texts["roomtext"] = new Text("Rooms", new Vector2(SkillTree.baseRoomButtonPos.X, SkillTree.baseRoomButtonPos.Y - 50), Text.fonts["6809Chargen-32"], Color.White);
        }

        public static void loadContent(ContentManager content)
        {
            menuBackground = content.Load<Texture2D>("DummyHero");
            next = new Button(content.Load<Texture2D>("next"), 1000, 1000, 113, 32);
        }

        public static LeaveMeAlone.GameState Update(GameTime g)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            texts["gold"].changeMessage("Gold: " + Resources.gold);
            foreach (Skill s in skilltree.SkillButtons.Keys)
            {
                if (s.cost > Resources.gold)
                {
                    skilltree.SkillButtons[s].text.color = Color.Red;
                }
                else
                {
                    skilltree.SkillButtons[s].text.color = Color.Black;
                }
            }

            selectedBoss.Update(g);
            if (lastMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {

                if (next.Intersects(currentMouseState.X, currentMouseState.Y))
                {
                    BattleManager.bossDefaultPosition();
                    return LeaveMeAlone.GameState.Lair;
                }
                foreach (Skill s in skilltree.SkillButtons.Keys)
                {
                    if (skilltree.SkillButtons[s].Intersects(currentMouseState.X, currentMouseState.Y))
                    {
                        if (BattleManager.boss.skills.Contains(s) == false)
                        {
                            if(s.cost < Resources.gold)
                            {
                                BattleManager.boss.addSkill(s);
                                Resources.gold -= s.cost;
                                Console.WriteLine(BattleManager.boss.skills.Count);
                            }
                        }
                        else
                        {
                            if (selectedSkillSwapButton.s != default(Skill) && BattleManager.boss.selected_skills.Contains(s) == false)
                            {
                                int index = BattleManager.boss.selected_skills.IndexOf(selectedSkillSwapButton.s);
                                BattleManager.boss.selected_skills[index] = s;
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
                        flag = true;
                        selectedSkillSwapButton = buttonStuff;
                        Console.WriteLine("selected " + buttonStuff.s.name);
                    }
                }
                if (flag == false)
                {
                    selectedSkillSwapButton = default(ButtonSkill);
                    Console.WriteLine("unselected");
                }
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
            foreach (Button button in skilltree.SkillButtons.Values)
            {
                button.Draw(sb);
            }
            foreach (Text t in texts.Values)
            {
                t.Draw(sb);
                //Console.WriteLine(t.message);
            }
            for(int x = 0; x < SelectedSkills.Length; x++)
            {
                SelectedSkills[x].Draw(sb);
            }
            next.Draw(sb);
        }
    }
}
