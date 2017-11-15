using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandDrop : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            Room room = story.getRoom(story.player.posX, story.player.posY);

            if (args.Length == 1 && story.player.equipped != null)
            {
                string msg = "Dropped " + story.player.equipped.name;
                room.contents.Add(story.player.equipped);
                story.player.equipped = null;
                Console.WriteLine(msg);
                return;
            }
            else if (args.Length > 1)
            {
                string name = args[1].Trim();

                for (int i = 2; i < args.Length; i++)
                {
                    name = name + " " + args[i].Trim().ToLower();
                }

                for (int i = 0; i < story.player.invo.Count; i++)
                {
                    if (story.player.invo.ElementAt(i) is Items.ItemBase)
                    {
                        Items.ItemBase item = (Items.ItemBase)story.player.invo.ElementAt(i);

                        if (item.name.ToLower().StartsWith(name.ToLower()))
                        {
                            string msg = "Dropped " + item.name;
                            room.contents.Add(item);
                            story.player.invo.Remove(item);
                            Console.WriteLine(msg);
                            return;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                if (story.player.equipped != null && story.player.equipped.name.ToLower().StartsWith(name.ToLower()))
                {
                    string msg = "Dropped " + story.player.equipped.name;
                    room.contents.Add(story.player.equipped);
                    story.player.equipped = null;
                    Console.WriteLine(msg);
                    return;
                }

                Console.WriteLine("Cannot find " + name);
                return;
            }
            else
            {
                Console.WriteLine("Drop what?");
                return;
            }
        }
    }
}
