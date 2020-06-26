using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DAL
{
    public interface Idal
    {
        #region Tester
        bool Add_Tester(Tester tester);
        bool Remove_Tester(Tester tester);
        bool Update_Tester(Tester tester);
        IEnumerable<Tester> Get_Tester(Func<Tester, bool> predicate = null);
        Tester get_Tester(string ID);

        #endregion

        #region Trainee
        bool Add_Trainee(Trainee trainee);
        bool Remove_Trainee(Trainee trainee);
        bool Update_Trainee(Trainee trainee);
        /// <summary>
        /// Returns all the Trainees
        /// </summary>
        /// <param name="predicate"> Arbitrator function</param>
        /// <returns>Returns all the Trainees</returns>
        IEnumerable<Trainee> Get_Treinee(Func<Trainee, bool> predicate = null);
        Trainee get_Trainee(string ID);
        #endregion

        #region Test
        bool Add_Test(Test test);
        bool Remove_Test(Test test);
        bool Update_Test(Test test);
        IEnumerable<Test> Get_Test(Func<Test, bool> predicate = null);
        Test get_Test(string test_number);
        DateTime Last_Test(string Trainee_Id);
        bool Last_TestCondition(string Trainee_Id);
        #endregion
    }
}
