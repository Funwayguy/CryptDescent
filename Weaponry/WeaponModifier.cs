using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Weaponry
{
    public class WeaponModifier
    {
        string name = "";
        int damage = 0;
        int defence = 0;
        int accuracy = 0;
        int durability = 0;

        public WeaponModifier(string name, int damage, int defence, int accuracy, int durability)
        {
            this.name = name;
            this.damage = damage;
            this.defence = defence;
            this.accuracy = accuracy;
            this.durability = durability;
        }

        public void ApplyModifier(Items.ItemBase item)
        {
            if (item.name.Length > 0)
            {
                item.name = this.name + "-" + item.name;
            }
            else
            {
                item.name = this.name;
            }

            item.damage += this.damage;
            item.block += this.defence;
            item.accuracy += this.accuracy;
            item.durability += this.durability;

            if (item.damage < 0)
            {
                item.damage = 0;
            }

            if (item.durability <= 0)
            {
                item.durability = 1;
            }
        }
    }
}
