using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MedicalRecordPage.xaml
    /// </summary>
    public partial class MedicalRecordPage : Page
    {
        private static StartPatientWindow pp;
        private static Patient _patient;
        private PatientMedicalRecordPage parentp;
        private PatientController _patientController = new PatientController();

        public MedicalRecordPage(StartPatientWindow p, PatientMedicalRecordPage pmrp)
        {
            pp = p;
            parentp = pmrp;
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


        private void EditInformations_OnClick(object sender, RoutedEventArgs e)
        {
            prezimeTextBox.IsEnabled = true;
            imeTextBox.IsEnabled = true;
            emailTextBox.IsEnabled = true;
            kontaktTextBox.IsEnabled = true;



        }

        private void ImeTextBox_OnMouseEnter(object sender, MouseEventArgs e)
        {
           SetValueToPrezimeUpozorenje();
           SetValueToKontaktUpozorenje();
           SetValueToEmailUpozorenje();
        }


        private void PrezimeTextBox_OnMouseEnter(object sender, MouseEventArgs e)
        {
            SetValueToImeUpozorenje();
            SetValueToKontaktUpozorenje();
            SetValueToEmailUpozorenje();
        }

        private void KontaktTextBox_OnMouseEnter(object sender, MouseEventArgs e)
        {
           SetValueToImeUpozorenje();
           SetValueToPrezimeUpozorenje();
           SetValueToEmailUpozorenje();
            

        }

        private void SetValueToImeUpozorenje()
        {
            if (imeTextBox.Text == "")
            {
                imeUpozorenje.Text = "Polje ne sme biti prazno";
            }
            else if (imeTextBox.Text[0] != Char.ToUpper(imeTextBox.Text[0]))
            {
                imeUpozorenje.Text = "Ime mora početi velikim slovom!";
            }
            else
            {
                imeUpozorenje.Text = "";
            }
        }

        private void SetValueToPrezimeUpozorenje()
        {
            if (prezimeTextBox.Text == "")
            {
                prezimeUpozorenje.Text = "Polje ne sme biti prazno";
            }
            else if (prezimeTextBox.Text[0] != Char.ToUpper(prezimeTextBox.Text[0]))
            {
                prezimeUpozorenje.Text = "Prezime mora početi velikim slovom!";
            }
            else
            {
                prezimeUpozorenje.Text = "";
            }

        }

        private void SetValueToKontaktUpozorenje()
        {
            if (kontaktTextBox.Text == "")
            {
                kontaktUpozorenje.Text = "Polje ne sme biti prazno";
            }
            else if (!IsValidPhone(kontaktTextBox.Text))
            {
                kontaktUpozorenje.Text = "Format nije ispravan!";
            }
            else
            {
                kontaktUpozorenje.Text = "";
            }
        }

        private void SetValueToEmailUpozorenje()
        {
            if (emailTextBox.Text == "")
            {
                emailUpozorenje.Text = "Polje ne sme biti prazno";
            }
            else if (!IsValidEmailAddress(emailTextBox.Text))
            {
                emailUpozorenje.Text = "Neispravan format!";
            }

            else
            {
                emailUpozorenje.Text = "";
            }
        }

        private void EmailTextBox_OnMouseEnter(object sender, MouseEventArgs e)
        {
            SetValueToImeUpozorenje();
            SetValueToPrezimeUpozorenje();
            SetValueToKontaktUpozorenje();
        }
        public static bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Boolean temporary = checkCondition();
            if (temporary)
            {
                _patient.Name = imeTextBox.Text;
                _patient.Surname = prezimeTextBox.Text;
                _patient.Email = emailTextBox.Text;
                _patient.PhoneNumber = kontaktTextBox.Text;
                _patientController.Update(_patient.Username, _patient);

                MessageBoxResult result = MessageBox.Show(
                    "Izmene su izvršene uspešno!", "Uspeh",
                    MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    SetInformationsInComponents();
                }
            }
        }
        private Boolean checkCondition()
        {
            Boolean temp = true;
            if (imeTextBox.Text == "" || prezimeTextBox.Text == "" || emailTextBox.Text == "" ||
                kontaktTextBox.Text == "" || adresaTextBox.Text == "")
            {
                MessageBox.Show("Neko polje je ostalo nepopunjeno!", "Greška");
                temp = false;
                return temp;
            }

            if (!IsValidEmailAddress(emailTextBox.Text) || imeTextBox.Text[0] != Char.ToUpper(imeTextBox.Text[0]) ||
                prezimeTextBox.Text[0] != Char.ToUpper(prezimeTextBox.Text[0]) || !IsValidPhone(kontaktTextBox.Text))
            {
                MessageBox.Show("Proverite da li su sva polja unesena u dobrom formatu!", "Greška");
                temp = false;
                return temp;
            }

            return temp;
        }

        
        public bool IsValidPhone(string Phone)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                var r = new Regex(@"\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|
2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|
4[987654310]|3[9643210]|2[70]|7|1)\d{1,14}$");
                return r.IsMatch(Phone);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            pp.startWindow.Content = new ChangePasswordPage(pp, parentp);
        }

       
    }
}
