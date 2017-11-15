using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Enemies
{
    public class Zombie : EnemyBase
    {
        public Zombie(Story story, string name) : base(story, name)
        {
            this.damage = 5;
            this.xp = 5;
            this.health = 10;
        }

        public override string attackMsg(Random rand)
        {
            return "The " + name + " bit you";
        }

        public override string deathMsg(Random rand)
        {
            if (story.player.equipped == null)
            {
                return "You cave in the " + this.name + "'s skull with your fist";
            }
            else
            {
                return "You knock off the " + this.name + "'s head with your " + story.player.equipped.name;
            }
        }
    }
}
