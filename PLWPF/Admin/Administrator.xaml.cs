using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PLWPF;
using PLWPF.Admin.TesterInAdmin;
using PLWPF.Admin.TraineeInAdmin;
using PLWPF.Admin.TestInAdmin;
namespace PLWPF.Admin
{
    /// <summary>
    /// Interaction logic for Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        #region Fields
        Tester_Datas userTester;
        traineeData userTrainee;
        testData userTest;
        BL.Bl_imp bl = BL.Bl_imp.GetInstance();
        IEnumerable<BE.Tester> dataTester;
        IEnumerable<BE.Test> dataTest;
        IEnumerable<BE.Trainee> dataTrainee;
        #endregion fields;

        #region Constractor
        public Administrator()
        {
            InitializeComponent();

            dataTester= BL.Bl_imp.GetInstance().Get_Tester();
            dataTest= BL.Bl_imp.GetInstance().Get_Test(); 
            dataTrainee = BL.Bl_imp.GetInstance().Get_Treinee();

            traineeDataGrid.DataContext = dataTrainee;
            testerDataGrid.DataContext = dataTester;
            testDataGrid.DataContext = dataTest;

            comboBoxTesterOption.ItemsSource = Enum.GetValues(typeof(BE.TesterOptions));
            comboBoxTraineOption.ItemsSource= Enum.GetValues(typeof(BE.TraineeOptions));
            comboBoxTestOption.ItemsSource= Enum.GetValues(typeof(BE.TestOptions));

            comboBoxTraineOption.SelectedItem = BE.TraineeOptions.All_Trainees;
            comboBoxTesterOption.SelectedItem = BE.TesterOptions.All_Testers;
            comboBoxTestOption.SelectedItem = BE.TestOptions.All_Tests;
        }
        #endregion

        #region Test
        private void button1_Click(object sender, RoutedEventArgs e)
        {


            if ((string) addAndUpdateTest.Content == "update/finish")
            {
                userTest.update();
                dataTest = bl.Get_Test();
                comboBoxTestOption.SelectedIndex = 0;
                comboBoxTestCarTip.SelectedItem = null;
                refrash();
                reset();
                userTest.Visibility = Visibility.Hidden;
                Grid.SetColumn(testDataGrid, 0);
                Grid.SetColumn(gridTetMenu, 0);
                addAndUpdateTest.Content = "add test";
            }
            else
            {
                addTest addTestWin = new addTest();
                addTestWin.ShowDialog();
                dataTest = bl.Get_Test();
                comboBoxTestOption.SelectedIndex = 0;
                comboBoxTestCarTip.SelectedItem = null;
                refrash();
                reset();
            }
        }
        private void testDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //מוחק את האס אחד הפנימי
            if (gridTest.Children.Count == 5)
            {
                gridTest.Children.RemoveAt(4);
            }
            if (testDataGrid.SelectedItem != null)
            {
                Grid.SetColumn(testDataGrid, 1);
                Grid.SetColumn(gridTetMenu, 1);
                userTest = new testData(((BE.Test)testDataGrid.SelectedItem).Test_Number);
                gridTest.Children.Add(userTest);
                if (bl.IsValidTest(bl.getTest(((BE.Test)testDataGrid.SelectedItem).Test_Number)))
                    userTest.IsEnabled = false;
                addAndUpdateTest.Content = "update/finish";
            }
        }
        private void buttonSearchTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string testNumber = textBoxSearchTest.Text;
                textBoxSearchTest.Text = "";
                if (!bl.Get_Test().Any(item => item.Test_Number == testNumber))
                {
                    MessageBox.Show("The test number: " + testNumber + " is not found!");
                }
                else
                {
                    dataTest = bl.Get_Test(item => item.Test_Number == testNumber);
                    refrash();
                }
            }
            catch (Exception mess)

            {
                MessageBox.Show(mess.Message);
            }
        }
        private void comboBoxTestOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxTestOption.SelectedIndex == 0)
            {
                buttonGetTest.IsEnabled = true;
                comboBoxTestCarTip.IsEnabled = false;

            }
            if (comboBoxTestOption.SelectedIndex == 1)
            {
                buttonGetTest.IsEnabled = false;
                comboBoxTestCarTip.IsEnabled = true;

                List<bool> lists = new List<bool>();
                foreach (var item in bl.GroupTestByInvalid(true))
                {
                    lists.Add(item.Key);
                }
                comboBoxTestCarTip.ItemsSource = lists;
            }
            if (comboBoxTestOption.SelectedIndex == 2)
            {
                buttonGetTest.IsEnabled = false;
                comboBoxTestCarTip.IsEnabled = true;

                List<bool> list = new List<bool>();
                foreach (var item in bl.GroupTestBySucceded(true))
                {
                    list.Add(item.Key);
                }
                comboBoxTestCarTip.ItemsSource = list;
            }
            if (comboBoxTestOption.SelectedIndex == 3)
            {
                buttonGetTest.IsEnabled = false;
                comboBoxTestCarTip.IsEnabled = true;

                List<bool> list = new List<bool>();
                foreach (var item in bl.GroupTestByNeedUpdate(true))
                {
                    list.Add(item.Key);
                }
                comboBoxTestCarTip.ItemsSource = list;
            }


        }
        private void buttonGetTest_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTestOption.SelectedIndex == 0)
            {
                dataTest = bl.Get_Test();
            }
            if (comboBoxTestOption.SelectedIndex == 1)
            {
                dataTest = bl.GroupTestByInvalid().Single(item => item.Key == (bool)comboBoxTestCarTip.SelectedValue);
            }

            if (comboBoxTestOption.SelectedIndex == 2)
            {
                dataTest = bl.GroupTestBySucceded().Single(item => item.Key == (bool)comboBoxTestCarTip.SelectedValue);
            }

            if (comboBoxTestOption.SelectedIndex == 3)
            {
                dataTest = bl.GroupTestByNeedUpdate().Single(item => item.Key == (bool)comboBoxTestCarTip.SelectedValue);
            }
            refrash();
            comboBoxTestCarTip.SelectedItem = null;
            if (comboBoxTestOption.SelectedIndex != 0)
            {
                buttonGetTest.IsEnabled = false;
            }
        }
        private void comboBoxTestCarTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonGetTest.IsEnabled = true;
        }
        private void textBoxSearchTest_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bl.Get_Test(item => item.Test_Number.Contains((sender as TextBox).Text)).Any())
            {
                testDataGrid.DataContext = bl.Get_Test(item => item.Test_Number.Contains((sender as TextBox).Text));
            }
            else if (bl.Get_Test(item => (item.Test_Date.Day.ToString() + item.Test_Date.Month.ToString() + item.Test_Date.Year.ToString()).Contains((sender as TextBox).Text)).Any())
            {
                testDataGrid.DataContext = bl.Get_Test(item => (item.Test_Date.Day.ToString() + item.Test_Date.Month.ToString() + item.Test_Date.Year.ToString()).Contains((sender as TextBox).Text));
            }
            else if (bl.Get_Test(item => (item.Test_Date.Month.ToString() + item.Test_Date.Day.ToString() + item.Test_Date.Year.ToString()).Contains((sender as TextBox).Text)).Any())
            {
                testDataGrid.DataContext = bl.Get_Test(item => (item.Test_Date.Month.ToString() + item.Test_Date.Day.ToString() + item.Test_Date.Year.ToString()).Contains((sender as TextBox).Text));
            }
            else
            {
                testDataGrid.DataContext = bl.Get_Test(item => (item.Test_Date.ToShortDateString()).Contains((sender as TextBox).Text));
            }


        }
        private void deleteButton1_Click(object sender, RoutedEventArgs e)
        {
            addAndUpdateTest.Content = "add test";
            if (testDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?","Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
               
                    try
                    {
                    if (result == MessageBoxResult.Yes)
                    {
                        bl.Remove_Test(bl.getTest(((BE.Test)testDataGrid.SelectedItem).Test_Number));
                        dataTest = bl.Get_Test();
                        comboBoxTestOption.SelectedIndex = 0;
                        comboBoxTestCarTip.SelectedItem = null;
                        refrash();
                        reset();
                        userTest.Visibility = Visibility.Hidden;
                        Grid.SetColumn(testDataGrid, 0);
                        Grid.SetColumn(gridTetMenu, 0);
                    }
                    else
                    {
                        userTest.Visibility = Visibility.Hidden;
                        Grid.SetColumn(testDataGrid, 0);
                        Grid.SetColumn(gridTetMenu, 0);
                    }
                }
                    catch (Exception)
                    {
                    }
               
            }
            else
            {
                MessageBox.Show("choose item please!");
            }
        }
        #endregion

        #region Tester
        private void testerDataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            //מוחק את האס הפנימי
            if (gridPnimi.Children.Count == 5)
            {
                gridPnimi.Children.RemoveAt(4);
            }
            if (testerDataGrid.SelectedItem != null)
            {

                Grid.SetColumn(gridTesterMenu, 1);
                Grid.SetColumn(testerDataGrid, 1);

                userTester = new Tester_Datas(((BE.Tester)testerDataGrid.SelectedItem).ID);
                gridPnimi.Children.Add(userTester);
                addAndUpdateButtonTester.Content = "update/finish";
            }
            //   refrash();

        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            addAndUpdateButtonTester.Content = "add tester";
            if (testerDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                
                    StringBuilder str = new StringBuilder();
                    try
                    {
                    if (result == MessageBoxResult.Yes)
                    {
                        if (bl.GroupTestByInvalid().Any(item => item.Key == true))
                        {
                            if (bl.GroupTestByInvalid(true).Single(item => item.Key == true).Any(item => item.Tester_ID == ((BE.Tester)testerDataGrid.SelectedItem).ID))
                            {
                                foreach (var item in bl.GroupTestByInvalid(true).Single(item => item.Key == true))
                                {
                                    if (item.Tester_ID == ((BE.Tester)testerDataGrid.SelectedItem).ID)
                                    {
                                        str.Append(item.ToString());
                                        str.Append("\n");
                                    }
                                }
                                MessageBox.Show("you must complete all test\n" + str.ToString());
                            }

                        }
                        bl.Remove_Tester(bl.get_Tester(((BE.Tester)testerDataGrid.SelectedItem).ID));
                        dataTester = bl.Get_Tester();
                        refrash();
                        reset();
                        userTester.Visibility = Visibility.Hidden;
                        Grid.SetColumn(gridTesterMenu, 0);
                        Grid.SetColumn(testerDataGrid, 0);
                    }
                    else
                    {
                        userTester.Visibility = Visibility.Hidden;
                        Grid.SetColumn(gridTesterMenu, 0);
                        Grid.SetColumn(testerDataGrid, 0);
                    }

                }
                catch (Exception)
                    {
                    }
                
            }
            else
            {
                MessageBox.Show("choose item please!");
            }

        }
        private void addAndUpdateButton_Click(object sender, RoutedEventArgs e)
        {

            if ((string)addAndUpdateButtonTester.Content == "update/finish")
            {
                userTester.Update();
                dataTester = bl.Get_Tester();
                refrash();
                reset();
                userTester.Visibility = Visibility.Hidden;
                Grid.SetColumn(testerDataGrid, 0);
                Grid.SetColumn(gridTesterMenu, 0);
                addAndUpdateButtonTester.Content = "add tester";
            }
            else
            {
                addTester newTesterWin = new addTester();
                newTesterWin.ShowDialog();
                dataTester = bl.Get_Tester();
                refrash();
                reset();
            }
        }
        private void comboBoxTesterOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxTesterOption.SelectedIndex == 0)
            {
                buttonGetTester.IsEnabled = true;
                comboBoxTesterCarTip.IsEnabled = false;

            }
            if (comboBoxTesterOption.SelectedIndex == 1)
            {
                buttonGetTester.IsEnabled = false;
                comboBoxTesterCarTip.IsEnabled = true;

                List<BE.CarTip> lists = new List<BE.CarTip>();
                foreach (var item in bl.GroupTesterByType(true))
                {
                    lists.Add(item.Key);
                }
                comboBoxTesterCarTip.ItemsSource = lists;
            }
        }
        private void comboBoxTesterCarTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonGetTester.IsEnabled = true;
        }
        private void buttonGetTester_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTesterOption.SelectedIndex == 0)
            {
                dataTester = bl.Get_Tester();
            }
            if (comboBoxTesterOption.SelectedIndex == 1)
            {
                dataTester = bl.GroupTesterByType().Single(item => item.Key == (BE.CarTip)comboBoxTesterCarTip.SelectedValue);
            }
            refrash();
            comboBoxTesterCarTip.SelectedItem = null;
            if (comboBoxTesterOption.SelectedIndex != 0)
            {
                buttonGetTester.IsEnabled = false;
            }
        }
        private void TextBoxTesterSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bl.Get_Tester(item => item.ID.Contains((sender as TextBox).Text)).Any())
                testerDataGrid.DataContext = bl.Get_Tester(item => item.ID.Contains((sender as TextBox).Text));
            else if (bl.Get_Tester(item => item.FirstName.ToUpper().Contains((sender as TextBox).Text.ToUpper())).Any())
            {
                testerDataGrid.DataContext = bl.Get_Tester(item => item.FirstName.ToUpper().Contains((sender as TextBox).Text.ToUpper()));
            }
            else
            {
                testerDataGrid.DataContext = bl.Get_Tester(item => item.LastName.ToUpper().Contains((sender as TextBox).Text.ToUpper()));
            }

        }
        private void buttonTesterSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = TextBoxTesterSearch.Text;
                TextBoxTesterSearch.Text = "";
                dataTester = bl.Get_Tester(item => item.ID == id);
                refrash();
            }
            catch (Exception mess)

            {
                MessageBox.Show(mess.Message);
            }
        }

        #endregion

        #region Trainee
        private void traineeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //מוחק את האס אחד הפנימי
            if (gridPnimi1.Children.Count == 5)
            {
                gridPnimi1.Children.RemoveAt(4);
            }
            if (traineeDataGrid.SelectedItem != null)
            {
                Grid.SetColumn(traineeDataGrid, 1);
                Grid.SetColumn(gridTraineeMenu, 1);
                userTrainee = new traineeData(((BE.Trainee)traineeDataGrid.SelectedItem).ID);
                gridPnimi1.Children.Add(userTrainee);
                addAndUpdateButtonTrainee.Content = "update/finish";
            }
        }
        private void addAndUpdateButtonTrainee_Click(object sender, RoutedEventArgs e)
        {
            if ((string)addAndUpdateButtonTrainee.Content == "update/finish")
            {
                userTrainee.Update();
                dataTrainee = bl.Get_Treinee();
                comboBoxTraineOption.SelectedIndex = 0;
                comboBoxTraineeCarTip.SelectedItem = null;
                refrash();
                reset();
                userTrainee.Visibility = Visibility.Hidden;
                Grid.SetColumn(traineeDataGrid, 0);
                Grid.SetColumn(gridTraineeMenu, 0);
                addAndUpdateButtonTrainee.Content = "add trainee";
            }
            else
            {
                addTrainee newTraineeWin = new addTrainee();
                newTraineeWin.ShowDialog();
                dataTrainee = bl.Get_Treinee();

                dataTrainee = bl.Get_Treinee();
                comboBoxTraineOption.SelectedIndex = 0;
                comboBoxTraineeCarTip.SelectedItem = null;
                refrash();
            }
        }
        private void deleteButtonTrainee_Click(object sender, RoutedEventArgs e)
        {
            addAndUpdateButtonTrainee.Content = "add trainee";
            if (traineeDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
               

                    try
                    {
                    if (result == MessageBoxResult.Yes)
                    {

                        bl.Remove_Trainee(bl.get_Trainee(((BE.Trainee)traineeDataGrid.SelectedItem).ID));
                        dataTrainee = bl.Get_Treinee();
                        dataTest = bl.Get_Test();
                        comboBoxTraineOption.SelectedIndex = 0;
                        comboBoxTraineeCarTip.SelectedItem = null;
                        refrash();
                        reset();
                        userTrainee.Visibility = Visibility.Hidden;
                        Grid.SetColumn(gridTraineeMenu, 0);
                        Grid.SetColumn(traineeDataGrid, 0);
                    }
                    else
                    {
                        userTrainee.Visibility = Visibility.Hidden;
                        Grid.SetColumn(gridTraineeMenu, 0);
                        Grid.SetColumn(traineeDataGrid, 0);
                    }
                }
                    catch (Exception)
                    {
                    }
              
            }

            else
            {
                MessageBox.Show("choose item please!");
            }
        }
        private void searchTrainee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = searchTraineeTextBox.Text;
                searchTraineeTextBox.Text = "";
                // var v = bl.get_Trainee(id);
                dataTrainee = bl.Get_Treinee(item => item.ID == id);
                refrash();
            }
            catch (Exception mess)

            {
                MessageBox.Show(mess.Message);
            }
        }
        private void buttonGetTrainee_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTraineOption.SelectedIndex == 0)
            {
                dataTrainee = bl.Get_Treinee();
            }
            if (comboBoxTraineOption.SelectedIndex == 1)
            {
                dataTrainee = bl.GroupTraineeByType(true).Single(item => item.Key == (BE.CarTip)comboBoxTraineeCarTip.SelectedValue);
            }
            if (comboBoxTraineOption.SelectedIndex == 2)
            {
                dataTrainee = bl.GroupTrainee_By_School(true).Single(item => item.Key == (string)comboBoxTraineeCarTip.SelectedValue);
            }
            if (comboBoxTraineOption.SelectedIndex == 3)
            {
                dataTrainee = bl.GroupTrainee_By_Teacher(true).Single(item => item.Key == (string)comboBoxTraineeCarTip.SelectedValue);
            }
            if (comboBoxTraineOption.SelectedIndex == 4)
            {
                dataTrainee = bl.GroupTrainee_By_Num_Tests(true).Single(item => item.Key == (int)comboBoxTraineeCarTip.SelectedValue);
            }
            if (comboBoxTraineOption.SelectedIndex == 5)
            {
                dataTrainee = bl.GroupTraineeBySucceded(true).Single(item => item.Key == (bool)comboBoxTraineeCarTip.SelectedValue);
            }
            refrash();
            comboBoxTraineeCarTip.SelectedItem = null;
            if (comboBoxTraineOption.SelectedIndex != 0)
            {
                buttonGetTrainee.IsEnabled = false;
            }
        }
        private void comboBoxTraineOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxTraineOption.SelectedIndex == 0)
            {
                buttonGetTrainee.IsEnabled = true;
                comboBoxTraineeCarTip.IsEnabled = false;

            }
            if (comboBoxTraineOption.SelectedIndex == 1)
            {
                buttonGetTrainee.IsEnabled = false;
                comboBoxTraineeCarTip.IsEnabled = true;

                List<BE.CarTip> list = new List<BE.CarTip>();
                foreach (var item in bl.GroupTraineeByType(true))
                {
                    list.Add(item.Key);
                }
                comboBoxTraineeCarTip.ItemsSource = list;
            }
            if (comboBoxTraineOption.SelectedIndex == 2)
            {
                buttonGetTrainee.IsEnabled = false;
                comboBoxTraineeCarTip.IsEnabled = true;

                List<string> lists = new List<string>();
                foreach (var item in bl.GroupTrainee_By_School(true))
                {
                    lists.Add(item.Key);
                }
                comboBoxTraineeCarTip.ItemsSource = lists;
            }
            if (comboBoxTraineOption.SelectedIndex == 3)
            {
                buttonGetTrainee.IsEnabled = false;
                comboBoxTraineeCarTip.IsEnabled = true;

                List<string> list = new List<string>();
                foreach (var item in bl.GroupTrainee_By_Teacher(true))
                {
                    list.Add(item.Key);
                }
                comboBoxTraineeCarTip.ItemsSource = list;
            }
            if (comboBoxTraineOption.SelectedIndex == 4)
            {
                buttonGetTrainee.IsEnabled = false;
                comboBoxTraineeCarTip.IsEnabled = true;

                List<int> list = new List<int>();
                foreach (var item in bl.GroupTrainee_By_Num_Tests(true))
                {
                    list.Add(item.Key);
                }
                comboBoxTraineeCarTip.ItemsSource = list;
            }
            if (comboBoxTraineOption.SelectedIndex == 5)
            {
                buttonGetTrainee.IsEnabled = false;
                comboBoxTraineeCarTip.IsEnabled = true;

                List<bool> list = new List<bool>();
                foreach (var item in bl.GroupTraineeBySucceded(true))
                {
                    list.Add(item.Key);
                }
                comboBoxTraineeCarTip.ItemsSource = list;
            }
        }
        private void comboBoxTraineeCarTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonGetTrainee.IsEnabled = true;
        }
        private void searchTraineeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bl.Get_Treinee(item => item.ID.Contains((sender as TextBox).Text)).Any())
                traineeDataGrid.DataContext = bl.Get_Treinee(item => item.ID.Contains((sender as TextBox).Text));
            else if (bl.Get_Treinee(item => item.FirstName.ToUpper().Contains((sender as TextBox).Text.ToUpper())).Any())
            {
                traineeDataGrid.DataContext = bl.Get_Treinee(item => item.FirstName.ToUpper().Contains((sender as TextBox).Text.ToUpper()));
            }
            else
            {
                traineeDataGrid.DataContext = bl.Get_Treinee(item => item.LastName.ToUpper().Contains((sender as TextBox).Text.ToUpper()));
            }
        }
        #endregion

        #region HelpFunctions
        private void refrash()
        {
            traineeDataGrid.DataContext = dataTrainee;
            testerDataGrid.DataContext = dataTester;
            testDataGrid.DataContext = dataTest;
        }
        private void reset()
        {
            comboBoxTestOption.SelectedIndex = 0;
            comboBoxTestCarTip.SelectedItem = null;
            comboBoxTesterOption.SelectedIndex = 0;
            comboBoxTesterCarTip.SelectedItem = null;
            comboBoxTraineOption.SelectedIndex = 0;
            comboBoxTraineeCarTip.SelectedItem = null;
        }
        #endregion

        #region KeyboardFunctions
        private void testDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Delete))
            {
                deleteButton1_Click(sender, e);
            }
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.D))
            {
                button1_Click(sender, e);
            }
        }

        private void testerDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Delete))
            {
                deleteButton_Click(sender, e);
            }
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.D))
            {
               addAndUpdateButton_Click(sender, e);
            }
        }

        private void traineeDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Delete))
            {
                deleteButtonTrainee_Click(sender, e);
            }
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.D))
            {
                addAndUpdateButtonTrainee_Click(sender, e);
            }
        }
        #endregion

        #region SystemFunctions
        private void TestDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void TesterDataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
            //  Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource traineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // traineeViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testViewSource.Source = [generic data source]
        }
        #endregion
    }
}
