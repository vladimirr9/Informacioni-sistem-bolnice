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

namespace InformacioniSistemBolnice.Secretary_ns
{
    /// <summary>
    /// Interaction logic for FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Page
    {
        private SecretaryMain _parent;
        private Secretary _currentSecretary;
        public FeedbackPage(SecretaryMain parent, Secretary currentSecretary)
        {
            _currentSecretary = currentSecretary;
            _parent = parent;
            InitializeComponent();
            this.DataContext = new FeedbackViewModel(_currentSecretary.Username);
        }
        public static FeedbackPage GetPage(SecretaryMain parent, Secretary currentSecretary)
        {
            parent.Title.Content = "Feedback";
            return new FeedbackPage(parent, currentSecretary);
        }
    }
}
