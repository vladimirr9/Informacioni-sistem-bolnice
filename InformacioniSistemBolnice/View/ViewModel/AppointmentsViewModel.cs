using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Doctor_ns;

namespace InformacioniSistemBolnice.View.ViewModel
{
    class AppointmentsViewModel : BindableBase
    {
        public DoctorWindow parent;
        private static AppointmentsViewModel instance;
        private Appointment selectedAppointment;
        private AppointmentController _appointmentController = new AppointmentController();
        public ObservableCollection<Appointment> Appointments { get; set; }
        public MyICommand NewAppointmentCommand { get; set; }
        public MyICommand EditAppointmentCommand { get; set; }
        public  MyICommand DeleteAppointmentCommand { get; set; }
        public MyICommand DoubleClickComand { get; set; }


        public Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
            }
        }

        public AppointmentsViewModel(DoctorWindow parent)
        {
            this.parent = parent;
            NewAppointmentCommand = new MyICommand(Add_Click);
            EditAppointmentCommand = new MyICommand(Edit_Click);
            DeleteAppointmentCommand = new MyICommand(Delete_Click);
            DoubleClickComand = new MyICommand(Double_Click);
            _appointmentController.CheckMissedAppointments();
            UpdateTable();
        }

        public static AppointmentsViewModel GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new AppointmentsViewModel(parent);
            return instance;
        }

        private void Add_Click()
        {
            DoctorAddAppointmentWindow addWindow = new DoctorAddAppointmentWindow(parent);
            Application.Current.MainWindow = addWindow;
            addWindow.Show();
        }

        private void Edit_Click()
        {
            if (selectedAppointment != null)
            {
                DoctorEditAppointmentWindow editWindow = new DoctorEditAppointmentWindow(_appointmentController.GetOne(selectedAppointment), parent);
                Application.Current.MainWindow = editWindow;
                editWindow.Show();
            }
        }

        private void Delete_Click()
        {
            if (selectedAppointment != null)
            {
                _appointmentController.Remove(selectedAppointment);
                UpdateTable();
            }
        }

        private void Double_Click()
        {
            MedicalRecordWindow recordWindow = new MedicalRecordWindow(selectedAppointment.Patient, parent, selectedAppointment);
            Application.Current.MainWindow = recordWindow;
            recordWindow.Show();
        }

        public void UpdateTable()
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in _appointmentController.GetScheduled())
            {
                appointments.Add(appointment);
            }
            this.Appointments = new ObservableCollection<Appointment>(appointments);
            OnPropertyChanged("Appointments");
        }
    }
}
