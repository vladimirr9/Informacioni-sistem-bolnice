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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for NoviHitanTermin.xaml
    /// </summary>
    public partial class NoviHitanTermin : Window
    {
        private TerminiPage parent;
        private List<string> Patients;
        private List<string> DoctorTypes;
        public NoviHitanTermin(TerminiPage parent)
        {
            this.parent = parent;
            InitializeComponent();
            InitializePatients();
        }


        public void InitializePatients()
        {
            Patients = new List<String>();
            foreach (Pacijent patient in PacijentFileStorage.GetAll())
            {
                if (!patient.isDeleted)
                    Patients.Add(patient.ime + " " + patient.prezime + " - " + patient.jmbg);
            }
            PatientsList.ItemsSource = Patients;
        }
        public void InitializeDoctorTypes()
        {
            DoctorTypes = new List<string>();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            TipLekara doctorType;
            int duration = Int32.Parse(DurationInMinutes.Text);
            string jmbg = PatientsList.SelectedItem.ToString().Split('-')[1].Trim();
            Pacijent patient = PacijentFileStorage.GetOneByJMBG(jmbg);
            

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NewGuestClick(object sender, RoutedEventArgs e)
        {
            NewGuestPatientWindow window = new NewGuestPatientWindow(this);
            window.ShowDialog();
        }


    }
}
