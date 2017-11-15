using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory
{
    public class GameDisplay
    {
        public GameDisplay(Story story)
        {
        }

        public void WriteLine(string text)
        {
        }

        public void DrawBounds()
        {
            Console.SetCursorPosition(0,0);
            Console.Write(new string("="[0], Console.WindowWidth-1));
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(new string("="[0], Console.WindowWidth - 1));
        }

        public void RedrawMap()
        {
            /*
             *           [?]
             *            :
             * [?]--[x]--[ ]
             *       :    :
             *      [?]--[!]
             */
        }

        public void RedrawAll()
        {
        }
    }
}
