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
        public MedicalRecordWindow(Pacijent patient , DoctorWindow parent)
        {
            this.selected = patient;
            this.parent = parent;
            InitializeComponent();
            FillMedicalRecord();
            WriteAllergies(selected);
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

            List<Lek> drugs = LekFileStorage.GetAll();
            DrugsComboBox.ItemsSource = drugs;
        }

        //izadvanje recepta i terapije
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DrugsComboBox.SelectedItem != null && BeginDatePicker.Text != "" && EndDatePicker.Text != "")
            {
                Lek drug = (Lek)DrugsComboBox.SelectedItem;
                List<Ingredient> ingredients = drug.ListaSastojaka;
                foreach (Ingredient ingredient in ingredients)
                {
                    if (selected.zdravstveniKarton.Alergen.Contains(ingredient))
                    {
                        MessageBox.Show("Pacijent je alergican na izabrani lek.", "Alergican");
                        return;
                    }
                }
                Prescription prescription = new Prescription((Lek)DrugsComboBox.SelectedItem, DateTime.Parse(BeginDatePicker.Text), parent.Doctor);
                selected.zdravstveniKarton.AddRecept(prescription);
                PacijentFileStorage.UpdatePacijent(selected.korisnickoIme, selected);
                DrugsComboBox.SelectedIndex = -1;
                DescriptionTextBox.Document.Blocks.Clear();
                BeginDatePicker.SelectedDate = null;
                EndDatePicker.SelectedDate = null;
                FrequencyTextBox.Text = "";
            }
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

        private void WriteAllergies(Pacijent patient)
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
    }
}
