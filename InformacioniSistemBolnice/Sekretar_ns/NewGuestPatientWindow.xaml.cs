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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for NewGuestPatientWindow.xaml
    /// </summary>
    public partial class NewGuestPatientWindow : Window
    {
        public NoviHitanTermin parent;
        public Pacijent Patient;
        public NewGuestPatientWindow(NoviHitanTermin parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string name = NameText.Text;
            string surname = SurnameText.Text;
            string JMBG = JMBGText.Text;
            string username = "Guest" + PacijentFileStorage.GetAll().Count.ToString();

            if (IsJMBGUnique(JMBG))
            {
                Patient = new Pacijent(name, surname, JMBG, ' ', "", "", new DateTime(), username, "", new AdresaStanovanja("", new MestoStanovanja("", "", new DrzavaStanovanja(""))), true, "", new ZdravstveniKarton(""));
                PacijentFileStorage.AddPacijent(Patient);
                parent.InitializePatients();
                parent.PatientsList.SelectedItem = Patient.ime + " " + Patient.prezime + " - " + Patient.jmbg;
                Close();
            }
            else
                MessageBox.Show("JMBG koji ste pokušali da unesete se već nalazi u sistemu.", "JMBG postoji u sistemu", MessageBoxButton.OK);
        }

        private static bool IsJMBGUnique(string JMBG)
        {
            foreach (Pacijent patient in PacijentFileStorage.GetAll())
            {
                if (patient.jmbg.Equals(JMBG))
                    return false;
            }

            return true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
