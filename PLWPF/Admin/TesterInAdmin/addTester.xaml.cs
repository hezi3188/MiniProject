using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF.Admin.TesterInAdmin
{
    /// <summary>
    /// Interaction logic for addTester.xaml
    /// </summary>
    public partial class addTester : Window
    {
        BE.Tester tester=new BE.Tester();
        BE.Addres addres = new BE.Addres();
        BL.Bl_imp bl = BL.Bl_imp.GetInstance();
        public addTester()
        {
            InitializeComponent();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.tester_CarTipComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarTip));
            this.dateOfBirthDatePicker.DisplayDateStart = DateTime.Now.AddYears(-BE.Configuration.MaximumAge_Of_Tester);
            this.dateOfBirthDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-BE.Configuration.MinimumAge_Of_Tester);
            this.DataContext = tester;  
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tester = DataContext as BE.Tester;
                myLuz.getLuze(tester);
                bl.Add_Tester(tester);
                Close();
            }
            catch (Exception mess)
            {
                MessageBox.Show(mess.Message);
            }
        }
       
        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(firstNameTextBox.Text== "" || lastNameTextBox.Text == "" || iDTextBox.Text==""|| phoneNumberTextBox.Text==""||tester_ExperienceTextBox.Text==""||tester_MaxDistanceTextBox.Text==""||tester_MaximumTestsPerWeekTextBox.Text==""||buildingNumberTextBox.Text==""||cityTextBox.Text==""||streetTextBox.Text=="") 
            {
                buttonSaveT.IsEnabled = false;
            }
            else
            {
                 buttonSaveT.IsEnabled = true;
               
            }

        }

        private void GenderComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (genderComboBox.Text == "" || tester_CarTipComboBox.Text == "")
            {
                buttonSaveT.IsEnabled = false;
            }
            }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void FirstNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                genderComboBox.Focus();
            }
        }

        private void GenderComboBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                iDTextBox.Focus();
            }
        }

        private void Label_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                lastNameTextBox.Focus();
            }
            
        }

        private void LastNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                phoneNumberTextBox.Focus();
            }

        }

        private void PhoneNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tester_CarTipComboBox.Focus();
            }   
        }
        private void Tester_CarTipComboBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                tester_ExperienceTextBox.Focus();
            }

        }

        private void Tester_ExperienceTextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                tester_MaxDistanceTextBox.Focus();
            }
        }

        private void Tester_MaxDistanceTextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                tester_MaximumTestsPerWeekTextBox.Focus();
            }
        }

        private void Tester_MaximumTestsPerWeekTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buildingNumberTextBox.Focus();
            }
        }

        private void BuildingNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cityTextBox.Focus();
            }
        }

        private void CityTextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                streetTextBox.Focus();
            }
        }
        # region TextIntegrity
        private void FirstNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Zא-ת]+").IsMatch(e.Text);
        }


       

        private void IDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        #endregion
    }
}
