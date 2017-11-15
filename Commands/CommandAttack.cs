using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandAttack : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Attack what?");
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
                if (room.contents.ElementAt(i) is Enemies.EnemyBase)
                {
                    Enemies.EnemyBase enemy = (Enemies.EnemyBase)room.contents.ElementAt(i);

                    if (enemy.name.ToLower().StartsWith(name.ToLower()))
                    {
                        int attChance = 50;

                        if (story.player.equipped != null)
                        {
                            attChance = story.player.equipped.accuracy;
                        }

                        if (story.rand.Next(100) < attChance)
                        {
                            enemy.attackThis(story.player);
                            string swordMsg = "";

                            if (story.player.equipped != null)
                            {
                                story.player.equipped.durability -= 1;

                                if (story.player.equipped.durability <= 0)
                                {
                                    swordMsg = "Your " + story.player.equipped.name + " broke!";
                                    story.player.equipped = null;
                                }
                            }

                            string enemyMsg = "";
                            if (enemy.health > 0)
                            {
                                enemyMsg = enemy.injuredMsg(story.rand);
                            }
                            else
                            {
                                room.contents.Remove(enemy);
                                enemyMsg = enemy.deathMsg(story.rand) + " (+" + (story.player.equipped == null? enemy.xp*2 : enemy.xp) + "XP)";
                            }

                            if (enemyMsg != "")
                            {
                                Console.WriteLine(enemyMsg);
                            }

                            if (swordMsg != "")
                            {
                                Console.WriteLine(swordMsg);
                            }

                            if (enemy.health <= 0)
                            {
                                story.player.addXP(story.player.equipped == null ? enemy.xp * 2 : enemy.xp);
                            }

                            return;
                        }
                        else
                        {
                            story.player.canBlock = false;
                            Console.WriteLine("You missed!");
                            return;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            Console.WriteLine("Can't find " + name + " to attack");
            return;
        }
    }
}
