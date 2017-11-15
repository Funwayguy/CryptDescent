using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Items
{
    public class Coin : ItemBase
    {
        public Coin(Story story, string name) : base(story, name)
        {
        }

        public override string onPickup(Player player)
        {
            story.points += 1;
            story.getRoom(player.posX, player.posY).contents.Remove(this);
            return "Picked up coin (+1 Point)";
        }
    }
}
