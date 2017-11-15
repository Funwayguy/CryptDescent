using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Enemies
{
    public class Skeleton : EnemyBase
    {
        public Skeleton(Story story, String name) : base(story, name)
        {
            this.damage = 10;
            this.xp = 10;
            this.health = 10;
        }

        public override string attackMsg(Random rand)
        {
            return "The " + name + " shot you with an arrow";
        }

        public override string deathMsg(Random rand)
        {
            if (story.player.equipped == null)
            {
                return "You snap the " + this.name + "'s bones with your bare hands";
            }
            else
            {
                return "You smash the " + this.name + "'s bones with your " + story.player.equipped.name;
            }
        }
    }
}
