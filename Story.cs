using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using InteractiveStory.Commands;
using InteractiveStory.Weaponry;
using InteractiveStory.Items;
using InteractiveStory.Enemies;

namespace InteractiveStory
{
    public class Story
    {
        public Dictionary<string, CommandBase> commandList = new Dictionary<string, CommandBase>();
        public WeaponMaker weaponMaker;
        public Room[,] map = new Room[5,5];
        public int points = 0;
        public int floor = 0;
        string exitPos = "";
        public Random rand = new Random();

        public Player player;
        public string name;
        public bool quit = false;
        bool loaded = false;

        public Story(string name)
        {
            this.name = name;
            InitCommands();
            weaponMaker = new WeaponMaker(this);

            this.newFloor();
            this.player = new Player(this, new int[] { 100, 1, 1 });
        }

        public void Start()
        {
            //int[] stats = ChooseStats();
            Console.Clear();
            if (!loaded)
            {
                Console.WriteLine(" --- Backstory --- ");
                Console.WriteLine();
                Console.Write("Walking through the night in the light of the full moon, you hear a loud explosion sound echo throught the air. ");
                Console.Write("You follow the source of the sound to investigate further, upon reaching a graveyard you notice a burial crypt has been smashed open. ");
                Console.WriteLine("Reluctantly you take a look inside, queitly creeping down the marble stairs, to see if any potential grave robbers are still around.");
                Console.Write("As if a supernatural force acted upon the entrance, it collapses sealing you in. ");
                Console.Write("Any attempt you make to dig your way out only causes more rubble to fill the space. ");
                Console.WriteLine("Accepting defeat you turn and continue onward to find another way out...");
                Console.WriteLine();
                Console.WriteLine("Press [ENTER] to continue...");
                Console.ReadLine();

                this.SaveGame();
            }
            Console.Clear();
            Console.WriteLine("Type 'help' to get a list of available actions...");
            Console.WriteLine();

            string[] command;
            commandList["look"].DoCommand(this, new string[0]);

            while (player.health > 0 && !quit)
            {
                player.canBlock = true;
                command = Console.ReadLine().Split(Char.Parse(" "));
                string msg = doCommand(command);
                if (msg != "")
                {
                    Console.WriteLine(msg);
                }
                doAttack();
            }

            if (!quit)
            {
                Console.WriteLine();
                Console.WriteLine("You died :(");
                Console.WriteLine("Score: " + points);
                Console.ReadLine();
            }
            else
            {
                Console.Clear();
            }
        }

        public void InitCommands()
        {
            AddCommand(new CommandAttack(), new string[] { "attack", "hit", "kill", "a" });
            AddCommand(new CommandDrop(), new string[] { "drop", "discard", "d" });
            AddCommand(new CommandEquip(), new string[] { "equip", "hold", "e" });
            AddCommand(new CommandInvo(), new string[] { "inventory", "invo", "items", "i" });
            AddCommand(new CommandLook(), new string[] { "look", "search", "l" });
            AddCommand(new CommandMove(), new string[] { "move", "go", "walk", "run", "w" });
            AddCommand(new CommandPickup(), new string[] { "pickup", "get", "grab", "p" });
            AddCommand(new CommandSkills(), new string[] { "skills", "x" });
            AddCommand(new CommandStats(), new string[] { "stats", "statistics", "s" });
            AddCommand(new CommandUse(), new string[] { "use", "u" });
            AddCommand(new CommandMap(), new string[] { "map", "m" });
            AddCommand(new CommandSave(), new string[] { "save", "savegame" });
            AddCommand(new CommandQuit(), new string[] { "quit" });
        }

        public void AddCommand(CommandBase command, string[] aliases)
        {
            for (int i = 0; i < aliases.Length; i++)
            {
                commandList.Add(aliases[i].Trim().ToLower(), command);
            }
        }

        public Room getRoom(int x, int y)
        {
            try
            {
                if (map[x, y] == null)
                {
                    map[x, y] = makeRoom(x,y);
                }

                return map[x, y];
            }
            catch (IndexOutOfRangeException e)
            {
                return null;
            }
        }

        public Room makeRoom(int x, int y)
        {
            Room room = new Room(this, x, y);

            for (int i = this.rand.Next(4) + floor; i >= 0; i--)
            {
                if ((x + "," + y) == exitPos)
                {
                    exitPos = "";
                    room.contents.Add(new Objects.ExitObject(this, "Exit"));
                    continue;
                }

                int num = rand.Next(3 + floor);
                switch (num)
                {
                    case 0:
                        {
                            if (rand.Next(2) == 0)
                            {
                                room.contents.Add(new Items.Coin(this, "Coin"));
                            }
                            else
                            {
                                room.contents.Add(new Objects.Chest(this, "Chest"));
                            }
                            break;
                        }
                    case 1:
                        {
                            room.contents.Add(weaponMaker.MakeWeapon());
                            break;
                        }
                    case 2:
                        {
                            room.contents.Add(new Items.Heart(this, "Heart"));
                            break;
                        }
                    default:
                        {
                            if (num - 2 >= 5)
                            {
                                room.contents.Add(new Spider(this, "Spider"));
                            }
                            else if (num - 2 >= 3)
                            {
                                room.contents.Add(new Skeleton(this, "Skeleton"));
                            }
                            else
                            {
                                room.contents.Add(new Zombie(this, "Zombie"));
                            }
                            break;
                        }
                }
            }

            return room;
        }

        public void newFloor()
        {
            floor += 1;
            map = new Room[5, 5];
            exitPos = rand.Next(map.GetLength(0)) + "," + rand.Next(map.GetLength(1));
            if (floor > 1 && player != null)
            {
                this.SaveGame();
            }
        }

        public bool SaveGame()
        {
            Dictionary<string, int> saveData = new Dictionary<string, int>();
            saveData.Add("floor", this.floor);
            saveData.Add("points", this.points);
            this.player.SavePlayer(saveData);

            try
            {
                BinaryWriter writer = new BinaryWriter(File.Open(@"saves\" + this.name + ".crypt", FileMode.OpenOrCreate));

                writer.Write(saveData.Count);

                for (int i = 0; i < saveData.Count; i++)
                {
                    writer.Write(saveData.Keys.ElementAt(i));
                    writer.Write(saveData.Values.ElementAt(i));
                }

                writer.Close();
                return true;
            } catch(Exception e)
            {
                Console.Clear();
                Console.WriteLine("An error occured while trying to save the game!");
                Console.WriteLine();
                Console.WriteLine("Error Report:");
                Console.WriteLine(e);
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
        }

        public void LoadGame(Dictionary<string, int> loadData)
        {
            this.loaded = true;
            this.floor = loadData["floor"];
            this.points = loadData["points"];
            this.player.LoadPlayer(loadData);
        }

        public int[] ChooseStats()
        {
            int[] stats = new int[]{100, 1, 5};

            /*while (true)
            {
                Console.Clear();
                Console.WriteLine("What class are you?");
                Console.WriteLine("1: Knight    (HP: 125, Att: 5, Inv: 3)");
                Console.WriteLine("2: Nimble    (HP: 100, Att: 3, Inv: 5)");
                Console.WriteLine("3: Scavanger (HP: 100, Att: 1, Inv: 7)");
                Console.WriteLine("4: Tank      (HP: 150, Att: 7, Inv: 1)");
                Console.WriteLine("5: Average   (HP: 100, Att: 1, Inv: 5)");
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (Char.IsNumber(keyInfo.KeyChar))
                {
                    int num = Convert.ToInt32(Char.ToString(keyInfo.KeyChar));

                    if (num == 1)
                    {
                        stats[0] = 125;
                        stats[1] = 5;
                        stats[2] = 3;
                        break;
                    }
                    else if(num == 2)
                    {
                        stats[0] = 100;
                        stats[1] = 3;
                        stats[2] = 5;
                        break;
                    }
                    else if (num == 3)
                    {
                        stats[0] = 100;
                        stats[1] = 1;
                        stats[2] = 7;
                        break;
                    }
                    else if (num == 4)
                    {
                        stats[0] = 150;
                        stats[1] = 7;
                        stats[2] = 1;
                        break;
                    }
                    else if (num == 5)
                    {
                        break;
                    }
                }
            }*/

            return stats;
        }

        public void doAttack()
        {
            Room room = this.getRoom(this.player.posX, this.player.posY);

            if (room.contents.Count <= 0)
            {
                return;
            }

            int index = rand.Next(room.contents.Count);

            if (rand.Next(2) == 0 && room.contents.ElementAt(index) is Enemies.EnemyBase)
            {
                Enemies.EnemyBase enemy = (Enemies.EnemyBase)room.contents.ElementAt(index);
                Console.WriteLine("You were attacked by a " + enemy.name);

                if (player.equipped != null && player.equipped.block > 0 && rand.Next(100) < player.equipped.block && player.canBlock)
                {
                    Console.WriteLine("You blocked the attack with your " + player.equipped.name);
                    player.equipped.durability--;
                    if (player.equipped.durability <= 0)
                    {
                        Console.WriteLine("Your " + player.equipped.name + " broke!");
                        player.equipped = null;
                    }
                }
                else
                {
                    enemy.attackPlayer(this.player);
                    if (player.health > 0)
                    {
                        Console.WriteLine("Your health is now at " + player.health);
                    }
                }
            }
        }

        public string doCommand(string[] args)
        {
            if(args.Length <= 0 || args[0] == "")
            {
                return "...";
            } else if (commandList.ContainsKey(args[0]))
            {
                commandList[args[0]].DoCommand(this, args);
                return "";
            } else if (args[0].ToLower() == "help")
            {
                Console.WriteLine("Available Commands:");
                Console.WriteLine("w/walk/move/go <direction>");
                Console.WriteLine("l/look/search");
                Console.WriteLine("p/pickup/get/grab <item>");
                Console.WriteLine("e/equip <item>");
                Console.WriteLine("d/drop <item>");
                Console.WriteLine("u/use <object>");
                Console.WriteLine("a/attack/kill <enemy>");
                Console.WriteLine("i/invo/inventory");
                Console.WriteLine("s/stats/statistics");
                Console.WriteLine("x/skills");
                Console.WriteLine("m/map");
                Console.WriteLine("save/savegame");
                Console.WriteLine("q/quit");
                return "";
            }
            else
            {
                return "Unknown action '" + args[0].ToLower() + "'";
            }
        }
    }
}
