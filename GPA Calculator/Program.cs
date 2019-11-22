using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPA_Calculator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());
            /*
            var appSettings = ConfigurationManager.AppSettings;
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            Console.WriteLine("Welcome to the GPA Calculator created by William Zhang!" +
                              "Before using, make sure you have Microsoft Server SQL Installed." +
                              "Type 'help' here to view some available commands.");
            var input = Console.ReadLine();
            switch (input)
            {
                case "help":
                    Console.WriteLine(Helpers.HelpCmd());
                    break;
                case "defaultUser":
                    Console.WriteLine(appSettings["defaultName"] + "\nis already set as the default user. Continue? (y/n)");
                    input = Console.ReadLine();
                    var yn = input[0];
                    if (yn == 'y')
                    {
                        Console.WriteLine(Variables.cmdDictionary["defaultUser"]);
                        input = Console.ReadLine();
                        bool valid = false;
                        do
                        {
                            try
                            {
                                string pattern = @"\s--\s";
                                string[] elements = System.Text.RegularExpressions.Regex.Split(input, pattern);

                                settings["defaultName"].Value = elements[0];
                                settings["defaultUsername"].Value = elements[1];
                                settings["defaultPassword"].Value = elements[2];
                                configFile.Save(ConfigurationSaveMode.Modified);
                                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                                Console.WriteLine($"Successfully set\n{input}\n as the default user");
                                valid = true;
                            }
                            catch
                            {
                                Console.WriteLine(
                                    "You wrote the name, username and password in the wrong format! Try again!");
                                valid = false;
                            }
                        } while (valid != true);

                    }

                    break;

            }*/
        }
    }
    //public class TextBoxWriter : TextWriter
    //{
    //    // The control where we will write text.
    //    private Control MyControl;
    //    public TextBoxWriter(Control control)
    //    {
    //        MyControl = control;
    //    }

    //    public override void Write(char value)
    //    {
    //        MyControl.Text += value;
    //    }

    //    public override void Write(string value)
    //    {
    //        MyControl.Text += value;
    //    }

    //    public override Encoding Encoding
    //    {
    //        get { return Encoding.Unicode; }
    //    }
    //}
}
