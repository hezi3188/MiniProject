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
using BE;
namespace PLWPF.Admin.TestInAdmin
{
    /// <summary>
    /// Interaction logic for addCriterion.xaml
    /// </summary>
    public partial class addCriterionWin : Window
    {
        BL.Bl_imp bl = BL.Bl_imp.GetInstance();
        Criterion criter = new Criterion();
        Test test;
        public addCriterionWin()
        {
            InitializeComponent();
        }
        public addCriterionWin(Test _test)
        {
            InitializeComponent();
            this.DataContext = criter;
            test = _test;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            this.criter = DataContext as Criterion;
            this.test.Criterionss.Add(criter);
            Close();
        }
    }
}
