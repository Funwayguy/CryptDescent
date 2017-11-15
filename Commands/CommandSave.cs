using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public class CommandSave : CommandBase
    {
        public override void DoCommand(Story story, string[] args)
        {
            if (story.SaveGame())
            {
                Console.WriteLine(" - Game Saved - ");
            }
        }
    }
}
