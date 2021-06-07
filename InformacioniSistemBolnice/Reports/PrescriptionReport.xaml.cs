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

namespace InformacioniSistemBolnice.Reports
{
    /// <summary>
    /// Interaction logic for PrescriptionReport.xaml
    /// </summary>
    public partial class PrescriptionReport : Page
    {
        public PrescriptionReport(Prescription prescription)
        {
            InitializeComponent();
            MedicineLabel.Content = prescription.Drug.Name;
            DateLabel.Content = prescription.Date.Date;
            DoctorLabel.Content = prescription.Doctor.Name + " " + prescription.Doctor.Surname;
        }
    }
}
