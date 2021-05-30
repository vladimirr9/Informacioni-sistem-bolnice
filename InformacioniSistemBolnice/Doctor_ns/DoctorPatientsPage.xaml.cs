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

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class DoctorPatientsPage : Page
    {
        public DoctorWindow parent;
        private static DoctorPatientsPage instance;
        private PatientController _patientController = new PatientController();

        public DoctorPatientsPage(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            UpdateTable();
        }

        public static DoctorPatientsPage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new DoctorPatientsPage(parent);
            return instance;
        }

        private void Medical_Record_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsDataGrid.SelectedItem != null)
            {
                MedicalRecordWindow recordWindow = new MedicalRecordWindow((Patient)PatientsDataGrid.SelectedItem, parent);
                Application.Current.MainWindow = recordWindow;
                recordWindow.Show();
            }
        }

        public void UpdateTable()
        {
            PatientsDataGrid.Items.Clear();
            foreach (Patient patient in _patientController.GetAll())
            {
                PatientsDataGrid.Items.Add(patient);
            }
        }

    }
}
