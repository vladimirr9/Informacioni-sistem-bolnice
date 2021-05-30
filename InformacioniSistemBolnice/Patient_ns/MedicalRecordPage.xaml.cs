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
            SetInformationsInComponents();
            if (_patient.Gender.Equals('M'))
            {
                ChangeAvailabilityOfComponentsM();
            }
            else 
            {
               ChangeAvailabilityOfComponentsZ();
            }
            
            SetEnabledComponents();
        }

        private void SetInformationsInComponents()
        {
            prezimeTextBox.Text = _patient.Surname;
            imeTextBox.Text = _patient.Name;
            JMBGTextBox.Text = _patient.JMBG;
            datumRodjenjaDatePicker.SelectedDate = _patient.DateOfBirth.Date;
            kontaktTextBox.Text = _patient.PhoneNumber;
            emailTextBox.Text = _patient.Email;
            brojKarticeTextBox.Text = _patient.SocialSecurityNumber;
            brojKartonaTextBox.Text = _patient.MedicalRecord.MedicalRecordNumber;
            adresaTextBox.Text = _patient.ResidentialAddress.StreetAndNumber + "," + _patient.ResidentialAddress.City;
        }

        private void ChangeAvailabilityOfComponentsM()
        {
            mRadioButton.IsChecked = true;
            mRadioButton.IsEnabled = false;
            zRadioButton.IsEnabled = false;
        }

        private void ChangeAvailabilityOfComponentsZ()
        {
            zRadioButton.IsChecked = true;
            mRadioButton.IsEnabled = false;
            zRadioButton.IsEnabled = false;
        }

        private void SetEnabledComponents()
        {
            prezimeTextBox.IsEnabled = false;
            imeTextBox.IsEnabled = false;
            JMBGTextBox.IsEnabled = false;
            datumRodjenjaDatePicker.IsEnabled = false;
            kontaktTextBox.IsEnabled = false;
            emailTextBox.IsEnabled = false;
            brojKarticeTextBox.IsEnabled = false;
            brojKartonaTextBox.IsEnabled = false;
            adresaTextBox.IsEnabled = false;

        }
    }
}
