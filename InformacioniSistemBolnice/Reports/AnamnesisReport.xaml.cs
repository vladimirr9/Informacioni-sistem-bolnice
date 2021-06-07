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
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Reports
{
    /// <summary>
    /// Interaction logic for AnamnesisReport.xaml
    /// </summary>
    public partial class AnamnesisReport : Page
    {
        private PatientController _patientController = new PatientController();
        private Patient patient;
        public AnamnesisReport(Anamnesis anamnesis, Doctor doctor)
        {
            InitializeComponent();
            patient = _patientController.GetOne(anamnesis.UsernameOfPatient);
            PatientLabel.Content = patient.Name + " " + patient.Surname;
            DoctorLabel.Content = doctor.Name + " " + doctor.Surname;
            DateLabel.Content = anamnesis.DateOfWritingAnamnesis;
            AnamnesisLabel.Text = anamnesis.DescriptionOfAnamnesis;
        }
    }
}
