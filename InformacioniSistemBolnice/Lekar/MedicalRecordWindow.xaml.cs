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
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Lekar
{
    public partial class MedicalRecordWindow : Window
    {
        private DoctorWindow parent;
        private Pacijent selected;
        private PatientController _patientController = new PatientController();

        public MedicalRecordWindow(Pacijent patient , DoctorWindow parent)
        {
            this.selected = patient;
            this.parent = parent;
            InitializeComponent();
            FillMedicalRecord();
            WriteAllergies(selected);
            BlackOutDates();
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
            //AnamnesisTextBox.Document.Blocks.Add(new Paragraph(new Run(appointment.anamneza)));

            List<Medicine> drugs = MedicineFileRepository.GetAll();
            DrugsComboBox.ItemsSource = drugs;
        }

        //izadvanje recepta i terapije
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DrugsComboBox.SelectedItem != null && BeginDatePicker.Text != "" && EndDatePicker.Text != "")
            {
                Medicine drug = (Medicine)DrugsComboBox.SelectedItem;
                if (IsAllergic(drug, selected))
                {
                    MessageBox.Show("Pacijent je alergican na izabrani lek.", "Alergican");
                    return;
                }
                
                Prescription prescription = new Prescription((Medicine)DrugsComboBox.SelectedItem, DateTime.Parse(BeginDatePicker.Text), parent.Doctor);
                selected.zdravstveniKarton.AddRecept(prescription);

                _patientController.Update(selected.korisnickoIme, selected);
                DrugsComboBox.SelectedIndex = -1;
                DescriptionTextBox.Document.Blocks.Clear();
                BeginDatePicker.SelectedDate = null;
                EndDatePicker.SelectedDate = null;
                FrequencyTextBox.Text = "";
            }
        }

        private bool IsAllergic(Medicine drug, Pacijent patient)              //izmestiti u pacijenta ili servis
        {
            List<Ingredient> ingredients = drug.IngredientsList;
            foreach (Ingredient ingredient in ingredients)
            {
                if (patient.zdravstveniKarton.Alergen.Contains(ingredient))
                {
                    return true;
                }
            }

            return false;
        }

        //Upisivanje anamneze
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*
            String anamnesis = new TextRange(AnamnesisTextBox.Document.ContentStart, AnamnesisTextBox.Document.ContentEnd).Text;
            if (anamnesis.Trim() != "")
            {
                appointment.anamneza = anamnesis;


                int idOfAnamnesis = AnamnesisFileRepository.GetAll().Count + 1;
                Anamnesis newAnamnesis = new Anamnesis(anamnesis, null, selected.korisnickoIme, idOfAnamnesis, DateTime.Now, appointment.iDTermina);
                AnamnesisFileRepository.AddAnamnesis(newAnamnesis);// - dodati u prikaz kartona



                TerminFileStorage.UpdateTermin(appointment.iDTermina, appointment);
            }
            */
        }

        //Izdavanje uputa
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DoctorAddAppointmentWindow addWindow = new DoctorAddAppointmentWindow(parent);
            Application.Current.MainWindow = addWindow;
            addWindow.Show();
        }

        //dodaj alergiju
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PatientController patientController = new PatientController();
            patientController.AddAllergen(selected, (Ingredient)AllergiesComboBox.SelectedItem);

            WriteAllergies(selected);
        }

        //bolnicko lecenje
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (RoomBeginDatePicker.SelectedDate != null && RoomEndDatePicker.SelectedDate != null &&
                RoomComboBox.SelectedIndex != -1 && BedComboBox.SelectedIndex != -1)
            {

            }
        }

        private void WriteAllergies(Pacijent patient)               //dodati ingredients kontroler i izmestiti
        {
            AllergiesList.Items.Clear();
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (Ingredient ingredient in IngredientFileStorage.GetAll())
            {
                if (patient.zdravstveniKarton.Alergen.Contains(ingredient))
                {
                    AllergiesList.Items.Add(ingredient.Name);
                }
                else
                {
                    ingredients.Add(ingredient);
                }

            }
            AllergiesComboBox.ItemsSource = ingredients;
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
            BlackOutDates();
        }

        private void UpdateComponents()
        {
            if (RoomBeginDatePicker.SelectedDate != null && RoomEndDatePicker.SelectedDate != null)
            {
                List<Prostorija> rooms = ProstorijaFileStorage.GetAll();
                List<Prostorija> available = new List<Prostorija>();
                DateTime begin = (DateTime) RoomBeginDatePicker.SelectedDate;
                DateTime end = (DateTime) RoomEndDatePicker.SelectedDate;
                foreach (Prostorija room in rooms)
                {
                    if (room.TipProstorije.Equals(TipProstorije.bolnickaSoba) && room.IsAvailable(begin, end))
                    {
                        available.Add(room);
                    }
                }
                RoomComboBox.IsEnabled = true;
                RoomComboBox.ItemsSource = available;


            }
        }

        private void BlackOutDates()
        {
            RoomBeginDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            RoomEndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            if (RoomBeginDatePicker.SelectedDate != null)
            {
                RoomEndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, (DateTime)RoomBeginDatePicker.SelectedDate));
            }

        }
    }
}
