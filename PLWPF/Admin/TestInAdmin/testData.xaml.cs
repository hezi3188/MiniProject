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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
namespace PLWPF.Admin.TestInAdmin
{
    /// <summary>
    /// Interaction logic for testData.xaml
    /// </summary>
    public partial class testData : UserControl
    {
        BE.Test test;
        List<Criterion> list=new List<Criterion>();
        BL.Bl_imp bl = BL.Bl_imp.GetInstance();
        public testData()
        {
            InitializeComponent();
        }

        public testData(string Test_Number)
        {
            InitializeComponent();
            test = bl.getTest(Test_Number);
            test = test.Clone() as Test;
            this.DataContext = test;
        }
        public void update()
        {
           
            try
            {
                if (bl.IsValidTest(test))
                    IsEnabled = false;
                else
                {
                    bl.Update_Test(test);
                    this.DataContext = test.Clone();
                }
            }
            catch (Exception ea)
            {
                MessageBox.Show(ea.Message);
            }
        }
       
        private void addCritrionsButton_Click(object sender, RoutedEventArgs e)
        {
            addCriterionWin criterion = new addCriterionWin(test);
            criterion.ShowDialog();
            DataContext =test;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAddCriterion_Click(object sender, RoutedEventArgs e)
        {
            addCriterionWin addCriterion = new addCriterionWin(test);
            addCriterion.ShowDialog();
            this.DataContext = test.Clone();
        }

        private void criterionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
