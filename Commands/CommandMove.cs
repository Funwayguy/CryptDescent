using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandMove : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Go where? (north, south, east, west)");
                return;
            }

            string dir = args[1].ToLower();
            if ("forward".StartsWith(dir) || "north".StartsWith(dir))
            {
                if (story.player.posY + 1 >= story.map.GetLength(1))
                {
                    Console.WriteLine("Can't go " + dir);
                    return;
                }
                else
                {
                    story.player.posY++;
                    Console.Clear();
                    Console.WriteLine("Walking North...");
                    story.commandList["look"].DoCommand(story, new string[0]);
                    return;
                }
            }
            if ("backward".StartsWith(dir) || "south".StartsWith(dir))
            {
                if (story.player.posY - 1 < 0)
                {
                    Console.WriteLine("Can't go " + dir);
                    return;
                }
                else
                {
                    story.player.posY--;
                    Console.Clear();
                    Console.WriteLine("Walking South...");
                    story.commandList["look"].DoCommand(story, new string[0]);
                    return;
                }
            }
            if ("right".StartsWith(dir) || "east".StartsWith(dir))
            {
                if (story.player.posX + 1 >= story.map.GetLength(0))
                {
                    Console.WriteLine("Can't go " + dir);
                    return;
                }
                else
                {
                    story.player.posX++;
                    Console.Clear();
                    Console.WriteLine("Walking East...");
                    story.commandList["look"].DoCommand(story, new string[0]);
                    return;
                }
            }
            if ("left".StartsWith(dir) || "west".StartsWith(dir))
            {
                if (story.player.posX - 1 < 0)
                {
                    Console.WriteLine("Can't go " + dir);
                    return;
                }
                else
                {
                    story.player.posX--;
                    Console.Clear();
                    Console.WriteLine("Walking West...");
                    story.commandList["look"].DoCommand(story, new string[0]);
                    return;
                }
            }

            Console.WriteLine("Unknown direction " + dir);
            return;
        }
    }
}
