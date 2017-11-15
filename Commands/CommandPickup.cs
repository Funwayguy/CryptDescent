using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandPickup: CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Pickup what?");
                return;
            }

            Room room = story.getRoom(story.player.posX, story.player.posY);

            string name = args[1].Trim();

            for (int i = 2; i < args.Length; i++)
            {
                name = name + " " + args[i];
            }

            for (int i = 0; i < room.contents.Count; i++)
            {
                if (room.contents.ElementAt(i) is Items.ItemBase)
                {
                    Items.ItemBase item = (Items.ItemBase)room.contents.ElementAt(i);

                    if (item.name.ToLower().StartsWith(name.ToLower()))
                    {
                        Console.WriteLine(item.onPickup(story.player));
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            Console.WriteLine("Cannot find " + name);
            return;
        }
    }
}
