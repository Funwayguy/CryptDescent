using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Enemies
{
    public class EnemyBase
    {
        protected Story story;
        public string name { get; set; }
        public int damage { get; set; }
        int hpActual;
        public int health
        {
            get { return hpActual; }
            set
            {
                if (value < 0)
                {
                    hpActual = 0;
                }
                else
                {
                    hpActual = value;
                }
            }
        }
        public int xp { get; set; }

        public EnemyBase(Story story, string name)
        {
            this.story = story;
            this.name = name;
            this.health = 5;
            this.damage = 1;
            this.xp = 1;
        }

        public virtual void attackPlayer(Player player)
        {
            player.health -= this.damage;
        }

        public virtual void attackThis(Player player)
        {
            health -= player.getDamage();
        }

        public virtual string attackMsg(Random rand)
        {
            return "The " + name + " attacked you";
        }

        public virtual string injuredMsg(Random rand)
        {
            return "You attacked the " + name + " (HP: "+ this.health +")";
        }

        public virtual string deathMsg(Random rand)
        {
            return "You killed the " + this.name;
        }
    }
}
