using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Items
{
    public class ItemBase
    {
        protected Story story;
        public string name { get; set; }
        public int baseID { get; set; }
        public int modID { get; set; }
        int durActual;
        public int durability
        {
            get { return durActual; }
            set
            {
                if (value < 0)
                {
                    durActual = 0;
                }
                else
                {
                    durActual = value;
                }
            }
        }
        public int damage { get; set; }
        int blActual;
        public int block
        {
            get { return blActual; }
            set
            {
                if (value < 0)
                {
                    blActual = 0;
                }
                else if (value > 100)
                {
                    blActual = 100;
                }
                else
                {
                    blActual = value;
                }
            }
        }
        int accActual;
        public int accuracy
        {
            get { return accActual; }
            set
            {
                if (value < 0)
                {
                    accActual = 0;
                }
                else if(value > 100)
                {
                    accActual = 100;
                }
                else
                {
                    accActual = value;
                }
            }
        }

        public ItemBase(Story story, string name)
        {
            this.story = story;
            this.name = name;
            this.damage = 0;
            this.durability = 1;
            this.block = 0;
            this.accuracy = 0;
        }

        public virtual string useItem(Player player)
        {
            return "Can't use " + this.name;
        }

        public virtual string onPickup(Player player)
        {
            if (player.invo.Count < player.invSpace)
            {
                player.invo.Add(this);
                story.getRoom(player.posX, player.posY).contents.Remove(this);
                return "Picked up " + this.name;
            }
            else if (player.equipped == null)
            {
                player.equipped = this;
                story.getRoom(player.posX, player.posY).contents.Remove(this);
                return "Picked up & equipped " + this.name;
            }
            else
            {
                return "Inventory full!";
            }
        }
    }
}
