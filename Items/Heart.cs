using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Items
{
    public class Heart : ItemBase
    {
        public Heart(Story story, string name) : base(story, name)
        {
        }

        public override string onPickup(Player player)
        {
            player.health += 10;
            if (player.health > player.maxHP)
            {
                player.health = player.maxHP;
            }
            story.getRoom(player.posX, player.posY).contents.Remove(this);
            return "Picked up heart (+10 Health). Health is now " + player.health;
        }
    }
}
