using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Doctor_ns;

namespace InformacioniSistemBolnice.View.ViewModel
{
    public class DoctorWindowViewModel : BindableBase
    {
        public Doctor Doctor;
        public DoctorWindow parent;
        public MyICommand PatientsCommand { get; set; }
        public MyICommand AppointmentsCommand { get; set; }
        public MyICommand MedicineCommand { get; set; }
        public MyICommand TutorialCommand { get; set; }
        public MyICommand ProfileCommand { get; set; }
        public MyICommand LogoutCommand { get; set; }
        public MyICommand FeedbackCommand { get; set; }

        public DoctorWindowViewModel(Doctor doctor, DoctorWindow parent)
        {
            this.Doctor = doctor;
            this.parent = parent;
            parent.Title = doctor.Name + " " + doctor.Surname;
            parent.Main.Content = ProfilePage.GetPage(parent);
            PatientsCommand = new MyICommand(Patients_Click);
            AppointmentsCommand = new MyICommand(Appointments_Click);
            MedicineCommand = new MyICommand(Medicine_Click);
            TutorialCommand = new MyICommand(Tutorial_Click);
            ProfileCommand = new MyICommand(Profile_Click);
            LogoutCommand = new MyICommand(Logout_Click);
            FeedbackCommand = new MyICommand(Feedback_Click);
        }

        private void Patients_Click()
        {
            parent.Main.Content = DoctorPatientsPage.GetPage(parent);
        }

        private void Appointments_Click()
        {
            parent.Main.Content = new AppointmentsPage(parent);
        }

        private void Medicine_Click()
        {
            parent.Main.Content = DrugsPage.GetPage(parent);
        }

        private void Feedback_Click()
        {
            parent.Main.Content = DoctorFeedbackPage.GetPage(parent);
        }

        private void Tutorial_Click()
        {
            parent.Main.Content = TutorialPage.GetPage(parent);
        }

        private void Profile_Click()
        {
            parent.Main.Content = ProfilePage.GetPage(parent);
        }

        private void Logout_Click()
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            parent.Close();
        }
    }
}
