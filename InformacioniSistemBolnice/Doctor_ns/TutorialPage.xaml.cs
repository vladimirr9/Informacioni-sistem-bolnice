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
            string projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            //MediaElement.Source = new Uri(projectPath + "\\Images\\7174186.png", UriKind.Absolute);
            MediaElement.LoadedBehavior = MediaState.Manual;
            MediaElement.Play();
            StartStopButton.Content = "Pauziraj";
        }

        public static TutorialPage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new TutorialPage(parent);
            return instance;
        }

        private void Start_Stop_Click(object sender, RoutedEventArgs e)
        {
            if (StartStopButton.Content == "Pauziraj")
            {
                MediaElement.Stop();
                StartStopButton.Content = "Nastavi";
            }
            else
            {
                MediaElement.Play();
                StartStopButton.Content = "Pauziraj";
            }
        }
    }
}
