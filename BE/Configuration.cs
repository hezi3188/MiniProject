using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
namespace BE
{
    public class Configuration
    {
        public static string path = @"XMLConfiguration.xml";
        public static XElement config;
        public static void LoadFile()
        {
            if(!File.Exists(path))
            {
                config = new XElement("Configuration", new XElement("testNumber", 1));
                config.Save(path);
            }
            config = XElement.Load(path);
        }
        public static string AdminUsername = "Admin";
        public static string AdminPassword = "123";
        public static int Minimum_NumberOfLessons = 20;
        public static int MaximumAge_Of_Tester = 80;
        public static int MinimumAge_Of_Tester = 40;
        public static int MinimumAge_Of_Studen = 18;
        public static int MeximiumAge_Of_Studen = 120;
        public static int Time_Between_Tests = 7;
        public static int code
        {
            get
            {
                LoadFile();
                return int.Parse(config.Element("testNumber").Value);
            }
            set
            {
                XElement testNumber = new XElement("testNumber", value);
                XElement element = new XElement("Configuration", testNumber);
                element.Save(path);
            }
        }
    }
}
