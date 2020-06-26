using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using BE;
using DAL;
namespace BL
{
   public class Bl_imp : IBL
    {
        Idal dal = Dal_XML_imp.GetInstance();
        private Bl_imp() { }
        private static Bl_imp instance = null;

        public static Bl_imp GetInstance()
        {
            if (instance == null)
                instance = new Bl_imp();
            return instance;

        }
        #region Add
        public bool Add_Test(Test test)
        {
            #region Conditions
            if (dal.Get_Test().Any(item => item.Test_Number == test.Test_Number))
                throw new Exception("the test is already exist");
            if (!dal.Get_Treinee().Any(item => item.ID == test.Trainee_ID))
                throw new Exception("The trainee is not exist!");
            Trainee trainee = get_Trainee(test.Trainee_ID);
            if (trainee.Trainee_CarTip != test.Test_Type)
                throw new Exception("the trainee no learn about this vecicles!");
            if(GroupTestByInvalid().Any(item=>item.Key==true))
            {
                if (GroupTestByInvalid().Single(item => item.Key == true).Any(item => item.Trainee_ID == trainee.ID&&item.Test_Type==trainee.Trainee_CarTip))
                    throw new Exception("You have already test valid in this vecicel!");
            }
            if(dal.Get_Test().Any(item=>item.Trainee_ID==trainee.ID&&!IsValidTest(item)&&item.Success==null))
            {
                    throw new Exception("You have test that doesn't update go check!");
            }       
            while (!Condition_Days_Beetwin_Tests(test))
            {
                test.Test_Date =test.Test_Date.AddDays(1);
            }
            DateTime temp = test.Test_Date;
            test= Find_Test(test);
            if (test == null)
                throw new Exception("no tester for 2 mounth please return later!");
            if (test == null)
                throw new Exception("No tests in half year sorry!");
            Tester tester = get_Tester(test.Tester_ID);
            Condition_Number_Lesson(trainee);
            Condition_Avilable_Test(test);
            Condition_Same_Test_in_Day(test);
            if(!Condition_Is_Already_Succeeded(trainee))
                throw new Exception("Trainee id:" + trainee.ID + " is already have license for this vecicle!");
            Condition_Match_Trainee_Tester(test);
            #endregion
            //all the conditions succeded
            trainee.Num_Tests++;
            dal.Update_Trainee(trainee);
            dal.Add_Test(test);
            return true;
        }
        public bool Add_Tester(Tester tester)
        {
            if (!CheckIDNo(tester.ID))
                throw new Exception("The id is error!");
            if (dal.Get_Tester().Any(item => item.ID == tester.ID))
            {
                throw new Exception("The tester is already exist!");
            }
            if (Condition_Tester_Age(tester))
                dal.Add_Tester(tester);
            return true;
        }
        public bool Add_Trainee(Trainee trainee)
        {
            if (!CheckIDNo(trainee.ID))
                throw new Exception("The id is error!");
            if (dal.Get_Treinee().Any(item => item.ID == trainee.ID))
                throw new Exception("The trainee is already exist!");
            if (Condition_Trainee_Age(trainee))
                dal.Add_Trainee(trainee);
            return true;
        }
        #endregion

        #region Get
        public IEnumerable<Test> Get_Test(Func<Test, bool> predicate = null)
        {
            return dal.Get_Test(predicate);
        }
        public IEnumerable<Tester> Get_Tester(Func<Tester, bool> predicate = null)
        {
            return dal.Get_Tester(predicate);
        }
        public IEnumerable<Trainee> Get_Treinee(Func<Trainee, bool> predicate = null)
        {
            return dal.Get_Treinee(predicate);
        }
        public Tester get_Tester(string ID)
        {
            return dal.get_Tester(ID);
        }
        public Test getTest(string testNumber)
        {
            return dal.get_Test(testNumber);
        }
        public Trainee get_Trainee(string ID)
        {
            return dal.get_Trainee(ID);
        }
        #endregion

        #region Remove
        public bool Remove_Test(Test test)
        {
            if (Get_Test().Any(item => item.Test_Number == test.Test_Number))
            {
                //delete number for the trainee
                try
                {

                    var v = get_Trainee(test.Trainee_ID);
                    
                    v.Num_Tests--;
                }
                catch (Exception)
                {
                }
                ///////////////////////////////
                dal.Remove_Test(test);
                return true;
            }
            else
                throw new Exception("The test is not exist!");
        }
        public bool Remove_Tester(Tester tester)
        {
            if (dal.Get_Tester().Any(item => item.ID == tester.ID))
            {
                dal.Remove_Tester(tester);
                return true;
            }
            else
                throw new Exception("The tester is not exist!");
        }
        public bool Remove_Trainee(Trainee trainee)
        {
            if (Get_Treinee().Any(item => item.ID == trainee.ID))
            {
                if(GroupTestByInvalid().Any(item=>item.Key==true))
                {
                    foreach (var item in GroupTestByInvalid().Single(item => item.Key == true))
                    {
                        if (item.Trainee_ID == trainee.ID)
                            dal.Remove_Test(dal.get_Test(item.Test_Number));
                    }
                }
                dal.Remove_Trainee(trainee);
                return true;
            }
            else
                throw new Exception("The trainee is not exist!");
        }
        #endregion

        #region Update
        public bool Update_Test(Test test)
        {
            if(!Get_Test(item=>item.Test_Number==test.Test_Number).Any())
                throw new Exception("The test is not found!");
           if(!IsValidTest(test))
            {
                if (test.Success != null && test.Tester_Comment != "" && test.Criterionss.Count >= 2)
                {
                    dal.Update_Test(test);
                    return true;
                }
                throw new Exception("The test doesn't update because you don't update all the conditions and 2 criterions!");
            }
            throw new Exception("The test valid, you cant update!");
        }
        public bool Update_Tester(Tester tester)
        {
            Condition_Tester_Age(tester);
            dal.Update_Tester(tester);
            return true;
        }
        public bool Update_Trainee(Trainee trainee)
        {
            Condition_Trainee_Age(trainee);
            dal.Update_Trainee(trainee);
            return true;
        }
        #endregion

        #region Grouping
        public IEnumerable<IGrouping<int, Tester>> GroupTesters_By_Experience(CarTip carTip, bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Tester()
                       orderby item.ID
                       group item by item.Tester_Experience;
            }
            else
            {
                return from item in dal.Get_Tester()
                       group item by item.Tester_Experience;
            }
        }
        public IEnumerable<IGrouping<string, Trainee>> GroupTrainee_By_School( bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Treinee()
                       orderby item.ID
                       group item by item.School_name;
            }
            else
            {
                return from item in dal.Get_Treinee()
                       group item by item.School_name;
            }
        }
        public IEnumerable<IGrouping<string, Trainee>> GroupTrainee_By_Teacher(bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Treinee()
                       orderby item.ID
                       group item by item.My_Teacher;
            }
           else 
            {
                return from item in dal.Get_Treinee()
                       group item by item.My_Teacher;
            }
           
        }
        public IEnumerable<IGrouping<int, Trainee>> GroupTrainee_By_Num_Tests(bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Treinee()
                       orderby item.ID
                       group item by item.Num_Tests;
            }
            else
            {
                return from item in dal.Get_Treinee()
                       group item by item.Num_Tests;
            }
        }
        public IEnumerable<IGrouping<CarTip,Trainee>> GroupTraineeByType(bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Treinee()
                       orderby item.ID
                       group item by item.Trainee_CarTip;
            }
            else
            {
                return from item in dal.Get_Treinee()
                       group item by item.Trainee_CarTip;
            }
        }
        public IEnumerable<IGrouping<CarTip, Tester>> GroupTesterByType(bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Tester()
                       orderby item.ID
                       group item by item.Tester_CarTip;
            }
            else
            {
                return from item in dal.Get_Tester()
                       group item by item.Tester_CarTip;
            }
        }
        public IEnumerable<IGrouping<bool, Trainee>> GroupTraineeBySucceded(bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Treinee()
                       orderby item.ID
                       group item by !Condition_Is_Already_Succeeded(get_Trainee(item.ID));
            }
            else
            {
                return from item in dal.Get_Treinee()
                       group item by !Condition_Is_Already_Succeeded(get_Trainee(item.ID));
            }
        }
        public IEnumerable<IGrouping<bool, Test>> GroupTestBySucceded(bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Test()
                       orderby item.Test_Number
                       group item by IsSucceededHelpForGroup(item);
            }
            else
            {
                return from item in dal.Get_Test()
                       group item by IsSucceededHelpForGroup(item);
            }
        }
        public IEnumerable<IGrouping<bool, Test>> GroupTestByInvalid(bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Test()
                       orderby item.Test_Number
                       group item by IsValidTest(item);
            }
            else
            {
                return from item in dal.Get_Test()
                       group item by IsValidTest(item);
            }
        }
        public IEnumerable<IGrouping<bool, Test>> GroupTestByNeedUpdate(bool sort = false)
        {
            if (sort)
            {
                return from item in dal.Get_Test()
                       orderby item.Test_Number
                       group item by !IsValidTest(item)&&item.Success==null;
            }
            else
            {
                return from item in dal.Get_Test()
                       group item by !IsValidTest(item) && item.Success == null;
            }
        }
        /// <summary>
        /// get all testers with this addres
        /// </summary>
        /// <param name="addres"></param>
        /// <returns>get all testers with this addres</returns>
        public IEnumerable<Tester> Get_Testers_By_Address(Addres addres)
        {
            return dal.Get_Tester(item => item.Tester_MaxDistance < addres.BuildingNumber);
        }
        /// <summary>
        /// Get all testers from the list with this datatime and cartip
        /// </summary>
        /// <param name="time">specific</param>
        /// <param name="tip">Cartip of test</param>
        /// <param name="list">tester list</param>
        /// <returns>Get all testers from the list with this datatime and cartip</returns>
        public IEnumerable<Tester> Get_Testers_By_Time(DateTime time, CarTip tip, IEnumerable<Tester> list)
        {
            int i = (int)time.DayOfWeek;
            int j = time.Hour - 9;

            var v = list.Where(item => item.TesterTime[i, j] == true && item.Tester_CarTip == tip);
            if (dal.Get_Test().Any() || !v.Any())
            {
                return from tester in v
                       where !dal.Get_Test().Any(item => item.Tester_ID == tester.ID && time == item.Test_Date)
                       select tester;
            }

            return v;
        }
        /// <summary>
        /// Get all the test of the trainee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get all the test of the trainee</returns>
        public IEnumerable<Test> Get_All_Tests_Trainee(string id)
        {
            return dal.Get_Test(item => item.Trainee_ID == id);
        }
        /// <summary>
        /// Get all tester from the list that matched for the test
        /// </summary>
        /// <param name="test">the test</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public IEnumerable<Tester> getTesterByDistance(Test test, IEnumerable<Tester> list)
        {
            return from item in list
                   let x = DistanceBetween2Addresses(test.Test_Location, item.addres)
                   where x >= 0 && x < item.Tester_MaxDistance
                   select item;
        }
        #endregion

        #region Scrutiny
        public bool IsValidTest(Test item)
        {

            TimeSpan time = DateTime.Now - item.Test_Date;
            if ((DateTime.Now - item.Test_Date).Days == 0)
            {
                if ((DateTime.Now - item.Test_Date).Hours <= 0)
                    return true;
                else
                    return false;

            }
            if ((DateTime.Now - item.Test_Date).Days < 0)
                return true;
            else
                return false;
        }
        public bool Is_Succeeded_Test(Test test)
        {
            int yes = 0; int no = 0;
            foreach (var item in test.Criterionss)
            {
                if (item.Is_Succeeded == true)
                    yes++;
                else
                    no++;
            }
            if (yes > no)
                return true;
            else
                return false;

        }
        #endregion

        #region HelpConditions
        private bool Condition_Same_Test_in_Day(Test test)
        {
            if (dal.Get_Test().Any(item => item.Test_Date == test.Test_Date && item.Trainee_ID == test.Trainee_ID))
                throw new Exception("Trainee id:" + test.Trainee_ID + " have already test in this time!");
            return true;
        }
        private bool Condition_Same_Test_In_Day(Tester tester)
        {
            throw new NotImplementedException();
        }
        private bool Condition_Is_Already_Succeeded(Trainee trainee)
        {
            if (dal.Get_Test().Any(item => item.Trainee_ID == trainee.ID && item.Success == true && item.Test_Type == trainee.Trainee_CarTip))
                return false;
            return true;
        }
        private bool Condition_Match_Trainee_Tester(Test test)
        {
            Tester tester = dal.get_Tester(test.Tester_ID);
            Trainee trainee = dal.get_Trainee(test.Trainee_ID);
            if (tester.Tester_CarTip != trainee.Trainee_CarTip)
                throw new Exception("The tester and the trainee are not same car tip!");
            return true;
        }
        private bool Condition_Updata_Test(Test test)
        {
            if (test.Criterionss.All(item => item.Is_Succeeded == true || item.Is_Succeeded == false))
                throw new Exception("The tester id:" + test.Tester_ID + " is not markd all the conditions!");
            return true;
        }
        private bool Condition_Maximum_Test(Tester tester, Test test)
        {
            if (dal.Get_Test().Count(item => item.Tester_ID == tester.ID && startWeek(test.Test_Date).Date == startWeek(item.Test_Date).Date) >= tester.Tester_MaximumTestsPerWeek)
            {
                return false;
            }
            return true;
        }
        private bool Condition_Trainee_Age(Trainee trainee)
        {
            TimeSpan time = DateTime.Now - trainee.DateOfBirth;
            if ((DateTime.Now - trainee.DateOfBirth).TotalDays / 365 < Configuration.MinimumAge_Of_Studen)
                throw new Exception("The trainee is too young!");
            return true;
        }
        private bool Condition_Days_Beetwin_Tests(Test test)
        {
            Trainee trainee = dal.get_Trainee(test.Trainee_ID);
            if (trainee.Num_Tests != 0 && dal.Last_TestCondition(test.Trainee_ID))
            {
                TimeSpan span = test.Test_Date - dal.Last_Test(test.Trainee_ID);
                if ((span.Days < (Configuration.Time_Between_Tests-1)))
                    return false;
            }
            return true;
        }
        private bool Condition_Number_Lesson(Trainee trainee)
        {
            if (trainee.NumberOfLesson < Configuration.Minimum_NumberOfLessons)
                throw new Exception("The trainee id:" + trainee + " is not completed " + Configuration.Minimum_NumberOfLessons + " lessons!");
            return true;
        }
        private bool Condition_Avilable_Test(Test test)
        {
            int i = (int)test.Test_Date.DayOfWeek;
            int j = test.Test_Date.Hour - 9;
            Tester tester = dal.get_Tester(test.Tester_ID);
            if (!tester.TesterTime[i, j])
                throw new Exception("The tester no work in this time!");
            if (dal.Get_Test().Any(item => item.Tester_ID == test.Tester_ID && item.Test_Date == test.Test_Date))
            {
                throw new Exception("The tester work in this time!");
            }
            return true;
        }
        private bool Condition_Tester_Age(Tester tester)
        {
            if (DateTime.Now.Year - tester.DateOfBirth.Year < Configuration.MinimumAge_Of_Tester)
                throw new Exception("The tester is too young!");
            return true;
        }
        //public bool Is_Succeeded_Test(string id)
        //{
        //    return Get_Test().Any(item => item.Trainee_ID == id && Is_Succeeded_Test(item));
        //}
        #endregion

        #region HelpFunctions
        private bool IsSucceededHelpForGroup(Test test)
        {
            if (test.Success == true)
                return true;
            return false;
        }
        private bool CheckIDNo(String strID)
        {
            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            int count = 0;

            if (strID == null)
                return false;

            strID = strID.PadLeft(9, '0');

            for (int i = 0; i < 9; i++)
            {
                int num = Int32.Parse(strID.Substring(i, 1)) * id_12_digits[i];

                if (num > 9)
                    num = (num / 10) + (num % 10);

                count += num;
            }

            return (count % 10 == 0);
        }
        /// <summary>
        /// search the next time that match for luz
        /// </summary>
        /// <param name="time"></param>
        /// <returns>search the next time that match for luz</returns>
        private DateTime nextTime(DateTime time)
        {
            if (time.Hour < 14)
            {
                time = time.AddHours(1);
            }
            else
            {
                if (time.DayOfWeek == DayOfWeek.Thursday)
                {
                    time = time.AddDays(2);
                }
                time = time.AddDays(1);
                time = time.AddHours(-5);
            }
            return time;
        }
        /// <summary>
        /// return the date that start this(now) week
        /// </summary>
        /// <param name="date"></param>
        /// <returns>return the date that start the week</returns>
        private DateTime startWeek(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Sunday)
            {
                date = date.AddDays(-1);
            }
            return date;
        }
        private Test Find_Test(Test test)
        {
            Trainee trainee = get_Trainee(test.Trainee_ID);
            DateTime date = test.Test_Date;
            DateTime temp = DateTime.Now.AddMonths(1);
            var a = getTesterByDistance(test, dal.Get_Tester()).ToList();
            if (a.Any())
            {
                while (date <= temp)
                {
                    IEnumerable<Tester> v = Get_Testers_By_Time(date, test.Test_Type, a);
                    if (!v.Any())
                    {
                        date = nextTime(date);
                        test.Test_Date = date;

                    }
                    else
                    {
                        foreach (var item in v)
                        {
                            if (item.Tester_CarTip == trainee.Trainee_CarTip && Condition_Maximum_Test(item, test))
                            {
                                test.Tester_ID = item.ID;
                                test.Test_Date = date;
                                return test;
                            }
                        }
                        date = nextTime(date);

                        test.Test_Date = date;

                    }
                }

            }
            return null;
        }
        /// <summary>
        /// Calculates the distance between addresses
        /// </summary>
        /// <param name="a1">origin</param>
        /// <param name="a2">destination</param>
        /// <returns>
        /// Return -1:one of the addresses is error
        /// Return -2: the server is busy or other error
        /// Else: return the ditance between the addresses
        /// </returns>
        private double DistanceBetween2Addresses(Addres a1, Addres a2)
        {
            string origin = a1.ToString(); //or "תקווה פתח 100 העם אחד "etc.
            string destination = a2.ToString();//or "גן רמת 10 בוטינסקי'ז "etc.
            string KEY = @"8qaS1JqJRWw4aAYIb7hA1wvdQehGQ54Z";
            string url = @"https://www.mapquestapi.com/directions/v2/route" +
             @"?key=" + KEY +
             @"&from=" + origin +
             @"&to=" + destination +
             @"&outFormat=xml" +
             @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
             @"&enhancedNarrative=false&avoidTimedConditions=false";
            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response;
            try
            {
                response = request.GetResponse();
            }
            catch (Exception)
            {

                throw new Exception("No conection please try again later!");
            }
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                return distInMiles * 1.609344;
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            //we have an answer that an error occurred, one of the addresses is not found
            {
                return -1;
            }
            else //busy network or other error...
            {
                return -2;
            }

        }
        /// <summary>
        /// Print all the luz of this specific tester
        /// </summary>
        /// <param name="tester"></param>
        private void getTesterTime(Tester tester)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (tester.TesterTime[i, j])
                    {
                        DayOfWeek d = (DayOfWeek)i;
                        int h = j + 9;
                        Console.WriteLine("{0}, {1:00}:00", d, h);
                    }

                }
            }
        }
        #endregion
    }

}
