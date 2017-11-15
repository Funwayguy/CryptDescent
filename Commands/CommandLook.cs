using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandLook : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            Console.WriteLine("You are at (" + story.player.posX + "," + story.player.posY + ") on floor " + story.floor);
            Room room = story.getRoom(story.player.posX, story.player.posY);
            int listSize = room.contents.Count;
            Console.Write("There is ");

            if (listSize <= 0)
            {
                Console.Write("nothing");
            }
            else
            {
                for (int i = 0; i < listSize; i++)
                {
                    if (listSize >= 2 && i == listSize - 1)
                    {
                        if (listSize > 2)
                        {
                            Console.Write("and ");
                        }
                        else
                        {
                            Console.Write(" and ");
                        }
                    }

                    object obj = room.contents.ElementAt(i);

                    if (obj is Items.ItemBase)
                    {
                        Console.Write("a " + ((Items.ItemBase)obj).name);
                    }
                    else if (obj is Enemies.EnemyBase)
                    {
                        Console.Write("a " + ((Enemies.EnemyBase)obj).name);
                    }
                    else if (obj is Objects.OtherObject)
                    {
                        Console.Write("a " + ((Objects.OtherObject)obj).name);
                    }
                    else
                    {
                        Console.Write("a [?] (" + obj.GetType().Name + ")");
                    }

                    if (listSize >= 3 && i < listSize - 1)
                    {
                        Console.Write(", ");
                    }
                }
            }

            Console.WriteLine(" here");
        }
    }
}
