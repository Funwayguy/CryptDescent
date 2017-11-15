using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveStory.Items;

namespace InteractiveStory
{
    public class Player
    {
        public int maxHP { get; set; }
        public int health { get; set; }
        public int level { get; set; }
        public int xp { get; set; }
        public List<ItemBase> invo { get; set; }
        public ItemBase equipped { get; set; }

        public bool canBlock;

        public Story story;
        public int posX;
        public int posY;

        public int invSpace;
        public int str;
        public int skillPoints;

        public Player(Story story, int[] stats)
        {
            this.story = story;
            health = stats[0];
            maxHP = health;
            level = 1;
            xp = 0;
            posX = 0;
            posY = 0;
            canBlock = true;
            invo = new List<ItemBase>();
            str = stats[1];
            invSpace = stats[2];
            skillPoints = 0;
        }

        public int getStrength()
        {
            return str;
        }

        public void addXP(int added)
        {
            xp += added;
            //Console.WriteLine("+" + added + " XP (" + this.xp + " / " + (this.level*5) + ")");

            if (xp >= level * 5)
            {
                xp -= level * 5;
                level += 1;
                Console.Write("Level Up! You are now level " + level);

                if (level % 3 == 0)
                {
                    Console.WriteLine(", +1 Skill point!");
                    this.skillPoints++;
                }
                else
                {
                    Console.WriteLine("!");
                }
            }
        }

        public int getDamage()
        {
            if (this.equipped != null)
            {
                return this.equipped.damage + this.getStrength();
            }
            else
            {
                return this.getStrength();
            }
        }

        public void SavePlayer(Dictionary<string, int> savedata)
        {
            savedata.Add("stat_str", this.str);
            savedata.Add("stat_hp", this.health);
            savedata.Add("stat_pos_x", this.posX);
            savedata.Add("stat_pos_y", this.posY);
            savedata.Add("stat_xp", this.xp);
            savedata.Add("stat_lvl", this.level);
            savedata.Add("stat_mxh", this.maxHP);
            savedata.Add("stat_skp", this.skillPoints);
            savedata.Add("stat_inv", this.invSpace);

            for (int i = 0; i < this.invo.Count; i++)
            {
                string id = "invo_" + i + "_";
                ItemBase item = invo.ElementAt(i);

                if (item == null)
                {
                    continue;
                }

                savedata.Add(id + "base", item.baseID);
                savedata.Add(id + "mod", item.modID);
                savedata.Add(id + "dur", item.durability);
            }

            if (this.equipped != null)
            {
                savedata.Add("equip_base", this.equipped.baseID);
                savedata.Add("equip_mod", this.equipped.modID);
                savedata.Add("equip_dur", this.equipped.durability);
            }
        }

        public void LoadPlayer(Dictionary<string, int> loaddata)
        {
            this.str = loaddata["stat_str"];
            this.health = loaddata["stat_hp"];
            this.posX = loaddata["stat_pos_x"];
            this.posY = loaddata["stat_pos_y"];
            this.xp = loaddata["stat_xp"];
            this.level = loaddata["stat_lvl"];
            this.maxHP = loaddata["stat_mxh"];
            this.skillPoints = loaddata["stat_skp"];
            this.invSpace = loaddata["stat_inv"];

            for (int i = 0; i < this.invSpace; i++)
            {
                string id = "invo_" + i + "_";

                if (!loaddata.ContainsKey(id + "base") || !loaddata.ContainsKey(id + "mod"))
                {
                    continue;
                }

                ItemBase item = this.story.weaponMaker.MakeWeapon(loaddata[id + "base"], loaddata[id + "mod"]);
                item.durability = loaddata[id + "dur"];
                this.invo.Add(item);
            }

            if (loaddata.ContainsKey("equip_base") && loaddata.ContainsKey("equip_mod"))
            {
                ItemBase item = this.story.weaponMaker.MakeWeapon(loaddata["equip_base"], loaddata["equip_mod"]);
                item.durability = loaddata["equip_dur"];
                this.equipped = item;
            }
        }
    }
}