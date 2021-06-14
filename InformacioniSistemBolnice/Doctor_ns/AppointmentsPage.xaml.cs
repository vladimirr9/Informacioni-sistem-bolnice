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
using InformacioniSistemBolnice.View.ViewModel;
using Microsoft.Win32;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class AppointmentsPage : Page
    {
        private static AppointmentsPage instance;
        private StatusOfAppointmentController _statusOfAppointmentController = new StatusOfAppointmentController();

        public AppointmentsPage(DoctorWindow parent)
        {
            InitializeComponent();
            this.DataContext = new AppointmentsViewModel(parent);
            _statusOfAppointmentController.CheckMissedAppointments();
        }
    }
}
