using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenProfileFN
{

    class ConsoleQueue
    {
        private static List<string> towrite = new List<string>();
        public static bool Write = false;

        public static void Init()
        {
            do
            {
                Thread.Sleep(50);
            } while (Write == false);

            Console.WriteLine(String.Join("\n", towrite));

        }

        public static void AddToWrite(string towrite)
        {
            if(Write == true)
            {
                Console.WriteLine(towrite);
            } else
            {
                ConsoleQueue.towrite.Add(towrite);
            }
        }
    }
}
