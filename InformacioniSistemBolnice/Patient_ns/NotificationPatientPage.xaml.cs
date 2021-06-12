using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.Patient_ns.ViewModelPatient;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for NotificationPatientPage.xaml
    /// </summary>
    public partial class NotificationPatientPage : Page
    {
       
        public NotificationPatientPage(StartPatientWindow pp)
        {
            
            InitializeComponent();
            DataContext = new NotificationPatientViewModel(pp);


        }

       
    }
}
