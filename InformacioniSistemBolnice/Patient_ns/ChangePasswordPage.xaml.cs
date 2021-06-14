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
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        private StartPatientWindow parentp;
        private PatientMedicalRecordPage pparent;
        private Patient _patient;
        private PatientController _patientController = new PatientController();
        public ChangePasswordPage(StartPatientWindow spw, PatientMedicalRecordPage pmrp)
        {
            parentp = spw;
            pparent = pmrp;
            _patient = spw.Patient;
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parentp.startWindow.Content = pparent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_patientController.IsValidPassword(_patient, currentPass.Password))
            {
                SuccessfulChangingPassword();
            }
            else
            {
                MessageBox.Show("Trenutna lozinka nije validno unesena!", "Greška");
            }

        }

        private void SuccessfulChangingPassword()
        {
            _patientController.SetNewPassword(_patient, newPass.Password);
            MessageBoxResult result = MessageBox.Show(
                "Lozinka je uspešno promenjena.Bićete izlogovani.Prijavite se sa novom lozinkom!", "Uspeh",
                MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                ShowStartWindow();

            }
        }

        private void ShowStartWindow()
        {
            MainWindow m = new MainWindow();
            Application.Current.MainWindow = m;
            parentp.Close();
            m.Show();
        }
    }
}

