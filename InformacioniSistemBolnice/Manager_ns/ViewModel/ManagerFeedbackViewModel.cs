using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.User;
using InformacioniSistemBolnice.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Manager_ns.ViewModel
{
    class ManagerFeedbackViewModel : BindableBase
    {
        public String Username { get; set; }
        private String feedbackDescription;
        private ObservableCollection<String> feedbackTypes;
        private String selectedType;
        private FeedbackWindow parent;
        public MyICommand ConfirmCommand { get; set; }
        private FeedbackController _feedbackController = new FeedbackController();

        public String FeedbackDescription
        {
            get { return feedbackDescription; }
            set
            {
                this.feedbackDescription = value;
                OnPropertyChanged("FeedbackDescription");
            }
        }

        public ObservableCollection<String> FeedbackTypes
        {
            get { return feedbackTypes; }
            set
            {
                this.feedbackTypes = value;
                OnPropertyChanged("FeedbackTypes");
            }
        }

        public String SelectedType
        {
            get { return selectedType; }
            set
            {
                this.selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }

        public ManagerFeedbackViewModel(String username, FeedbackWindow parent)
        {
            this.Username = username;
            this.parent = parent;
            GenerateTypes();
            ConfirmCommand = new MyICommand(Confirm);
        }

        private void GenerateTypes()
        {
            List<String> feedbackTypes = new List<string>
            {
                "Prijava greške",
                "Poboljšanje aplikacije"
            };
            FeedbackTypes = new ObservableCollection<string>(feedbackTypes);
            OnPropertyChanged("FeedbackTypes");
        }

        private void Confirm()
        {
            if (selectedType != null && feedbackDescription != "")
            {
                Feedback feedback = new Feedback(_feedbackController.GenerateId(), Username, SelectedType, FeedbackDescription);
                _feedbackController.Add(feedback);
                /*SelectedType = null;
                FeedbackDescription = "";*/
                parent.Close();
            }
        }
    }
}
