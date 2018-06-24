using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i+=10)
            {
                for (int j = i; j < i + 10; j++)
                {
                    Console.Write(" "+j);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
