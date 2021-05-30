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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class ProfilePage : Page
    {

        public DoctorWindow parent;
        public Doctor doctor;
        private static ProfilePage instance;
        public ProfilePage(DoctorWindow parent)
        {
            this.parent = parent;
            this.doctor = parent.Doctor;
            InitializeComponent();
            FillLabels();
        }

        public static ProfilePage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new ProfilePage(parent);
            return instance;
        }

        private void FillLabels()
        {
            NameLabel.Content = doctor.Name + " " + doctor.Surname;
            TitleLabel.Content = doctor.doctorType.ToString();
            DateLabel.Content = doctor.DateOfBirth.Date;
            JmbgLabel.Content = doctor.JMBG;
            AddressLabel.Content = doctor.ResidentialAddress.ToString();
            EmailLabel.Content = doctor.Email;
        }
    }
}
