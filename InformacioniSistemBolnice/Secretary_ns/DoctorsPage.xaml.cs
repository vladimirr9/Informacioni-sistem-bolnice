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

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class DoctorsPage : Page
    {

        public static DoctorsPage _instance;
        private DoctorControler _doctorControler = new DoctorControler();
        public static DoctorsPage GetPage(SecretaryMain parent)
        {
            if (_instance == null)
                _instance = new DoctorsPage();
            else
                _instance.UpdateTable();
            parent.Title.Content = "Doktori";
            return _instance;
        }
        public DoctorsPage()
        {
            InitializeComponent();
            UpdateTable();
        }

        private void Worktime_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorsDataGrid.SelectedItem == null)
                return;
            Doctor selectedDoctor = (Doctor)DoctorsDataGrid.SelectedItem;
            DoctorWorktimeWindow worktimeWindow = new DoctorWorktimeWindow(selectedDoctor);
            worktimeWindow.ShowDialog();
            UpdateTable();
        }
        public void UpdateTable()
        {
            DoctorsDataGrid.Items.Clear();
            List<Doctor> doctors = _doctorControler.GetAll();
            foreach (Doctor doctor in doctors)
            {
                if (!doctor.IsDeleted)
                    DoctorsDataGrid.Items.Add(doctor);
            }
        }
    }
}
