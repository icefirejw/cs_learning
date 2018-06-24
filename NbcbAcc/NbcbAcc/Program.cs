using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;


namespace NbcbAcc
{
    class Program
    {
        /*
        private struct UnistallInfo 
        {
            string DisplayName;       //software name
            string DisplayVersion;    //software version
            string Publisher;         
            string InstallLocation;
            string UninstallString;          
        }        
        */
 
        private static int GetUninstallInfo(Dictionary<string, string[]> uninstallinfo)
        {
            
            string[] subkeyNames;

            //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey microsoft = software.OpenSubKey("Microsoft", true);
            RegistryKey windows = microsoft.OpenSubKey("Windows", true);
            RegistryKey currentversion = windows.OpenSubKey("CurrentVersion", true);
            RegistryKey unistallinfokey = currentversion.OpenSubKey("Uninstall", true);
            subkeyNames = unistallinfokey.GetSubKeyNames();
            
            foreach (string keyname in subkeyNames)
            {                
                RegistryKey subkey = unistallinfokey.OpenSubKey(keyname,true);
                if (subkey != null)
                { 
                    //get the key values
                    string DisplayName = (string)subkey.GetValue("DisplayName");
                    string[] UnistallInfo = new string[4]; // DisplayVersion;Publisher;InstallLocation;UninstallString
                    UnistallInfo[0] = (string)subkey.GetValue("DisplayVersion");
                    UnistallInfo[1] = (string)subkey.GetValue("Publisher");
                    UnistallInfo[2] = (string)subkey.GetValue("InstallLocation");
                    UnistallInfo[3] = (string)subkey.GetValue("UninstallString");

                    if (DisplayName != null && DisplayName != "")
                    {
                        if (!uninstallinfo.ContainsKey(DisplayName))
                            uninstallinfo.Add(DisplayName, UnistallInfo);
                    }

                }
            }

            return 0;

        }

        private static int GetFatalSoftwares(string filename, Dictionary<string, string> softwares)
        {
            Encoding ansi = Encoding.GetEncoding("GB2312");

            if (null == filename || null == softwares)
                return -1;

            if (!File.Exists(filename))
                return -2;

            string[] lines = File.ReadAllLines(filename,ansi);
            char[] splitchars = { ',' , ' '};
            foreach (string s in lines)
            {
                string[] softinfo = s.Split(splitchars);
                if (!softwares.ContainsKey(softinfo[0])) 
                {
                    softwares.Add(softinfo[0], softinfo[1]);
                }
            }

            return 0;
        }
        static void Main(string[] args)
        {
            int ret = 0;
            Dictionary<string, string[]> uninstallInfo = new Dictionary<string, string[]>();
            Dictionary<string, string> fatalsoftware = new Dictionary<string, string>();
            string fatalsoftfile = @"fatalsoft.txt";

            ret = GetFatalSoftwares(fatalsoftfile, fatalsoftware);
            if (ret < 0)
            {
                Console.WriteLine("Get fatal software Error: {0}", ret);
            }

            ret = GetUninstallInfo(uninstallInfo);
            if (0 == ret)
            {
                string tmpbuff = "";
                int i = 0;
 
                Encoding ansi = Encoding.GetEncoding("GB2312");
                const string FileName = @"unisinfo.log";
                if (File.Exists(FileName))
                    File.Delete(FileName);

                foreach (KeyValuePair<string, string[]> ht in uninstallInfo)
                {
                    string[] uninsinfo = (string[])ht.Value;
                    /*
                    i++;
                    tmpbuff += "----- " + i + ": " + ht.Key + " -----" + Environment.NewLine;
                    tmpbuff += "Softname: " + ht.Key + Environment.NewLine;
                    tmpbuff += "Version: " + uninsinfo[0] + Environment.NewLine;
                    tmpbuff += "Publisher: " + uninsinfo[1] + Environment.NewLine;
                    tmpbuff += "InstallLocation: " + uninsinfo[2] + Environment.NewLine;
                    tmpbuff += "UninstallString: " + uninsinfo[2] + Environment.NewLine;
                    */
                    bool bContain = false;
                    foreach (KeyValuePair<string, string> soft in fatalsoftware)
                    {
                        if (soft.Key.Length <= 0)
                            continue;

                        if (ht.Key.ToLower().Contains(soft.Key.ToLower()))
                        {
                            bContain = true;
                            break;
                        }
                    }

                    if (bContain)
                    {
                        i++;
                        tmpbuff += "----- " + i + ": " + ht.Key + " -----" + Environment.NewLine;
                        tmpbuff += "Softname: " + ht.Key + Environment.NewLine;
                        tmpbuff += "Version: " + uninsinfo[0] + Environment.NewLine;
                        tmpbuff += "Publisher: " + uninsinfo[1] + Environment.NewLine;
                        tmpbuff += "InstallLocation: " + uninsinfo[2] + Environment.NewLine;
                        tmpbuff += "UninstallString: " + uninsinfo[2] + Environment.NewLine;
                    }
                     
                }

                File.AppendAllText(FileName, tmpbuff, ansi);
            }

            Console.Read();
        }
    }
}

