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
        private List<string> Patients;
        public NoviHitanTermin()
        {
            InitializeComponent();
            InitializePatients();
        }


        private void InitializePatients()
        {
            Patients = new List<String>();
            foreach (Pacijent patient in PacijentFileStorage.GetAll())
            {
                if (!patient.isDeleted)
                    Patients.Add(patient.ime + " " + patient.prezime + " - " + patient.jmbg);
            }
            PatientsList.ItemsSource = Patients;
            PatientsList.SelectedIndex = 0;
        }

        private void NewGuest(object sender, RoutedEventArgs e)
        {

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
