using System;

namespace ConsoleApplication1
{
    class MyFirstConsoleApp
    {
        static void Main(string[] args)
        {
            string name = "What's ur name?";
            Int32 length = 32;
            bool isRabbit = true;

            Type nameType = name.GetType();
            Type lenType = length.GetType();
            Type isRabbitType = isRabbit.GetType();

            Console.WriteLine("name's type is: " + nameType.ToString());
            Console.WriteLine("length's type is: " + lenType.ToString());
            Console.WriteLine("isRabbit's type is: " + isRabbitType.ToString());
            Console.ReadLine();
            return;
        }
    }
}
