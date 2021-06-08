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
using InformacioniSistemBolnice.Reports;
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class MedicalRecordWindow : Window
    {
        private DoctorWindow parent;
        private Patient selected;
        private Prescription prescription = null;
        private Hospitalisation hospitalisation;
        private PatientController _patientController = new PatientController();
        private AnamnesisController _anamnesisController = new AnamnesisController();
        private AppointmentController _appointmentController = new AppointmentController();
        private MedicineController _medicineController = new MedicineController();
        private RoomController _roomController = new RoomController();
        private HospitalisationControler _hospitalisationControler = new HospitalisationControler();
        private PrintDialog _printDialog = new PrintDialog();

        public MedicalRecordWindow(Patient patient , DoctorWindow parent, Appointment selectedAppointment = null)
        {
            this.selected = patient;
            this.parent = parent;
            InitializeComponent();
            FillMedicalRecord(selectedAppointment);
            WriteAllergies(selected);
            WriteHospitalisation();
            BlackOutDates();

        }

        private void FillMedicalRecord(Appointment selectedAppointment = null)
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
                if (selectedAppointment != null && appointment.AppointmentID == selectedAppointment.AppointmentID)
                {
                    TabControl.SelectedIndex = 2;
                    AppointmentsDataGrid.SelectedItem = appointment;
                }
            }

            Selection_Changed(null, null);
            DrugsComboBox.ItemsSource = _medicineController.GetAllMedicines();
        }

        private void Prescription_Click(object sender, RoutedEventArgs e)
        {
            if (DrugsComboBox.SelectedItem != null && BeginDatePicker.Text != "" && EndDatePicker.Text != "")
            {
                if (_patientController.IsAllergic((Medicine)DrugsComboBox.SelectedItem, selected))
                {
                    MessageBox.Show("Pacijent je alergičan na izabrani lek.", "Alergičan");
                    return;
                }
                
                prescription = new Prescription((Medicine)DrugsComboBox.SelectedItem, DateTime.Parse(BeginDatePicker.Text).AddHours(8), parent.Doctor);
                selected.MedicalRecord.AddRecept(prescription);

                _patientController.Update(selected.Username, selected);
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
            if(AppointmentsDataGrid.SelectedIndex != -1 && _anamnesisController.AppointmentAnamnesis(appointment) != null)
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
                Scheduled_Click(null, null);
                AppointmentsViewModel.GetPage(parent).UpdateTable();
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

        private void Anamnesis_Report_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment) AppointmentsDataGrid.SelectedItem;
            if (appointment != null && appointment.AppointmentStatus == AppointmentStatus.finished)
            {
                _printDialog.PrintVisual(new AnamnesisReport(_anamnesisController.AppointmentAnamnesis(appointment), parent.Doctor), "Izveštaj recepta");
            }
        }

        private void Prescription_Report_Click(object sender, RoutedEventArgs e)
        {
            if (prescription != null)
            {
                _printDialog.PrintVisual(new PrescriptionReport(prescription), "Izveštaj recepta");
                ClearTherapy();
            }
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

        private void Hospitalisation_Click(object sender, RoutedEventArgs e)
        {
            if (RoomBeginDatePicker.SelectedDate != null && RoomEndDatePicker.SelectedDate != null && RoomComboBox.SelectedIndex != -1 && BedComboBox.SelectedIndex != -1)
            {
                Room room = (Room)RoomComboBox.SelectedItem;
                int hospitalisationId = GetHospitalisationId();
                Hospitalisation newHospitalisation = new Hospitalisation(hospitalisationId, selected.Username, room.RoomId, (DateTime)RoomBeginDatePicker.SelectedDate, (DateTime)RoomEndDatePicker.SelectedDate, (int)BedComboBox.SelectedItem);
                _hospitalisationControler.Save(newHospitalisation);
                hospitalisation = newHospitalisation;
            }
        }

        public int GetHospitalisationId()
        {
            if (hospitalisation == null)
            {
                return _hospitalisationControler.GenerateNewId();
            }
            else
            {
                return hospitalisation.HospitalisationId;
            }
        }

        private void WriteHospitalisation()
        {
            hospitalisation = _hospitalisationControler.GetHospitalisationForPatient(selected);
            if (hospitalisation != null)
            {
                Room room = _roomController.GetOneRoom(hospitalisation.RoomId);
                int bed = _roomController.GetAvailableBed(room, hospitalisation.BeginDate, hospitalisation.EndDate);
                RoomBeginDatePicker.SelectedDate = hospitalisation.BeginDate;
                RoomEndDatePicker.SelectedDate = hospitalisation.EndDate;
                List<Room> rooms = new List<Room>();
                rooms.Add(room);
                RoomComboBox.ItemsSource = rooms;
                RoomComboBox.SelectedIndex = 0;
                BedComboBox.Items.Add(bed - 1);
                BedComboBox.SelectedIndex = 0;
            }
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRooms();
            BlackOutDates();
            Room_SelectionChanged(null, null);
        }

        private void Room_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomComboBox.SelectedIndex != -1 && hospitalisation == null)
            {
                BedComboBox.Items.Clear();
                BedComboBox.Items.Add(_roomController.GetAvailableBed((Room)RoomComboBox.SelectedItem, (DateTime)RoomBeginDatePicker.SelectedDate, (DateTime)RoomEndDatePicker.SelectedDate));
                BedComboBox.SelectedIndex = 0;
            }
        }

        private void UpdateRooms()
        {
            if (RoomBeginDatePicker.SelectedDate != null && RoomEndDatePicker.SelectedDate != null)
            {
                RoomComboBox.ItemsSource = _roomController.GetRoomsForHospitalisation((DateTime)RoomBeginDatePicker.SelectedDate, (DateTime)RoomEndDatePicker.SelectedDate);
                RoomComboBox.SelectedIndex = 0;
            }
        }

        private void BlackOutDates()
        {
            RoomEndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            if (RoomBeginDatePicker.SelectedDate != null)
            {
                RoomEndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, (DateTime)RoomBeginDatePicker.SelectedDate));
            }
        }
    }
}
