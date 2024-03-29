﻿using InformacioniSistemBolnice.Controller;
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
        private Patient _patient;
        private int _appointmentDuration;
        private DoctorType _doctorType;
        private RoomType _roomType;
        private AppointmentType _appointmentType;
        private DateTime _earliestAppointmentTime;
        private NewUrgentAppointment _parent;
        private List<Appointment> _appointmentsInTheUpcomingWeek;

        private List<Appointment> _appointments;
        private List<Appointment> _appointmentsForPostponing;

        private AppointmentController _appointmentController = new AppointmentController();
        private AppointmentTimesController _appointmentTimesController = new AppointmentTimesController();
        public PostponeAppointmentWIndow(NewUrgentAppointment parent, Patient patient, int duration, DoctorType doctorType, RoomType roomType, AppointmentType appointmentType, DateTime earliestAppointmentTime)
        {
            _parent = parent;
            _patient = patient;
            _appointmentDuration = duration;
            _doctorType = doctorType;
            _roomType = roomType;
            _appointmentType = appointmentType;
            _earliestAppointmentTime = earliestAppointmentTime;
            _appointmentsForPostponing = new List<Appointment>();
            InitializeComponent();
            _appointmentsInTheUpcomingWeek = GetAppointmentsForUpcomingWeek();
            InitializeAppointments(_appointmentsInTheUpcomingWeek);
            
        }

        private void PostponeButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentData.SelectedIndex == -1)
                return;

            Appointment selectedAppointment = (Appointment)AppointmentData.SelectedItem;
            int id = _appointmentController.GetAll().Count + 1;
            Appointment newAppointment = new Appointment(id, selectedAppointment.AppointmentDate, _appointmentDuration, _appointmentType, AppointmentStatus.scheduled, _patient, selectedAppointment.Doctor, selectedAppointment.Room);
            
            foreach (Appointment appointment in _appointmentsForPostponing)
                Postpone(appointment);
           
            _appointmentController.Add(newAppointment);

            _parent.Close();
            Close();




        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void Postpone(Appointment appointment)
        {
            int postPostponementDuration = GetPostponementDuration(appointment);
            appointment.AppointmentDate = appointment.AppointmentDate.AddMinutes(postPostponementDuration);
            _appointmentController.Update(appointment);
        }

        private int GetPostponementDuration(Appointment appointment)
        {
            appointment.AppointmentStatus = AppointmentStatus.cancelled;
            _appointmentController.Update(appointment);
            DateTime originalStart = appointment.AppointmentDate;
            appointment.AppointmentDate = appointment.AppointmentDate.AddMinutes(_appointmentDuration + 1);
            appointment.AppointmentDate = _appointmentTimesController.GetNextEarliestAppointmentTime(appointment.AppointmentDate);
            while (!(_appointmentTimesController.GetPossibleAppointmentTimes().Contains(appointment.AppointmentDate.ToString("HH:mm")) && appointment.AreAllEntitiesAvailable(_appointmentsInTheUpcomingWeek)))
            {
                appointment.AppointmentDate = appointment.AppointmentDate.AddMinutes(15);
            }
            DateTime potentialNewStart = appointment.AppointmentDate;
            appointment.AppointmentDate = originalStart;
            appointment.AppointmentStatus = AppointmentStatus.scheduled;
            _appointmentController.Update(appointment);
            return Convert.ToInt32((potentialNewStart - originalStart).TotalMinutes);

        }
        private void InitializeAppointments(List<Appointment> appointmentsInTheUpcomingWeek)
        {
            AppointmentData.Items.Clear();
            _appointments = new List<Appointment>();
            foreach (Appointment appointment in appointmentsInTheUpcomingWeek)
            {
                if (appointment.AppointmentStatus == AppointmentStatus.scheduled && appointment.AppointmentDate.Date.Equals(_earliestAppointmentTime.Date) && appointment.AppointmentDate.TimeOfDay >= _earliestAppointmentTime.TimeOfDay && appointment.Room.RoomType == _roomType && appointment.Doctor.doctorType == _doctorType)
                {
                    appointment.PostponementDuration = GetPostponementDuration(appointment);
                    _appointments.Add(appointment);
                }
            }
            _appointments.Sort(Appointment.SortByPostponementDurationAscending);
            foreach (Appointment appointment in _appointments)
            {
                 AppointmentData.Items.Add(appointment);
            }

        }

        private void AppointmentData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentData.SelectedIndex == -1)
                return;
            _appointmentsForPostponing.Clear();
            Appointment selectedAppointment = (Appointment)AppointmentData.SelectedItem;
            DateTime appointmentStart = selectedAppointment.AppointmentDate;
            DateTime appointmentEnd = appointmentStart.AddMinutes(_appointmentDuration);
            foreach(var item in AppointmentData.Items)
            {
                Appointment appointment = (Appointment)item;
                if (appointment.AppointmentDate >= appointmentStart && appointment.AppointmentDate <= appointmentEnd && (appointment.Doctor.Equals(selectedAppointment.Doctor) || (appointment.Room.Equals(selectedAppointment.Room))))
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

        private List<Appointment> GetAppointmentsForUpcomingWeek()
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in _appointmentController.GetAll())
            {
                if (appointment.AppointmentStatus != AppointmentStatus.cancelled && appointment.AppointmentDate >= DateTime.Today && appointment.AppointmentDate <= DateTime.Today.AddDays(7))
                    appointments.Add(appointment);
            }
            return appointments;
        }

    }
}
