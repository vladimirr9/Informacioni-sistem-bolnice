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

namespace InformacioniSistemBolnice.Doctor_ns
{
    /// <summary>
    /// Interaction logic for DoctorFeedbackPage.xaml
    /// </summary>
    public partial class DoctorFeedbackPage : Page
    {
        private static DoctorFeedbackPage instance;
        public DoctorFeedbackPage(DoctorWindow parent)
        {
            InitializeComponent();
            this.DataContext = new FeedbackViewModel(parent.Doctor.Username);
        }

        public static DoctorFeedbackPage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new DoctorFeedbackPage(parent);
            return instance;
        }
    }
}
