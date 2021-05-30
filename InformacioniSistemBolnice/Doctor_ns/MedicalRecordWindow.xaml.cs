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

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class MedicalRecordWindow : Window
    {
        private DoctorWindow parent;
        private Patient selected;
        private PatientController _patientController = new PatientController();
        private AnamnesisController _anamnesisController = new AnamnesisController();
        private AppointmentController _appointmentController = new AppointmentController();

        public MedicalRecordWindow(Patient patient , DoctorWindow parent)
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
            NameTextBox.Text = selected.Name + " " + selected.Surname;
            BirthdayTextBox.Text = selected.DateOfBirth.ToString("dd/MM/yyyy");
            JmbgTextBox.Text = selected.JMBG;
            MedicalCardTextBox.Text = selected.SocialSecurityNumber;

            if (selected.Gender == 'M')
            {
                GenderComboBox.SelectedIndex = 0;
            }
            else
            {
                GenderComboBox.SelectedItem = 1;
            }

            foreach (Appointment appointment in _appointmentController.PatientsAppointments(selected))
            {
                AppointmentsDataGrid.Items.Add(appointment);
            }


            AnamnesisTextBox.Document.Blocks.Clear();


            List<Medicine> drugs = MedicineFileRepository.GetAll();               //kontroler
            DrugsComboBox.ItemsSource = drugs;
        }

        //izadvanje recepta i terapije
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DrugsComboBox.SelectedItem != null && BeginDatePicker.Text != "" && EndDatePicker.Text != "")
            {
                Medicine drug = (Medicine)DrugsComboBox.SelectedItem;
                if (_patientController.IsAllergic(drug, selected))
                {
                    MessageBox.Show("Patient je alergican na izabrani lek.", "Alergican");
                    return;
                }
                
                Prescription prescription = new Prescription((Medicine)DrugsComboBox.SelectedItem, DateTime.Parse(BeginDatePicker.Text), parent.Doctor);
                selected.MedicalRecord.AddRecept(prescription);

                _patientController.Update(selected.Username, selected);
                ClearTherapy();
            }
        }

        private void ClearTherapy()
        {
            DrugsComboBox.SelectedIndex = -1;
            DescriptionTextBox.Document.Blocks.Clear();
            BeginDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            FrequencyTextBox.Text = "";
        }

        private void Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            Appointment appointment = (Appointment) AppointmentsDataGrid.SelectedItem;
            AnamnesisTextBox.Document.Blocks.Clear();
            if(_anamnesisController.AppointmentAnamnesis(appointment) != null)
            {
                AnamnesisTextBox.Document.Blocks.Add(new Paragraph(new Run(_anamnesisController.AppointmentAnamnesis(appointment).DescriptionOfAnamnesis)));
            }

        }

        private void Save_Anamnesis_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)AppointmentsDataGrid.SelectedItem;
            String anamnesis = new TextRange(AnamnesisTextBox.Document.ContentStart, AnamnesisTextBox.Document.ContentEnd).Text;

            if (anamnesis.Trim() != "")
            {
                if (_anamnesisController.AppointmentAnamnesis(appointment) != null)
                {
                    _anamnesisController.Update(_anamnesisController.AppointmentAnamnesis(appointment));
                }
                else
                {
                    Anamnesis newAnamnesis = new Anamnesis(anamnesis, null, selected.Username, _anamnesisController.GenerateId(), DateTime.Now, appointment.AppointmentID);
                    _anamnesisController.Add(newAnamnesis);
                }
            }
            
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

        private void WriteAllergies(Patient patient)               //dodati ingredients kontroler i izmestiti
        {
            AllergiesList.Items.Clear();
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (Ingredient ingredient in IngredientFileStorage.GetAll())
            {
                if (patient.MedicalRecord.Allergens.Contains(ingredient))
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
                List<Room> rooms = RoomFileRepository.GetAll();
                List<Room> available = new List<Room>();
                DateTime begin = (DateTime) RoomBeginDatePicker.SelectedDate;
                DateTime end = (DateTime) RoomEndDatePicker.SelectedDate;
                foreach (Room room in rooms)
                {
                    if (room.RoomType.Equals(RoomType.recoveryRoom) && room.IsAvailable(begin, end))
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
