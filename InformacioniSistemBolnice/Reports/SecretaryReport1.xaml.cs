using InformacioniSistemBolnice.Controller;
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

namespace InformacioniSistemBolnice.Reports
{

    public partial class SecretaryReport1 : Page
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        private AppointmentController _appointmentController = new AppointmentController();
        public SecretaryReport1(DateTime from, DateTime to)
        {
            From = from;
            To = to;
            InitializeComponent();
            UpdateTable();
        }
        public void UpdateTable()
        {
            AppointmentPreview.Items.Clear();
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in _appointmentController.GetAll())
            {
                if (appointment.AppointmentStatus == AppointmentStatus.scheduled && appointment.AppointmentDate <= To && appointment.AppointmentDate >= From)
                    appointments.Add(appointment);
            }
            appointments.Sort((x, y) => DateTime.Compare(x.AppointmentDate, y.AppointmentDate));
            AppointmentPreview.ItemsSource = appointments;
            FromLabel.Content = From.ToString("dd/MM/yyyy");
            ToLabel.Content = To.ToString("dd/MM/yyyy");

        }
    }
}
