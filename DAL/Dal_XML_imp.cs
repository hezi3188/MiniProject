using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using BE;
using DS;

namespace DAL
{
    public class Dal_XML_imp : Idal
    {
        #region InitialSettings
        XElement testers, tests, trainees;
        string FPathTesters = @"Testerss.xml";
        string FPathTestes = @"Tests.xml";
        string FPathTrainees = @"Trainees.xml";

        private static Dal_XML_imp instance = null;
        private Dal_XML_imp()
        {
            if (!File.Exists(FPathTesters))
            {
                CreateFiles();
            }
            DataSource.Trainees = LoadFromXML<List<Trainee>>(FPathTrainees);
            DataSource.Tests = LoadFromXML<List<Test>>(FPathTestes);
            DataSource.Testers = LoadTestersFromXMLToList();
        }
        public static Dal_XML_imp GetInstance()
        {
            if (instance == null)
            {
                instance = new Dal_XML_imp();
            }
             return instance;
        }
        private void CreateFiles()
        {
            tests = new XElement("Tests");
            trainees = new XElement("Trainees");
            tests.Save(FPathTestes);
            trainees.Save(FPathTrainees);
        }
        /// <summary>
        /// Load testers data from XML
        /// </summary>
        private void LoadDataTesters()
        {
            try
            {
                testers = XElement.Load(FPathTesters);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        #endregion

        #region add
        public bool Add_Trainee(Trainee trainee)
        {
            if (DataSource.Trainees.Any(item => item.ID == trainee.ID))
                throw new Exception("The trainee is already exist!");
            DataSource.Trainees.Add(trainee);
            SaveToXML<List<Trainee>>(DataSource.Trainees, FPathTrainees);
            return true;
        }
        public bool Add_Test(Test test)
        {
            test.Test_Number = string.Format("{0:00000000}", Configuration.code);
            Configuration.code++;
            DataSource.Tests.Add(test);
            SaveToXML<List<Test>>(DataSource.Tests, FPathTestes);
            return true;
        }
        public bool Add_Tester(Tester tester)
        {
            if (DataSource.Testers.Any(item => item.ID == tester.ID))
                throw new Exception("The tester is already exist!");
            DataSource.Testers.Add(tester);
            saveTestersToXml(DataSource.Testers);
            return true;
        }
        #endregion add

        #region Get
        public Tester get_Tester(string ID)
        {
            if (DataSource.Testers.Any(item => item.ID == ID))
                return DataSource.Testers.First(item => item.ID == ID);
            throw new Exception("No this id!");
        }
        public Test get_Test(string test_number)
        {
            if (DataSource.Tests.Any(item => item.Test_Number == test_number))
                return DataSource.Tests.First(item => item.Test_Number == test_number);
            throw new Exception("No this test!");
        }
        public Trainee get_Trainee(string ID)
        {
            if (DataSource.Trainees.Any(item => item.ID == ID))
                return DataSource.Trainees.First(item => item.ID == ID);
            throw new Exception("No this id!");
        }
        public IEnumerable<Tester> Get_Tester(Func<Tester, bool> predicate = null)
        {
            var List = new List<Tester>();
            foreach (var item in DataSource.Testers)
            {
                List.Add(item.Clone() as Tester);
            }
            if (predicate == null)
            {
                return List;
            }
            return List.Where(predicate);
        }
        public IEnumerable<Trainee> Get_Treinee(Func<Trainee, bool> predicate = null)
        {
            var List = new List<Trainee>();
            foreach (var item in DataSource.Trainees)
            {
                List.Add(item.Clone() as Trainee);
            }
            if (predicate == null)
            {
                return List;
            }
            return List.Where(predicate);
        }
        public IEnumerable<Test> Get_Test(Func<Test, bool> predicate = null)
        {

            var List = new List<Test>();
            foreach (var item in DataSource.Tests)
            {
                List.Add(item.Clone() as Test);
            }
            if (predicate == null)
            {
                return List;
            }
            return List.Where(predicate);
        }
        #endregion 

        #region remove
        public bool Remove_Test(Test test)
        {

            if (DataSource.Tests.Any(item => item.Test_Number == test.Test_Number))
            {
                DataSource.Tests.Remove(test);
                SaveToXML<List<Test>>(DataSource.Tests, FPathTestes);
                return true;
            }
            else
                throw new Exception("The test is not exist!");
        }

        public bool Remove_Tester(Tester tester)
        {
            if (DataSource.Testers.Any(item => item.ID == tester.ID))
            {

                DataSource.Testers.Remove(tester);
                saveTestersToXml(DataSource.Testers);
                return true;
            }

            else
                throw new Exception("The tester is not exist!");
        }

        public bool Remove_Trainee(Trainee trainee)
        {
            if (DataSource.Trainees.Any(item => item.ID == trainee.ID))
            {
                DataSource.Trainees.Remove(trainee);
                SaveToXML<List<Trainee>>(DataSource.Trainees, FPathTrainees);
                return true;
            }

            else
                throw new Exception("The trainee is not exist!");
        }
        #endregion remove

        #region update
        public bool Update_Test(Test test)
        {
            int index = DataSource.Tests.FindIndex(item => item.Test_Number == test.Test_Number);
            if (index == -1)
                throw new Exception("The test is not found!");
            else
            {
                DataSource.Tests[index] = test;
                SaveToXML<List<Test>>(DataSource.Tests, FPathTestes);
                return true;
            }
        }

        public bool Update_Tester(Tester tester)
        {
            int index = DataSource.Testers.FindIndex(item => item.ID == tester.ID);
            if (index == -1)
                throw new Exception("The tester is not found!");
            else
            {
                DataSource.Testers[index] = tester;
                saveTestersToXml(DataSource.Testers);
                return true;
            }
        }
        public bool Update_Trainee(Trainee trainee)
        {
            int index = DataSource.Trainees.FindIndex(item => item.ID == trainee.ID);
            if (index == -1)
                throw new Exception("The trainee is not found!");
            else
            {
                DataSource.Trainees[index] = trainee;
                SaveToXML<List<Trainee>>(DataSource.Trainees, FPathTrainees);
                return true;
            }
        }
        #endregion update

        #region ADDITIONAL
        /// <summary>
        /// The condition for function last tast
        /// </summary>
        /// <param name="Trainee_Id"> Condition for the specific trainee</param>
        /// <returns>Returen true or false</returns>
        public bool Last_TestCondition(string Trainee_Id)
        {
            if (DataSource.Tests.Any(item => item.Trainee_ID == Trainee_Id))
            {
                return true;

            }
            return false;
        }
        /// <summary>
        /// Returen the last test for trainee
        /// Warning: You must to activate the function with the func Last_TestCondition
        /// </summary>
        /// <param name="Trainee_Id"></param>
        /// <returns></returns>
        public DateTime Last_Test(string Trainee_Id)
        {
            if (DataSource.Tests.Any(item => item.Trainee_ID == Trainee_Id))
                return DataSource.Tests.Last(item => item.Trainee_ID == Trainee_Id).Test_Date;
            throw new Exception("NO trainee id:" + Trainee_Id + " in system!");
        }
        #endregion

        #region XML
        //for tests and trainees
        private static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }
        //for tests and trainees
        private static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }
        //for only testers
        private void saveTestersToXml(List<Tester> list)
        {
            testers = new XElement("Testers",
                from p in list
                select new XElement("Tester",
                 new XElement("Id", p.ID),
                 new XElement("FirstName", p.FirstName),
                 new XElement("LastName", p.LastName),
                 new XElement("DateOfBirth", p.DateOfBirth),
                 new XElement("gender", p.gender),
                 new XElement("PhoneNumber", p.PhoneNumber),
                 new XElement("addres", new XElement("City", p.addres.City), new XElement("Street", p.addres.Street), new XElement("BuildingNumber", p.addres.BuildingNumber)),
                 new XElement("Tester_Experience", p.Tester_Experience),
                 new XElement("Tester_MaximumTestsPerWeek", p.Tester_MaximumTestsPerWeek),
                 new XElement("Tester_Tests", p.Tester_Tests),
                 new XElement("Tester_CarTip", p.Tester_CarTip),
                 new XElement("Tester_MaxDistance", p.Tester_MaxDistance),
                 new XElement(getTesterTime(p))));
            testers.Save(FPathTesters);
        }
        private List<Tester> LoadTestersFromXMLToList()
        {
            LoadDataTesters();
            List<Tester> testersList;
            try
            {
                testersList = (from p in testers.Elements()
                               select new Tester()
                               {
                                   ID = p.Element("Id").Value,
                                   FirstName = p.Element("FirstName").Value,
                                   LastName = p.Element("LastName").Value,
                                   DateOfBirth = DateTime.Parse(p.Element("DateOfBirth").Value),
                                   gender = (BE.Gender)Enum.Parse(typeof(BE.Gender), p.Element("gender").Value),
                                   PhoneNumber = p.Element("PhoneNumber").Value,
                                   addres = new Addres(p.Element("addres").Element("City").Value, p.Element("addres").Element("Street").Value, int.Parse(p.Element("addres").Element("BuildingNumber").Value)),
                                   Tester_Experience = int.Parse(p.Element("Tester_Experience").Value),
                                   Tester_MaximumTestsPerWeek = int.Parse(p.Element("Tester_MaximumTestsPerWeek").Value),
                                   Tester_Tests = int.Parse(p.Element("Tester_Tests").Value),
                                   Tester_CarTip = (BE.CarTip)Enum.Parse(typeof(BE.CarTip), p.Element("Tester_CarTip").Value),
                                   Tester_MaxDistance = int.Parse(p.Element("Tester_MaxDistance").Value),
                                   TesterTime = GetTesterTimeFromXml(p.Element("TesterTime"))
                               }).ToList();
            }
            catch
            {
                testersList = null;
            }
            return testersList;
        }
        //helping for tester time in function LoadTestersFromXMLToList
        private bool[,] GetTesterTimeFromXml(XElement TesterTime)
        {
            bool[,] matrix = new bool[5, 6];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    matrix[i, j] = bool.Parse(TesterTime.Element("A" + i.ToString()).Element("B" + j.ToString()).Value);
                }
            }
            return matrix;
        }
        //helping for tester time in function saveTestersToXml
        private XElement getTesterTime(Tester tester)
        {
            XElement TesterTime = new XElement("TesterTime");
            for (int i = 0; i < 5; i++)
            {
                XElement temp = new XElement("A" + i.ToString());
                TesterTime.Add(temp);
                for (int j = 0; j < 6; j++)
                {
                    string name = j.ToString();
                    TesterTime.Element("A" + i.ToString()).Add(new XElement("B" + j.ToString(), tester.TesterTime[i, j].ToString()));
                }
            }
            return TesterTime;
        }
        #endregion
    }
}
