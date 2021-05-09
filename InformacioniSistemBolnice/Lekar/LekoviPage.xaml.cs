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
    /// <summary>
    /// Interaction logic for LekoviPage.xaml
    /// </summary>
    public partial class LekoviPage : Page
    {
        public LekarWindow parent;
        private static LekoviPage instance;

        public LekoviPage(LekarWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        public static LekoviPage GetPage(LekarWindow parent)
        {
            if (instance == null)
                instance = new LekoviPage(parent);
            return instance;
        }

        private void LekoviZaPotvrduClick(object sender, RoutedEventArgs e)
        {
            LekoviZaPotvrduWindow lekoviWindow = new LekoviZaPotvrduWindow();
            Application.Current.MainWindow = lekoviWindow;
            lekoviWindow.Show();
        }
    }
}
