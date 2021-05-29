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
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Lekar
{
    public partial class DoctorEditAppointmentWindow : Window
    {
        private DoctorWindow parent;
        private Termin selected;
        public DoctorEditAppointmentWindow(Termin selected, DoctorWindow parent)
        {
            this.selected = selected;
            this.parent = parent;
            InitializeComponent();

            List<Doctor> doctors = LekarFileStorage.GetAll();
            DoctorComboBox.ItemsSource = doctors;
            List<Pacijent> patients = PacijentFileStorage.GetAll();
            PatientComboBox.ItemsSource = patients;
            List<Prostorija> rooms = ProstorijaFileStorage.GetAll();
            RoomComboBox.ItemsSource = rooms;

            date.SelectedDate = selected.datumZakazivanja;
            time.SelectedValue = selected.datumZakazivanja.ToString("HH:mm");

            foreach(Doctor doctor in doctors)
            {
                if (doctor.jmbg == selected.Doctor.jmbg)
                    DoctorComboBox.SelectedItem = doctor;
            }

            foreach (Pacijent patient in patients)
            {
                if (patient.jmbg != null && patient.jmbg == selected.Pacijent.jmbg)
                    PatientComboBox.SelectedItem = patient;
            }

            TypeComboBox.SelectedIndex = (int)selected.tipTermina;

            foreach (Prostorija room in rooms)
            {
                if (room.IDprostorije == selected.Prostorija.IDprostorije)
                    RoomComboBox.SelectedItem = room;
            }
        }
        //odustani
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //potvrdi
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pacijent patient = (Pacijent)PatientComboBox.SelectedItem;
            Doctor doctor = (global::Doctor)DoctorComboBox.SelectedItem;
            Prostorija room = (Prostorija)RoomComboBox.SelectedItem;
            if (time.SelectedIndex != -1)
            {
                ComboBoxItem item = time.SelectedItem as ComboBoxItem;
                String t = item.Content.ToString();
                String d = date.Text;
                DateTime dateTime = DateTime.Parse(d + " " + t);
                TipTermina type = (TipTermina)TypeComboBox.SelectedIndex;
                Termin appointment = new Termin(selected.iDTermina, dateTime, 15, type, StatusTermina.zakazan, patient, doctor, room);
                TerminFileStorage.UpdateTermin(selected.iDTermina, appointment);
                parent.UpdateTable();
                this.Close();
            }
        }
    }
}
