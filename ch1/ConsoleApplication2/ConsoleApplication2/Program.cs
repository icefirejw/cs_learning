using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication2
{
    class Vector
    {
        public int Value;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Vector x, y;
            x = new Vector();
            x.Value = 30;
            y = x;
            Console.WriteLine(y.Value);
            y.Value = 50;
            Console.WriteLine(x.Value);
            Console.ReadLine();
        }
    }
}
