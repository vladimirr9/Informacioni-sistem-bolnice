using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

namespace InformacioniSistemBolnice.Secretary_ns
{

    public partial class NewUrgentAppointment : Window
    {
        public AppointmentsPage _parent;
        private List<string> _patients;
        private List<string> _doctorTypes;

        private AppointmentController _appointmentController = new AppointmentController();
        private RoomController _roomController = new RoomController();
        private DoctorControler _doctorController = new DoctorControler();
        private PatientController _patientController = new PatientController();
        public NewUrgentAppointment(AppointmentsPage parent)
        {
            this._parent = parent;
            InitializeComponent();
            InitializePatients();
            InitializeDoctorTypes();
        }


        

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            DoctorType doctorType = Doctor.DoctorTypeFromString(DoctorTypeCombo.SelectedItem.ToString());
            int duration = int.Parse(DurationInMinutes.Text);
            string jmbg = PatientsList.SelectedItem.ToString().Split('-')[1].Trim();
            Patient patient = _patientController.GetOneByJMBG(jmbg);
            DateTime appointmentStart = _appointmentController.GetNextEarliestAppointmentTime(DateTime.Today.AddHours(DateTime.Now.TimeOfDay.Hours).AddMinutes(DateTime.Now.TimeOfDay.Minutes));
            
            DateTime appointmentEnd = appointmentStart.AddMinutes(duration);
            AppointmentType appointmentType;
            RoomType roomType;
            if (AppointmentTypeCombo.Text.Equals("Operacija"))
            {
                appointmentType = AppointmentType.operation;
                roomType = RoomType.operatingRoom;
            }
            else
            {
                appointmentType = AppointmentType.generalPractitionerCheckup;
                roomType = RoomType.examinationRoom;
            }
            bool retVal = _appointmentController.CreateNewUrgentAppointment(patient, duration, doctorType, roomType, appointmentType, appointmentStart, appointmentEnd);
            if (retVal)
            {
                _parent.UpdateTable();
                Close();
            }
            else
            {
                PostponeAppointmentWIndow postponeWindow = new PostponeAppointmentWIndow(this, patient, duration, doctorType, roomType, appointmentType, appointmentStart);
                postponeWindow.ShowDialog();
            }
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


        public void InitializePatients()
        {
            _patients = new List<String>();
            foreach (Patient patient in _patientController.GetAll())
            {
                if (!patient.IsDeleted)
                    _patients.Add(patient.Name + " " + patient.Surname + " - " + patient.JMBG);
            }
            PatientsList.ItemsSource = _patients;
        }
        public void InitializeDoctorTypes()
        {
            _doctorTypes = global::Doctor.GetDoctorTypes();
            DoctorTypeCombo.ItemsSource = _doctorTypes;
        }

    }
}
