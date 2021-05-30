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
        public DoctorWorktimeWindow(Doctor doctor)
        {
            Doctor = doctor;
            InitializeComponent();
            Start = DateTime.Today.Date;
            End = DateTime.Today.Date;
            Vacations = Doctor.Vacations;
            this.DataContext = this;
            if (Doctor.Shift == WorkShift.firstShift)
                FirstShift.IsChecked = true;
            else
                SecondShift.IsChecked = true;
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
            Doctor.DaysOfVacation -= newVacation.DurationInBusinessDays;
            Doctor.Vacations.Add(newVacation);
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
            Doctor.Vacations.Remove(selectedVacation);
            Doctor.DaysOfVacation += selectedVacation.DurationInBusinessDays;
            UpdateValues();
        }

        public void UpdateValues()
        {
            Vacations = new List<Vacation>();
            foreach (Vacation vacation in Doctor.Vacations)
            {
                Vacations.Add(vacation);
            }
            VacationList.ItemsSource = Vacations;
            DaysOfVacation = Doctor.DaysOfVacation;
            FreeDays.Content = DaysOfVacation;
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
            DoctorFileRepository.UpdateDoctor(Doctor.Username, Doctor);
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FirstShift_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)FirstShift.IsChecked)
                Doctor.Shift = WorkShift.firstShift;
            else
                Doctor.Shift = WorkShift.secondShift;
        }

        private void SecondShift_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)FirstShift.IsChecked)
                Doctor.Shift = WorkShift.firstShift;
            else
                Doctor.Shift = WorkShift.secondShift;
        }
    }
}
