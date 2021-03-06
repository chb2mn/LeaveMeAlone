﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LeaveMeAlone;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace LeaveMeAlone
{
    public class SkillTree
    {
        public Dictionary<int, List<Skill>> skill_tiers;
        public Dictionary<int, List<Room>> room_tiers;

        public static Texture2D spike_room_image;
        public static Texture2D poison_pit_image;
        public static Texture2D poor_booby_traps_image;
        public static Texture2D puzzle_temple_image;
        public static Texture2D papparazzi_image;
        public static Texture2D retirement_image;

        //>>>>>>>>>>>>Brute Specific Rooms<<<<<<<<<<<<//
        public static Texture2D the_gym_image;
        public static Texture2D massive_treadmill_image;

        //>>>>>>>>Mastermind Specific Rooms<<<<<<<<<<<//
        public static Texture2D laser_room_image;
        public static Texture2D mind_erase_chamber_image;

        //>>>>>>>>Operative Specific Rooms<<<<<<<<<<<<<//
        public static Texture2D sea_bass_room_image;
        public static Texture2D interrogation_chamber_image;
        public static Texture2D austin_marlin_garage_image;
        

        public static Dictionary<Character.Type, SkillTree> skilltrees = new Dictionary<Character.Type, SkillTree>();
        public Dictionary<Skill, Button> SkillButtons = new Dictionary<Skill,Button>();

        public static Dictionary<Character.Type, Skill> final_skill = new Dictionary<Character.Type, Skill>();


        public static SoundEffect fireball;
        public static SoundEffectInstance fireball_instance;

        public static SoundEffect cure_sound;
        public static SoundEffect battle_cry;
        public static SoundEffect battle_cry_hit;
        public static SoundEffect bomb_sound;
        public static SoundEffect chem_set;
        public static SoundEffect chuckle;
        public static SoundEffect freeze_ray_sound;
        public static SoundEffect arrow_flying;
        public static SoundEffect portal_punch_sound;
        public static SoundEffect garrote;
        public static SoundEffect small_explosives_sound;
        public static SoundEffect slash_sound;
        public static SoundEffect smoke_bomb_sound;
        public static SoundEffect wolves_sound;
        public static SoundEffect spinning_blade_sound;
        public static SoundEffect gadget1;
        public static SoundEffect gadget2;
        public static SoundEffect grunt_hit;
        public static SoundEffect two_grunts_hit;
        public static SoundEffect gun;

        public static SoundEffect shield_clank;
        public static SoundEffectInstance shield_clank_instance;




        //>>>>>>>>>>>>>>>>>>>>Skill Declarations<<<<<<<<<<<<//
        public static Skill basic_attack;
        public static Skill defend;
        public static Skill enemy_defend;

        //Brute
        public static Skill ethereal_fist;
        public static Skill blind_charge;

        public static Skill intimidate;
        public static Skill beat_chest;
        public static Skill roid_rage;
        public static Skill break_armor;
        public static Skill library;
        public static Skill counter;

        public static Skill rub_dirt;
        public static Skill holk_smash;
        public static Skill norris_kick;
        public static Skill bloodlust_strike;
        public static Skill raised_by_wolves;

        //Mastermind
        public static Skill portal_punch;
        public static Skill flamethrower;
        public static Skill nuclear_waste;

        public static Skill antimaterial_bubble;
        public static Skill unstable_weapon;
        public static Skill recombobulator;
        public static Skill basic_chem_set;
        public static Skill adv_chem_set;
        public static Skill exp_chem_set;

        public static Skill abomination_form;
        public static Skill summon_igor;
        public static Skill freeze_ray;
        public static Skill speedy_shoes;

        //Operative
        public static Skill slash;
        public static Skill shuriken;
        public static Skill backstab;

        public static Skill deadly_weapons;
        public static Skill espionage;
        public static Skill double_cross;
        public static Skill smoke_bomb;
        public static Skill small_explosives;

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
        public static Room poor_booby_traps;
        public static Room puzzle_temple;
        public static Room papparazzi;
        public static Room retirement_lounge;

        //>>>>>>>>>>>>Brute Specific Rooms<<<<<<<<<<<<//
        public static Room the_gym;
        public static Room massive_treadmill;

        //>>>>>>>>Mastermind Specific Rooms<<<<<<<<<<<//
        public static Room laser_room;
        public static Room mind_erase_chamber;

        //>>>>>>>>Operative Specific Rooms<<<<<<<<<<<<<//
        public static Room sea_bass_room;
        public static Room interrogation_chamber;
        public static Room austin_marlin_garage;


        public SkillTree()
        {
            skill_tiers = new Dictionary<int, List<Skill>>();
            room_tiers = new Dictionary<int, List<Room>>();

        }

        public static void PlaySound(SoundEffect s)
        {
            SoundEffectInstance sinstance = s.CreateInstance();
            sinstance.Volume = .9f;
            sinstance.Play();
        }
        public static void LoadContent(ContentManager content)
        {
        //>>>>>>>>>>>>>>>>>>Sound Effect<<<<<<<<<<<<<<<<<<<//
            fireball = content.Load<SoundEffect>("Sounds/fireball");
            fireball_instance = fireball.CreateInstance();
            
            shield_clank = content.Load<SoundEffect>("Sounds/shield_clank");
            shield_clank_instance = shield_clank.CreateInstance();
            cure_sound = content.Load<SoundEffect>("Sounds/cure_sound2");
            battle_cry = content.Load<SoundEffect>("Sounds/battle_cry");
            battle_cry_hit = content.Load<SoundEffect>("Sounds/battle_cry_hit");
            bomb_sound = content.Load<SoundEffect>("Sounds/Bomb");
            chem_set = content.Load<SoundEffect>("Sounds/chem_set");
            chuckle = content.Load<SoundEffect>("Sounds/chuckle");
            freeze_ray_sound = content.Load<SoundEffect>("Sounds/freeze_ray");
            arrow_flying = content.Load<SoundEffect>("Sounds/arrow_flying");
            portal_punch_sound = content.Load<SoundEffect>("Sounds/portal_punch");
            garrote = content.Load<SoundEffect>("Sounds/garrote");
            small_explosives_sound = content.Load<SoundEffect>("Sounds/small_explosives");
            slash_sound = content.Load<SoundEffect>("Sounds/slash");
            smoke_bomb_sound = content.Load<SoundEffect>("Sounds/smoke_bomb");
            wolves_sound = content.Load<SoundEffect>("Sounds/Wolves");
            spinning_blade_sound = content.Load<SoundEffect>("Sounds/spinning_blade");
            gadget1 = content.Load<SoundEffect>("Sounds/gadget1");
            gadget2 = content.Load<SoundEffect>("Sounds/gadget2");
            grunt_hit = content.Load<SoundEffect>("Sounds/grunt_hit");
            two_grunts_hit = content.Load<SoundEffect>("Sounds/2_grunts_hit");
            gun = content.Load<SoundEffect>("Sounds/gunish");

        //>>>>>>>>>>>>>>>>>>Generic Rooms<<<<<<<<<<<<<<<<<<//    
            spike_room_image = content.Load<Texture2D>("Lair/spikeRoom");
            poison_pit_image = content.Load<Texture2D>("Lair/PoisonPit");
            poor_booby_traps_image = content.Load<Texture2D>("Lair/PoorBoobyTraps");
            puzzle_temple_image = content.Load<Texture2D>("Lair/PuzzleTemple");
            papparazzi_image = content.Load<Texture2D>("Lair/Papparazzi");
            retirement_image = content.Load<Texture2D>("RetirementHome");

        //>>>>>>>>>>>>Brute Specific Rooms<<<<<<<<<<<<//
            the_gym_image = content.Load<Texture2D>("Lair/TheGym"); //
            massive_treadmill_image = content.Load<Texture2D>("Lair/MassiveTreadmill"); //

        //>>>>>>>>Mastermind Specific Rooms<<<<<<<<<<<//
            laser_room_image = content.Load<Texture2D>("Lair/LaserRoom");
            mind_erase_chamber_image = content.Load<Texture2D>("Lair/MindErase");


        //>>>>>>>>Operative Specific Rooms<<<<<<<<<<<<<//
            sea_bass_room_image = content.Load<Texture2D>("Lair/SeaBassRoom");
            interrogation_chamber_image = content.Load<Texture2D>("Lair/InterrogationChamber");
            austin_marlin_garage_image = content.Load<Texture2D>("Lair/AustinMarlinGarage"); //
            //buttonPic = content.Load<Texture2D>("buttonbase");

            //>>>>>>>>>>>>>>>>>>>>Skill Instances<<<<<<<<<<<<<<<<<<<//
            basic_attack = new Skill("Attack", 0, 0, 1, 0, Skill.Target.Single, 0, "Basic Attack", BasicAttack);
            defend = new Skill("Defend", 0, 0, 1, 1, Skill.Target.Self, 0, "Heal yourself!", Defend);
            enemy_defend = new Skill("Defend", 0, 0, 1, 1, Skill.Target.Self, 0, "Defend", EnemyDefend);
            
            //>>>>>>>>>>>>>>>>>>>>Boss Skill Instances<<<<<<<<<<<<<<<<<<<<//
            //Brute

            //------------------------Name---------energy,cost,level,cooldown,target,type
            ethereal_fist = new Skill("Ethereal Fist", 5, 100, 1, 0, Skill.Target.Single, 1, "Change your fist into a magical force. Does Sp.Atk. Dmg", PortalPunch);
            blind_charge = new Skill("Blind Charge",   5, 100, 1, 0, Skill.Target.Single, 1, "Heroes can't handle him. Does a lot of Atk Damage, but stuns you", BlindCharge); // damage a lot, but stun myself
            intimidate = new Skill("Inimidating Roar", 5, 250, 1, 0, Skill.Target.All, 1, "Send a shout that penetrates their very soul. Reduce oppponents Atk and Special Atk", Intimidate);
            beat_chest = new Skill("Beat Chest",       5, 500, 2, 0, Skill.Target.Self, 1, "Physical Trainers hate you. Increase Atk and Special Atk with one easy step.", BeatChest);
            rub_dirt =       new Skill("Rub Dirt",     4, 100, 2, 1, Skill.Target.Single, 1, "Rub some dirt in it, dealing damaged based on missing health", RubDirt); //damage in proportion to health
            norris_kick = new Skill("Norris Kick",     8, 100, 3, 1, Skill.Target.Single, 1, "Kick an enemy so hard they slam into another enemy randomly", NorrisKick); //damage in proportion to health //damage one a lot and another a little
            break_armor = new Skill("Eviscerate Armor",     5, 2500, 3, 1, Skill.Target.Single, 1, "Do significant damage and break your opponent's armor", BreakArmor);
            holk_smash = new Skill("Holk Smush",       10,4500, 5, 1, Skill.Target.All, 1, "With your Zeta radiation infusing your cells with extra power, smash all the enemies", HolkSmush);
            library = new Skill("What Library???",     5, 1500, 5, 3, Skill.Target.Self, 1, "Those nerds can't handle you! You're immune to Special Attack!", Library);
            bloodlust_strike = new Skill("Bloodlust Strike", 10, 300, 7, 2, Skill.Target.Single, 1, "Attack and steal the puny hero's lifeforce for yourself", BloodlustStrike);  //vampiric
            roid_rage = new Skill("Roid Rage",         5, 5000, 7, 0, Skill.Target.Self, 1, "Pop a few pills, Cut your max hp (for a battle) and raise your attack by 3 stages", RoidRage);
            raised_by_wolves = new Skill("Raised by wolves", 25, 10000, 10, 4, Skill.Target.Single, 1, "When you were young, the hero's village exiled you out on your lonesome. You learned from a pack of wolves for how to survive.\nYour pack now comes to live and protect their own you until death. THIS is the statement you have been finally looking for. Picking this will trigger the end game.\nEffect: Kill an enemy, damage another, increase attack by 1 stage", RaisedByWolves);           
            
            final_skill[Character.Type.Brute] = raised_by_wolves;

            //Mastermind
            portal_punch = new Skill("Portal Punch",    1,  0, 1, 0, Skill.Target.Single, 1, "One of your first inventions! This will allow you to punch past bulky opponent's defenses. Does Sp.Atk. damae", PortalPunch);
            flamethrower = new Skill("Flamethrower",    10, 300, 1, 1, Skill.Target.All, 1, "What a classic!! Light it up and burn all of your enemies!", FlameThrower);
            nuclear_waste = new Skill("Nuclear Waste",  5,  100, 2, 0, Skill.Target.Single, 1, "You've created a new strand of quick-acting poison! Infect an enemy with poision and do damage over time", NuclearWaste, _inflicts: Status.check_poison);
            abomination_form = new Skill("Abomination Form", 10, 500, 2, 3, Skill.Target.All, 1, "Your biological tamperings have allowed you to turn into a hulking warrior! But all of your inventions are now less effective. Swap Atk and Sp. Atk", AbominationForm);
            summon_igor = new Skill("Summon Igor",      5,  300, 3, 1, Skill.Target.Single, 1, "Summon your faithful minion to prod away the heroes, but he's always slacking behind. Does damage one turn later", SummonIgor);
            freeze_ray = new Skill("Freeze Ray",        15, 10000, 10, 1, Skill.Target.All, 1, "Lost in your drawers from your early years of masterminding, you find the copy of a blueprint that your mother ripped to shreds. \"This is childish nonsense will get you nowhere\" she would scold.\n\nWell we'll show her...This ray freezes all enemies and will finally keep those pesky plumbers-I mean- heroes at bay. Picking this will trigger the end game.", FreezeRay, _inflicts: Status.check_stun);
            speedy_shoes = new Skill("Speedy Shoes",    15, 1500, 7, 3, Skill.Target.Self, 2, "Gotta get speedy! Your new shoes go so fast you take 2 turns in the time it takes the heroes to go once!", SpeedyShoes, _inflicts: Status.check_haste);
            final_skill[Character.Type.Mastermind] = freeze_ray;

            antimaterial_bubble = new Skill("Anti-Material Bubble", 15, 7500, 7, 3, Skill.Target.Self, 2, "The bullies at school could never get past this bubble. Although it did make them call you bubble boy until college...Make yourself immune to all physical attacks", AntimaterialBubble);
            unstable_weapon = new Skill("Unstable Weapon", 15, 5000, 5, 3, Skill.Target.Single, 1, "Testing is for losers. You made this and you can test it later! Does damage to enemy....probably", UnstableWeapon);
            recombobulator = new Skill("Recombobulator", 20, 5000, 5, 5, Skill.Target.Single, 1, "Wait...what was I saying? Who are you?", Recombobulator);
            basic_chem_set = new Skill("Basic Chemistry Set", 5, 300, 1, 1, Skill.Target.Self, 2, "For my 3rd Birthday: Increase a random Physical and Special stat by 1 stage", BasicChem);
            adv_chem_set = new Skill("Adv. Chemistry Set", 7, 1500, 3, 1, Skill.Target.Self, 2, "40 weeks of allowance for this! Increase 3 random stats by 1 stage", AdvChem);
            exp_chem_set = new Skill("Exp. Chemistry Set", 15, 7500, 7, 2, Skill.Target.Self, 2, "My Bar Mitzvah Present: Increase a random Physical or Special stat by 3 stages. L'Chaim!", ExpChem);

            //Operative
            slash = new Skill("Slash", 5, 0, 1, 0, Skill.Target.Single, 1, "Pull out your switch blade. They'll never see you coming. Does Sp.Atk. Dmg", PortalPunch);
            shuriken = new Skill("Bladed Tophat", 10, 300, 1, 1, Skill.Target.All, 1, "This is odd... hmm... tossing this will curve around and hit all enemy", FlameThrower);
            deadly_weapons = new Skill("Deadly Weapons", 5, 300, 1, 1, Skill.Target.Single, 0, "You have been trained in the finesse of hand-to-hand combat. 50% crit rate", DeadlyWeapons);

            backstab = new Skill("Backstab", 10, 800, 2, 2, Skill.Target.Single, 0, "Betrayal. Such an easy concept. stab an enemy ignoring defense", Backstab); // hit ignore defense
            double_cross = new Skill("Double Cross", 10, 3000, 2, 1, Skill.Target.Single, 0, "You've created such a plot twist, that one hero doesn't even know who is friend and who is foe", DoubleCross);

            silver_alloy_gun = new Skill("Silver Alloy Gun", 15, 1000, 3, 3, Skill.Target.Single, 0, "The biggest things come in small packages. Hit and Stun an enemy", SilverAlloyGun); //hit and stun
            espionage = new Skill("Espionage", 10, 3000, 3, 3, Skill.Target.All, 0, "You've establish such an elaborate rouse that you entice these heroes to take your special serum. %chance to poison all.", Espionage);
            exploding_pen = new Skill("Exploding Pen", 5, 1500, 3, 2, Skill.Target.Single, 1, "Give them a present, complimentary with body bags. (explodes next turn)", SummonIgor); ;

            smoke_bomb = new Skill("Smoke Bomb", 20, 6000, 5, 2, Skill.Target.All, 0, "Encasing the entire field in smoke, nobody know who to hit anymore. They'll swing and swing. All enemies have a chance to be inflicted with confusion", SmokeBomb);
            garrote_watch = new Skill("Garrote Watch", 5, 3000, 5, 0, Skill.Target.Single, 1, "The enemy is weak and dazed. Get behind them and finish the job. Kill an enemy below 25% hp", GarroteWatch); //remove at low health

            small_explosives = new Skill("Small Explosives", 10, 4000, 7, 2, Skill.Target.All, 0, "You've manages to line their half of the battlefield with explosives doing random amounts of damage to all enemies", SmallExplosives);
            bladed_shoes = new Skill("Bladed Shoes", 15, 7000, 7, 4, Skill.Target.Self, 2, "You like to fight dirty, but you always get stuck in airport security lines. Your new pointy shoes give you a second attack", SpeedyShoes, _inflicts: Status.check_haste);
            nuclear_warhead = new Skill("Nuclear Launch", 30, 10000, 10, 3, Skill.Target.All, 1, "You contriving policies and nefarious schemes have led to this moment. By bribing several officials and executing many higher-ups, the operative has secured nuclear launch codes.\nHe plans on using them to destroy his enemies and as a loaded gun towards any who dare interfere with him.\nThis was his last mission, and he can now retire peacefully.\nHow does he survive? Lead refrigerator. Do huge damage to all enemies", NuclearWarhead); // hit all for a lot of damage combo str and spec.


            final_skill[Character.Type.Operative] = nuclear_warhead;

            //>>>>>>>>>>>>>>>>>>>>>Hero Skill Instances<<<<<<<<<<<<<<<<<<<//

            cure = new Skill("cure", 20, 0 ,1, 1, Skill.Target.Single, 2, "Heals and ally or self", Cure);
            fire = new Skill("fire", 5, 0, 1, 1, Skill.Target.Single, 1, "Burn an enemy", Fire);
            magefire = new Skill("magefire", 0, 0, 0, 0, Skill.Target.Single, 1, "Mage basic attack, does Sp_Atk damage", Fire);
            bash = new Skill("bash", 5, 0 ,1, 1, Skill.Target.Single, 0, "Hit an enemy using physical attack", Bash);
            haste = new Skill("haste", 15, 0, 5, 3, Skill.Target.Single, 2, "Speed an ally up so he can hit twice in a row", Haste);
            panacea = new Skill("panacea", 10, 0, 3, 0, Skill.Target.Single, 2, "Cure Self or Ally of all Status effects", Panacea);
            poison_dagger = new Skill("poison_dagger", 5, 0, 1, 1, Skill.Target.Single, 0, "Do physical damage and give poison", PoisonDagger, _inflicts: Status.check_poison);

            //>>>>>>>>>>>>>>>>>>>Room Instances<<<<<<<<<<<<<<<<<<<<<//
            spike_trap = new Room("Spike Trap", 200, 1, 0, "Does damage to hero relative to their defense", SpikeTrap, spike_room_image);
            poison_pit = new Room("Poison Pit", 300, 1, 0, "Has 50% chance of infecting each hero with poison", PoisonPit, poison_pit_image);
            poor_booby_traps = new Room("Poor Booby Traps", 500, 2, 0, "They were 80% off! Temporarily lowers all heroes' stats", PoorTraps, poor_booby_traps_image);
            puzzle_temple = new Room("Puzzle Temple", 5000, 2, 0, "has a small chance to remove an enemy from a party", PuzzleRoom, puzzle_temple_image);
            papparazzi = new Room("Papparazzi Tunnel", 2500, 2, 0, "has a chance to inflict with confusion (attacks random character)", Papparazzi, papparazzi_image);
            retirement_lounge = new Room("Retirement Porch", 1250, 2, 0, "Gives the heroes buffs, but increases their rewards", RetirementPorch, retirement_image);

            //>>>>>>>>>>>>Brute Specific Rooms<<<<<<<<<<<<//
            the_gym = new Room("The Gym", 500, 2, 0, "Lowers attack and special attack by 2 stages", TheGym, the_gym_image);
            massive_treadmill =  new Room("Massive Treadmill", 1500, 4, 0, "Reduces energy to 0, and has a chance to inflict stun", MassiveTreadmill, massive_treadmill_image);

            //>>>>>>>>Mastermind Specific Rooms<<<<<<<<<<<//
            laser_room = new Room("Laser Room", 500, 2, 0, "All heroes except Rangers take damage!", LaserRoom, laser_room_image);
            mind_erase_chamber = new Room("Mind Erase Chamber", 3000, 7, 0, "Stuns all heroes except Mages", MindEraseChamber, mind_erase_chamber_image);


            //>>>>>>>>Operative Specific Rooms<<<<<<<<<<<<<//
            sea_bass_room = new Room("Tank of Ill-Tempered Sea Bass", 2000, 4, 0, "inflicts twice the poison on all heroes", SeaBassRoom, sea_bass_room_image);
            interrogation_chamber = new Room("Interrogation Chamber", 3000, 5, 0, "Stuns all heroes except Knights", InterrogationChamber, interrogation_chamber_image);
            austin_marlin_garage = new Room("Austin Marlin Garage", 10000, 8, 0, "Has a chance to inflict 40% damage on each hero", AustinMarlinGarage, austin_marlin_garage_image);
        }

        //Updates or creates the buttons and text
        //If this is run multiple times it might ruin the texts in UpgradeMenu
        public void updateTree()
        {
           
            List<int> keys = skill_tiers.Keys.ToList();
            keys.Sort();
            Text leveltxt = new Text("level", new Vector2(UpgradeMenu.baseSkillButtonPos.X - 45, (int)UpgradeMenu.baseSkillButtonPos.Y-15), Text.fonts["6809Chargen-12"], Color.White);
            if (!UpgradeMenu.texts.ContainsKey("leveltxt"))
            {
                UpgradeMenu.texts.Add("leveltxt", leveltxt);
            }
            else
            {
                UpgradeMenu.texts["leveltxt"] = leveltxt;
            }
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

                Text level = new Text(key.ToString(), new Vector2(UpgradeMenu.baseSkillButtonPos.X - 30, (int)UpgradeMenu.baseSkillButtonPos.Y + 10 + 65 * kindex), Text.fonts["6809Chargen-12"], Color.White);
                Console.WriteLine("Created "+level.message+" at " + level.position.Y);
                if (!UpgradeMenu.texts.ContainsKey(key.ToString()))
                {
                    UpgradeMenu.texts.Add(key.ToString(), level);
                }
                else
                {
                    UpgradeMenu.texts[key.ToString()] = level;
                }
                foreach (Skill skill in skilltier)
                {
                    Console.WriteLine("Creating button for skill "+skill.name);
                    Console.Out.Flush();
                    Button b = new Button(Button.buttonPic, (int)UpgradeMenu.baseSkillButtonPos.X + sindex * 225, (int)UpgradeMenu.baseSkillButtonPos.Y + 65 * kindex, 200, 50);
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
            st.addSkill(intimidate);
            st.addSkill(beat_chest);
            st.addSkill(roid_rage);
            st.addSkill(break_armor);
            st.addSkill(library);

            st.addRoom(puzzle_temple);
            st.addRoom(retirement_lounge);
            st.addRoom(papparazzi);
            st.addRoom(spike_trap);
            st.addRoom(poison_pit);
            st.addRoom(the_gym);
            st.addRoom(massive_treadmill);
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

            st.addSkill(recombobulator);
            st.addSkill(basic_chem_set);
            st.addSkill(antimaterial_bubble);
            st.addSkill(unstable_weapon);
            st.addSkill(adv_chem_set);
            st.addSkill(exp_chem_set);

            st.addRoom(puzzle_temple);
            st.addRoom(retirement_lounge);
            st.addRoom(papparazzi);
            st.addRoom(spike_trap);
            st.addRoom(poison_pit);
            st.addRoom(laser_room);
            st.addRoom(mind_erase_chamber);
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
            st.addSkill(deadly_weapons);
            st.addSkill(espionage);
            st.addSkill(double_cross);
            st.addSkill(smoke_bomb);
            st.addSkill(small_explosives);


            st.addRoom(puzzle_temple);
            st.addRoom(retirement_lounge);
            st.addRoom(papparazzi);
            st.addRoom(spike_trap);
            st.addRoom(poison_pit);
            st.addRoom(sea_bass_room);
            st.addRoom(interrogation_chamber);
            st.addRoom(austin_marlin_garage);
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
            int crit = LeaveMeAlone.random.Next(100);
            if (crit < caster.crit_chance && BattleManager.enemy_turn == -1)
            {
                int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 200);
                target.health -= damage;
                String str_damage = (-damage).ToString();
                //target.damage_text.changeMessage(str_damage);
                target.PushDamage("Critical: "+ str_damage);

            }
            else
            {
                int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 100); target.health -= damage;
                String str_damage = (-damage).ToString();
                //target.damage_text.changeMessage(str_damage);
                target.PushDamage(str_damage);

            }
            PlaySound(slash_sound);
            
        }
        public static void Defend(Character caster, Character target = null)
        {
            PlaySound(shield_clank);
            int damage = (int)(((double)caster.max_health) * .2);
            caster.health += damage;
            caster.PushDamage(damage.ToString());
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
        public static void EnemyDefend(Character caster, Character target = null)
        {
            PlaySound(shield_clank);
            caster.health += (int)(((double)caster.max_health) * .05);
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


        //>>>>>>>>>>>>>>>>>>>>>>>>>>Mastermind Delegates<<<<<<<<<<<<<<<<<<<<//
        public static void PortalPunch(Character caster, Character target = null)
        {
            int damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 100);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            //target.damage_text.changeMessage(str_damage);
            target.PushDamage(str_damage);
            PlaySound(portal_punch_sound);
        }
        public static void FlameThrower(Character caster, Character target = null)
        {
            if (BattleManager.boss.charType == Character.Type.Mastermind)
            {
                PlaySound(fireball);
            }
            else
            {
                PlaySound(spinning_blade_sound);
            }
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    target = BattleManager.heroes[i];
                    if (target == null) { continue; }
                    int damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 80);
                    target.health -= damage;
                    String str_damage = (-damage).ToString();
                    //target.damage_text.changeMessage(str_damage);
                    target.PushDamage(str_damage);
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
            target.PushDamage((-damage).ToString());

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
            PlaySound(chem_set);
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
            PlaySound(battle_cry);
        }
        public static void SummonIgor(Character caster, Character target = null)
        {
            //No need to check if it's already there based on the nature of the skill
            //There is a hack here where I use an already instantiated Status to get the delegate function for Summoning Igor
            PlaySound(chuckle);
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
            PlaySound(freeze_ray_sound);
        }
        public static void SpeedyShoes(Character caster, Character target = null)
        {
            caster.statuses.Add(new Status("haste", 3*2, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.haste_image, Status.DoNothing));
            PlaySound(gadget1);
        }

        public static void AntimaterialBubble(Character caster, Character target = null)
        {
            caster.statuses.Add(new Status("immune_atk", 3, 0, Status.Effect_Time.Before, Status.Type.Buff, Status.immune_atk_image, Status.DoNothing, Status.DoNothing));
            PlaySound(gadget2);
        }

        public static void UnstableWeapon(Character caster, Character target = null)
        {
            int rand;
            rand = LeaveMeAlone.random.Next(4);
            //1-in-4 chance of healing the enemy
            int damage;
            if (rand == 0)
            {
                damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 50);
                target.health += damage;
                target.PushDamage("Healed: " + (damage).ToString());
            }
            else
            {
                damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 175);
                target.health -= damage;
                target.PushDamage((-damage).ToString());
            }
            PlaySound(gadget2);
        }

        public static void Recombobulator(Character caster, Character target = null)
        {
            target.statuses.Add(new Status("confuse", 2, 0, Status.Effect_Time.Before, Status.Type.Debuff, Status.confused_image, Status.DoNothing, Status.DoNothing));
            PlaySound(gadget1);
            /*
             * Doesn't work
             * 
            //Recreates a new hero of similar level
            List<int> target_stats = new List<int>();
            int type; //The type of hero we will produce
            type = LeaveMeAlone.random.Next(3);
            int level; //The relative change of level we will give the hero
            level = target.level + LeaveMeAlone.random.Next(4) - 3; //25% of gain level 25% chance of nothing 50% chance of losing levels
            Vector2 target_vector = target.sPosition;
            Character new_hero = PartyManager.CreateHero(type, level, target_vector);
            Console.WriteLine("Hero of type {0}", new_hero.charType);
            target = new_hero;
            */
        }

        public static void BasicChem(Character caster, Character target = null)
        {
            PlaySound(chem_set);
            
            //Gain 1 stage in something physical and 1 stage in something special
            
            String message = "";
            int rand;
            
            //Add a physical stage
            rand = LeaveMeAlone.random.Next(2);
            if (rand == 0)
            {
                message += "Atk+ ";
                caster.attack += Status.StageValue(caster.attack, caster.level);
                caster.statuses.Add(new Status("atk+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.atkplus_image, Status.DoNothing, Status.ReduceAttack));
            }
            else
            {
                message += "Def+ ";
                caster.defense += Status.StageValue(caster.defense, caster.level);
                caster.statuses.Add(new Status("def+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.defplus_image, Status.DoNothing, Status.ReduceDefense));
            }
            //Add a special stage
            rand = LeaveMeAlone.random.Next(2);
            if (rand == 0)
            {
                message += "SpecAtk+";
                caster.special_attack += Status.StageValue(caster.special_attack, caster.level);

                caster.statuses.Add(new Status("spec+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.specplus_image, Status.DoNothing, Status.ReduceSAttack));
            }
            else
            {
                message += "SpecDef+";
                caster.special_defense += Status.StageValue(caster.special_defense, caster.level);
                caster.statuses.Add(new Status("specdef+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.specdefplus_image, Status.DoNothing, Status.ReduceSDefense));
            }

            caster.PushDamage(message);
        }

        public static void AdvChem(Character caster, Character target = null)
        {
            PlaySound(chem_set);
            //Gain 2 stage in something physical and 2 stage in something special
            String message = "";
            int rand;
            for (int i = 0; i < 3; i++)
            {
                rand = LeaveMeAlone.random.Next(4);
                if (rand == 0)
                {
                    message += "Atk+ ";
                    caster.attack += Status.StageValue(caster.attack, caster.level);
                    caster.statuses.Add(new Status("atk+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.atkplus_image, Status.DoNothing, Status.ReduceAttack));
                }
                else if (rand == 1)
                {
                    message += "Def+ ";
                    caster.defense += Status.StageValue(caster.defense, caster.level);
                    caster.statuses.Add(new Status("def+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.defplus_image, Status.DoNothing, Status.ReduceDefense));
                }
                else if (rand == 2)
                {
                    message += "Spec+ ";
                    caster.special_attack += Status.StageValue(caster.special_attack, caster.level);
                    caster.statuses.Add(new Status("spec+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.specplus_image, Status.DoNothing, Status.ReduceDefense));
                }
                else if (rand == 3)
                {
                    message += "SpecDef+ ";
                    caster.special_defense += Status.StageValue(caster.special_defense, caster.level);
                    caster.statuses.Add(new Status("specdef+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.specdefplus_image, Status.DoNothing, Status.ReduceDefense));
                }
            }
            caster.PushDamage(message);
        }

        public static void ExpChem(Character caster, Character target = null)
        {
            PlaySound(chem_set);
            //Gain 3 stage in something physical and 3 stage in something special
            String message = "";
            int rand;
            rand = LeaveMeAlone.random.Next(4);
            if (rand == 0)
            {
                message += "Atk+ ";
                caster.attack += Status.StageValue(caster.attack, caster.level);
                caster.statuses.Add(new Status("atk+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.atkplus_image, Status.DoNothing, Status.ReduceAttack));
                caster.attack += Status.StageValue(caster.attack, caster.level);
                caster.statuses.Add(new Status("atk+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceAttack));
                caster.attack += Status.StageValue(caster.attack, caster.level);
                caster.statuses.Add(new Status("atk+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceAttack));
            }
            else if (rand == 1)
            {
                message += "Def+ ";
                caster.defense += Status.StageValue(caster.defense, caster.level);
                caster.statuses.Add(new Status("def+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.defplus_image, Status.DoNothing, Status.ReduceDefense));
                caster.defense += Status.StageValue(caster.defense, caster.level);
                caster.statuses.Add(new Status("def+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceDefense));
                caster.defense += Status.StageValue(caster.defense, caster.level);
                caster.statuses.Add(new Status("def+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceDefense));
            }
            else if (rand == 2)
            {
                message += "Spec+ ";
                caster.special_attack += Status.StageValue(caster.special_attack, caster.level);
                caster.statuses.Add(new Status("spec+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.specplus_image, Status.DoNothing, Status.ReduceSAttack));
                caster.special_attack += Status.StageValue(caster.special_attack, caster.level);
                caster.statuses.Add(new Status("spec+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceSAttack));
                caster.special_attack += Status.StageValue(caster.special_attack, caster.level);
                caster.statuses.Add(new Status("spec+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceSAttack));
            }
            else if (rand == 3)
            {
                message += "SpecDef+ ";
                caster.special_defense += Status.StageValue(caster.special_defense, caster.level);
                caster.statuses.Add(new Status("specdef+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.specdefplus_image, Status.DoNothing, Status.ReduceSDefense));
                caster.special_defense += Status.StageValue(caster.special_defense, caster.level);
                caster.statuses.Add(new Status("specdef+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceSDefense));
                caster.special_defense += Status.StageValue(caster.special_defense, caster.level);
                caster.statuses.Add(new Status("specdef+", 100, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceSDefense));
            }
            caster.PushDamage(message);

        }
        /*
        public static Skill antimaterial_bubble;
        public static Skill unstable_weapon;
        public static Skill recombobulator;
        public static Skill basic_chem_set;
        public static Skill adv_chem_set;
        public static Skill exp_chem_set;
        */

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>Brute Delegates<<<<<<<<<<<<<<<<<<<<<<<<//
        public static void BlindCharge(Character caster, Character target = null)
        {
            PlaySound(battle_cry_hit);
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 200);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            //target.damage_text.changeMessage(str_damage);
            target.PushDamage(str_damage);
            
            caster.statuses.Add(new Status("stun", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.stun_image, Status.DoNothing));
        }

        public static void RubDirt(Character caster, Character target = null)
        {
            PlaySound(grunt_hit);
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 75 + (caster.max_health-caster.health));
            target.health -= damage;
            String str_damage = (-damage).ToString();
            //target.damage_text.changeMessage(str_damage);
            target.PushDamage(str_damage);
        }
        public static void NorrisKick(Character caster, Character target = null)
        {
            PlaySound(two_grunts_hit);
            int power = 100;
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, power);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            //target.damage_text.changeMessage(str_damage);
            target.PushDamage(str_damage);
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
                //newTarget.damage_text.changeMessage(str_damage);
                newTarget.PushDamage(str_damage);
            }

        }
        public static void BloodlustStrike(Character caster, Character target = null)
        {
            PlaySound(battle_cry_hit);
            int power = 100;
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, power);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            //target.damage_text.changeMessage(str_damage);
            target.PushDamage(str_damage);

            caster.health += damage;
            str_damage = "+"+(Math.Abs(damage)).ToString();
            //caster.damage_text.changeMessage(str_damage);
            caster.PushDamage(str_damage);
        }
        public static void RaisedByWolves(Character caster, Character target = null)
        {
            PlaySound(wolves_sound);
            String str_damage = (-target.health).ToString();
            //target.damage_text.changeMessage(str_damage);
            target.PushDamage(str_damage);
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
                int power = 100;
                int damage = Skill.damage(caster, newTarget, Skill.Attack.Attack, Skill.Defense.Defense, power);
                newTarget.health -= damage;
                str_damage = (-damage).ToString();
                //newTarget.damage_text.changeMessage(str_damage);
                target.PushDamage(str_damage);
            }


            if (caster.statuses.Contains(Status.check_attackplus) == false)
            {
                caster.statuses.Add(new Status("atk+", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.atkplus_image, Status.DoNothing, Status.ReduceAttack));
                caster.attack += Status.StageValue(caster.attack, caster.level);
            }
        }

        public static void HolkSmush(Character caster, Character target = null)
        {
            PlaySound(battle_cry_hit);
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    target = BattleManager.heroes[i];
                    if (target == null) { continue; }
                    int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 70);
                    target.health -= damage;
                    String str_damage = (-damage).ToString();
                    //target.damage_text.changeMessage(str_damage);
                    target.PushDamage(str_damage);
                }
                catch (ArgumentOutOfRangeException)
                {

                }
            }       
        }
        public static void Intimidate(Character caster, Character target = null) 
        {
            PlaySound(battle_cry);
            for (int i = 0; i < BattleManager.heroes.Count(); i++)
            {
                target = BattleManager.heroes[i];
                if (target == null) { continue; }
                int damage = Status.StageValue(target.attack, target.level);
                target.attack -= damage;
                target.statuses.Add(new Status("atk-", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.atkminus_image, Status.DoNothing, Status.RaiseAttack));


                damage = Status.StageValue(target.special_attack, target.level);
                target.special_attack -= damage;
                target.statuses.Add(new Status("spec-", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.specminus_image, Status.DoNothing, Status.RaiseSAttack));

            }  
        }

        public static void BeatChest(Character caster, Character target = null)
        {
            PlaySound(battle_cry);    
            target = caster;
            int damage = Status.StageValue(target.attack, target.level);
            target.attack += damage;
            caster.statuses.Add(new Status("atk+", 10, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.atkplus_image, Status.DoNothing, Status.ReduceAttack));


            damage = Status.StageValue(target.special_attack, target.level);
            target.special_attack += damage;
            caster.statuses.Add(new Status("spec+", 10, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.specplus_image, Status.DoNothing, Status.ReduceSAttack));
        }

        public static void RoidRage(Character caster, Character target = null)
        {
            PlaySound(battle_cry);
            target = caster;
            int damage = Status.StageValue(target.attack, target.level);
            caster.max_health -= Status.StageValue(target.health, target.level*6);
            caster.statuses.Add(new Status("hp-", 10, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.RaiseHealth));
            if (caster.max_health < caster.health)
            {
                caster.health = caster.max_health;
            }

            damage = Status.StageValue(target.attack, target.level)*3;
            target.attack += damage;
            caster.statuses.Add(new Status("atk+", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.atkplus_image, Status.DoNothing, Status.ReduceAttack));
            caster.statuses.Add(new Status("atk+", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceAttack));
            caster.statuses.Add(new Status("atk+", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, null, Status.DoNothing, Status.ReduceAttack));
        }

        public static void BreakArmor(Character caster, Character target = null)
        {
            PlaySound(battle_cry);
            PlaySound(slash_sound);
            //recalculate the stage every time
            int damage = Status.StageValue(target.defense, target.level);
            target.defense -= damage;
            damage = Status.StageValue(target.defense, target.level);
            target.defense -= damage;
            damage = Status.StageValue(target.defense, target.level);
            target.defense -= damage;
            target.statuses.Add(new Status("def-", 10, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.defminus_image, Status.DoNothing, Status.RaiseDefense));

            damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 120);
            Console.WriteLine(target.defense);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            //target.damage_text.changeMessage(str_damage);
            target.PushDamage(str_damage);
        }

        public static void Library(Character caster, Character target = null)
        {
            PlaySound(chuckle);
            caster.statuses.Add(new Status("immune_spec", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.immune_spec_image, Status.DoNothing, Status.DoNothing));
        }


        
        
        

        //>>>>>>>>>>>>>>>>>>>>>>>>Operative Delegates<<<<<<<<<<<<<<<<<<<<<<<<//
        public static void GarroteWatch(Character caster, Character target = null)
        {
            PlaySound(garrote);
            if (target.health <= (int) (target.max_health * .25))
            {
                target.health -= target.health; //kill them
            }
            else
            {
                target.health -= (int) (target.max_health * .15);
            }
        }

        public static void SilverAlloyGun(Character caster, Character target = null)
        {
            PlaySound(gun);
            int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 100);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            //target.damage_text.changeMessage(str_damage);
            target.PushDamage(str_damage);

            target.statuses.Add(new Status("stun", LeaveMeAlone.random.Next(2,5), 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.stun_image, Status.DoNothing));
        }

        public static void Backstab(Character caster, Character target = null)
        {
            PlaySound(grunt_hit);
            int damage = Skill.damage(caster.attack/3, 1, caster.level, 50);
            target.health -= damage;
            String str_damage = (-damage).ToString();
            //target.damage_text.changeMessage(str_damage);
            target.PushDamage(str_damage);
        }

        public static void NuclearWarhead(Character caster, Character target = null)
        {
            PlaySound(bomb_sound);
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    target = BattleManager.heroes[i];
                    if (target == null) { continue; }
                    int damage = Skill.damage(caster.attack + caster.special_attack, target.special_defense, caster.level, 250);
                    target.health -= damage;
                    String str_damage = (-damage).ToString();
                    //target.damage_text.changeMessage(str_damage);
                    target.PushDamage(str_damage);

                }
                catch (ArgumentOutOfRangeException)
                {

                }
            }      
        }
        public static void DeadlyWeapons(Character caster, Character target = null)
        {
            PlaySound(slash_sound);
            int rand;
            rand = LeaveMeAlone.random.Next(100);
            int damage;
            String message = "";
            if(rand > 50)
            {
                damage = Skill.damage(caster.attack, target.defense, caster.level, 200);
                message += "Critical: ";
            }
            else
            {
                damage = Skill.damage(caster.attack, target.defense, caster.level, 100);
            }
            target.health -= damage;
            message += (-damage).ToString();
            target.PushDamage(message);
        }
        public static void Espionage(Character caster, Character target = null)
        {
            PlaySound(chuckle);
            foreach (Character hero in BattleManager.heroes)
            {
                if (hero != null)
                {
                    int rand;
                    rand = LeaveMeAlone.random.Next(100);
                    if (rand < 65)
                    {
                        hero.statuses.Add(new Status("poison", 2, 0, Status.Effect_Time.After, Status.Type.Debuff, Status.poison_image, Status.Poison, Status.DoNothing));
                    }
                }
            }
        }
        public static void DoubleCross(Character caster, Character target = null)
        {
            PlaySound(chuckle);
            target.statuses.Add(new Status("confuse", 4, 0, Status.Effect_Time.Before, Status.Type.Debuff, Status.confused_image, Status.DoNothing, Status.DoNothing));
        }
        public static void SmokeBomb(Character caster, Character target = null)
        {
            PlaySound(smoke_bomb_sound);
            int rand;
            foreach (Character hero in BattleManager.heroes)
            {
                if (hero != null)
                {
                    rand = LeaveMeAlone.random.Next(100);
                    if (rand < 60)
                    {
                        hero.statuses.Add(new Status("confuse", 1, 0, Status.Effect_Time.Before, Status.Type.Debuff, Status.confused_image, Status.DoNothing, Status.DoNothing));
                    }
                }
            }
        }
        public static void SmallExplosives(Character caster, Character target = null)
        {
            PlaySound(small_explosives_sound);
            foreach (Character hero in BattleManager.heroes)
            {
                if (hero == null)
                {
                    continue;
                }
                int rand = LeaveMeAlone.random.Next(100);
                int damage;
                if (rand < 25)
                {
                    damage = Skill.damage(caster.attack, hero.defense, caster.level, 25);

                }
                else if (rand < 75)
                {
                    damage = Skill.damage(caster.attack, hero.defense, caster.level, 75);
                }
                else
                {
                    damage = Skill.damage(caster.attack, hero.defense, caster.level, 125);
                }
                hero.health -= damage;
                String str_damage = (-damage).ToString();
                //target.damage_text.changeMessage(str_damage);
                hero.PushDamage(str_damage);
            }  
        }
        
        //>>>>>>>>>>>>>>>>>Hero Skill Delegates<<<<<<<<<<<<<<<<<//
        public static void Cure(Character caster, Character target = null)
        {
            PlaySound(cure_sound);
            int heal_pts = caster.special_attack/4;
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
                //target.damage_text.changeMessage(str_damage);
                target.PushDamage(str_damage);
                caster.Learn(damage, Character.Knowledge.Weak_SDef);
                PlaySound(fireball);
            }
        }
        public static void Bash(Character caster, Character target = null)
        {
            PlaySound(grunt_hit);
            {
                int damage = Skill.damage(caster, target, Skill.Attack.Attack, Skill.Defense.Defense, 150);
                target.health -= damage;
                String str_damage = (-damage).ToString();
                //target.damage_text.changeMessage(str_damage);
                target.PushDamage(str_damage);

                caster.Learn(damage, Character.Knowledge.Weak_Def);

            }
        }
        public static void PoisonDagger(Character caster, Character target = null)
        {
            PlaySound(slash_sound);
            int damage = Skill.damage(caster, target, Skill.Attack.SpecialAttack, Skill.Defense.SpecialDefense, 40);
            target.health -= damage;
            //target.damage_text.changeMessage((-damage).ToString());
            target.PushDamage((-damage).ToString());


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
            PlaySound(cure_sound);
            for (int i = target.statuses.Count() - 1; i >= 0; i--)
            {
                if (target.statuses[i].type == Status.Type.Debuff)
                {
                    target.statuses.RemoveAt(i);
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
                    int damage = (int)(hero.max_health * .2);
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
        
        public static void PoorTraps(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                //reduce everything
                hero.attack -= Status.StageValue(hero.attack, hero.level);
                hero.statuses.Add(new Status("atk-", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.atkminus_image, Status.DoNothing, Status.RaiseAttack));
                hero.defense -= Status.StageValue(hero.defense, hero.level);
                hero.statuses.Add(new Status("spec-", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.specminus_image, Status.DoNothing, Status.RaiseDefense));
                hero.special_attack -= Status.StageValue(hero.special_attack, hero.level);
                hero.statuses.Add(new Status("atk-", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.atkminus_image, Status.DoNothing, Status.RaiseSAttack));
                hero.special_defense -= Status.StageValue(hero.special_defense, hero.level);
                hero.statuses.Add(new Status("spec-", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.specminus_image, Status.DoNothing, Status.RaiseSDefense));
            }
        }

        public static void PuzzleRoom(List<Character> heroes)
        {
            if (heroes.Count() > 1)
            {
                int random = LeaveMeAlone.random.Next(100);
                if (random < 10)
                {
                    Console.WriteLine("Hero at {0} removed", random % heroes.Count());
                    heroes.RemoveAt(random % heroes.Count());
                    
                }
            }
        }

        public static void Papparazzi(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                int random = LeaveMeAlone.random.Next(100);
                if (random < 25)
                {
                    hero.statuses.Add(new Status("confuse", 2, 0, Status.Effect_Time.Before, Status.Type.Debuff, Status.confused_image, Status.DoNothing, Status.DoNothing));
                }
            }
        }

        public static void RetirementPorch(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                hero.statuses.Add(new Status("atk+", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.atkplus_image, Status.DoNothing, Status.ReduceAttack));
                hero.statuses.Add(new Status("def+", 3, 0, Status.Effect_Time.Once, Status.Type.Buff, Status.defplus_image, Status.DoNothing, Status.ReduceDefense));
                hero.gold += (int)(hero.gold*1.3);
                hero.exp += (int)(hero.exp*1.2);
            }
        }

        public static void TheGym(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                hero.attack -= Status.StageValue(hero.attack, hero.level);
                hero.statuses.Add(new Status("atk-", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.atkminus_image, Status.DoNothing, Status.RaiseAttack));
                hero.special_attack -= Status.StageValue(hero.special_attack, hero.level);
                hero.statuses.Add(new Status("spec-", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.specminus_image, Status.DoNothing, Status.RaiseSAttack));
                //Yes, twice
                hero.attack -= Status.StageValue(hero.attack, hero.level);
                hero.statuses.Add(new Status("atk-", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, null, Status.DoNothing, Status.RaiseAttack));
                hero.special_attack -= Status.StageValue(hero.special_attack, hero.level);
                hero.statuses.Add(new Status("spec-", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, null, Status.DoNothing, Status.RaiseSAttack));
            }
        }
        public static void MassiveTreadmill(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                hero.energy = 0;
                if (LeaveMeAlone.random.Next(100) > 60){
                    hero.statuses.Add(new Status("stun", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.stun_image, Status.DoNothing, Status.DoNothing));
                }

            }
        }
        public static void LaserRoom(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                if (hero.charType != Character.Type.Ranger)
                {
                    hero.health -= 15;
                }
            }
        }
        public static void MindEraseChamber(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                if (hero.charType != Character.Type.Mage)
                {
                    hero.statuses.Add(new Status("stun", 2, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.stun_image, Status.DoNothing, Status.DoNothing));
                }
            }
        }
        public static void SeaBassRoom(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                    hero.statuses.Add(new Status("poison", 3, 0, Status.Effect_Time.After, Status.Type.Debuff, Status.poison_image, Status.Poison));
                    hero.statuses.Add(new Status("poison", 3, 0, Status.Effect_Time.After, Status.Type.Debuff, Status.poison_image, Status.Poison));
            }
        }
        public static void InterrogationChamber(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                if (hero.charType != Character.Type.Knight)
                {
                    hero.statuses.Add(new Status("stun", 3, 0, Status.Effect_Time.Once, Status.Type.Debuff, Status.stun_image, Status.DoNothing, Status.DoNothing));
                }
            }
        }
        public static void AustinMarlinGarage(List<Character> heroes)
        {
            foreach (Character hero in heroes)
            {
                hero.health -= (int)(hero.health * .3);
            }
        }
    }
}


