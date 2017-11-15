using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandEquip : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            string name = "nothing";

            if (args.Length >= 2)
            {
                name = args[1].Trim();
            }

            for (int i = 2; i < args.Length; i++)
            {
                name = name + " " + args[i].Trim();
            }

            if (name.ToLower() == "nothing")
            {
                if (story.player.invo.Count < 5 && story.player.equipped != null)
                {
                    story.player.invo.Add(story.player.equipped);
                    story.player.equipped = null;
                    Console.WriteLine("Unequiped item");
                    return;
                }
                else
                {
                    Console.WriteLine("Unable to store equipped item.");
                    return;
                }
            }

            for (int i = 0; i < story.player.invo.Count; i++)
            {
                if (story.player.invo.ElementAt(i) is Items.ItemBase)
                {
                    Items.ItemBase item = (Items.ItemBase)story.player.invo.ElementAt(i);

                    if (item.name.ToLower().StartsWith(name.ToLower()))
                    {
                        if (story.player.equipped != null)
                        {
                            story.player.invo.Add(story.player.equipped);
                            story.player.equipped = null;
                        }
                        story.player.invo.Remove(item);
                        story.player.equipped = item;
                        Console.WriteLine("Equipped " + story.player.equipped.name + " (+" + item.damage + " attack)");
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
