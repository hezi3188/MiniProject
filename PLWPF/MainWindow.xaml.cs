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
using BL;
using BE;
using PLWPF.Admin;
namespace PLWPF
{
    public partial class MainWindow : Window
    {
        private Bl_imp bl = Bl_imp.GetInstance();
        public MainWindow()
        {

            InitializeComponent();
            UserTextbox.Focus();
   //         bl.Add_Tester(new BE.Tester("318834579", "BenAtar", "Yehezkel", new DateTime(1960, 9, 2), Gender.male, "0543411320", new Addres("jerusalem", "fatal", 45), 10, 5, CarTip.PrivateCarAutomatic, 100.5,
   //    new bool[5, 6]
   //{
   //                               {true,false,false,false,true,true },
   //                               { true,false,false,false,true,true},
   //                               { false,false,false,false,true,true},
   //                               {false,false,false,false,true,true },
   //                               {false,false,false,false,true,true },
   //}));
   //         bl.Add_Tester(new BE.Tester("304883796", "Horvitz", "Meir", new DateTime(1960, 6, 01), Gender.male, "0543454320", new Addres("jerusalem", "fatal", 45), 10, 5, CarTip.HeavyTruckAutomatic, 100.5,
   //             new bool[5, 6]
   //         {
   //                      {false,false,false,false,true,true },
   //                      { false,false,false,false,true,true},
   //                      { false,false,false,false,true,true},
   //                      {false,false,false,false,true,true },
   //                      {false,false,false,false,true,true },
   //         }));


            //         bl.Add_Trainee(new Trainee("338760804", "trainee1", "trainee1", new DateTime(1999, 9, 9), Gender.male, "0543411320", new Addres("jerusalem", "fatal", 45), CarTip.HeavyTruckAutomatic, "or yarok", "Meir horviz", 40));
            //         bl.Add_Trainee(new Trainee("058457227", "trainee2", "trainee2", new DateTime(1990, 10, 1), Gender.male, "0543411320", new Addres("jerusalem", "fatal", 45), CarTip.PrivateCarAutomatic, "maof", "yehezkel ben atar", 30));
            //         bl.Add_Trainee(new Trainee("311319776", "trainee3", "trainee3", new DateTime(1990, 10, 1), Gender.female, "0543411320", new Addres("jerusalem", "fatal", 45), CarTip.motorcycleAutomatic, "or yarok", "Meir horviz", 19));
            //         bl.Add_Trainee(new Trainee("308480219", "trainee4", "four", new DateTime(1990, 10, 1), Gender.female, "0543411320", new Addres("jerusalem", "fatal", 45), CarTip.motorcycleAutomatic, "maof", "yehezkel ben atar", 40));

            //         List<Criterion> c = new List<Criterion>() { new Criterion("parking", true), new Criterion("speed", true), new Criterion("sides") };
            //         bl.Add_Test(new Test(new DateTime(2019,1,1,9,0,0), null, "318834579", CarTip.PrivateCarAutomatic, "058457227", new Addres("jerusalem", "fatal", 45), null, null));
            //         bl.Add_Test(new Test(new DateTime(2019,1, 23, 9, 0, 0), c, "304883796", CarTip.HeavyTruckAutomatic, "338760804", new Addres("jerusalem", "fatal", 45), null, "no good"));


        }

        private void UserTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(UserTextbox.Text=="")
            {
                buttonl.IsEnabled = false;
            }
        }

        private void Buttonl_Click(object sender, RoutedEventArgs e)
        {
            if (UserTextbox.Text == Configuration.AdminUsername && passworduser.Password == Configuration.AdminPassword)
            {
                passworduser.Password = "";
                Administrator win = new Administrator();
                Hide();
                win.ShowDialog();
                Show();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password.");
                passworduser.Password = "";
            }
        }
        private void UserTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                passworduser.Focus();
            }
        }

        private void Passworduser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buttonl.Focus();
            }
        }

        private void Buttonl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (UserTextbox.Text == Configuration.AdminUsername && passworduser.Password == Configuration.AdminPassword)
                {
                    passworduser.Password = "";
                    Administrator win = new Administrator();
                    Hide();
                    win.ShowDialog();
                    Show();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password.");
                    passworduser.Password = "";
                }
            }
        }
    }
}
