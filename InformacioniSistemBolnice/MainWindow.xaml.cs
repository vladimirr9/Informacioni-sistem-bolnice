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
using InformacioniSistemBolnice.Patient_ns;
using InformacioniSistemBolnice.Secretary_ns.HelpWizard;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Patient Patient { get; set; }
        public MainWindow()
        {
            //DoctorFileRepository.AddDoctor(new Doctor("Petar", "Petrovic", "1507970600034", 'm', "+381661238407", "ppetrovic@gmail.com", new DateTime(1970,4,12), "PPetrovic", "petrovic123", null, DoctorType.generalPractitioner, WorkShift.firstShift, false));
            //SecretaryFileRepository.AddSecretary(new Secretary("Vladimir", "Rokvic", "0405993705030", 'M', "+32432343232", "neko@nesto.com", new DateTime(), "vladimir", "rokvic", new ResidentialAddress("Kralja Petra 1", new City("Novi Sad", "23223", new Country("Republika Srbija"))), false));
            //PatientFileRepository.AddPatient(new Patient("Pera","Peric", "09320434533",'M',"+3245344323","Pera@peric.com",new DateTime(),"pera","peric",new ResidentialAddress("Kralja Petra 12", new City("Novi Sad", "23232", new Country("Republika Srbija"))),false,"2323224343",new MedicalRecord("232"),false));
            InitializeComponent();
            //DoctorFileRepository.AddDoctor(new global::Lekar("Rada", "Radić", "143234230034", 'ž', "+38341664548407", "radaradic@gmail.com", new DateTime(1972, 5, 14), "Rada", "rdic", new ResidentialAddress("Kralja Petra 11", new City("Novi Sad", "23232", new Country("Republika Srbija"))), TipLekara.generalPractitioner, false));
            //ManagerFileRepository.AddManager( new Manager("Petar", "Petrovic", "1234567890123", 'm', "060 / 555 - 555", "petar@gmail.com", new DateTime(1970, 4, 12), "pera1", "perica123", new ResidentialAddress("Novosadska 2", new City("Novi Sad", "21000", new Country("Republika Srbija"))), false));

        }

        

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Patient> pacijenti = PatientFileRepository.GetAll();
            List<Appointment> termini = AppointmentFileRepository.GetAll();
            foreach (var pacijent in pacijenti)
            {
                if (ime.Text.Equals(pacijent.Username)  && lozinka.Password.Equals(pacijent.Password))
                {

                    String ime = pacijent.Username;
                            
                            StartPatientWindow pp = new StartPatientWindow(pacijent);
                            Application.Current.MainWindow = pp;
                            pp.startWindow.Content = new StartPatientPage(pp);
                            pp.Show();
                            this.Close();
                            return;
                        
                    
                }
            }
            List<Secretary> sekretari = SecretaryFileRepository.GetAll();
            foreach (Secretary s in sekretari)
            {
                if (ime.Text.Equals(s.Username) && lozinka.Password.Equals(s.Password))
                {
                    HelpWizardMain hwm = new HelpWizardMain();
                    hwm.Show();
                    SecretaryMain sw = new SecretaryMain(s);
                    Application.Current.MainWindow = sw;
                    sw.Main.Content = StartingPage.GetPage(s, sw);
                    sw.Show();
                    this.Close();
                    return;
                }
            }
            List<global::Doctor> lekari = DoctorFileRepository.GetAll();
            foreach (global::Doctor l in lekari)
            {
                if (ime.Text.Equals(l.Username) && lozinka.Password.Equals(l.Password))
                {
                    DoctorWindow lw = new DoctorWindow(l);
                    Application.Current.MainWindow = lw;
                    lw.Show();
                    this.Close();
                    return;
                }
            }

            List<global::Manager> upravnikLista = ManagerFileRepository.GetAll();
            foreach (global::Manager u in upravnikLista)
            {
                if (ime.Text.Equals(u.Username) && lozinka.Password.Equals(u.Password))
                {
                    ManagerWindow uw = new ManagerWindow(u);
                    Application.Current.MainWindow = uw;
                    uw.Show();
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Neuspesno logovanje!");
        }

       

    }
}
