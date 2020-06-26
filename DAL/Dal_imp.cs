using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
//Dal
namespace DAL
{
    public  class Dal_imp : Idal
    {
        private Dal_imp() {}
        private static  Dal_imp instance = null;
        public static Dal_imp GetInstance()
        {
            if (instance == null)  
               instance = new Dal_imp(); 
           return instance;  
        }
       
        #region add
        public bool Add_Test(Test test)
        {
            test.Test_Number = string.Format("{0:00000000}",Configuration.code);
            Configuration.code++;
            DataSource.Tests.Add(test);
            return true;
        }
        public bool Add_Tester(Tester tester)
        {
            
            if (DataSource.Testers.Any(item => item.ID == tester.ID))
                throw new Exception("The tester is already exist!");
            DataSource.Testers.Add(tester);
            return true;
        }

        public bool Add_Trainee(Trainee trainee)
        {

            if (DataSource.Trainees.Any(item => item.ID == trainee.ID))
                throw new Exception("The trainee is already exist!");
            DataSource.Trainees.Add(trainee);
            return true;

        }
        #endregion add

        #region Get
        public Tester get_Tester(string ID)
        {
            if(DataSource.Testers.Any(item => item.ID == ID))
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
        #endregion getList

        #region remove
        public bool Remove_Test(Test test)
        {

            if (DataSource.Tests.Any(item => item.Test_Number == test.Test_Number))
            {
                DataSource.Tests.Remove(test);
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
                return true;
            }
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
            //if (!List.Any(predicate))
            //    return null;
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


        #endregion update

        #region Additional
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
    }
}
