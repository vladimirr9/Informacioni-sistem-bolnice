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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for PostponeAppointmentWIndow.xaml
    /// </summary>
    public partial class PostponeAppointmentWIndow : Window
    {
        private Pacijent TargetPatient;
        private int AppointmentDuration;
        private TipLekara DoctorType;
        private TipProstorije RoomType;
        private TipTermina AppointmentType;
        private DateTime EarliestAppointmentTime;
        private NoviHitanTermin Parent;
        private List<Termin> AppointmentsInTheUpcomingWeek;

        private List<Termin> Appointments;
        private List<Termin> AppointmentsForPostponing;
        public PostponeAppointmentWIndow(NoviHitanTermin parent, Pacijent patient, int duration, TipLekara doctorType, TipProstorije roomType, TipTermina appointmentType, DateTime earliestAppointmentTime)
        {
            Parent = parent;
            TargetPatient = patient;
            AppointmentDuration = duration;
            DoctorType = doctorType;
            RoomType = roomType;
            AppointmentType = appointmentType;
            EarliestAppointmentTime = earliestAppointmentTime;
            AppointmentsForPostponing = new List<Termin>();
            InitializeComponent();
            InitializeAppointments();
            AppointmentsInTheUpcomingWeek = GetAppointmentsForUpcomingWeek();
        }

        private void PostponeButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentData.SelectedIndex == -1)
                return;

            Termin selectedAppointment = (Termin)AppointmentData.SelectedItem;
            int id = TerminFileStorage.GetAll().Count + 1;
            Termin newAppointment = new Termin(id, selectedAppointment.datumZakazivanja, AppointmentDuration, AppointmentType, StatusTermina.zakazan, TargetPatient, selectedAppointment.Lekar, selectedAppointment.Prostorija);
            
            foreach (Termin appointment in AppointmentsForPostponing)
                Postpone(appointment);
           
            TerminFileStorage.AddTermin(newAppointment);

            Parent.parent.updateTable();
            Parent.Close();
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
            appointment.datumZakazivanja = appointment.datumZakazivanja.AddMinutes(AppointmentDuration);
            appointment.datumZakazivanja = GetNextEarliestAppointmentTime(appointment.datumZakazivanja);
            while (!(GetPossibleAppointmentTimes().Contains(appointment.datumZakazivanja.ToString("HH:mm")) && appointment.AreAllEntitiesAvailable(AppointmentsInTheUpcomingWeek)))
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

        private void InitializeAppointments()
        {
            AppointmentData.Items.Clear();
            Appointments = new List<Termin>();
            foreach (Termin appointment in TerminFileStorage.GetAll())
            {
                if (appointment.status == StatusTermina.zakazan && appointment.datumZakazivanja.Date.Equals(EarliestAppointmentTime.Date) && appointment.datumZakazivanja.TimeOfDay >= EarliestAppointmentTime.TimeOfDay && appointment.Prostorija.TipProstorije == RoomType && appointment.Lekar.tipLekara == DoctorType)
                {
                    appointment.PostponementDuration = GetPostponementDuration(appointment);
                    AppointmentData.Items.Add(appointment);
                }
            }
            
        }



        private void AppointmentData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentData.SelectedIndex == -1)
                return;
            AppointmentsForPostponing.Clear();
            Termin selectedAppointment = (Termin)AppointmentData.SelectedItem;
            DateTime appointmentStart = selectedAppointment.datumZakazivanja;
            DateTime appointmentEnd = appointmentStart.AddMinutes(AppointmentDuration);
            foreach(var item in AppointmentData.Items)
            {
                Termin appointment = (Termin)item;
                if (appointment.datumZakazivanja >= appointmentStart && appointment.datumZakazivanja <= appointmentEnd && (appointment.Lekar.Equals(selectedAppointment.Lekar) || (appointment.Prostorija.Equals(selectedAppointment.Prostorija))))
                {
                    DataGridRow row = (DataGridRow)AppointmentData.ItemContainerGenerator.ContainerFromItem(item);
                    row.Background = Brushes.Red;
                    AppointmentsForPostponing.Add(appointment);
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
