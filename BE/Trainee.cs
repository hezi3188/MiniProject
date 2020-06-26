using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Trainee:person,ICloneable
    {
        public Trainee(string iD, string lastName, string firstName, DateTime dateOfBirth, Gender gender, string phoneNumber, Addres addres,CarTip trainee_CarTip,  string school_name, string my_Testers_Name, int numberOfLesson):base(iD,lastName,firstName,dateOfBirth,gender,phoneNumber,addres)
        {
            Num_Tests = 0;
            Trainee_CarTip = trainee_CarTip;
            School_name = school_name;
            My_Teacher = my_Testers_Name;
            NumberOfLesson = numberOfLesson;
        }
        public Trainee()
        {
            DateOfBirth = DateTime.Now.AddYears(-18);
        }

        public CarTip Trainee_CarTip { get; set; }
        public string School_name { get; set; }
        public string My_Teacher { get; set; }
        public int NumberOfLesson { get; set; }
        public int Num_Tests { get; set; }

        public object Clone()
        {
            var s = new Trainee()
            {
                ID = ID,
                FirstName = FirstName,
                LastName = LastName,
                gender = gender,
                addres =addres,
                Trainee_CarTip = Trainee_CarTip,
                My_Teacher = My_Teacher,
                DateOfBirth = DateOfBirth,
                PhoneNumber = PhoneNumber,
                Num_Tests = Num_Tests,
                NumberOfLesson = NumberOfLesson,
                School_name = School_name
            };
            return s;
        }

        public override string ToString()
        {
            return "First name: " + FirstName + " id: " + ID;
        }
    }
}
