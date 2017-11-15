using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveStory.Enemies;
using InteractiveStory.Items;
using InteractiveStory.Objects;

namespace InteractiveStory.Commands
{
    public class CommandMap : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            Console.Clear();
            Console.WriteLine("  Compass:  | Map Key:");
            Console.WriteLine("     N      | ? - Unexplored");
            Console.WriteLine("     ^      | o - Player position");
            Console.WriteLine("W <- + -> E | x - Exit");
            Console.WriteLine("     v      | ");
            Console.WriteLine("     S      | ");
            Console.WriteLine();
            Console.WriteLine("Dungeon Map - Floor " + story.floor);

            Console.WriteLine("+" + (new String("-"[0], story.map.GetLength(0)*3 + 2)) + "+");

            // Draw Map
            for (int j = story.map.GetLength(0) -1; j >= 0; j--)
            {
                for (int i = 0; i < story.map.GetLength(1); i++)
                {
                    if (i == 0)
                    {
                        Console.Write("| ");
                    }

                    Console.Write(this.RoomIcon(story, i, j));

                    if(i == story.map.GetLength(0) - 1)
                    {
                        Console.WriteLine(" |");
                    }
                }
            }

            Console.WriteLine("+" + (new String("-"[0], story.map.GetLength(0) * 3 + 2)) + "+");

            Console.WriteLine();

            Console.WriteLine("Press any key to return to game...");
            Console.ReadKey();
            Console.Clear();
            story.commandList["look"].DoCommand(story, new string[0]);
            return;
        }

        public string RoomIcon(Story story, int x, int y)
        {
            if (x >= story.map.GetLength(0) || x < 0 || y >= story.map.GetLength(1) || y < 0)
            {
                return "ERR";
            }

            Room room = story.map[x, y];
            int flag = 0;
            bool exNeighbour = false;

            if (room != null)
            {
                exNeighbour = true;
                for (int i = 0; i < room.contents.Count; i++)
                {
                    if (room.contents[i] is EnemyBase)
                    {
                        flag = 1;
                    }
                    else if (room.contents[i] is ExitObject)
                    {
                        flag = 2;
                        break;
                    }
                }
            }
            else
            {
                if (x > 0 && story.map[x - 1, y] != null)
                {
                    exNeighbour = true;
                }
                else if(x + 1 < story.map.GetLength(0) && story.map[x + 1, y] != null)
                {
                    exNeighbour = true;
                }
                else if(y > 0 && story.map[x, y - 1] != null)
                {
                    exNeighbour = true;
                }
                else if(y + 1 < story.map.GetLength(1) && story.map[x, y + 1] != null)
                {
                    exNeighbour = true;
                }
            }

            if (room == null)
            {
                return exNeighbour? "[?]" : "   ";
            }
            else if(story.player.posX == x && story.player.posY == y)
            {
                return "[o]";
            }
            else if(flag == 2)
            {
                return "[x]";
            }
            else if(flag == 1)
            {
                return "[ ]";
            }
            else if (room.contents.Count > 0)
            {
                return "[ ]";
            }
            else
            {
                return "[ ]";
            }
        }
    }
}
