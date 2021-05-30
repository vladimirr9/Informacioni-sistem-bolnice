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
    /// <summary>
    /// Interaction logic for NewUrgentAppointment.xaml
    /// </summary>
    public partial class NewUrgentAppointment : Window
    {
        public AppointmentsPage _parent;
        private List<string> _patients;
        private List<string> _doctorTypes;
        public NewUrgentAppointment(AppointmentsPage parent)
        {
            this._parent = parent;
            InitializeComponent();
            InitializePatients();
            InitializeDoctorTypes();
        }


        

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            TipLekara doctorType = global::Lekar.LekarTypeFromString(DoctorTypeCombo.SelectedItem.ToString());
            int duration = int.Parse(DurationInMinutes.Text);
            string jmbg = PatientsList.SelectedItem.ToString().Split('-')[1].Trim();
            Pacijent patient = PacijentFileStorage.GetOneByJMBG(jmbg);
            DateTime appointmentStart = GetNextEarliestAppointmentTime(DateTime.Today.AddHours(DateTime.Now.TimeOfDay.Hours).AddMinutes(DateTime.Now.TimeOfDay.Minutes));
            
            DateTime appointmentEnd = appointmentStart.AddMinutes(duration);
            TipTermina appointmentType;
            RoomType roomType;
            if (AppointmentTypeCombo.Text.Equals("Operacija"))
            {
                appointmentType = TipTermina.operacija;
                roomType = RoomType.operatingRoom;
            }
            else
            {
                appointmentType = TipTermina.pregledKodLekaraOpstePrakse;
                roomType = RoomType.examinationRoom;
            }
                

            List<Room> availableRooms = GetAvailableRooms(appointmentStart, appointmentEnd);
            List<global::Lekar> availableDoctors = GetAvailableDoctors(appointmentStart, appointmentEnd);

            List<Room> filteredRooms = GetFilteredRooms(availableRooms, appointmentType);
            List<global::Lekar> filteredDoctors = GetFilteredDoctors(availableDoctors, doctorType);


            if (filteredRooms.Count > 0 && filteredDoctors.Count > 0)
            {
                foreach (Termin appointmentItem in TerminFileStorage.GetAll())
                {
                    if (appointmentItem.status == StatusTermina.zakazan && appointmentItem.Pacijent.Equals(patient) && (appointmentItem.datumZakazivanja >= appointmentStart && appointmentItem.datumZakazivanja <= appointmentEnd))
                        TerminFileStorage.RemoveTermin(appointmentItem.iDTermina);
                }


                Room room = filteredRooms[0];
                global::Lekar doctor = filteredDoctors[0];
                int id = TerminFileStorage.GetAll().Count + 1;

                Termin appointment = new Termin(id, appointmentStart, duration, appointmentType, StatusTermina.zakazan, patient, doctor, room);
                TerminFileStorage.AddTermin(appointment);
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




        private List<Room> GetFilteredRooms(List<Room> rooms, TipTermina appointmentType)
        {
            List<Room> filteredRooms = new List<Room>();
            if (appointmentType == TipTermina.operacija)
            {
                foreach (Room room in rooms)
                {
                    if (room.RoomType == RoomType.operatingRoom)
                        filteredRooms.Add(room);
                }
            }
            else
            {
                foreach (Room room in rooms)
                {
                    if (room.RoomType == RoomType.examinationRoom)
                        filteredRooms.Add(room);
                }
            }
            return filteredRooms;
        }
        private List<global::Lekar> GetFilteredDoctors(List<global::Lekar> doctors, TipLekara doctorType)
        {
            List<global::Lekar> filteredDoctors = new List<global::Lekar>();
            foreach (global::Lekar doctor in doctors)
            {
                if (doctor.tipLekara == doctorType)
                    filteredDoctors.Add(doctor);
            }
            return filteredDoctors;
        }

        private List<Room> GetAvailableRooms(DateTime pocetak, DateTime kraj)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room room in RoomFileRepoistory.GetAll())
            {
                if (room.IsAvailable(pocetak, kraj) && !room.IsDeleted)
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }
        private List<global::Lekar> GetAvailableDoctors(DateTime pocetak, DateTime kraj)
        {
            List<global::Lekar> doctors = new List<global::Lekar>();
            foreach (global::Lekar doctor in LekarFileStorage.GetAll())
            {
                if (doctor.IsAvailable(pocetak, kraj) && !doctor.isDeleted)
                {
                    doctors.Add(doctor);
                }
            }
            return doctors;
        }
        private List<string> GetPossibleAppointmentTimes()
        {
            List<string> times = new List<string>();
            DateTime lastPossibleTime = DateTime.Parse("01-Jan-1970" + " " + "19:30");
            for (DateTime potentialTime = DateTime.Parse("01-Jan-1970" + " " + "08:00"); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                times.Add(potentialTime.ToString("HH:mm"));
            }
            return times;
        }
        private DateTime GetNextEarliestAppointmentTime(DateTime datetime)
        {
            List<string> times = GetPossibleAppointmentTimes();
            while (!times.Contains(datetime.ToString("HH:mm")))
            {
                datetime = datetime.AddMinutes(1);
            }
            return datetime;
        }

        public void InitializePatients()
        {
            _patients = new List<String>();
            foreach (Pacijent patient in PacijentFileStorage.GetAll())
            {
                if (!patient.isDeleted)
                    _patients.Add(patient.ime + " " + patient.prezime + " - " + patient.jmbg);
            }
            PatientsList.ItemsSource = _patients;
        }
        public void InitializeDoctorTypes()
        {
            _doctorTypes = global::Lekar.GetLekarTypes();
            DoctorTypeCombo.ItemsSource = _doctorTypes;
        }

    }
}
