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

namespace InformacioniSistemBolnice.Secretary_ns
{
    /// <summary>
    /// Interaction logic for SekretarMain.xaml
    /// </summary>
    public partial class SecretaryMain : Window
    {
        private Secretary _currentSecretary;
        public SecretaryMain(Secretary currentSecretary)
        {
            this._currentSecretary = currentSecretary;
            InitializeComponent();
        }

        private void PatientsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = PatientsPage.GetPage();
        }

        private void StartingButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = StartingPage.GetPage(_currentSecretary);
        }

        private void AppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = AppointmentsPage.GetPage();
        }
    }
}
