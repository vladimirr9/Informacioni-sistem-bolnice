using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.User;

namespace InformacioniSistemBolnice.View.ViewModel
{
    class FeedbackViewModel : BindableBase
    {
        public String Username { get; set; }
        private String description;
        private ObservableCollection<String> types;
        private String selectedType;
        public MyICommand ConfirmCommand { get; set; }
        private FeedbackController _feedbackController = new FeedbackController();

        public String Description
        {
            get { return description; }
            set
            {
                this.description = value;
                OnPropertyChanged("Description");
            }
        }

        public ObservableCollection<String> Types
        {
            get { return types; }
            set
            {
                this.types = value;
                OnPropertyChanged("Types");
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

        public FeedbackViewModel(String username)
        {
            this.Username = username;
            GenerateTypes();
            ConfirmCommand = new MyICommand(Confirm_Click);
        }

        private void Confirm_Click()
        {
            if (selectedType != null && description != "")
            {
                Feedback feedback = new Feedback(_feedbackController.GenerateId(), Username, SelectedType, Description);
                _feedbackController.Add(feedback);
                SelectedType = null;
                Description = "";
            }
        }

        private void GenerateTypes()
        {
            List<String> types = new List<string>();
            types.Add("Prijava greske");
            types.Add("Poboljsanje aplikacije");
            Types = new ObservableCollection<string>(types);
            OnPropertyChanged("Types");
        }

        
    }
}
