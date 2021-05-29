using InformacioniSistemBolnice.FileStorage;
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

namespace InformacioniSistemBolnice.Lekar
{
    public partial class MedicalRecordWindow : Window
    {
        private DoctorWindow parent;
        private Pacijent selected;
        private Termin appointment;
        public MedicalRecordWindow(Termin appointment, DoctorWindow parent)
        {
            this.selected = appointment.Pacijent;
            this.parent = parent;
            this.appointment = appointment;
            InitializeComponent();
            FillMedicalRecord();
        }

        private void FillMedicalRecord()
        {
            NameTextBox.Text = selected.ime + " " + selected.prezime;
            BirthdayTextBox.Text = selected.datumRodenja.ToString("dd/MM/yyyy");
            JmbgTextBox.Text = selected.jmbg;
            MedicalCardTextBox.Text = selected.brojZdravstveneKartice;

            if (selected.pol == 'M')
            {
                GenderComboBox.SelectedIndex = 0;
            }
            else
            {
                GenderComboBox.SelectedItem = 1;
            }

            AnamnesisTextBox.Document.Blocks.Clear();
            AnamnesisTextBox.Document.Blocks.Add(new Paragraph(new Run(appointment.anamneza)));
        }

        //izadvanje recepta i terapije
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String drug = DrugsTextBox.Text;
            if (drug != "" && BeginDatePicker.Text != "")
            {
                Prescription prescription = new Prescription(drug, DateTime.Parse(BeginDatePicker.Text), parent.Doctor);
                selected.zdravstveniKarton.AddRecept(prescription);
                PacijentFileStorage.UpdatePacijent(selected.korisnickoIme, selected);
                DrugsTextBox.Text = "";
                DescriptionTextBox.Document.Blocks.Clear();
                BeginDatePicker.SelectedDate = null;
                EndDatePicker.SelectedDate = null;
                FrequencyTextBox.Text = "";
            }
        }

        //Upisivanje anamneze
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String anamnesis = new TextRange(AnamnesisTextBox.Document.ContentStart, AnamnesisTextBox.Document.ContentEnd).Text;
            if (anamnesis.Trim() != "")
            {
                appointment.anamneza = anamnesis;


                int idOfAnamnesis = AnamnesisFileRepository.GetAll().Count + 1;
                Anamnesis newAnamnesis = new Anamnesis(anamnesis, null, selected.korisnickoIme, idOfAnamnesis, DateTime.Now, appointment.iDTermina);
                AnamnesisFileRepository.AddAnamnesis(newAnamnesis);// - dodati u prikaz kartona



                TerminFileStorage.UpdateTermin(appointment.iDTermina, appointment);
            }
        }

        //Izdavanje uputa
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DoctorAddAppointmentWindow addWindow = new DoctorAddAppointmentWindow(parent);
            Application.Current.MainWindow = addWindow;
            addWindow.Show();
        }
    }
}
