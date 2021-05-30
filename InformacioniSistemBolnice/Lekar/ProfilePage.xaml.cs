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

namespace InformacioniSistemBolnice.Lekar
{
    public partial class ProfilePage : Page
    {

        public DoctorWindow parent;
        public Doctor Doctor;
        private static ProfilePage instance;
        public ProfilePage(DoctorWindow parent)
        {
            this.parent = parent;
            this.Doctor = parent.Doctor;
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
            NameLabel.Content = Doctor.ime + " " + Doctor.prezime;
            TitleLabel.Content = Doctor.doctorType.ToString();
            DateLabel.Content = Doctor.datumRodenja.Date;
            JmbgLabel.Content = Doctor.jmbg;
            AddressLabel.Content = Doctor.adresaStanovanja.ToString();
            EmailLabel.Content = Doctor.email;
        }
    }
}
