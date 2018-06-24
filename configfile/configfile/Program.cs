using System;
using System.Collections.Specialized;
using System.Configuration;

namespace AspNet.Samples
{
    class UsingAppSettingsSection
    {
        static void ShowAppSettings()
        {


            AppSettingsReader reader =
                new AppSettingsReader();


            NameValueCollection appStgs =
                ConfigurationManager.AppSettings;

            string[] names =
                ConfigurationManager.AppSettings.AllKeys;

            String value = String.Empty;

            for (int i = 0; i < appStgs.Count; i++)
            {


                string key = names[i];

                value = (String)reader.GetValue(key, value.GetType());

                Console.WriteLine("#{0} Name: {1} Value: {2}",
                  i, key, value);

            }

        }

        static void Main(string[] args)
        {

            // Get the count of the Application Settings.
            int appStgCnt = ConfigurationManager.AppSettings.Count;

            string asName = "AppStg" + appStgCnt.ToString();

            // Get the configuration file.
            System.Configuration.Configuration config =
              ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Add an Application setting.
            config.AppSettings.Settings.Add(asName,
              DateTime.Now.ToLongDateString() + " " +
              DateTime.Now.ToLongTimeString());

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");


            Console.WriteLine();
            Console.WriteLine("Application Settings:");
            ShowAppSettings();
            Console.WriteLine();

            Console.WriteLine("Press 'Enter' to exit.");
            Console.ReadLine();
        }
    }
}


