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

namespace InformacioniSistemBolnice.Lekar
{
    public partial class DoctorPatientsPage : Page
    {
        public DoctorWindow parent;
        private static DoctorPatientsPage instance;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsDataGrid.SelectedItem != null)
            {
                MedicalRecordWindow recordWindow = new MedicalRecordWindow((Pacijent)PatientsDataGrid.SelectedItem, parent);
                Application.Current.MainWindow = recordWindow;
                recordWindow.Show();
            }
        }

        public void UpdateTable()
        {
            PatientsDataGrid.Items.Clear();
            List<Pacijent> patients = PacijentFileStorage.GetAll();                 //dodati patient kontroler
            foreach (Pacijent patient in patients)
            {
                if (!patient.isDeleted)
                    PatientsDataGrid.Items.Add(patient);
            }
        }

    }
}
