using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HashTableLearning
{
    class Program
    {
        public static void Main()
        {

            // Creates and initializes a new Hashtable.
            Hashtable myHT = new Hashtable();
            myHT.Add("one", "The");
            myHT.Add("two", "quick");
            myHT.Add("three", "brown");
            myHT.Add("four", "fox");

            // Displays the Hashtable.
            Console.WriteLine("The Hashtable contains the following:");
            PrintKeysAndValues(myHT);
            Console.Write(myHT.
            Console.ReadLine();
        }


        public static void PrintKeysAndValues(Hashtable myHT)
        {
            Console.WriteLine("\t-KEY-\t-VALUE-");
            foreach (DictionaryEntry de in myHT)
                Console.WriteLine("\t{0}:\t{1}", de.Key, de.Value);
            Console.WriteLine();
        }
    }
}
