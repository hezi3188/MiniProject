using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
namespace PLWPF.Admin.TestInAdmin
{
    /// <summary>
    /// Interaction logic for addTest.xaml
    /// </summary>
    public partial class addTest : Window
    {
        BL.Bl_imp bl = BL.Bl_imp.GetInstance();
        BE.Test test = new BE.Test();
        BackgroundWorker thread;
        public addTest()
        {
            InitializeComponent();
            thread = new BackgroundWorker();
            thread.DoWork += Thread_DoWork;
            thread.RunWorkerCompleted += Thread_RunWorkerCompleted;
            this.DataContext = test;
            this.test_TypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarTip));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

       
        public  void tempThread()
        {
        }
        private void saveTast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                test = DataContext as BE.Test;
                saveTast.IsEnabled = false;
                saveTast.Content = "Saving Test Please Wait...";
                saveTast.Foreground = Brushes.Red;
                thread.RunWorkerAsync();
            }
            catch (Exception mess)
            {
                MessageBox.Show(mess.Message);
            }
        }

        private void Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((string)e.Result == "SUCCESFUL_SAVE")
            {
                MessageBox.Show("your test in " + test.Test_Date + "your tester is: " + test.Tester_ID);
                Close();
            }
            else
            {
                MessageBox.Show((string)e.Result);
                saveTast.Content = "Save";
                saveTast.Foreground = Brushes.Black;
                saveTast.IsEnabled = true;
            }

        }
        private void Thread_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bl.Add_Test(test);
                e.Result = "SUCCESFUL_SAVE";
            }catch(Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void Trainee_IDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void CityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Zא-ת]+").IsMatch(e.Text);
        }

        private void Trainee_IDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (trainee_IDTextBox.Text!=""&& buildingNumberTextBox.Text!=""&& cityTextBox.Text!=""&& streetTextBox.Text!="")
            {
                saveTast.IsEnabled = true;
            }
        }

        private void Test_TypeComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }
    }
}
