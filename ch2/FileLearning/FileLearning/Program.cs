using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions; 

namespace FileLearning
{
    struct LOCAL_MEMBER
    {
        string Name;
        string depId;
        string depName;
    };

    class Program
    {
        static void Main(string[] args)
        {
            string srcfile = @"A.txt";
            string newfile = @"c.txt";
            string errfile = @"error.txt";
            Encoding ansi = Encoding.GetEncoding("GB2312");
            Encoding unicode = Encoding.Unicode;
            int i = 0;
            //Directory<string, LOCAL_MEMBER> localMember = new Directory<string, LOCAL_MEMBER>[20];

            // This text is added only once to the file.
            if (!File.Exists(srcfile))
            {
                // Create a file to write to.
                //string[] createText = { "Hello", "And", "Welcome" };
                //File.WriteAllLines(path, createText);
                return;
            }

            string srcfiletext = File.ReadAllText(srcfile, ansi);
            File.WriteAllText(newfile, srcfiletext, unicode);

            if (!File.Exists(newfile))
                return;

            // This text is always added, making the file longer over time
            // if it is not deleted.
            //string appendText = "This is extra text" + Environment.NewLine;
            //File.AppendAllText(path, appendText);

            // Open the file to read from.
            string[] readText = File.ReadAllLines(newfile);
            foreach (string s in readText)
            {
                string memid = s.Substring(0, 6);
                Regex r = new Regex(@"^[0-9]+$");
                
                String error = "";
                error += s + Environment.NewLine;
                //error.Insert(error.Length-1,"Line:");
                //error.Insert(error.Length - 1, i.ToString());
                if (!r.Match(memid).Success)
                {
                    File.AppendAllText(errfile, error);
                    continue;
                }
                i++;
                string[] dp = s.Split(' ');
                Console.WriteLine("{0} {1}",memid, dp[dp.Length-1]);

            }
            Console.WriteLine("Total Line: {0}", i);
            Console.ReadLine();

        }
    }
}
