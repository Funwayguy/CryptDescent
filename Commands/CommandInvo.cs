using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandInvo : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            int invoSize = story.player.invo.Count;
            Console.Write("You carrying:");

            if (invoSize <= 0)
            {
                Console.WriteLine(" nothing");
            }
            else
            {
                Console.WriteLine("");
                for (int i = 0; i < invoSize; i++)
                {
                    object obj = story.player.invo.ElementAt(i);

                    if (obj is Items.ItemBase)
                    {
                        Items.ItemBase item = (Items.ItemBase)obj;
                        Console.WriteLine(" - " + item.name + " (Att:+" + item.damage + ", Def:" + item.block + ", Acc:" + item.accuracy + ", Dur: " + item.durability + ")");
                    }
                    else if (obj is Enemies.EnemyBase)
                    {
                        Console.WriteLine(" - " + ((Enemies.EnemyBase)obj).name);
                    }
                    else if (obj is Objects.OtherObject)
                    {
                        Console.WriteLine(" - " + ((Objects.OtherObject)obj).name);
                    }
                    else
                    {
                        Console.WriteLine(" - [?] (" + obj.GetType().Name + ")");
                    }
                }
            }

            Console.WriteLine("You have " + (story.player.invSpace - invoSize) + " free slot(s).");

            if (story.player.equipped != null)
            {
                Console.WriteLine("You are also holding a " + story.player.equipped.name + " (Att:+" + story.player.equipped.damage + ", Def:" + story.player.equipped.block + ", Acc:" + story.player.equipped.accuracy + ", Dur: " + story.player.equipped.durability + ")");
            }

            return;
        }
    }
}
