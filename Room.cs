using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory
{
    public class Room
    {
        public Story story;
        public List<object> contents;
        public int posX;
        public int posY;

        public Room(Story story, int x, int y)
        {
            this.story = story;
            this.posX = x;
            this.posY = y;
            contents = new List<object>();
        }
    }
}
