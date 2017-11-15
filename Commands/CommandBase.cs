using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Commands
{
    public abstract class CommandBase
    {
        public CommandBase()
        {
        }

        public abstract void DoCommand(Story story, string[] args);
    }
}
