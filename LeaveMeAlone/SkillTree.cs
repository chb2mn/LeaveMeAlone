using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeaveMeAlone;

namespace LeaveMeAlone
{
    class SkillTree
    {
        public Dictionary<int, List<Skill>> skill_tiers;
        public Dictionary<int, List<Room>> room_tiers;
        
        public SkillTree()
        {
            skill_tiers = new Dictionary<int, List<Skill>>();
            room_tiers = new Dictionary<int, List<Room>>();
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
        public static Dictionary<Character.Type, SkillTree> skilltrees = new Dictionary<Character.Type, SkillTree>();
        public static void initBrute()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Brute] = st;
        }
        public static void initMastermind()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Mastermind] = st;
        }
        public static void initOperative()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Operative] = st;
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
            //addSkill(level, skill)
            skilltrees[Character.Type.Mage] = st;
        }
        public static void initKnight()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Knight] = st;
        }

        //>>>>>>>>>>>>>>>>>>>>Skill Instances<<<<<<<<<<<<<<<<<<<//
        public static Skill basic_attack = new Skill("Attack", 0, 0, 1, 0, Skill.Target.Single, 0, "Basic Attack", BasicAttack);
        public static Skill defend = new Skill("Defend", 0, 0, 1, 1, Skill.Target.Self, 0, "Heal yourself!", Defend);
        public static Skill portal_punch = new Skill("Portal Punch", 5, 0, 1, 0, Skill.Target.Single, 1, "Does Sp.Atk. Dmg", PortalPunch);
        public static Skill flamethrower = new Skill("Flamethrower", 10, 0, 1, 0, Skill.Target.All, 1, "Burn all of your enemies!", FlameThrower);
        public static Skill nuclear_waste = new Skill("Nuclear Waste", 5, 0, 1, 0, Skill.Target.Single, 1, "Infect an enemy with poision", NuclearWaste);
        //>>>>>>>>>>>>>>>>>>>>Skill Delegates<<<<<<<<<<<<<<<<<<<//
        public static void BasicAttack(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster, target, 0, 1, 100);
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
            int damage = Skill.damage(caster, target, 2, 3, 100);
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
                    int damage = Skill.damage(caster, target, 2, 3, 40);
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
            int damage = Skill.damage(caster, target, 2, 3, 40);
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

    }
}
