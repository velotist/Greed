using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greed
{
    class UserInteraction
    {
        public static void AwaitKeyAndClearConsole(string textToShowOnConsole)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(textToShowOnConsole);
            Console.ReadKey();
            Console.Clear();
        }
    }
}
