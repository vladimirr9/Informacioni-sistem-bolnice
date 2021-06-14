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
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for PriorityDoctorPage.xaml
    /// </summary>
    public partial class PriorityDoctorPage : Page
    {
        private const int trajanjePregleda = 15;
        private ActivityLogController _activityLogController = new ActivityLogController();
        private DoctorControler _doctorControler = new DoctorControler();
        private AppointmentController _appointmentController = new AppointmentController();
        private List<global::Doctor> ljekariLista;
        private List<Appointment> termini;
        private List<string> availableTimes;
        private List<Room> prostorije;
        private StartPatientWindow parent;
        private RoomController _roomController = new RoomController();
        private PatientMakesAppointmentByPriorityPage parentp;

        public PriorityDoctorPage(StartPatientWindow pp, PatientMakesAppointmentByPriorityPage pzppp)
        {
            parent = pp;
            parentp = pzppp;
            termini = new List<Appointment>();
            InitializeComponent();
            ljekariLista = new List<global::Doctor>();
            foreach (Doctor l in _doctorControler.GetAll())
            {
                if (l.doctorType == DoctorType.generalPractitioner)
                {
                    ljekariLista.Add(l);
                }
            }
            ljekari.ItemsSource = ljekariLista;
            submit.IsEnabled = false;
            BlackOutDates();
            date.IsEnabled = false;
            timeLabel.Visibility = Visibility.Hidden;
            times.Visibility = Visibility.Hidden;
            availableTimes = new List<string>();
        }

        private void ljekari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date.IsEnabled = true;
            date.IsEnabled = true;
        }

        private void BlackOutDates()
        {
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            date.BlackoutDates.Add(kalendar);

        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            timeLabel.Visibility = Visibility.Visible;
            times.Visibility = Visibility.Visible;
            pretraziTermine();

        }

        private void pretraziTermine()
        {
            times.Items.Clear();
            availableTimes.Clear();
            termini.Clear();
            global::Doctor selektovanLjekar = (global::Doctor)ljekari.SelectedItem;
            foreach (Appointment t in _appointmentController.GetAll())
            {
                if (t.Doctor.JMBG.Equals(selektovanLjekar.JMBG))
                {
                    if (t.AppointmentDate.Date.Equals(date.SelectedDate) && t.AppointmentStatus == AppointmentStatus.scheduled)
                    {
                        termini.Add(t);
                    }
                }

                if (parent.Patient.Username == t.Patient.Username && t.AppointmentDate.Date.Equals(date.SelectedDate))
                {
                    if (t.AppointmentStatus == AppointmentStatus.scheduled)
                    {

                        termini.Add(t);

                    }
                }
            }

            List<Appointment> terminiBezDuplikata = termini.Distinct().ToList();
            DateTime danas = DateTime.Today;

            for (DateTime tm = danas.AddHours(8); tm < danas.AddHours(20); tm = tm.AddMinutes(15))
            {
                bool slobodno = true;
                foreach (Appointment termin in terminiBezDuplikata)
                {
                    DateTime start = DateTime.Parse(termin.AppointmentDate.ToString("HH:mm"));
                    DateTime end = DateTime.Parse(termin.AppointmentDate.AddMinutes(termin.DurationInMinutes).ToString("HH:mm"));
                    if (tm >= start && tm < end)
                    {
                        slobodno = false;
                    }
                }
                if (slobodno)
                    availableTimes.Add(tm.ToString("HH:mm"));

                if (date.SelectedDate == danas)
                {
                    if (tm < DateTime.Now.AddMinutes(30))
                    {
                        availableTimes.Remove(tm.ToString("HH:mm"));
                    }
                }

            }
            foreach (string time in availableTimes)
            {
                times.Items.Add(time);
            }

        }

        private void times_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            submit.IsEnabled = true;
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            MakeAppointment();
            ActivityLog activity = new ActivityLog(DateTime.Now, parent.Patient.Username, TypeOfActivity.makingAppointment);
            _activityLogController.AddActivity(activity);
        }

        private void MakeAppointment()
        {
            global::Doctor l = (global::Doctor)ljekari.SelectedItem;
            Patient p = parent.Patient;


            var item = times.SelectedItem;
            String t = item.ToString();
            String d = date.Text;
            DateTime start = DateTime.Parse(d + " " + t);
            AppointmentType tipt;
            if (l.doctorType.Equals(DoctorType.generalPractitioner))
            {
                tipt = AppointmentType.generalPractitionerCheckup;
            }
            else if (l.doctorType.Equals(DoctorType.surgeon))
            {
                tipt = AppointmentType.operation;
            }
            else
            {
                tipt = AppointmentType.specialistCheckup;
            }
            int id = _appointmentController.GetAll().Count + 1;
            DateTime end = start.AddMinutes(trajanjePregleda);

            Room prvaDostupnaProstorija = GetAvailableRoom(start, end);
            Appointment appointment = new Appointment(id, start, trajanjePregleda, tipt, AppointmentStatus.scheduled, p, l, prvaDostupnaProstorija);
            _appointmentController.Add(appointment);
            PatientExaminesAppointmentPage ptp = new PatientExaminesAppointmentPage(parent);
            updateVisibility();
            parent.startWindow.Content = ptp;
            ptp.updateTable();

        }

        private void updateVisibility()
        {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.iconAndName.Visibility = Visibility.Visible;
        }

        private Room GetAvailableRoom(DateTime pocetak, DateTime kraj)
        {
            prostorije = new List<Room>();
            foreach (Room prostorija in _roomController.GetAllRooms())
            {
                if (prostorija.IsAvailable(pocetak, kraj) && !prostorija.IsDeleted)
                {
                    prostorije.Add(prostorija);
                }
            }

            return prostorije.ElementAt(0);

        }

    }
}
