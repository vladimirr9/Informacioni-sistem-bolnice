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
            this.DataContext = this;
        }

        private void PatientsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = PatientsPage.GetPage();
        }

        private void StartingButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = StartingPage.GetPage(_currentSecretary, this);
        }

        private void AppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = AppointmentsPage.GetPage();
        }

        private void DoctorsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = DoctorsPage.GetPage();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void StartingPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Main.Content = StartingPage.GetPage(_currentSecretary, this);
        }
        private void Patients_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Main.Content = PatientsPage.GetPage();
        }
        private void Doctors_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Main.Content = DoctorsPage.GetPage();
        }
        private void Appointments_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Main.Content = AppointmentsPage.GetPage();
        }

        private void Reports_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
