using InformacioniSistemBolnice.Manager_ns.ViewModel;
using InformacioniSistemBolnice.Upravnik;
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
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Manager_ns
{
    /// <summary>
    /// Interaction logic for FeedbackWindow.xaml
    /// </summary>
    public partial class FeedbackWindow : Window
    {
        public FeedbackWindow(UpravnikWindow parent)
        {
            InitializeComponent();
            this.DataContext = new ManagerFeedbackViewModel(parent._loggedManager.Username, this);
        }
    }
}
