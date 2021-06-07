//using GalaSoft.MvvmLight.Command;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.User;
using InformacioniSistemBolnice.Doctor_ns;
using InformacioniSistemBolnice.Secretary_ns;
using InformacioniSistemBolnice.Upravnik;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Patient_ns;
using InformacioniSistemBolnice.Secretary_ns.HelpWizard;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginControler _loginControler = new LoginControler();
        public MainWindow()
        {
            //DoctorFileRepository.AddDoctor(new Doctor("Petar", "Petrovic", "1507970600034", 'm', "+381661238407", "ppetrovic@gmail.com", new DateTime(1970,4,12), "PPetrovic", "petrovic123", null, DoctorType.generalPractitioner, WorkShift.firstShift, false));
            //SecretaryFileRepository.AddSecretary(new Secretary("Vladimir", "Rokvic", "0405993705030", 'M', "+32432343232", "neko@nesto.com", new DateTime(), "vladimir", "rokvic", new ResidentialAddress("Kralja Petra 1", new City("Novi Sad", "23223", new Country("Republika Srbija"))), false));
            //PatientFileRepository.AddPatient(new Patient("Pera","Peric", "09320434533",'M',"+3245344323","Pera@peric.com",new DateTime(),"pera","peric",new ResidentialAddress("Kralja Petra 12", new City("Novi Sad", "23232", new Country("Republika Srbija"))),false,"2323224343",new MedicalRecord("232"),false));
            //DoctorFileRepository.AddDoctor(new global::Lekar("Rada", "Radić", "143234230034", 'ž', "+38341664548407", "radaradic@gmail.com", new DateTime(1972, 5, 14), "Rada", "rdic", new ResidentialAddress("Kralja Petra 11", new City("Novi Sad", "23232", new Country("Republika Srbija"))), TipLekara.generalPractitioner, false));
            //ManagerFileRepository.AddManager( new Manager("Petar", "Petrovic", "1234567890123", 'm', "060 / 555 - 555", "petar@gmail.com", new DateTime(1970, 4, 12), "pera1", "perica123", new ResidentialAddress("Novosadska 2", new City("Novi Sad", "21000", new Country("Republika Srbija"))), false));
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_loginControler.FindPatient(Username.Text, Password.Password) != null)
            {
                StartPatientWindow patientWindow = new StartPatientWindow(_loginControler.FindPatient(Username.Text, Password.Password));
                Application.Current.MainWindow = patientWindow;
                patientWindow.startWindow.Content = new StartPatientPage(patientWindow);
                patientWindow.Show();
                this.Close();
                return;
            }

            if (_loginControler.FindSecretary(Username.Text, Password.Password) != null)
            {
                HelpWizardMain helpWizardMain = new HelpWizardMain();
                helpWizardMain.Show();
                SecretaryMain secretaryMain = new SecretaryMain(_loginControler.FindSecretary(Username.Text, Password.Password));
                Application.Current.MainWindow = secretaryMain;
                secretaryMain.Main.Content = StartingPage.GetPage(_loginControler.FindSecretary(Username.Text, Password.Password), secretaryMain);
                secretaryMain.Show();
                this.Close();
                return;
            }

            if (_loginControler.FindDoctor(Username.Text, Password.Password) != null)
            {
                DoctorWindow doctorWindow = new DoctorWindow(_loginControler.FindDoctor(Username.Text, Password.Password));
                Application.Current.MainWindow = doctorWindow;
                doctorWindow.Show();
                this.Close();
                return;
            }

            if (_loginControler.FindManager(Username.Text, Password.Password) != null)
            {
                UpravnikWindow managerWindow = new UpravnikWindow(_loginControler.FindManager(Username.Text, Password.Password));
                Application.Current.MainWindow = managerWindow;
                managerWindow.Show();
                this.Close();
                return;
            }

            MessageBox.Show("Neuspesno logovanje!");



            /*
            List<Patient> patients = PatientFileRepository.GetAll();
            foreach (var patient in patients)
            {
                if (Username.Text.Equals(patient.Username)  && Password.Password.Equals(patient.Password))
                {
                    StartPatientWindow patientWindow = new StartPatientWindow(patient);
                    Application.Current.MainWindow = patientWindow;
                    patientWindow.startWindow.Content = new StartPatientPage(patientWindow);
                    patientWindow.Show();
                    this.Close();
                    return;
                }
            }

            List<Secretary> secretaries = SecretaryFileRepository.GetAll();
            foreach (Secretary secretary in secretaries)
            {
                if (Username.Text.Equals(secretary.Username) && Password.Password.Equals(secretary.Password))
                {
                    HelpWizardMain helpWizardMain = new HelpWizardMain();
                    helpWizardMain.Show();
                    SecretaryMain secretaryMain = new SecretaryMain(secretary);
                    Application.Current.MainWindow = secretaryMain;
                    secretaryMain.Main.Content = StartingPage.GetPage(secretary, secretaryMain);
                    secretaryMain.Show();
                    this.Close();
                    return;
                }
            }

            List<Doctor> doctors = DoctorFileRepository.GetAll();
            foreach (Doctor doctor in doctors)
            {
                if (Username.Text.Equals(doctor.Username) && Password.Password.Equals(doctor.Password))
                {
                    DoctorWindow doctorWindow = new DoctorWindow(doctor);
                    Application.Current.MainWindow = doctorWindow;
                    doctorWindow.Show();
                    this.Close();
                    return;
                }
            }

            List<Manager> managers = ManagerFileRepository.GetAll();
            foreach (Manager manager in managers)
            {
                if (Username.Text.Equals(manager.Username) && Password.Password.Equals(manager.Password))
                {
                    UpravnikWindow managerWindow = new UpravnikWindow(manager);
                    Application.Current.MainWindow = managerWindow;
                    managerWindow.Show();
                    this.Close();
                    return;
                }
            }
            */


        }
    }
}
