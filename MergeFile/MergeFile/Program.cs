using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace MergeFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string output_path = @"./";
            string src_path = @"./src/";
            string output_file = @"mergefile.txt";

            if (args.Length < 3)
            {
                Console.WriteLine("Example: MergeFile.exe (DstDir) (SrcDir) (Filename)");
                Console.WriteLine("         DstDir  : destination file directory");
                Console.WriteLine("         SrcDir  : source files directory");
                Console.WriteLine("         Filename: destination filename");
                Console.ReadLine();
            }

            switch (args.Length)
            {
                case 1:
                    output_path = args[0];
                    src_path = args[0];
                    break;

                case 2:
                    output_path = args[0];
                    src_path = args[1];
                    break;

                case 3:
                    output_path = args[0];
                    src_path = args[1];
                    output_file = args[2];
                    break;

                default:
                    break;
            }
            
            ArrayList src_line_list = new ArrayList();
            int ret = get_src_content(src_path.Replace('\\', '/'), ref src_line_list);
            if (0 != ret)
            {
                Console.WriteLine("get_src_content failed, error code:{0}", ret);
                return;
            }

            string out_full_path = output_path.Replace('\\', '/');
            if ('/' != out_full_path[out_full_path.Length - 1]) //must end of '/'
                out_full_path += "/";
            out_full_path += output_file;

            ret = MergeLines(out_full_path, src_line_list);

            if (0 != ret)
            {
                Console.WriteLine("MergeLines failed, error code:{0}", ret);
                return;
            }

            //Console.ReadLine();
        }

        static private int get_src_content(string src_path, ref ArrayList line_list)
        {
            Encoding encoding = Encoding.UTF8;
            DirectoryInfo dir = new DirectoryInfo(src_path);

            try
            {
                foreach (FileInfo f in dir.GetFiles("*.txt"))
                {
                    string filename = f.FullName;
                    string[] lineinfo = File.ReadAllLines(filename, encoding);
                    for (int i = 1; i < lineinfo.Length; i++)
                        line_list.Add(lineinfo[i]);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return -1;
            }

            return 0;
        }

        static private int MergeLines(string out_full_path, ArrayList line_list)
        {
            Encoding encoding = Encoding.UTF8;

            if (File.Exists(out_full_path))
                File.Delete(out_full_path);

            string tmp_buf = "";
            try
            {
                foreach (Object tmp_obj in line_list)
                {
                    tmp_buf += tmp_obj.ToString() + Environment.NewLine;
                }

                File.AppendAllText(out_full_path, tmp_buf, encoding);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -3;
            }

            return 0;
        }
    }
}
