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

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class ReportsPage : Page
    {
        private static ReportsPage _instance;
        public static ReportsPage GetPage()
        {
            if (_instance == null)
                _instance = new ReportsPage();
            return _instance;
        }
        private ReportsPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Generate1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
