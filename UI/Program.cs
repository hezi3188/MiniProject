using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;
namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
           Bl_imp bl = Bl_imp.GetInstance();

                bl.Add_Tester(new Tester("318834579", "BenAtar", "Yehezkel1", new DateTime(1960, 9, 2), Gender.male, "0543411320", new Addres("jerusalem", "fatal", 45), 10, 3, CarTip.HeavyTruckGear, 100.5,
                    new bool[5, 6]
                {
                {true,true,false,false,false,false },
                { false,false,false,false,false,false},
                { false,false,false,false,false,false},
                {false,false,false,false,false,false },
                {false,false,false,false,false,false },
                }));
                bl.Add_Tester(new Tester("304883796", "Horvitz", "Meir", new DateTime(1970, 6, 01), Gender.male, "0543454320", new Addres("jerusalem", "fatal", 45), 10, 5, CarTip.HeavyTruckGear, 100.5,
                    new bool[5, 6]
                {
                {false,false,false,false,false,false },
                { false,false,false,false,false,false},
                { false,false,false,false,false,false},
                {false,false,false,false,false,false },
                {false,false,false,false,false,false },
                }));


                bl.Add_Trainee(new Trainee("338760804", "atar", "david", new DateTime(1999, 9, 9), Gender.male, "0543411320", new Addres("jerusalem", "fatal", 45), CarTip.HeavyTruckGear, "or", "Meir horviz", 20));
                List<Criterion> c = new List<Criterion>() { new Criterion("parking",true), new Criterion("speed",true), new Criterion("sides") };
//              bl.Add_Test(new Test(DateTime.Now, c, "318834579", CarTip.PrivateCarAutomatic, "338760804", new Addres("jerusalem", "fatal", 45), true, "completly"));

                bl.Add_Trainee(new Trainee("058457227", "atar", "david", new DateTime(1990, 10, 1), Gender.male, "0543411320", new Addres("jerusalem", "fatal", 45), CarTip.HeavyTruckGear, "or", "Meir horviz", 20));


            

            try
            {
                Test test = new Test();
                test.Test_Type = CarTip.HeavyTruckGear;
                test.Trainee_ID = "058457227";
                bl.Add_Test(test);
                Console.WriteLine(test.Test_Date);
                Console.WriteLine(test.Tester_ID);

                Test test2 = new Test();
                test2.Test_Type = CarTip.HeavyTruckGear;
                test2.Trainee_ID = "058457227";
                bl.Add_Test(test2);

                Console.WriteLine(test2.Test_Date);
                Console.WriteLine(test2.Tester_ID);
                Test test3 = new Test();
                test3.Test_Type = CarTip.HeavyTruckGear;
                test3.Trainee_ID = "338760804";
                bl.Add_Test(test3);
                Console.WriteLine(test3.Test_Date);
                Console.WriteLine(test3.Tester_ID);
                foreach (var item in bl.GroupTrainee_By_School().Single(item=>item.Key=="or"))
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Source);
                Console.WriteLine(e.TargetSite);

                Console.WriteLine( e.Message);
            }




        }
    }
}
