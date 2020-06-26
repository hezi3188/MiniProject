using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  public  class Tester:person,ICloneable
    {
        public bool[,] TesterTime = new bool[5, 6];
        public int Tester_Experience { get; set; }
        public int Tester_MaximumTestsPerWeek { get; set; }
        public int Tester_Tests { get; set; }
        public CarTip Tester_CarTip { get; set; }
        public double Tester_MaxDistance { get; set; }
        public Tester()
        {
            DateOfBirth =  DateTime.Now.AddYears(-BE.Configuration.MaximumAge_Of_Tester);
        }
        public Tester(string iD, string lastName, string firstName, DateTime dateOfBirth, Gender gender, string phoneNumber, Addres addres, int tester_Experience, int tester_MaximumTestsPerWeek, CarTip tester_CarTip, double tester_MaxDistance, bool[,] testerTime) : base(iD, lastName, firstName, dateOfBirth, gender,phoneNumber,addres)
        {
            Tester_Experience = tester_Experience;
            Tester_MaximumTestsPerWeek = tester_MaximumTestsPerWeek;
            Tester_CarTip = tester_CarTip;
            Tester_MaxDistance = tester_MaxDistance;
            TesterTime = testerTime;
            Tester_Tests = 0;
        }

        public override string ToString()
        {
            return "First name: "+FirstName+" id: "+ID;
        }

        public object Clone()
        {
            var t = new Tester()
            {
                ID = ID,
                gender = gender,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Tester_Experience = Tester_Experience,
                Tester_MaxDistance = Tester_MaxDistance,
                Tester_MaximumTestsPerWeek = Tester_MaximumTestsPerWeek,
                Tester_CarTip = Tester_CarTip,
                Tester_Tests = Tester_Tests,
                addres = addres,
                DateOfBirth = DateOfBirth
                
            };
            var sch = new bool[5, 6];
            if (t.TesterTime != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        sch[i, j] = this.TesterTime[i, j];
                    }
                }
                t.TesterTime = sch;
            }
            return t;
            }
        }
    }
  
   

