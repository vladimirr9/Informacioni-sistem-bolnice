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
    /// Interaction logic for SekretarMain.xaml
    /// </summary>
    public partial class SekretarMain : Window
    {
        private Sekretar tekSekretar;
        public SekretarMain(Sekretar tekSekretar)
        {
            this.tekSekretar = tekSekretar;
            InitializeComponent();
        }

        private void PacijentiButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = PacijentiPage.GetPage();
        }

        private void PocetnaButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = PocetnaPage.GetPage(tekSekretar);
        }

        private void TerminiButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = TerminiPage.GetPage();
        }
    }
}
