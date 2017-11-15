using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Objects
{
    public class OtherObject
    {
        public string name { get; set; }
        protected Story story { get; set; }

        public OtherObject(Story story, string name)
        {
            this.story = story;
            this.name = name;
        }

        public virtual string onUse()
        {
            return "Cannot use " + this.name;
        }
    }
}
