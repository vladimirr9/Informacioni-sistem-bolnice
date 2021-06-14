using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for PatientEditsAppointmentPage.xaml
    /// </summary>
    public partial class PatientEditsAppointmentPage : Page
    {
        private const int trajanjePregleda = 15;
        private ActivityLogController _activityLogController = new ActivityLogController();
        private DoctorControler _doctorControler = new DoctorControler();
        private AppointmentController _appointmentController = new AppointmentController();
        private StartPatientWindow parent;
        private Appointment selektovan;
        private List<string> availableTimes;
        private List<Appointment> termini;
        private List<global::Doctor> lekari;
        private List<Room> prostorije;
        private RoomController _roomController = new RoomController();
        private int brojac;
        public PatientEditsAppointmentPage(Appointment selektovan, StartPatientWindow prozor)
        {
            this.selektovan = selektovan;
            this.parent = prozor;
            brojac = 0;
            InitializeComponent();
            parent.titleLabel.Content = "Pomeranje termina";
            parent.titleLabel.Visibility = Visibility.Visible;
            availableTimes = new List<string>();
            termini = _appointmentController.GetAll();
            time.ItemsSource = availableTimes;
            prostorije = _roomController.GetAllRooms();
            LoadTimes();
            BlackOutDates();
            date.SelectedDate = selektovan.AppointmentDate;
            time.SelectedItem = selektovan.AppointmentDate.ToString("HH:mm");
            lekari = new List<global::Doctor>();
            FillDoctorsComboBox();
            SetSelectedDoctor();
        }

        private void FillDoctorsComboBox()
        {
            foreach (global::Doctor doctor in _doctorControler.GetDoctorsByType(DoctorType.generalPractitioner))
            { 
                lekari.Add(doctor);
            }
            lekar.ItemsSource = lekari;
        }

        private void SetSelectedDoctor()
        {
            foreach (global::Doctor l in lekari)
            {
                if (l.JMBG == selektovan.Doctor.JMBG)
                    lekar.SelectedItem = l;
            }
        }

        private void LoadTimes()
        {
            DateTime datum;
            global::Doctor l = (global::Doctor)lekar.SelectedItem;
            if (date.SelectedDate != null)
            {
                datum = DateTime.Parse(date.Text);
            }
            else
            {
                datum = DateTime.Now;
            }
            availableTimes = new List<string>();
            List<Appointment> termini = new List<Appointment>();
            DateTime danas = DateTime.Today;

            for (DateTime tm = danas.AddHours(8); tm < danas.AddHours(20); tm = tm.AddMinutes(15))
            {
                bool slobodno = true;
                foreach (Appointment termin in termini)
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

            time.ItemsSource = availableTimes;
        }

        private void BlackOutDates()
        {
            date.SelectedDate = selektovan.AppointmentDate.Date;
            CalendarDateRange kal = new CalendarDateRange(DateTime.Today, DateTime.Today);
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, selektovan.AppointmentDate.AddDays(-3));
            CalendarDateRange kalendar1 = new CalendarDateRange(selektovan.AppointmentDate.AddDays(3), DateTime.MaxValue);
            date.BlackoutDates.Add(kalendar);
            date.BlackoutDates.Add(kalendar1);
            date.BlackoutDates.Add(kal);
        }
        private void UpdateComponents()
        {
            DateTime start;
            DateTime end;

            CalculateStartAndEnd(out start, out end);

            setEnabledButtonSubmit();
            Debug.WriteLine(brojac);
            if (brojac > 4)
            {
                CheckAvailableTimes();
            }
            else
            {
                LoadTimes();
            }

            lekari = new List<global::Doctor>();
            foreach (Doctor l in _doctorControler.GetAll())
            {
                if (l.IsAvailable(start, end) && !l.IsDeleted)
                {
                    lekari.Add(l);
                }
            }



        }

        private void setEnabledButtonSubmit()
        {
            if (lekar.SelectedItem != null && date.SelectedDate != null && time.SelectedItem != null)
            {
                submitButton.IsEnabled = true;
            }
            else
            {
                submitButton.IsEnabled = false;
            }
        }

        private void CheckAvailableTimes()
        {

            DateTime datum;
            global::Doctor l = (global::Doctor)lekar.SelectedItem;
            if (date.SelectedDate != null)
            {
                datum = DateTime.Parse(date.Text);
            }
            else
            {
                datum = DateTime.Now;
            }

            availableTimes = new List<string>();
            List<Appointment> termini = new List<Appointment>();
            if (lekar.SelectedItem != null && date.SelectedDate != null)
            {
                foreach (Appointment termin in _appointmentController.GetAll())
                {
                    if (l.JMBG == termin.Doctor.JMBG)
                    {
                        if (termin.AppointmentStatus == AppointmentStatus.scheduled && termin.AppointmentDate.Date.Equals(date.SelectedDate))
                        {
                            termini.Add(termin);
                        }
                    }

                    if (parent.Patient.Username == termin.Patient.Username)
                    {
                        if (termin.AppointmentStatus == AppointmentStatus.scheduled && termin.AppointmentDate.Date.Equals(date.SelectedDate))
                        {
                            termini.Add(termin);
                        }
                    }


                }
            }

            DateTime danas = DateTime.Today;

            for (DateTime tm = danas.AddHours(8); tm < danas.AddHours(20); tm = tm.AddMinutes(15))
            {
                bool slobodno = true;
                foreach (Appointment termin in termini)
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

                /*if (DatePicker.SelectedDate == danas)
                {
                    if (tm < DateTime.Now.AddMinutes(30))
                    {
                        availableTimes.Remove(tm.ToString("HH:mm"));
                    }
                }*/

            }

            time.ItemsSource = availableTimes;
            if (availableTimes.Contains(selektovan.AppointmentDate.ToString("HH:mm")))
            {
                time.SelectedItem = selektovan.AppointmentDate.ToString("HH:mm");
            }


        }

        private void CalculateStartAndEnd(out DateTime start, out DateTime end)
        {
            if (time.SelectedItem != null && date.SelectedDate != null)
            {
                String timeSelected = time.SelectedItem.ToString();
                String dateSelected = date.Text;

                start = DateTime.Parse(dateSelected + " " + timeSelected);
                end = start.AddMinutes(trajanjePregleda);
            }
            else
            {
                start = DateTime.Now;
                end = DateTime.Now;
            }
        }

        private void time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void lekar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ++brojac;
            UpdateComponents();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PatientExaminesAppointmentPage(parent);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            updateVisibility();
            parent.startWindow.Content = new PatientExaminesAppointmentPage(parent);

        }

        private void updateVisibility()
        {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.iconAndName.Visibility = Visibility.Visible;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            global::Doctor l = (global::Doctor)lekar.SelectedItem;
            Patient p = parent.Patient;

            if (time.SelectedIndex != -1)
            {
                var item = time.SelectedItem;
                String t = item.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                AppointmentType tt = AppointmentType.generalPractitionerCheckup;

                DateTime start;
                DateTime end;
                CalculateStartAndEnd(out start, out end);

                Room prvaDostupnaProstorija = GetAvailableRoom(start, end);

                Appointment appointment = new Appointment(selektovan.AppointmentID, dt, trajanjePregleda, tt, AppointmentStatus.scheduled, p, l, prvaDostupnaProstorija);
                _appointmentController.Update(appointment);
                PatientExaminesAppointmentPage ptp = new PatientExaminesAppointmentPage(parent);
                updateVisibility();
                parent.startWindow.Content = ptp;
                ptp.updateTable();

                ActivityLog activity = new ActivityLog(DateTime.Now, parent.Patient.Username, TypeOfActivity.editingAppointment);
                _activityLogController.AddActivity(activity);
            }



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
