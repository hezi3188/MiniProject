using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Addres
    {
        public Addres(){}
        public Addres(string city, string street, int buildingNumber)
        {
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
        }

        public string City { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }

        public override string ToString()
        {
            return Street + " " + BuildingNumber+ "st. "+ City;
        }
    }
    
}
