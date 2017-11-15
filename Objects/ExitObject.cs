using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStory.Objects
{
    public class ExitObject : OtherObject
    {
        public ExitObject(Story story, string name) : base(story, name)
        {
        }

        public override string onUse()
        {
            Console.Clear();
            this.story.newFloor();

            if (this.story.floor == 2)
            {
                Console.WriteLine("Going down further into the crypts you wonder if you will ever find your way out...");
                Console.WriteLine();
            }
            else if (this.story.floor == 3)
            {
                Console.WriteLine("The air has becomes is musky and thick with the smell of decayed flesh. Many of the remains have been reduced to bones by age.");
                Console.WriteLine();
            }
            else if (this.story.floor == 5)
            {
                Console.WriteLine("A collapsed hole in the floor reveals an endless maze of caves in every direction. You fear there are more than just zombies here.");
                Console.WriteLine();
            }
            else if (this.story.floor == 7)
            {
                Console.WriteLine("Creatures of the dark linger in these depths. Treading lightly, you continue your descent with any hope of escape fading quickly.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Heading deeper...");
                Console.WriteLine();
            }

            this.story.points += 50;
            this.story.commandList["look"].DoCommand(this.story, new string[0]);
            return "";
        }
    }
}
