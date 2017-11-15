using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Items
{
    public class Dagger : ItemBase
    {
        public Dagger(Story story, string name) : base(story, name)
        {
            this.damage = 5;
            this.durability = 25;
            this.block = 0;
            this.accuracy = 75;
        }
    }
}
