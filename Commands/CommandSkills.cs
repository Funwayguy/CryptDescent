using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandSkills : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            this.SkillScreen(story.player);
            if (story.player.health > story.player.maxHP)
            {
                story.player.health = story.player.maxHP;
            }
            story.commandList["look"].DoCommand(story, new string[0]);
            return;
        }

        public void SkillScreen(Player player) // Activity 2
        {
            int menu = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Player Stats");
                Console.WriteLine("========================");
                Console.WriteLine("Maximum Health:      " + player.maxHP);
                Console.WriteLine("Attack Strength:     " + player.str);
                Console.WriteLine("Inventory Space:     " + player.invSpace);
                Console.WriteLine("========================");
                Console.WriteLine();
                Console.WriteLine("Skill Points: " + player.skillPoints);
                Console.WriteLine();
                if (menu == 0)
                {
                    Console.WriteLine("1: Spend points");
                    Console.WriteLine("0: Exit");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (Char.IsNumber(keyInfo.KeyChar))
                    {
                        int num = Convert.ToInt32(Char.ToString(keyInfo.KeyChar));

                        if (num == 1)
                        {
                            menu = 1;
                            continue;
                        }
                        else if (num == 0)
                        {
                            Console.Clear();
                            break;
                        }
                    }
                }
                else if (menu == 1)
                {
                    Console.WriteLine("1: +10 Maximum Health");
                    Console.WriteLine("2: +1  Attack Strength");
                    Console.WriteLine("3: +1  Inventory Space");
                    //Console.WriteLine("4: -10 Maximum Health");
                    //Console.WriteLine("5: -1  Attack Strength");
                    //Console.WriteLine("6: -1  Inventory Space");
                    Console.WriteLine("0: Back");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (Char.IsNumber(keyInfo.KeyChar))
                    {
                        int num = Convert.ToInt32(Char.ToString(keyInfo.KeyChar));

                        if (num == 1 && player.skillPoints > 0)
                        {
                            player.maxHP += 10;
                            player.skillPoints--;
                            continue;
                        }
                        else if (num == 2 && player.skillPoints > 0)
                        {
                            player.str++;
                            player.skillPoints--;
                            continue;
                        }
                        else if (num == 3 && player.skillPoints > 0)
                        {
                            player.invSpace++;
                            player.skillPoints--;
                            continue;
                        }
                        /*else if (num == 4 && player.maxHP > 15)
                        {
                            player.maxHP -= 10;
                            player.skillPoints++;
                            continue;
                        }
                        else if (num == 5 && player.str > 1)
                        {
                            player.str--;
                            player.skillPoints++;
                            continue;
                        }
                        else if (num == 6 && player.invSpace > 1)
                        {
                            player.invSpace--;
                            player.skillPoints++;
                            continue;
                        }*/
                        else if (num == 0)
                        {
                            menu = 0;
                            continue;
                        }
                    }
                }
            }
        }
    }
}
