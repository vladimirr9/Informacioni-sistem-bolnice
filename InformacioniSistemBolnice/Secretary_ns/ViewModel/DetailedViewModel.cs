using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Secretary_ns.ViewModel
{
    class DetailedViewModel : BindableBase
    {
        private DetailedWindow _parent;
        private Patient _patient;
        private ObservableCollection<Ingredient> _allergensCombo;
        private ObservableCollection<Ingredient> _allergensListView;
        private Ingredient _selectedAllergenCombo;
        private Ingredient _selectedAllergenListView;
        private PatientController _patientController = new PatientController();
        private MedicineController _medicineController = new MedicineController();
        private MedicineIngredientsController _medicineIngredientsController = new MedicineIngredientsController();
        public MyICommand CloseCommand { get; set; }
        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }




        public Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                OnPropertyChanged("Patient");
            }
        }
        public ObservableCollection<Ingredient> AllergensCombo
        {
            get { return _allergensCombo; }
            set
            {
                _allergensCombo = value;
                OnPropertyChanged("AllergensCombo");
            }
        }
        public ObservableCollection<Ingredient> AllergensListView
        {
            get { return _allergensListView; }
            set
            {
                _allergensListView = value;
                OnPropertyChanged("AllergensListView");
            }
        }
        public Ingredient SelectedAllergenCombo
        {
            get { return _selectedAllergenCombo; }
            set
            {
                _selectedAllergenCombo = value;
                OnPropertyChanged("SelectedAllergenCombo");
            }
        }
        public Ingredient SelectedAllergenListView
        {
            get { return _selectedAllergenListView; }
            set
            {
                _selectedAllergenListView = value;
                OnPropertyChanged("SelectedAllergenListView");
            }
        }


        public DetailedViewModel(DetailedWindow parent, string username)
        {
            _parent = parent;
            _patient = _patientController.GetOne(username);
            CloseCommand = new MyICommand(CloseButton_Click);
            AddCommand = new MyICommand(AddButton_Click);
            DeleteCommand = new MyICommand(DeleteButton_Click);
            UpdateTable();
        }

        private void CloseButton_Click()
        {
            _parent.Close();
        }

        private void AddButton_Click()
        {
            if (SelectedAllergenCombo == null)
                return;
            _patientController.AddAllergen(_patient, SelectedAllergenCombo);
            UpdateTable();
        }

        private void DeleteButton_Click()
        {
            if (SelectedAllergenListView == null)
                return;

            var result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovaj alergen?", "Potvrda brisanja",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            _patientController.RemoveAllergen(Patient, SelectedAllergenListView);
            UpdateTable();
        }

        public void UpdateTable()
        {
            var allergensComboList = new List<Ingredient>();
            var allergensListView = new List<Ingredient>();
            foreach (var ingredient in _medicineIngredientsController.GetAllIngredients())
                if (Patient.MedicalRecord.Allergens.Contains(ingredient))
                    allergensListView.Add(ingredient);
                else
                    allergensComboList.Add(ingredient);

            AllergensCombo = new ObservableCollection<Ingredient>(allergensComboList);
            AllergensListView = new ObservableCollection<Ingredient>(allergensListView);
        }
    }
}

