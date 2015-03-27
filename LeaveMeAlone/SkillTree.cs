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
        public static void Init(Character.Type type)
        {
            ;
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


        public static Dictionary<Character.Type, SkillTree> skilltrees = new Dictionary<Character.Type, SkillTree>();
        public void initBrute()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Brute] = st;
        }
        public void initMastermind()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Mastermind] = st;
        }
        public void initOperative()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Operative] = st;
        }
        public void initRanger()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Ranger] = st;
        }
        public void initMage()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Mage] = st;
        }
        public void initKnight()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Knight] = st;
        }

    }
}
