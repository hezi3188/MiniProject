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

namespace PLWPF.Admin.TraineeInAdmin
{
    /// <summary>
    /// Interaction logic for addTrainee.xaml
    /// </summary>
    public partial class addTrainee : Window
    {
        BE.Trainee trainee = new BE.Trainee();
        BL.Bl_imp bl = BL.Bl_imp.GetInstance();
        public addTrainee()
        {
            InitializeComponent();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.trainee_CarTipComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarTip));
            this.DataContext = trainee;
            this.dateOfBirthDatePicker.DisplayDateStart = DateTime.Now.AddYears(-BE.Configuration.MeximiumAge_Of_Studen);
            this.dateOfBirthDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-BE.Configuration.MinimumAge_Of_Studen);
        }

        private void SaveTrainee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                trainee = DataContext as BE.Trainee;
                bl.Add_Trainee(trainee);
                Close();
            }
            catch (Exception mess)
            {
                MessageBox.Show(mess.Message);
            }
        }

        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(firstNameTextBox.Text==""||lastNameTextBox.Text==""||iDTextBox.Text==""||my_TeacherTextBox.Text==""||numberOfLessonTextBox.Text==""||phoneNumberTextBox.Text==""||school_nameTextBox.Text==""||buildingNumberTextBox.Text==""||cityTextBox.Text==""||streetTextBox.Text=="")
            {
                saveTrainee.IsEnabled = false;
            }
            else
            {
                saveTrainee.IsEnabled = true;
            }
        }

        private void FirstNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Zא-ת]+").IsMatch(e.Text);
        }

        private void IDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
