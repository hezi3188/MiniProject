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
namespace PLWPF.Admin.TesterInAdmin
{
    /// <summary>
    /// Interaction logic for luz.xaml
    /// </summary>
    public partial class luz : UserControl
    {
        BL.Bl_imp bl = BL.Bl_imp.GetInstance();
        bool[,] luzing = new bool[5, 6];
        public luz()
        {
            InitializeComponent();
        }
        public luz(bool[,] luz)
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    string temp = "checkBox" + i + j;
                    var checkBox = FindName(temp) as CheckBox;
                    checkBox.IsChecked= true;
                }
            }
        }
        /// <summary>
        /// give the luz on the screen to the tester
        /// </summary>
        /// <param name="tester"></param>
        public void getLuze(BE.Tester tester)
        {
            BE.Tester t = tester;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    string temp = "checkBox" + i + j;
                    CheckBox checkBox = FindName(temp) as CheckBox;
                    t.TesterTime[j, i] = (bool)checkBox.IsChecked;
                }
            }
        }
        /// <summary>
        /// set the kuz on the screen from the tester
        /// </summary>
        /// <param name="tester"></param>
        public void setLuze(BE.Tester tester)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    string temp = "checkBox" + i + j;
                    CheckBox checkBox = FindName(temp) as CheckBox;
                    checkBox.IsChecked =tester.TesterTime[j,i];
                }
            }
        }
    }
}
