using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LeaveMeAlone;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content;

namespace LeaveMeAlone
{
    class SkillTree
    {
        public Dictionary<int, List<Skill>> skill_tiers;
        public Dictionary<int, List<Room>> room_tiers;

        public static Texture2D spikeroom;
        
        public static Dictionary<Character.Type, SkillTree> skilltrees = new Dictionary<Character.Type, SkillTree>();
        public Dictionary<Skill, Button> buttons = new Dictionary<Skill,Button>();
        public static Texture2D buttonPic;

        //>>>>>>>>>>>>>>>>>>>>Skill Declarations<<<<<<<<<<<<//
        public static Skill basic_attack;
        public static Skill defend;
        public static Skill portal_punch;
        public static Skill flamethrower;
        public static Skill nuclear_waste;

        //>>>>>>>>>>>>>>>>Room Declarations<<<<<<<<<<<//
        public static Room spike_trap;

        public SkillTree()
        {
            skill_tiers = new Dictionary<int, List<Skill>>();
            room_tiers = new Dictionary<int, List<Room>>();
        }
        public static void LoadContent(ContentManager content)
        {
            spikeroom = content.Load<Texture2D>("spikeRoom2");
            buttonPic = content.Load<Texture2D>("buttonbase");

            //>>>>>>>>>>>>>>>>>>>>Skill Instances<<<<<<<<<<<<<<<<<<<//
            basic_attack = new Skill("Attack", 0, 0, 1, 0, Skill.Target.Single, 0, "Basic Attack", BasicAttack);
            defend = new Skill("Defend", 0, 0, 1, 1, Skill.Target.Self, 0, "Heal yourself!", Defend);
            portal_punch = new Skill("Portal Punch", 5, 0, 1, 0, Skill.Target.Single, 1, "Does Sp.Atk. Dmg", PortalPunch);
            flamethrower = new Skill("Flamethrower", 10, 0, 1, 0, Skill.Target.All, 1, "Burn all of your enemies!", FlameThrower);
            nuclear_waste = new Skill("Nuclear Waste", 5, 0, 1, 0, Skill.Target.Single, 1, "Infect an enemy with poision", NuclearWaste);

            //>>>>>>>>>>>>>>>>>>>Room Instances<<<<<<<<<<<<<<<<<<<<<//
            spike_trap = new Room("Spike Trap", 100, 1, 0, "Does damage to hero relative to their defense", SpikeTrap, spikeroom);

        }

        //Instantiates all classes     
        public void updateTree()
        {
            List<int> keys = skill_tiers.Keys.ToList();
            keys.Sort();
            //int boss_level = BattleManager.boss.level;
            int kindex = 0;
            foreach (int key in keys)
            {
                Console.WriteLine(key);
                List<Skill> tier = skill_tiers[key];
                int slength = tier.Count;
                int sindex = 0;
                foreach (Skill skill in tier)
                {
                    Console.WriteLine(skill.name);
                    Button b = new Button(buttonPic, 200 + sindex*175, 50 + 75*kindex, 150, 50);
                    b.UpdateText(skill.name);
                    buttons[skill] = b;
                    sindex++;
                }
                Console.WriteLine();
                kindex++;
            }
        }


        //Instantiates all classes
        public static void Init()
        {
            initBrute();
            initMastermind();
            initOperative();
            initRanger();
            initMage();
            initKnight();

        }
        public void Draw(SpriteBatch s)
        {
            foreach(Button button in buttons.Values)
            {
                button.Draw(s);
            }

        }
        public void addSkill(int level, Skill skill)
        {
            addToDict(skill_tiers, ref level, ref skill);
        }
        public void addRoom(int level, Room room)
        {
            addToDict(room_tiers, ref level, ref room);
        }
        private void addToDict<T,F>(Dictionary<T,List<F>> d, ref T index, ref F value)
        {
            List<F> existing;
            if (!d.TryGetValue(index, out existing))
            {
                existing = new List<F>();
                d[index] = existing;
            }
            // At this point we know that "existing" refers to the relevant list in the 
            // dictionary, one way or another.
            existing.Add(value);
        }


        //The thing with all the trees
        
        public static void initBrute()
        {
            SkillTree st = new SkillTree();
            st.addSkill(1, portal_punch);
            st.addSkill(1, flamethrower);
            st.addSkill(2, nuclear_waste);
            skilltrees[Character.Type.Brute] = st;
            st.updateTree();
        }
        public static void initMastermind()
        {
            SkillTree st = new SkillTree();
            st.addSkill(1, portal_punch);
            st.addSkill(1, flamethrower);
            st.addSkill(2, nuclear_waste);
            skilltrees[Character.Type.Mastermind] = st;
            st.updateTree();

        }
        public static void initOperative()
        {
            SkillTree st = new SkillTree();
            st.addSkill(1, portal_punch);
            st.addSkill(1, flamethrower);
            st.addSkill(2, nuclear_waste);
            skilltrees[Character.Type.Operative] = st;
            st.updateTree();

        }
        public static void initRanger()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Ranger] = st;
        }
        public static void initMage()
        {
            SkillTree st = new SkillTree();
            st.addSkill(1, portal_punch);
            st.addSkill(1, flamethrower);
            st.addSkill(2, nuclear_waste);
            skilltrees[Character.Type.Mage] = st;
            st.updateTree();

        }
        public static void initKnight()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Knight] = st;
            st.updateTree();
        }

        

        //>>>>>>>>>>>>>>>>>>>>Skill Delegates<<<<<<<<<<<<<<<<<<<//
        public static void BasicAttack(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 100);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            target.damage_text.changeMessage(str_damage);
        }
        public static void Defend(Character caster, Character target = null)
        {
            caster.health += (int)(((double)caster.max_health) * .2);
            if (caster.health > caster.max_health)
            {
                caster.health = caster.max_health;
            }
        }
        public static void PortalPunch(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 100);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            target.damage_text.changeMessage(str_damage);
        }
        public static void FlameThrower(Character caster, Character target = null)
        {
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    target = BattleManager.heroes[i];
                    if (target == null) { continue; }
                    int damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 40);
                    target.health -= damage;
                    String str_damage = (-damage).ToString();
                    target.damage_text.changeMessage(str_damage);
                }
                catch (ArgumentOutOfRangeException)
                {

                }
            }          
        }
        public static void NuclearWaste(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 40);
            target.health -= damage;
            target.damage_text.changeMessage((-damage).ToString());

            //If the status already exists, increase its duration
            if (target.statuses.Contains(Status.poison))
            {
                int status_index = target.statuses.IndexOf(Status.poison);
                target.statuses[status_index].duration_left += Status.poison.duration;
            }
            //Otherwise add it
            else
            {
                target.statuses.Add(Status.poison);
            }
        }
        //>>>>>>>>>>>>>>>>>>>>>>>Room Delegates<<<<<<<<<<<<<<<<<<<<//
        public static void SpikeTrap(List<Character> heroes)
        {
            for (int i = 0; i < heroes.Count(); i++)
            {
                Character hero = heroes[i];
                if (hero != null)
                {
                    Console.WriteLine("Applying damage");
                    int damage = (int)(hero.defense * .1);
                    hero.health -= damage;
                    if (hero.health <= 0)
                    {
                        hero.health = 1;
                        Console.WriteLine("Destroying enemy");
                    }
                }
            }
        }
    }
}
