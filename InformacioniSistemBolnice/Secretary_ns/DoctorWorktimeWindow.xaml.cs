using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Doctor_ns;
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
    /// Interaction logic for DoctorWorktimeWindow.xaml
    /// </summary>
    public partial class DoctorWorktimeWindow : Window
    {
        public Doctor Doctor { get; set; }
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
        public List<Vacation> Vacations { get; set; }
        public WorkShift Shift { get; set; }
        public int DaysOfVacation { get; set; }
        private DoctorControler _doctorController = new DoctorControler();
        private AppointmentController _appointmentController = new AppointmentController();
        public DoctorWorktimeWindow(Doctor doctor) 
        {
            Doctor = doctor;
            InitializeComponent();
            Start = DateTime.Today.Date;
            End = DateTime.Today.Date;
            Vacations = Doctor.Vacations;
            this.DataContext = this;
            foreach (string val in _appointmentController.GetPossibleAppointmentTimes())
            {
                WorkTimeFromCombo.Items.Add(val);
                WorkTimeToCombo.Items.Add(val);
            }
            WorkTimeFromCombo.SelectedItem = Doctor.WorkHours.Start.ToString("HH:mm");
            WorkTimeToCombo.SelectedItem = Doctor.WorkHours.End.ToString("HH:mm");

            UpdateValues();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Start == null || End == null)
                return;
            if (GetNumberOfBusinessDays(Start, End) > Doctor.DaysOfVacation || GetNumberOfBusinessDays(Start, End) <= 0)
                return;
            Vacation newVacation = new Vacation(Start, End);
            if (newVacation.Overlaps(Vacations))
                return;
            _doctorController.AddVacation(Doctor,  newVacation);
            UpdateValues();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (VacationList.SelectedItem == null)
                return;
            var result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovaj godišnji odmor?", "Potvrda brisanja", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;
            Vacation selectedVacation = (Vacation)VacationList.SelectedItem;
            _doctorController.RemoveVacation(Doctor, selectedVacation);
            UpdateValues();
        }

        public void UpdateValues()
        {
            Vacations = new List<Vacation>();  
            foreach (Vacation vacation in Doctor.Vacations)
            {
                Vacations.Add(vacation);
            }
            AberrationData.Items.Clear();
            foreach (WorkHourAberration aberration in Doctor.WorkHours.Aberrations)
            {
                AberrationData.Items.Add(aberration);
            }
            VacationList.ItemsSource = Vacations;
            DaysOfVacation = Doctor.DaysOfVacation;
            FreeDays.Content = DaysOfVacation;
            WorkTimeStart.Content = Doctor.WorkHours.Start.ToString("HH:mm");
            WorkTimeEnd.Content = Doctor.WorkHours.End.ToString("HH:mm");
        }
        private int GetNumberOfBusinessDays(DateTime start, DateTime end)
        {
            int durationInBusinessDays = 0;
            DateTime i;
            for (i = Start.Date; i <= End.Date; i = i.AddDays(1))
            {
                if (i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday)
                    continue;
                durationInBusinessDays++;
            }
            return durationInBusinessDays;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            _doctorController.Update(Doctor);
            _appointmentController.UpdateAppointmentsForDoctor(Doctor);
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmAbberation_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = DateTime.Parse(DateTime.Now.Date.ToString("dd/MM/yyyy") + " " + WorkTimeFromCombo.Text);
            DateTime end = DateTime.Parse(DateTime.Now.Date.ToString("dd/MM/yyyy") + " " + WorkTimeToCombo.Text);
            if (end <= start)
                return;
            if (AberrationDate.SelectedDate == null)
            {
                Doctor.WorkHours.Start = start;
                Doctor.WorkHours.End = end;
            }
            else
            {
                DateTime date = AberrationDate.SelectedDate.Value.Date;
                WorkHourAberration aberration;
                if (Doctor.WorkHours.AberrationExists(date))
                {
                    aberration = Doctor.WorkHours.GetAberrationByDate(date);
                    aberration.Start = start;
                    aberration.End = end;
                }
                else
                {
                    aberration = new WorkHourAberration(date, start, end);
                    Doctor.WorkHours.Aberrations.Add(aberration);
                }
            }


            UpdateValues();
        }

        private void DeleteAberration_Click(object sender, RoutedEventArgs e)
        {
            if (AberrationData.SelectedItem == null)
                return;
            WorkHourAberration aberration = (WorkHourAberration)AberrationData.SelectedItem;
            Doctor.WorkHours.Aberrations.Remove(aberration);
            UpdateValues();
        }
    }
}
