using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.Addres;


namespace BE
{
    public class person 
    {
        public string ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender gender { get; set; }
        public string PhoneNumber { get; set; }
        public Addres addres { get; set; }
        public person() { addres = new Addres(); }
        public person(string iD, string lastName, string firstName, DateTime dateOfBirth, Gender gender, string phoneNumber, Addres addres)
         {
            ID = iD;
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
            this.gender = gender;
            PhoneNumber = phoneNumber;
            this.addres = addres;
         }

        public override string ToString()
        {
            return null;
        }
    }

}
