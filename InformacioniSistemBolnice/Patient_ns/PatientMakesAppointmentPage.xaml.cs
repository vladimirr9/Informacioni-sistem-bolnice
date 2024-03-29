﻿using System;
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
    /// Interaction logic for PatientMakesAppointmentPage.xaml
    /// </summary>
    public partial class PatientMakesAppointmentPage : Page
    {
        private const int trajanjePregleda = 15;
        private ActivityLogController _activityLogController = new ActivityLogController();
        private DoctorControler _doctorController = new DoctorControler();
        private AppointmentController _appointmentController = new AppointmentController();
        private StartPatientWindow parent;
        private List<string> availableTimes;
        private List<global::Doctor> lekari;
        private List<Room> prostorije;
        private Patient _patient;
        private RoomController _roomController = new RoomController();

        public PatientMakesAppointmentPage(StartPatientWindow p)
        {
            parent = p;
            InitializeComponent();
            parent.UpdateVisibilityOfComponents();
            availableTimes = new List<string>();
            timeComboBox();
            dugmePotvrdi.IsEnabled = false;
            prostorije = _roomController.GetAllRooms();
            BlackOutDates();
            lekari = new List<global::Doctor>();
            FillDoctorsComboBox();
            lekar.ItemsSource = lekari;
            _patient = parent.Patient;
            parent.titleLabel.Content = "Zakazivanje pregleda";
            parent.titleLabel.Visibility = Visibility.Visible;
        }
        private void FillDoctorsComboBox()
        {
            foreach (global::Doctor doctor in _doctorController.GetDoctorsByType(DoctorType.generalPractitioner))
            {
                lekari.Add(doctor);
            }
            lekar.ItemsSource = lekari;
        }

        private void timeComboBox()
        {

            DateTime danas = DateTime.Today;
            for (DateTime tm = danas.AddHours(8); tm < danas.AddHours(20); tm = tm.AddMinutes(15))
            {
                availableTimes.Add(tm.ToString("HH:mm"));
            }
            time.ItemsSource = availableTimes;
        }

        private void BlackOutDates()
        {
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            date.BlackoutDates.Add(kalendar);
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            global::Doctor l = (global::Doctor)lekar.SelectedItem;
            Patient p = parent.Patient;

            if (time.SelectedIndex != -1)
            {
                var item = time.SelectedItem;
                String t = item.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
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

                DateTime start;
                DateTime end;
                CalculateStartAndEnd(out start, out end);

                Room prvaDostupnaProstorija = GetAvailableRoom(start, end);
                Appointment appointment = new Appointment(id, dt, trajanjePregleda, tipt, AppointmentStatus.scheduled, p, l,
                    prvaDostupnaProstorija);
                _appointmentController.Add(appointment);
                PatientExaminesAppointmentPage ptp = new PatientExaminesAppointmentPage(parent);
                updateVisibility();
                parent.startWindow.Content = ptp;
                ptp.updateTable();
                ActivityLog activity = new ActivityLog(DateTime.Now, _patient.Username,
                        TypeOfActivity.makingAppointment);
                _activityLogController.AddActivity(activity);
            }
        }




        private void UpdateComponents()
        {
            DateTime start;
            DateTime end;
            CalculateStartAndEnd(out start, out end);
            SetEnabledButtonSubmit();
            CheckAvailableTimes();
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
                        if (termin.AppointmentStatus == AppointmentStatus.scheduled &&
                            termin.AppointmentDate.Date.Equals(date.SelectedDate))
                        {
                            termini.Add(termin);
                        }
                    }

                    if (_patient.Username == termin.Patient.Username)
                    {
                        if (termin.AppointmentStatus == AppointmentStatus.scheduled &&
                            termin.AppointmentDate.Date.Equals(date.SelectedDate))
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
                    DateTime end = DateTime.Parse(termin.AppointmentDate.AddMinutes(termin.DurationInMinutes)
                        .ToString("HH:mm"));
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

        private void SetEnabledButtonSubmit()
        {
            if (lekar.SelectedItem != null && date.SelectedDate != null && time.SelectedItem != null)
            {
                dugmePotvrdi.IsEnabled = true;
            }
            else
            {
                dugmePotvrdi.IsEnabled = false;
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

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void lekar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private Room GetAvailableRoom(DateTime pocetak, DateTime kraj) //u neki servis premjestiti
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

        private void search_Click_1(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PatientMakesAppointmentByPriorityPage(parent);
        }
    }
}
