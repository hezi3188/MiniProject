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
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;
using PLWPF;
using PLWPF.Admin;
namespace PLWPF.Admin.TesterInAdmin
{
    /// <summary>
    /// Interaction logic for Tester_Datas.xaml
    /// </summary>
    public partial class Tester_Datas : UserControl
    {
        BL.Bl_imp bl = BL.Bl_imp.GetInstance();
        BE.Tester tester;         
        public Tester_Datas()
        {
            InitializeComponent();           
        }
        public Tester_Datas(string id)
        {
            InitializeComponent();
            tester = (BE.Tester) bl.get_Tester(id).Clone();
            this.DataContext = tester;
            this.tester_CarTipComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarTip));
            myLuz.setLuze(tester);
            dateOfBirthDatePicker.DisplayDateStart = DateTime.Now.AddYears(-BE.Configuration.MaximumAge_Of_Tester);
            dateOfBirthDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-BE.Configuration.MinimumAge_Of_Tester);
        }


        public void Update()
        {
            try
            {
                myLuz.getLuze(tester);
                bl.Update_Tester(tester);
                this.DataContext = tester.Clone();
            }
            catch (Exception ea)
            {

                MessageBox.Show(ea.Message);
            }
        }

        private void iDTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void phoneNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void UserControl_Loaded_2(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }
        private void PhoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
        private void FirstNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        e.Handled = new Regex("[^a-zA-Zא-ת]+").IsMatch(e.Text);
        }
    }
}
