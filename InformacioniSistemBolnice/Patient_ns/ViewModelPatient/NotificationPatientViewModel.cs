using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice.Patient_ns.ViewModelPatient
{
    class NotificationPatientViewModel : BindableBase
    {
        private StartPatientWindow parent;
        private Patient loggedInPatient;
        private PatientController _patientController = new PatientController();
        private AnamnesisController _anamnesisController = new AnamnesisController();
        private AppointmentController _appointmentController = new AppointmentController();
        private ObservableCollection<string> _notificationListView;
        private List<string> notifications = new List<string>();

        public MyICommand BackCommand { get; set; }

        public ObservableCollection<string> NotificationListView
        {
            get { return _notificationListView; }
            set
            {
                _notificationListView = value;
                OnPropertyChanged("NotificationsShow");
            }
        }

        

        public NotificationPatientViewModel(StartPatientWindow pp) {
            parent = pp;
            loggedInPatient = pp.Patient;
            BackCommand = new MyICommand(Back_Click);
            updateVisibility();
            LoadRemindersForAppointment();
            LoadNotifications();
            LoadReminders();
            NotificationListView = new ObservableCollection<string>(notifications);
        }

        private void Back_Click()
        {
            parent.startWindow.Content = new StartPatientPage(parent);
        }
        private void LoadRemindersForAppointment()
        {
            
            _appointmentController.CheckMissedAppointments();
            foreach (Appointment a in _appointmentController.GetAll())
            {
                if (_appointmentController.IsAppointmentTomorrow(a) &&
                    a.PatientUsername.Equals(loggedInPatient.Username))
                {
                    notifications.Add("Imate zakazan termin u " +
                                                 a.AppointmentDate.ToShortTimeString() + ".");
                }
            }

            
        }

        private void LoadNotifications()
        {
            
            foreach (Therapy t in _patientController.GetTherapiesFromMedicalRecord(loggedInPatient))
            {
                notifications.Add(t.Description);
            }
            
        }

        private void LoadReminders()
        {
            foreach (Note n in _anamnesisController.GetNotesWithReminder(loggedInPatient))
            {
                notifications.Add(n.DescriptionOfNote);
            }
            

        }

        private void updateVisibility()
        {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.iconAndName.Visibility = Visibility.Visible;
        }
    }
}
