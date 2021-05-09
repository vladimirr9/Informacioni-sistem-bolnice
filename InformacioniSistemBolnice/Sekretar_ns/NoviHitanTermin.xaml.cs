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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for NoviHitanTermin.xaml
    /// </summary>
    public partial class NoviHitanTermin : Window
    {
        public TerminiPage parent;
        private List<string> Patients;
        private List<string> DoctorTypes;
        public NoviHitanTermin(TerminiPage parent)
        {
            this.parent = parent;
            InitializeComponent();
            InitializePatients();
            InitializeDoctorTypes();
        }


        

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            TipLekara doctorType = global::Lekar.LekarTypeFromString(DoctorTypeCombo.SelectedItem.ToString());
            int duration = Int32.Parse(DurationInMinutes.Text);
            string jmbg = PatientsList.SelectedItem.ToString().Split('-')[1].Trim();
            Pacijent patient = PacijentFileStorage.GetOneByJMBG(jmbg);
            DateTime appointmentStart = GetNextEarliestAppointmentTime(DateTime.Today.AddHours(DateTime.Now.TimeOfDay.Hours).AddMinutes(DateTime.Now.TimeOfDay.Minutes));
            
            DateTime appointmentEnd = appointmentStart.AddMinutes(duration);
            TipTermina appointmentType;
            TipProstorije roomType;
            if (AppointmentTypeCombo.Text.Equals("Operacija"))
            {
                appointmentType = TipTermina.operacija;
                roomType = TipProstorije.operacionaSala;
            }
            else
            {
                appointmentType = TipTermina.pregledKodLekaraOpstePrakse;
                roomType = TipProstorije.ordinacija;
            }
                

            List<Prostorija> availableRooms = GetAvailableRooms(appointmentStart, appointmentEnd);
            List<global::Lekar> availableDoctors = GetAvailableDoctors(appointmentStart, appointmentEnd);

            List<Prostorija> filteredRooms = GetFilteredRooms(availableRooms, appointmentType);
            List<global::Lekar> filteredDoctors = GetFilteredDoctors(availableDoctors, doctorType);


            if (filteredRooms.Count > 0 && filteredDoctors.Count > 0)
            {
                foreach (Termin appointmentItem in TerminFileStorage.GetAll())
                {
                    if (appointmentItem.status == StatusTermina.zakazan && appointmentItem.Pacijent.Equals(patient) && (appointmentItem.datumZakazivanja >= appointmentStart && appointmentItem.datumZakazivanja <= appointmentEnd))
                        TerminFileStorage.RemoveTermin(appointmentItem.iDTermina);
                }


                Prostorija room = filteredRooms[0];
                global::Lekar doctor = filteredDoctors[0];
                int id = TerminFileStorage.GetAll().Count + 1;

                Termin appointment = new Termin(id, appointmentStart, duration, appointmentType, StatusTermina.zakazan, patient, doctor, room);
                TerminFileStorage.AddTermin(appointment);
                parent.updateTable();
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




        private List<Prostorija> GetFilteredRooms(List<Prostorija> rooms, TipTermina appointmentType)
        {
            List<Prostorija> filteredRooms = new List<Prostorija>();
            if (appointmentType == TipTermina.operacija)
            {
                foreach (Prostorija room in rooms)
                {
                    if (room.TipProstorije == TipProstorije.operacionaSala)
                        filteredRooms.Add(room);
                }
            }
            else
            {
                foreach (Prostorija room in rooms)
                {
                    if (room.TipProstorije == TipProstorije.ordinacija)
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

        private List<Prostorija> GetAvailableRooms(DateTime pocetak, DateTime kraj)
        {
            List<Prostorija> rooms = new List<Prostorija>();
            foreach (Prostorija room in ProstorijaFileStorage.GetAll())
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
            List<string> vremena = new List<string>();
            DateTime lastPossibleTime = DateTime.Parse("01-Jan-1970" + " " + "19:30");
            for (DateTime potentialTime = DateTime.Parse("01-Jan-1970" + " " + "08:00"); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                vremena.Add(potentialTime.ToString("HH:mm"));
            }
            return vremena;
        }
        private DateTime GetNextEarliestAppointmentTime(DateTime datetime)
        {
            List<string> vremena = GetPossibleAppointmentTimes();
            while (!vremena.Contains(datetime.ToString("HH:mm")))
            {
                datetime = datetime.AddMinutes(1);
            }
            return datetime;
        }

        public void InitializePatients()
        {
            Patients = new List<String>();
            foreach (Pacijent patient in PacijentFileStorage.GetAll())
            {
                if (!patient.isDeleted)
                    Patients.Add(patient.ime + " " + patient.prezime + " - " + patient.jmbg);
            }
            PatientsList.ItemsSource = Patients;
        }
        public void InitializeDoctorTypes()
        {
            DoctorTypes = global::Lekar.GetLekarTypes();
            DoctorTypeCombo.ItemsSource = DoctorTypes;
        }

    }
}
