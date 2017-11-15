using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveStory.Items;

namespace InteractiveStory.Weaponry
{
    public class WeaponMaker
    {
        Story story;
        List<WeaponModifier> weaponMods = new List<WeaponModifier>();
        List<WeaponModifier> weaponBases = new List<WeaponModifier>();

        public WeaponMaker(Story story)
        {
            this.story = story;
            InitModifiers();
            InitBases();
        }

        void InitModifiers()
        {
            weaponMods.Add(new WeaponModifier("Petty", -5, -10, -10, -5));
            weaponMods.Add(new WeaponModifier("Rusty", 15, -5, 0, -5));
            weaponMods.Add(new WeaponModifier("Brutal", 25, 0, -10, 10));
            weaponMods.Add(new WeaponModifier("Epic", 15, 15, 15, 5));
            weaponMods.Add(new WeaponModifier("Reaping", 25, 0, 75, 25));
            weaponMods.Add(new WeaponModifier("Demonic", 50, -10, 25, -10));
            weaponMods.Add(new WeaponModifier("Legendary", 50, 50, 50, 25));
        }

        void InitBases()
        {
            weaponBases.Add(new WeaponModifier("Sword", 10, 75, 50, 15));
            weaponBases.Add(new WeaponModifier("Dagger", 5, 0, 75, 25));
            weaponBases.Add(new WeaponModifier("LongSword", 25, 25, 25, 10));
            weaponBases.Add(new WeaponModifier("Axe", 25, 10, 50, 15));
            weaponBases.Add(new WeaponModifier("BattleAxe", 50, 25, 10, 10));
            weaponBases.Add(new WeaponModifier("Spear", 10, 0, 75, 25));
        }

        public int getWeightedInt(int max)
        {
            int i = 0;

            while (i + 1 < max)
            {
                if(story.rand.Next(2) == 0)
                {
                    i++;
                } else
                {
                    break;
                }
            }

            return i;
        }

        public ItemBase MakeWeapon()
        {
            ItemBase item = new ItemBase(this.story, "");

            int bIndex = story.rand.Next(weaponBases.Count);
            WeaponModifier baseType = weaponBases.ElementAt(bIndex);
            baseType.ApplyModifier(item);
            item.baseID = bIndex;

            int mIndex = getWeightedInt(weaponMods.Count);
            WeaponModifier modType = weaponMods.ElementAt(mIndex);
            modType.ApplyModifier(item);
            item.modID = mIndex;

            return item;
        }

        public ItemBase MakeWeapon(int bID, int mID)
        {
            ItemBase item = new ItemBase(this.story, "");

            WeaponModifier baseType = weaponBases.ElementAt(bID);
            baseType.ApplyModifier(item);
            item.baseID = bID;

            WeaponModifier modType = weaponMods.ElementAt(mID);
            modType.ApplyModifier(item);
            item.modID = mID;

            return item;
        }
    }
}
