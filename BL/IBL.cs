using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    interface IBL
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
        IEnumerable<Trainee> Get_Treinee(Func<Trainee, bool> predicate = null);
        Trainee get_Trainee(string ID);
        #endregion

        #region Test
        bool Add_Test(Test test);
        bool Remove_Test(Test test);
        bool Update_Test(Test test);
        IEnumerable<Test> Get_Test(Func<Test, bool> predicate = null);
        #endregion

        #region Scrutiny
        //bool Condition_Tester_Age(Tester tester);
        //bool Condition_Trainee_Age(Trainee trainee);
        //bool Condition_Days_Beetwin_Tests(Test test);
        //bool Condition_Number_Lesson(Trainee trainee);
        //bool Condition_Avilable_Test(Test test);
        //bool Condition_Maximum_Test(Tester tester,Test test);
        //bool Condition_Updata_Test(Test test);
        //bool Condition_Same_Test_in_Day(Test test);
        //bool Condition_Is_Already_Succeeded(Trainee trainee);
        //bool Condition_Match_Trainee_Tester(Test test);
        bool IsValidTest(Test item);
        bool Is_Succeeded_Test(Test test);

        #endregion

        #region Grouping
        IEnumerable<IGrouping<int, Tester>> GroupTesters_By_Experience(CarTip carTip, bool sort = false);
        IEnumerable<IGrouping<string, Trainee>> GroupTrainee_By_School( bool sort = false);
        IEnumerable<IGrouping<string, Trainee>> GroupTrainee_By_Teacher(bool sort = false);
        IEnumerable<IGrouping<int, Trainee>> GroupTrainee_By_Num_Tests(bool sort = false);
        IEnumerable<IGrouping<CarTip, Trainee>> GroupTraineeByType(bool sort = false);
        IEnumerable<IGrouping<CarTip, Tester>> GroupTesterByType(bool sort = false);
        IEnumerable<Tester> Get_Testers_By_Address(Addres addres);
        IEnumerable<Tester> Get_Testers_By_Time(DateTime time, CarTip tip, IEnumerable<Tester> list);
        #endregion
    }
}
