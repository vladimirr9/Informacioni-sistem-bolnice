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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for PocetnaPage.xaml
    /// </summary>
    public partial class PocetnaPage : Page
    {
        private static PocetnaPage instance;
        private PocetnaPage()
        {
            InitializeComponent();
        }

        public static PocetnaPage GetPage()
        {
            if (instance == null)
                instance = new PocetnaPage();
            return instance;
        }
    }
}
