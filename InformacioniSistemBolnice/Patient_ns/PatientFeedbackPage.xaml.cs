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
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for PatientFeedbackPage.xaml
    /// </summary>
    public partial class PatientFeedbackPage : Page
    {
        private StartPatientWindow _parent;
        public PatientFeedbackPage(StartPatientWindow swp)
        {
            _parent = swp;
            InitializeComponent();
            this.DataContext = new FeedbackViewModel(_parent.Patient.Username);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            _parent.startWindow.Content = new StartPatientPage(_parent);
        }

        private void back_Click_1(object sender, RoutedEventArgs e)
        {
            _parent.startWindow.Content = new StartPatientPage(_parent);
        }
    }
}
