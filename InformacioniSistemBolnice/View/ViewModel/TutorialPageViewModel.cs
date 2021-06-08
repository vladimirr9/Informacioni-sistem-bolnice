using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.View.ViewModel
{
    class TutorialPageViewModel : BindableBase
    {
        private static TutorialPageViewModel instance;
        private String buttonText;
        private DoctorWindow parent;
        private MediaElement mediaElementObject = new MediaElement();
        public MyICommand StartStopCommand { get; set; }

        public MediaElement MediaElementObject
        {
            get { return mediaElementObject; }
            set
            {
                mediaElementObject = value;
                OnPropertyChanged("MediaElementObject");
            }
        }

        public String ButtonText
        {
            get { return buttonText; }
            set
            {
                buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }

        public TutorialPageViewModel(DoctorWindow parent)
        {
            this.parent = parent;
            StartStopCommand = new MyICommand(Start_Stop_Click);
            string projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            mediaElementObject.Source = new Uri(projectPath + "\\Images\\tutorial.mp4", UriKind.Absolute);
            mediaElementObject.LoadedBehavior = MediaState.Manual;
            mediaElementObject.Play();
            buttonText = "Pauziraj";
        }

        public static TutorialPageViewModel GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new TutorialPageViewModel(parent);
            return instance;
        }

        private void Start_Stop_Click()
        {
            if (buttonText == "Pauziraj")
            {
                mediaElementObject.Pause();
                buttonText = "Nastavi";
            }
            else
            {
                mediaElementObject.Play();
                buttonText = "Pauziraj";
            }
            OnPropertyChanged("ButtonText");
        }
    }
}
