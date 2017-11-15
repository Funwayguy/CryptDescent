using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Enemies
{
    public class Spider : EnemyBase
    {
        public Spider(Story story, String name) : base(story, name)
        {
            this.damage = 10;
            this.xp = 10;
            this.health = 20;
        }

        public override string attackMsg(Random rand)
        {
            return "The " + name + " bit you";
        }

        public override string deathMsg(Random rand)
        {
            if (story.player.equipped == null)
            {
                return "You beat " + this.name + " to death with your fists";
            }
            else
            {
                return "You chop off the " + this.name + "'s legs with your " + story.player.equipped.name;
            }
        }
    }
}
