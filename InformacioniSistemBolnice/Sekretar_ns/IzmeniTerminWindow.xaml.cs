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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for IzmeniTerminWindow.xaml
    /// </summary>
    public partial class IzmeniTerminWindow : Window
    {
        public IzmeniTerminWindow()
        {
            InitializeComponent();
        }

        private void PotvrdiB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OdustaniB_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
