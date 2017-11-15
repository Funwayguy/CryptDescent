using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Objects
{
    public class Chest : OtherObject
    {
        private Object[] holding = null;

        public Chest(Story story, String name) : base(story, name)
        {
            this.holding = this.getLoot();
        }

        public override string onUse()
        {
            Room room = story.getRoom(story.player.posX, story.player.posY);
            Object received = holding[story.rand.Next(holding.Length)];

            if (received == null)
            {
                room.contents.Remove(this);
                return "The chest contained nothing";
            }

            room.contents.Add(received);
            room.contents.Remove(this);

            if (received is Enemies.EnemyBase)
            {
                return "A " + ((Enemies.EnemyBase)received).name + " jumped out of the chest!";
            }
            else if (received is Objects.OtherObject)
            {
                return "The chest contained a " + ((Objects.OtherObject)received).name;
            }
            else if (received is Items.ItemBase)
            {
                return "The chest contained a " + ((Items.ItemBase)received).name;
            }
            else
            {
                return "The chest contained [?]" + received.GetType().Name;
            }
        }

        public Object[] getLoot()
        {
            Object[] loot = new Object[6];

            loot[0] = new Items.Coin(this.story, "Coin");
            loot[1] = this.story.weaponMaker.MakeWeapon();
            loot[2] = new Items.Heart(this.story, "Heart");
            //loot[3] = new Items.Dagger(this.story, "Dagger");
            loot[3] = new Enemies.Zombie(this.story, "Zombie");

            return loot;
        }
    }
}
