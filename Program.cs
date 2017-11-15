using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InteractiveStory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Crypt Descent";
            string byLine = "by Jon.A (Funwayguy)";

            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth / 2 - Console.Title.Length / 2, Console.WindowHeight / 2 - 1);
                Console.Write(Console.Title);
                Console.SetCursorPosition(Console.WindowWidth / 2 - byLine.Length / 2, Console.WindowHeight / 2);
                Console.Write(byLine);
                Console.SetCursorPosition(0, Console.WindowHeight - 3);
                Console.WriteLine("1: Load");
                Console.WriteLine("2: New Game");
                Console.WriteLine("3: Quit");
                Char charIn = Console.ReadKey().KeyChar;

                if (Char.IsNumber(charIn))
                {
                    int numIn = int.Parse(charIn.ToString());

                    if (numIn == 1)
                    {
                        Story myStory = LoadScreen();

                        if (myStory != null)
                        {
                            myStory.Start();

                            Console.Clear();
                            Console.SetCursorPosition(Console.WindowWidth / 2 - "GAME OVER".Length / 2, Console.WindowHeight / 2 - 1);
                            Console.Write("GAME OVER");
                            Console.SetCursorPosition(Console.WindowWidth / 2 - ("Score: " + myStory.points).Length / 2, Console.WindowHeight / 2);
                            Console.Write("Score: " + myStory.points);
                            Console.SetCursorPosition(0, Console.WindowHeight - 1);
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                        }
                    }
                    else if (numIn == 2)
                    {
                        string name = "";

                        while (name == "")
                        {
                            Console.Clear();
                            Console.SetCursorPosition(Console.WindowWidth / 2 - "Enter Name:".Length / 2, Console.WindowHeight / 2 - 1);
                            Console.Write("Enter Name:");
                            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
                            name = Console.ReadLine();
                        }

                        Story myStory = new Story(name);
                        myStory.Start();

                        Console.Clear();
                        Console.SetCursorPosition(Console.WindowWidth / 2 - "GAME OVER".Length / 2, Console.WindowHeight / 2 - 1);
                        Console.Write("GAME OVER");
                        Console.SetCursorPosition(Console.WindowWidth / 2 - ("Score: " + myStory.points).Length / 2, Console.WindowHeight / 2);
                        Console.Write("Score: " + myStory.points);
                        Console.SetCursorPosition(0, Console.WindowHeight - 1);
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else if (numIn == 3)
                    {
                        break;
                    }
                }
            }
        }

        static Story LoadScreen()
        {
            if (!Directory.Exists(@"saves\"))
            {
                try
                {
                    DirectoryInfo dirInfo = Directory.CreateDirectory(@"saves\");
                } catch(Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("Failed to create saves directory!");
                    Console.WriteLine();
                    Console.WriteLine("Error Report:");
                    Console.WriteLine(e);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return null;
                }
            }

            string[] files = Directory.GetFiles(@"saves\", "*.crypt", SearchOption.TopDirectoryOnly);

            for (int fx = 0; fx < files.Length; fx++)
            {

                files[fx] = Path.GetFileNameWithoutExtension(files[fx]);
            }

            int selected = 0;

            while(true)
            {
                Console.Clear();

                if (files.Length > 0)
                {
                    Console.WriteLine("Choose file to load: ");

                    for (int i = 0; i < files.Length; i++)
                    {
                        if (i == selected)
                        {
                            Console.WriteLine(@"> " + files[i]);
                        }
                        else
                        {
                            Console.WriteLine(@"  " + files[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No save files");
                }

                Console.WriteLine();

                Console.WriteLine("1: Load");
                Console.WriteLine("2: Back");

                ConsoleKeyInfo lKey = Console.ReadKey();
                Char lChar = lKey.KeyChar;

                if (Char.IsNumber(lChar))
                {
                    int lNum = int.Parse(lChar.ToString());

                    if (lNum == 1)
                    {
                        return LoadFile(files[selected]);
                    }
                    else if(lNum == 2)
                    {
                        return null;
                    }
                }
                else if (lKey.Key == ConsoleKey.UpArrow && selected > 0)
                {
                    selected--;
                }
                else if (lKey.Key == ConsoleKey.DownArrow && selected + 1 < files.Length)
                {
                    selected++;
                }
            }
        }

        static Story LoadFile(string filename)
        {
            Dictionary<string, int> data;

            try
            {
                BinaryReader reader = new BinaryReader(File.Open(@"saves\" + filename + ".crypt", FileMode.OpenOrCreate));
                int size = reader.ReadInt32();
                data = new Dictionary<string, int>(size);

                for (int x = 0; x < size; x++)
                {
                    string key = reader.ReadString();
                    int obj = reader.ReadInt32();

                    data.Add(key, obj);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("An error occured while trying to load game:");
                Console.WriteLine(e);
                Console.ReadLine();
                return null;
            }

            Story story = new Story(filename);
            story.name = filename;
            story.LoadGame(data);

            //Player player = new Player(story, new int[] { data["hp"], data["str"], data["inv"] });
            //story.player = player;

            return story;
        }
    }
}
