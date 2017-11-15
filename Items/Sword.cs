using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Items
{
    public class Sword : ItemBase
    {
        public Sword(Story story, string name) : base(story, name)
        {
            this.damage = 10;
            this.durability = 10;
            this.block = 75;
            this.accuracy = 50;
        }
    }
}
