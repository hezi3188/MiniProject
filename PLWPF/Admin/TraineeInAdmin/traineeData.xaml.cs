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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF.Admin.TraineeInAdmin
{
    /// <summary>
    /// Interaction logic for traineeData.xaml
    /// </summary>
    public partial class traineeData : UserControl
    {
        BL.Bl_imp bl = BL.Bl_imp.GetInstance();
        BE.Trainee trainee;
        public traineeData()
        {
            InitializeComponent();
        }
        public traineeData(string id)
        {
            InitializeComponent();
            trainee = (BE.Trainee)bl.get_Trainee(id).Clone();
            this.DataContext = trainee;
            this.trainee_CarTipTextBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarTip));
        }

        public void Update()
        {
            try
            {
                bl.Update_Trainee(trainee);
                this.DataContext = trainee.Clone();
            }
            catch (Exception ea)
            {
                MessageBox.Show(ea.Message);
            }
        }

        private void FirstNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Zא-ת]+").IsMatch(e.Text);
        }

        private void PhoneNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
