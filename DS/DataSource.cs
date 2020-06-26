using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DS
{
    public class DataSource
    {
       static List<Tester> testers = new List<Tester>();
       static List<Trainee> trainees=new List<Trainee>();
       static List<Test> tests=new List<Test>();

        public static List<Tester> Testers { get => testers; set => testers = value; }
        public static List<Trainee> Trainees { get => trainees; set => trainees = value; }
        public static List<Test> Tests { get => tests; set => tests = value; }

    }
}
