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

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for PatientMedicalRecordPage.xaml
    /// </summary>
    public partial class PatientMedicalRecordPage : Page
    {
        private static StartPatientWindow parent;
        public Patient Patient;
        public PatientMedicalRecordPage(StartPatientWindow pp)
        {
            parent = pp;
            Patient = pp.Patient;
            InitializeComponent();
            borderWindow.Content = new MedicalRecordPage(pp);
        }


        private void informacijeKarton_Click(object sender, RoutedEventArgs e)
        {
            borderWindow.Content = new MedicalRecordPage(parent);
        }

        private void terapijaKarton_Click(object sender, RoutedEventArgs e)
        {
            borderWindow.Content = new TherapyPatientPage();
        }

        private void istorijaBolesti_Click(object sender, RoutedEventArgs e)
        {
            borderWindow.Content = new MedicalHistoryPage();
        }

        private void pregledAnamneza_Click(object sender, RoutedEventArgs e)
        {
            borderWindow.Content = new PatientExamineAnamnesesPage(parent, this);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPatientPage(parent);
        }
    }
}
