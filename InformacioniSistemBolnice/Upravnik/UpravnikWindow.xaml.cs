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

namespace InformacioniSistemBolnice.Upravnik
{
    /// <summary>
    /// Interaction logic for UpravnikWindow.xaml
    /// </summary>
    public partial class UpravnikWindow : Window
    {
        public UpravnikWindow()
        {
            InitializeComponent();
        }

        private void WindowProstorije(object sender, RoutedEventArgs e)
        {
            WindowProstorije wp = new WindowProstorije(this);
            wp.Show();
        }

        private void LekoviWindow(object sender, RoutedEventArgs e)
        {
            LekoviWindow lw = new LekoviWindow(this);
            lw.Show();
        }
    }
}
