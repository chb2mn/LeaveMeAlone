using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LeaveMeAlone;
using Microsoft.Xna.Framework;

namespace LeaveMeAlone
{
    public class SkillTree
    {
        public Dictionary<int, List<Skill>> skill_tiers;
        public Dictionary<int, List<Room>> room_tiers;

        public static Texture2D spike_room_image;
        public static Texture2D poison_pit_image;
        
        public static Dictionary<Character.Type, SkillTree> skilltrees = new Dictionary<Character.Type, SkillTree>();
        public Dictionary<Skill, Button> SkillButtons = new Dictionary<Skill,Button>();

        public static Dictionary<Character.Type, Skill> final_skill = new Dictionary<Character.Type, Skill>();


        //>>>>>>>>>>>>>>>>>>>>Skill Declarations<<<<<<<<<<<<//
        public static Skill basic_attack;
        public static Skill defend;

        //Brute
        public static Skill ethereal_fist;
        public static Skill blind_charge;
        public static Skill rub_dirt;
        public static Skill holk_smash;
        public static Skill norris_kick;
        public static Skill bloodlust_strike;
        public static Skill raised_by_wolves;

        //Mastermind
        public static Skill portal_punch;
        public static Skill flamethrower;
        public static Skill nuclear_waste;
        public static Skill abomination_form;
        public static Skill summon_igor;
        public static Skill freeze_ray;
        public static Skill speedy_shoes;

        //Operative
        public static Skill slash;
        public static Skill shuriken;
        public static Skill backstab;
        public static Skill garrote_watch;
        public static Skill silver_alloy_gun;
        public static Skill exploding_pen;
        public static Skill bladed_shoes;
        public static Skill nuclear_warhead;

        //heroes
        public static Skill cure;
        public static Skill panacea;
        public static Skill fire;
        public static Skill magefire;
        public static Skill bash;
        public static Skill poison_dagger;
        public static Skill haste;

        //>>>>>>>>>>>>>>>>Room Declarations<<<<<<<<<<<//
        public static Room spike_trap;
        public static Room poison_pit;


        public SkillTree()
        {
            skill_tiers = new Dictionary<int, List<Skill>>();
            room_tiers = new Dictionary<int, List<Room>>();

        }
        public static void LoadContent(ContentManager content)
        {
            spike_room_image = content.Load<Texture2D>("spikeRoom");
            poison_pit_image = content.Load<Texture2D>("PoisonPit");
            //buttonPic = content.Load<Texture2D>("buttonbase");

            //>>>>>>>>>>>>>>>>>>>>Skill Instances<<<<<<<<<<<<<<<<<<<//
            basic_attack = new Skill("Attack", 0, 0, 1, 0, Skill.Target.Single, 0, "Basic Attack", BasicAttack);
            defend = new Skill("Defend", 0, 0, 1, 1, Skill.Target.Self, 0, "Heal yourself!", Defend);
            
            //>>>>>>>>>>>>>>>>>>>>Boss Skill Instances<<<<<<<<<<<<<<<<<<<<//
            //Brute


            ethereal_fist = new Skill("Slash", 5, 0,         1, 0, Skill.Target.Single, 1, "Does Sp.Atk. Dmg", PortalPunch);
            blind_charge = new Skill("Blind Charge", 5, 0,   1, 0, Skill.Target.Single, 1, "Does a lot of Atk Damage, but stuns you", BlindCharge); // damage a lot, but stun myself
            rub_dirt =       new Skill("Rub Dirt",   4, 100, 3, 1, Skill.Target.Single, 1, "Rub some dirt in it, dealing damaged based on missing health", RubDirt); //damage in proportion to health
            holk_smash = new Skill("Holk Smush", 10, 300,    5, 0, Skill.Target.All, 1, "Burn all of your enemies!", FlameThrower);
            norris_kick = new Skill("Norris Kick", 8, 100,      3, 1, Skill.Target.Single, 1, "Kick an enemy so hard they hit another enemy randomly", NorrisKick); //damage in proportion to health //damage one a lot and another a little
            bloodlust_strike = new Skill("Bloodlust Strike", 10, 300, 7, 2, Skill.Target.Single, 1, "Attack and steal lifeforce for yourself", BloodlustStrike);  //vampiric
            raised_by_wolves = new Skill("Raised by wolves", 25, 500, 10, 4, Skill.Target.Single, 1, "Kill an enemy, damage another, buff self", RaisedByWolves);; //destroy one enemy, damage another, raise your own stats
            final_skill[Character.Type.Brute] = raised_by_wolves;

            //Mastermind
            portal_punch = new Skill("Portal Punch", 1, 0, 1, 0, Skill.Target.Single, 1, "Does Sp.Atk. Dmg", PortalPunch);
            flamethrower = new Skill("Flamethrower", 10, 300, 1, 0, Skill.Target.All, 1, "Burn all of your enemies!", FlameThrower);
            nuclear_waste = new Skill("Nuclear Waste", 5, 100, 3, 0, Skill.Target.Single, 1, "Infect an enemy with poision", NuclearWaste, Status.check_poison);
            abomination_form = new Skill("Abomination Form", 10, 500, 5, 3, Skill.Target.All, 1, "Science Gone Astray! Swap Atk and Sp. Atk", AbominationForm);
            summon_igor = new Skill("Summon Igor", 5, 300, 3, 1, Skill.Target.Single, 1, "Summon your minion to prod away the heroes", SummonIgor);
            freeze_ray = new Skill("Freeze Ray", 15, 2500, 10, 1, Skill.Target.All, 1, "Freeze all enemies", FreezeRay, Status.check_stun);
            speedy_shoes = new Skill("Speedy Shoes", 15, 1500, 7, 3, Skill.Target.Self, 1, "Your shoes go so fast you take 2 turns", SpeedyShoes, Status.check_haste);
            final_skill[Character.Type.Mastermind] = freeze_ray;



            //Operative
            slash = new Skill("Slash", 5, 0, 1, 0, Skill.Target.Single, 1, "Does Sp.Atk. Dmg", PortalPunch);
            shuriken = new Skill("Shuriken", 10, 300, 1, 0, Skill.Target.All, 1, "It bounces off of all enemies!", FlameThrower);

            backstab = new Skill("Backstab", 10, 300, 1, 2, Skill.Target.Self, 1, "Hit an enemy ignoring defense", Backstab); // hit ignore defense
            garrote_watch = new Skill("Garrote Watch", 5, 300, 5, 0, Skill.Target.Single, 1, "Kill an enemy below 15% hp", GarroteWatch); //remove at low health
            silver_alloy_gun = new Skill("Silver Alloy Gun", 15, 500, 7, 2, Skill.Target.Single, 1, "Hit and Stun an enemy", SilverAlloyGun); //hit and stun
            exploding_pen = new Skill("Exploding Pen", 5, 300, 5, 1, Skill.Target.Single, 1, "Give them a present! (explodes next turn)", SummonIgor); ;
            bladed_shoes = new Skill("Bladed Shoes", 15, 1500, 7, 3, Skill.Target.Self, 1, "Your new pointy shoes give you a second attack", SpeedyShoes, Status.check_haste);
            nuclear_warhead = new Skill("Nuclear Warhead", 20, 3000, 10, 3, Skill.Target.All, 1, "Do huge damage to all enemies", NuclearWarhead); // hit all for a lot of damage combo str and spec.
            final_skill[Character.Type.Operative] = nuclear_warhead;

            //>>>>>>>>>>>>>>>>>>>>>Hero Skill Instances<<<<<<<<<<<<<<<<<<<//

            cure = new Skill("cure", 5, 0 ,1, 1, Skill.Target.Single, 1, "Heals and ally or self", Cure);
            fire = new Skill("fire", 5, 0, 1, 1, Skill.Target.Single, 1, "Burn an enemy", Fire);
            magefire = new Skill("magefire", 0, 0, 0, 0, Skill.Target.Single, 0, "Mage basic attack, does Sp_Atk damage", Fire);
            bash = new Skill("bash", 5, 0 ,1, 1, Skill.Target.Single, 1, "Hit an enemy using physical attack", Bash);
            haste = new Skill("haste", 15, 0, 5, 3, Skill.Target.Single, 1, "Speed an ally up so he can hit twice in a row", Haste);
            panacea = new Skill("panacea", 10, 0, 3, 0, Skill.Target.Single, 1, "Cure Self or Ally of all Status effects", Panacea);
            poison_dagger = new Skill("poison_dagger", 5, 0, 1, 1, Skill.Target.Single, 1, "Do physical damage and give poison", PoisonDagger, Status.check_poison);
            //>>>>>>>>>>>>>>>>>>>Room Instances<<<<<<<<<<<<<<<<<<<<<//
            spike_trap = new Room("Spike Trap", 100, 1, 0, "Does damage to hero relative to their defense", SpikeTrap, spike_room_image);
            poison_pit = new Room("Poison Pit", 100, 1, 0, "Has 50% chance of infecting each hero with poison", PoisonPit, poison_pit_image);
        }

        //Updates or creates the buttons and text
        //If this is run multiple times it might ruin the texts in UpgradeMenu
        public void updateTree()
        {
           
            List<int> keys = skill_tiers.Keys.ToList();
            keys.Sort();
            //int boss_level = BattleManager.boss.level;
            //used to separate buttons horizontally by level
            int kindex = 0;
            for (int x = 0; x < keys.Count; x++)
            {
                var key = keys[x];
                Console.WriteLine("Writing stuff for level" + key);
                List<Skill> skilltier = skill_tiers[key];
                int slength = skilltier.Count;
                int sindex = 0;

                Text level = new Text(key.ToString(), new Vector2(UpgradeMenu.baseSkillButtonPos.X - 40, (int)UpgradeMenu.baseSkillButtonPos.Y + 75 * kindex), Text.fonts["6809Chargen-12"], Color.White);
                Console.WriteLine("Created "+level.message+" at " + level.position.Y);
                UpgradeMenu.texts[key.ToString()] = level;
                foreach (Skill skill in skilltier)
                {
                    Console.WriteLine("Creating button for skill "+skill.name);
                    Console.Out.Flush();
                    Button b = new Button(Button.buttonPic, (int)UpgradeMenu.baseSkillButtonPos.X + sindex * 225, (int)UpgradeMenu.baseSkillButtonPos.Y + 75 * kindex, 200, 50);
                    b.UpdateText(skill.name);
                    b.text.font = Text.fonts["6809Chargen-12"];
                    SkillButtons[skill] = b;
                    sindex++;
                }
                kindex++;
            }
        }


        //Instantiates all classes
        public static void Init(Character.Type t)
        {
            switch(t)
            { 
                case Character.Type.Brute:
                    initBrute();
                    break;
                case Character.Type.Mastermind:
                    initMastermind();
                    break;
                case Character.Type.Operative:
                    initOperative();
                    break;
            }
            initRanger();
            initMage();
            initKnight();

        }

        public void addSkill(Skill skill)
        {
            addToDict(skill_tiers, ref skill.level, ref skill);
        }
        public void addRoom(Room room)
        {
            addToDict(room_tiers, ref room.level, ref room);
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
            st.addSkill(ethereal_fist);
            st.addSkill(blind_charge);            
            st.addSkill(rub_dirt);      
            st.addSkill(holk_smash);          
            st.addSkill(norris_kick);
            st.addSkill(bloodlust_strike);
            st.addSkill(raised_by_wolves);

            st.addRoom(spike_trap);
            st.addRoom(poison_pit);
            skilltrees[Character.Type.Brute] = st;
            st.updateTree();

        }
        public static void initMastermind()
        {
            SkillTree st = new SkillTree();
            st.addSkill(portal_punch);
            st.addSkill(flamethrower);

            st.addSkill(nuclear_waste);
            st.addSkill(speedy_shoes);
            st.addSkill(abomination_form);
            st.addSkill(summon_igor);
            st.addSkill(freeze_ray);
           
            st.addRoom(spike_trap);
            st.addRoom(poison_pit);
            skilltrees[Character.Type.Mastermind] = st;
            st.updateTree();

        }
        public static void initOperative()
        {

            SkillTree st = new SkillTree();
            st.addSkill(slash);
            st.addSkill(shuriken);
            st.addSkill(backstab);
            st.addSkill(garrote_watch);
            st.addSkill(silver_alloy_gun);
            st.addSkill(exploding_pen);
            st.addSkill(bladed_shoes);
            st.addSkill(nuclear_warhead);


            st.addRoom(spike_trap);
            st.addRoom(poison_pit);
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
            st.addSkill(portal_punch);
            st.addSkill(flamethrower);
            st.addSkill(nuclear_waste);
            skilltrees[Character.Type.Mage] = st;
        }
        public static void initKnight()
        {
            SkillTree st = new SkillTree();
            //addSkill(level, skill)
            skilltrees[Character.Type.Knight] = st;
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
            caster.energy += 10;
            if (caster.health > caster.max_health)
            {
                caster.health = caster.max_health;
            }
            if (caster.energy > caster.max_energy)
            {
                caster.energy = caster.max_energy;
            }
            
            //If the status already exists, increase its duration
            //Status this_defend = new Status("defend", 2, Status.Effect_Time.Once, Status.Type.Buff, Status.defplus_image, Status.DoNothing, Status.ReduceDefense);
            //We use DoNothing here^ because we raise defense here, and it is lowered in 2 turns

            if (caster.statuses.Contains(Status.check_defend))
            {
                int status_index = caster.statuses.IndexOf(Status.check_defend);
                caster.statuses[status_index].duration_left += 2;
            }
            //Otherwise add it
            else
            {
                caster.statuses.Add(new Status("defend", 2, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.defplus_image, Status.DoNothing, Status.ReduceDefense));
                caster.defense += (5 * 1 + (caster.level / 3));
            }
            //Status this_sdefend = new Status("specdefend", 2, Status.Effect_Time.Once, Status.Type.Buff, Status.defplus_image, Status.DoNothing, Status.ReduceSDefense);
            //If the status already exists, increase its duration
            if (caster.statuses.Contains(Status.check_specdefend))
            {
                int status_index = caster.statuses.IndexOf(Status.check_specdefend);
                caster.statuses[status_index].duration_left += 2;
            }
            //Otherwise add it
            else
            {
                caster.statuses.Add(new Status("specdefend", 2, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.specdefplus_image, Status.DoNothing, Status.ReduceSDefense)); 
                caster.special_defense += (5 * 1 + (caster.level / 3));
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

            //Status this_poison = new Status("poison", 3, Status.Effect_Time.After, Status.Type.Debuff, Status.poison_image, Status.Poison);
            //If the status already exists, increase its duration
            if (target.statuses.Contains(Status.check_poison))
            {
                int status_index = target.statuses.IndexOf(Status.check_poison);
                target.statuses[status_index].duration_left += 3;
            }
            //Otherwise add it
            else
            {
                target.statuses.Add(new Status("poison", 3, 0, Status.Effect_Time.After, Status.Type.Debuff, Status.poison_image, Status.Poison));
            }
        }
        public static void AbominationForm(Character caster, Character target = null)
        {
            //Change Sprite! or Something!
            int temp = caster.attack;
            caster.attack = caster.special_attack;
            caster.special_attack = temp;

            if (caster.statuses.Contains(Status.check_abom))
            {
                caster.statuses.Remove(Status.check_abom);
            }
            //Otherwise add it
            else
            {
                caster.statuses.Add(new Status("abom", 999, 0, Status.Effect_Time.Once, Status.Type.Other, null, Status.DoNothing, Status.rev_Abom));
            }
        }
        public static void SummonIgor(Character caster, Character target = null)
        {
            //No need to check if it's already there based on the nature of the skill
            //There is a hack here where I use an already instantiated Status to get the delegate function for Summoning Igor
            Status igor_target = new Status("Igor", 2, caster.special_attack, Status.Effect_Time.Once, Status.Type.Other, Status.target_status_image, Status.DoNothing, null);
            igor_target.reverse_affect = igor_target.rev_Igor;
            target.statuses.Add(igor_target);
        }
        public static void FreezeRay(Character caster, Character target = null)
        {
            foreach (Character a_target in BattleManager.heroes)
            {
                a_target.statuses.Add(new Status("stun", LeaveMeAlone.random.Next(2,5), 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.stun_image, Status.DoNothing));
            }
        }
        public static void SpeedyShoes(Character caster, Character target = null)
        {
            caster.statuses.Add(new Status("haste", 3*2, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.haste_image, Status.DoNothing));
        }

        public static void BlindCharge(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 200);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            target.damage_text.changeMessage(str_damage);
            
            caster.statuses.Add(new Status("stun", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.stun_image, Status.DoNothing));
        }

        public static void RubDirt(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 75 + (caster.max_health-caster.health));
            target.health -= damage;
            String str_damage = (-damage).ToString();
            target.damage_text.changeMessage(str_damage);
        }
        public static void NorrisKick(Character caster, Character target = null)
        {
            int power = 100;
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, power);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            target.damage_text.changeMessage(str_damage);
            //number of not-killed character
            List<Character> notDead = new List<Character>();
            foreach(Character h in BattleManager.heroes)
            {
                if(h != null && h != target)
                {
                    Console.WriteLine("Adding " + h.charType);
                    notDead.Add(h);
                }
            }
            if (notDead.Count > 0)
            {
                Character newTarget = notDead[LeaveMeAlone.random.Next(notDead.Count)];
                power = 25;
                damage = Skill.damage(caster, newTarget, Skill.Attack.Attack, Skill.Defense.Defense, power);
                newTarget.health -= damage;
                str_damage = (-damage).ToString();
                newTarget.damage_text.changeMessage(str_damage);
            }

        }
        public static void BloodlustStrike(Character caster, Character target = null)
        {
            int power = 100;
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, power);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            target.damage_text.changeMessage(str_damage);

            caster.health += damage;
            str_damage = "+"+(Math.Abs(damage)).ToString();
            caster.damage_text.changeMessage(str_damage);
        }
        public static void RaisedByWolves(Character caster, Character target = null)
        {
            String str_damage = (-target.health).ToString();
            target.damage_text.changeMessage(str_damage);
            target.health = 0;

            List<Character> notDead = new List<Character>();
            foreach (Character h in BattleManager.heroes)
            {
                if (h != null && h != target)
                {
                    Console.WriteLine("Adding " + h.charType);
                    notDead.Add(h);
                }
            }
            if (notDead.Count > 0)
            {
                Character newTarget = notDead[LeaveMeAlone.random.Next(notDead.Count)];
                int power = 40;
                int damage = Skill.damage(caster, newTarget, Skill.Attack.Attack, Skill.Defense.Defense, power);
                newTarget.health -= damage;
                str_damage = (-damage).ToString();
                newTarget.damage_text.changeMessage(str_damage);
            }


            if (caster.statuses.Contains(Status.check_attackplus) == false)
            {
                caster.statuses.Add(new Status("attack_up", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.atkplus_image, Status.RaiseAttack, Status.ReduceAttack));
            }
        }

        public static void GarroteWatch(Character caster, Character target = null)
        {
            if (target.health <= (int) (target.max_health * .15))
            {
                target.health -= target.health; //kill them
            }
            else
            {
                target.health -= (int) (target.max_health * .1);
            }
        }

        public static void SilverAlloyGun(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 100);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            target.damage_text.changeMessage(str_damage);

            target.statuses.Add(new Status("stun", LeaveMeAlone.random.Next(2,5), 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.stun_image, Status.DoNothing));
        }

        public static void Backstab(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster.attack, 1, caster.level, 50);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            target.damage_text.changeMessage(str_damage);
        }

        public static void NuclearWarhead(Character caster, Character target = null)
        {
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    target = BattleManager.heroes[i];
                    if (target == null) { continue; }
                    int damage = Skill.damage(caster.attack + caster.special_attack, target.special_defense, caster.level, 50);
                    target.health -= damage;
                    String str_damage = (-damage).ToString();
                    target.damage_text.changeMessage(str_damage);
                }
                catch (ArgumentOutOfRangeException)
                {

                }
            }      
        }
        //>>>>>>>>>>>>>>>>>Hero Skill Delegates<<<<<<<<<<<<<<<<<//
        public static void Cure(Character caster, Character target = null)
        {
            int heal_pts = caster.special_attack/3;
            target.health += heal_pts;
            if (target.health > target.max_health)
            {
                target.health = target.max_health;
            }
        }
        public static void Fire(Character caster, Character target = null)
        {
            {
                int damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 100);
                target.health -= damage;
                String str_damage = (-damage).ToString();
                target.damage_text.changeMessage(str_damage);
                caster.Learn(damage, Character.Knowledge.Weak_SDef);
            }
        }
        public static void Bash(Character caster, Character target = null)
        {
            {
                int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 150);
                target.health -= damage;
                String str_damage = (-damage).ToString();
                target.damage_text.changeMessage(str_damage);
                caster.Learn(damage, Character.Knowledge.Weak_Def);

            }
        }
        public static void PoisonDagger(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 40);
            target.health -= damage;
            target.damage_text.changeMessage((-damage).ToString());

            //Status this_poison = new Status("poison", 3, Status.Effect_Time.After, Status.Type.Debuff, Status.poison_image, Status.Poison);
            //If the status already exists, increase its duration
            if (target.statuses.Contains(Status.check_poison))
            {
                int status_index = target.statuses.IndexOf(Status.check_poison);
                target.statuses[status_index].duration_left += 2;
            }
            //Otherwise add it
            else
            {
                target.statuses.Add(new Status("poison", 1, 0, Status.Effect_Time.After, Status.Type.Debuff, Status.poison_image, Status.Poison));
            }
        }
        public static void Haste(Character caster, Character target = null) 
        {
            if (target.statuses.Contains(Status.check_haste))
            {
                //Do Nothing
            }
            //Otherwise add it
            else
            {
                target.statuses.Add(new Status("haste", 3, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.haste_image, Status.DoNothing));
            }
        }
        public static void Panacea(Character caster, Character target = null)
        {
            for (int i = target.statuses.Count() - 1; i >= 0; i--)
            {
                if (target.statuses[i].type == Status.Type.Debuff)
                {
                    target.statuses.Remove(target.statuses[i]);
                }
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
                    int damage = (int)(hero.max_health * .1);
                    hero.health -= damage;
                    if (hero.health <= 0)
                    {
                        hero.health = 1;
                    }
                }
            }
        }
        public static void PoisonPit(List<Character> heroes)
        {
            for (int i = 0; i < heroes.Count(); i++)
            {
                Character hero = heroes[i];
                if (hero != null)
                {
                    if (LeaveMeAlone.random.Next(100) < 50)
                    {
                        hero.statuses.Add(new Status("poison", 3, 0, Status.Effect_Time.After, Status.Type.Debuff, Status.poison_image, Status.Poison));
                    }
                }
            }
        }

        
    }
}


