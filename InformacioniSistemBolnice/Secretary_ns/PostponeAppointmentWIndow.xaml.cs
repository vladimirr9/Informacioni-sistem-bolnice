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
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Secretary_ns
{
    /// <summary>
    /// Interaction logic for PostponeAppointmentWIndow.xaml
    /// </summary>
    public partial class PostponeAppointmentWIndow : Window
    {
        private Pacijent _patient;
        private int _appointmentDuration;
        private DoctorType _doctorType;
        private RoomType _roomType;
        private TipTermina _appointmentType;
        private DateTime _earliestAppointmentTime;
        private NewUrgentAppointment _parent;
        private List<Termin> _appointmentsInTheUpcomingWeek;

        private List<Termin> _appointments;
        private List<Termin> _appointmentsForPostponing;
        public PostponeAppointmentWIndow(NewUrgentAppointment parent, Pacijent patient, int duration, DoctorType doctorType, RoomType roomType, TipTermina appointmentType, DateTime earliestAppointmentTime)
        {
            _parent = parent;
            _patient = patient;
            _appointmentDuration = duration;
            _doctorType = doctorType;
            _roomType = roomType;
            _appointmentType = appointmentType;
            _earliestAppointmentTime = earliestAppointmentTime;
            _appointmentsForPostponing = new List<Termin>();
            InitializeComponent();
            _appointmentsInTheUpcomingWeek = GetAppointmentsForUpcomingWeek();
            InitializeAppointments(_appointmentsInTheUpcomingWeek);
            
        }

        private void PostponeButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentData.SelectedIndex == -1)
                return;

            Termin selectedAppointment = (Termin)AppointmentData.SelectedItem;
            int id = TerminFileStorage.GetAll().Count + 1;
            Termin newAppointment = new Termin(id, selectedAppointment.datumZakazivanja, _appointmentDuration, _appointmentType, StatusTermina.zakazan, _patient, selectedAppointment.Doctor, selectedAppointment.Prostorija);
            
            foreach (Termin appointment in _appointmentsForPostponing)
                Postpone(appointment);
           
            TerminFileStorage.AddTermin(newAppointment);

            _parent._parent.UpdateTable();
            _parent.Close();
            Close();




        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void Postpone(Termin appointment)
        {
            int postPostponementDuration = GetPostponementDuration(appointment);
            appointment.datumZakazivanja = appointment.datumZakazivanja.AddMinutes(postPostponementDuration);
            TerminFileStorage.UpdateTermin(appointment.iDTermina, appointment);
        }

        private int GetPostponementDuration(Termin appointment)
        {
            appointment.status = StatusTermina.otkazan;
            TerminFileStorage.UpdateTermin(appointment.iDTermina, appointment);
            DateTime originalStart = appointment.datumZakazivanja;
            appointment.datumZakazivanja = appointment.datumZakazivanja.AddMinutes(_appointmentDuration + 1);
            appointment.datumZakazivanja = GetNextEarliestAppointmentTime(appointment.datumZakazivanja);
            while (!(GetPossibleAppointmentTimes().Contains(appointment.datumZakazivanja.ToString("HH:mm")) && appointment.AreAllEntitiesAvailable(_appointmentsInTheUpcomingWeek)))
            {
                appointment.datumZakazivanja = appointment.datumZakazivanja.AddMinutes(15);
            }
            DateTime potentialNewStart = appointment.datumZakazivanja;
            appointment.datumZakazivanja = originalStart;
            appointment.status = StatusTermina.zakazan;
            TerminFileStorage.UpdateTermin(appointment.iDTermina, appointment);
            return Convert.ToInt32((potentialNewStart - originalStart).TotalMinutes);

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

        private void InitializeAppointments(List<Termin> appointmentsInTheUpcomingWeek)
        {
            AppointmentData.Items.Clear();
            _appointments = new List<Termin>();
            foreach (Termin appointment in appointmentsInTheUpcomingWeek)
            {
                if (appointment.status == StatusTermina.zakazan && appointment.datumZakazivanja.Date.Equals(_earliestAppointmentTime.Date) && appointment.datumZakazivanja.TimeOfDay >= _earliestAppointmentTime.TimeOfDay && appointment.Prostorija.RoomType == _roomType && appointment.Doctor.doctorType == _doctorType)
                {
                    appointment.PostponementDuration = GetPostponementDuration(appointment);
                    _appointments.Add(appointment);
                }
            }
            _appointments.Sort(Termin.SortByPostponementDurationAscending);
            foreach (Termin appointment in _appointments)
            {
                 AppointmentData.Items.Add(appointment);
            }

        }



        private void AppointmentData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentData.SelectedIndex == -1)
                return;
            _appointmentsForPostponing.Clear();
            Termin selectedAppointment = (Termin)AppointmentData.SelectedItem;
            DateTime appointmentStart = selectedAppointment.datumZakazivanja;
            DateTime appointmentEnd = appointmentStart.AddMinutes(_appointmentDuration);
            foreach(var item in AppointmentData.Items)
            {
                Termin appointment = (Termin)item;
                if (appointment.datumZakazivanja >= appointmentStart && appointment.datumZakazivanja <= appointmentEnd && (appointment.Doctor.Equals(selectedAppointment.Doctor) || (appointment.Prostorija.Equals(selectedAppointment.Prostorija))))
                {
                    DataGridRow row = (DataGridRow)AppointmentData.ItemContainerGenerator.ContainerFromItem(item);
                    row.Background = Brushes.Red;
                    _appointmentsForPostponing.Add(appointment);
                }
                else
                {
                    DataGridRow row = (DataGridRow)AppointmentData.ItemContainerGenerator.ContainerFromItem(item);
                    row.Background = Brushes.White;
                }
            }
        }

        private List<Termin> GetAppointmentsForUpcomingWeek()
        {
            List<Termin> appointments = new List<Termin>();
            foreach (Termin appointment in TerminFileStorage.GetAll())
            {
                if (appointment.status != StatusTermina.otkazan && appointment.datumZakazivanja >= DateTime.Today && appointment.datumZakazivanja <= DateTime.Today.AddDays(7))
                    appointments.Add(appointment);
            }
            return appointments;
        }

    }
}
