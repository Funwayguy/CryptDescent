using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandStats : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            Console.WriteLine("Health: " + story.player.health);
            Console.WriteLine("Attack: " + story.player.getStrength() + (story.player.equipped != null ? "(+" + story.player.equipped.damage + ")" : ""));
            Console.WriteLine("Level: " + story.player.level);
            Console.WriteLine("Max Items: " + story.player.invSpace);
            Console.WriteLine("XP: " + story.player.xp + " / " + (story.player.level * 5));

            if (story.player.equipped != null)
            {
                Console.WriteLine("Equipped: " + story.player.equipped.name + " (" + story.player.equipped.durability + " uses)");
            }
            else
            {
                Console.WriteLine("Equipped: None");
            }
            Console.WriteLine("Points: " + story.points);
            return;
        }
    }
}
