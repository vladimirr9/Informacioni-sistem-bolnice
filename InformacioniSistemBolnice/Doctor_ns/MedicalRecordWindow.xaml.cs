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
        private MedicineController _medicineController = new MedicineController();
        private RoomController _roomController = new RoomController();

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
                GenderComboBox.SelectedIndex = 1;
            }

            foreach (Appointment appointment in _appointmentController.PatientsAppointments(selected))
            {
                AppointmentsDataGrid.Items.Add(appointment);
            }

            AnamnesisTextBox.Document.Blocks.Clear();
            DrugsComboBox.ItemsSource = _medicineController.GetAllMedicines();
        }

        private void Prescription_Click(object sender, RoutedEventArgs e)
        {
            if (DrugsComboBox.SelectedItem != null && BeginDatePicker.Text != "" && EndDatePicker.Text != "")
            {
                if (_patientController.IsAllergic((Medicine)DrugsComboBox.SelectedItem, selected))
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
                Anamnesis newAnamnesis = new Anamnesis(anamnesis, null, selected.Username, _anamnesisController.GenerateId(), DateTime.Now, appointment.AppointmentID);
                _anamnesisController.Update(newAnamnesis, appointment);
                _appointmentController.FinishAppointment(appointment);
            }
        }

        private void Finished_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsDataGrid.Items.Clear();
            foreach (Appointment appointment in _appointmentController.PatientsAppointments(selected))
            {
                if (appointment.AppointmentStatus == AppointmentStatus.finished)
                {
                    AppointmentsDataGrid.Items.Add(appointment);
                }
            }
        }

        private void Scheduled_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsDataGrid.Items.Clear();
            foreach (Appointment appointment in _appointmentController.PatientsAppointments(selected))
            {
                if (appointment.AppointmentStatus == AppointmentStatus.scheduled)
                {
                    AppointmentsDataGrid.Items.Add(appointment);
                }
            }
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            //dodati izvestaje
        }

        private void Referral_Click(object sender, RoutedEventArgs e)
        {
            DoctorAddAppointmentWindow addWindow = new DoctorAddAppointmentWindow(parent);
            Application.Current.MainWindow = addWindow;
            addWindow.Show();
        }

        private void Add_Allergy_Click(object sender, RoutedEventArgs e)
        {
            _patientController.AddAllergen(selected, (Ingredient)AllergiesComboBox.SelectedItem);
            WriteAllergies(selected);
        }

        private void Hospitalisation_Click(object sender, RoutedEventArgs e)
        {
            if (RoomBeginDatePicker.SelectedDate != null && RoomEndDatePicker.SelectedDate != null &&
                RoomComboBox.SelectedIndex != -1 && BedComboBox.SelectedIndex != -1)
            {

            }
        }

        private void WriteAllergies(Patient patient)
        {
            AllergiesList.Items.Clear();
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (Ingredient ingredient in _medicineController.GetAllIngredients())
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
                RoomComboBox.IsEnabled = true;
                RoomComboBox.ItemsSource = _roomController.GetRoomsForHospitalisation((DateTime)RoomBeginDatePicker.SelectedDate, (DateTime)RoomEndDatePicker.SelectedDate);
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
