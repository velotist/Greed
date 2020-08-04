using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greed
{
    class UserInteraction
    {
        public static void AwaitKeyAndClearConsole()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
