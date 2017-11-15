using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandQuit : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            Console.WriteLine("Quitting...");
            story.quit = true;
        }
    }
}
