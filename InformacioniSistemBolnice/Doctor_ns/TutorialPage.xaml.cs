using System;
using System.Collections.Generic;
using System.IO;
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
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice.Doctor_ns
{
    /// <summary>
    /// Interaction logic for TutorialPage.xaml
    /// </summary>
    public partial class TutorialPage : Page
    {
        private static TutorialPage instance;
        private DoctorWindow parent;
        public TutorialPage(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            this.DataContext = new TutorialPageViewModel(parent);
        }

        public static TutorialPage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new TutorialPage(parent);
            return instance;
        }
    }
}
