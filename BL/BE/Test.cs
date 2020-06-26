using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BE
{
    public class Test : ICloneable
    {
        private DateTime date;
        private  List<Criterion> criterions= new List<Criterion>();

        public Test(DateTime date, List<Criterion> criterions, string tester_ID, CarTip carTip, string trainee_ID, Addres test_Location, bool? success, string tester_Comment)
        {
            this.date = date;
            Tester_ID = tester_ID;
            Trainee_ID = trainee_ID;
            Test_Type = carTip;

            Test_Location = test_Location;
            //this.criterions = criterions;
            Success = success;
            Tester_Comment = tester_Comment;
        }
        //מוסיף שלוש ימים לטסט הבא ומקבל את היום התקין בחודש
        private DateTime addDate ()
        {
            DateTime v = DateTime.Now.AddDays(3);
            if (v.DayOfWeek == DayOfWeek.Friday)
                v= v.AddDays(2);
            if (v.DayOfWeek == DayOfWeek.Saturday)
                v= v.AddDays(1);
            return v;
        }
        public Test()
        {
            Success = null;
            date = new DateTime(addDate().Year, addDate().Month, addDate().Day, 9, 0, 0);
            //criterions = new List<Criterion>();
            //criterions.Add(new Criterion());
            //criterions.Add(new Criterion());
            //criterions.Add(new Criterion());
            //criterions.Add(new Criterion());
            //criterions.Add(new Criterion());
        }


        public string Test_Number { get; set; }
        public string Tester_ID { get; set; }
        public string Trainee_ID { get; set; }
        public DateTime Test_Date { get=>date; set=>date=value; }
        public TimeSpan Test_Time { get; set; }//////////////////////////////////
        public Addres Test_Location { get; set; }
        public List<Criterion> Criterionss { get=>criterions; set=>criterions=value; }
        public bool? Success { get; set; }
        public string Tester_Comment { get; set; }
        public CarTip Test_Type { get; set; }
        public override string ToString()
        {
            return "Number test is:"+Test_Number+" tester id:"+Tester_ID+"  trainee id:"+Trainee_ID;
        }

        public List<Criterion> cloneCriterions()
        {
            List<Criterion> toReturn = new List<Criterion>();
            foreach (var item in criterions)
            {
                toReturn.Add(item.Clone() as Criterion);
            }
            return toReturn;
        }
        public object Clone()
        {
            var d = new Test()
            {
                Criterionss = cloneCriterions(),
                Tester_ID = Tester_ID,
                Trainee_ID = Trainee_ID,
                Test_Number = Test_Number,
                Tester_Comment = Tester_Comment,
                Test_Date = Test_Date,
                Test_Location = Test_Location,
                Test_Time = Test_Time,
                Test_Type = Test_Type,
                Success = Success

            };
            return d;
        }
    }
}
