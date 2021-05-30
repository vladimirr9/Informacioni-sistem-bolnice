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

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for MedicalRecordPage.xaml
    /// </summary>
    public partial class MedicalRecordPage : Page
    {
        private static StartPatientWindow pp;
        private static Patient _patient;
        public MedicalRecordPage(StartPatientWindow p)
        {
            pp = p;
            _patient = pp.Patient;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            prezimeTextBox.Text = _patient.Surname;
            prezimeTextBox.IsEnabled = false;
            imeTextBox.Text = _patient.Name;
            imeTextBox.IsEnabled = false;
            JMBGTextBox.Text = _patient.JMBG;
            JMBGTextBox.IsEnabled = false;
            datumRodjenjaDatePicker.SelectedDate = _patient.DateOfBirth.Date;
            datumRodjenjaDatePicker.IsEnabled = false;
            kontaktTextBox.Text = _patient.PhoneNumber;
            kontaktTextBox.IsEnabled = false;
            emailTextBox.Text = _patient.Email;
            emailTextBox.IsEnabled = false;
            brojKarticeTextBox.Text = _patient.SocialSecurityNumber;
            brojKarticeTextBox.IsEnabled = false;
            brojKartonaTextBox.Text = _patient.MedicalRecord.MedicalRecordNumber;
            brojKartonaTextBox.IsEnabled = false;
            if (_patient.Gender.Equals('M'))
            {
                mRadioButton.IsChecked = true;
                mRadioButton.IsEnabled = false;
                zRadioButton.IsEnabled = false;
            }
            else
            {
                zRadioButton.IsChecked = true;
                mRadioButton.IsEnabled = false;
                zRadioButton.IsEnabled = false;
            }
            adresaTextBox.Text = _patient.ResidentialAddress.StreetAndNumber + "," + _patient.ResidentialAddress.City;
            adresaTextBox.IsEnabled = false;
        }
    }
}
