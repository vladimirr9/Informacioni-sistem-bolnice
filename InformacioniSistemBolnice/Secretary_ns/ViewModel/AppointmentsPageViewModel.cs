using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Secretary_ns.ViewModel
{
    class AppointmentsPageViewModel : BindableBase
    { 
        private Appointment selectedAppointment;
        private AppointmentController _appointmentController = new AppointmentController();
        private StatusOfAppointmentController _statusOfAppointmentController = new StatusOfAppointmentController();
        private ObservableCollection<Appointment> appointments;
        private DateTime? _filter;
        public MyICommand NewAppointmentCommand { get; set; }
        public MyICommand EditAppointmentCommand { get; set; }
        public MyICommand DeleteAppointmentCommand { get; set; }
        public MyICommand UrgentAppointmentCommand { get; set; }



        public DateTime? Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPropertyChanged("Filter");
                UpdateTable();
            }
        }
        public Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
            }
        }

        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }

        public AppointmentsPageViewModel()
        {
            NewAppointmentCommand = new MyICommand(Add_Click);
            EditAppointmentCommand = new MyICommand(Edit_Click);
            DeleteAppointmentCommand = new MyICommand(Delete_Click);
            UrgentAppointmentCommand = new MyICommand(UrgentAppointment_Click);
            _statusOfAppointmentController.CheckMissedAppointments();
            UpdateTable();
        }


        private void Add_Click()
        {
            NewAppointmentWindow window = new NewAppointmentWindow();
            window.ShowDialog();
            UpdateTable();
        }

        private void Edit_Click()
        {
            if (SelectedAppointment == null)
                return;
            Appointment initialAppointment = _appointmentController.GetOne(SelectedAppointment);
            _appointmentController.Remove(initialAppointment);
            EditAppointmentWindow window = new EditAppointmentWindow(initialAppointment);
            window.ShowDialog();
            UpdateTable();
        }

        private void Delete_Click()
        {
            if (SelectedAppointment == null)
                return;

            var result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovaj termin?", "Potvrda brisanja", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                return;

            _appointmentController.Remove(SelectedAppointment);
            UpdateTable();
        }
        private void UrgentAppointment_Click()
        {
            NewUrgentAppointment window = new NewUrgentAppointment();
            window.ShowDialog();
            UpdateTable();
        }



        public void UpdateTable()
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in _appointmentController.GetAll())
            {
                if (appointment.AppointmentStatus == AppointmentStatus.scheduled)
                    if (Filter == null)
                        appointments.Add(appointment);
                    else
                    {
                        if (appointment.AppointmentDate.Date == ((DateTime)Filter).Date)
                            appointments.Add(appointment);
                    }
            }
            appointments.Sort((x, y) => DateTime.Compare(x.AppointmentDate, y.AppointmentDate));
            Appointments = new ObservableCollection<Appointment>(appointments);
            OnPropertyChanged("Appointments");
        }
    }
}
