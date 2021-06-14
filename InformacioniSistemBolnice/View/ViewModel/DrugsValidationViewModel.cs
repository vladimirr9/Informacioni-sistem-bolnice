using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Doctor_ns;

namespace InformacioniSistemBolnice.View.ViewModel
{
    class DrugsValidationViewModel : BindableBase
    {
        private DoctorWindow parent;
        private String selectedMedicineName;
        private ObservableCollection<String> medicines;
        private ObservableCollection<String> ingredients;
        public MyICommand ValidateCommand { get; set; }
        public MyICommand ReturnCommand { get; set; }
        private MedicineController _medicineController = new MedicineController();
        private MedicineIngredientsController _medicineIngredientsController = new MedicineIngredientsController();

        public String SelectedMedicine
        {
            get { return selectedMedicineName; }
            set
            {
                this.selectedMedicineName = value;
                SelectionChange();
                OnPropertyChanged("SelectedMedicine");
            }
        }

        public ObservableCollection<String> Medicines
        {
            get { return medicines; }
            set
            {
                this.medicines = value;
                OnPropertyChanged("Medicines");
            }
        }

        public ObservableCollection<String> Ingredients
        {
            get { return ingredients; }
            set
            {
                this.ingredients = value;
                OnPropertyChanged("Ingredients");
            }
        }

        public DrugsValidationViewModel(DoctorWindow parent)
        {
            this.parent = parent;
            ValidateCommand = new MyICommand(Validate_Click);
            ReturnCommand = new MyICommand(Return_Click);
            UpdateList();
        }

        private void Validate_Click()
        {
            if (selectedMedicineName != null)
            {
                Medicine medicine = _medicineController.GetOneByname(selectedMedicineName);
                medicine.MedicineStatus = MedicineStatus.validated;
                _medicineController.UpdateMedicine(medicine);
                UpdateList();
                Ingredients = null;
                DrugsPage.GetPage(parent).UpdateList();
            }
        }

        private void Return_Click()
        {
            if (selectedMedicineName != null)
            {
                Medicine medicine = _medicineController.GetOneByname(selectedMedicineName);
                medicine.MedicineStatus = MedicineStatus.rejected;
                _medicineController.UpdateMedicine(medicine);
                UpdateList();
                Ingredients = null;
            }
        }

        private void UpdateList()
        {
            List<String> medicines = new List<String>();
            foreach (Medicine medicine in _medicineController.GetAllMedicines())
            {
                if (!medicine.IsDeleted && medicine.MedicineStatus.Equals(MedicineStatus.waitingForValidation))
                    medicines.Add(medicine.Name);
            }

            Medicines = new ObservableCollection<String>(medicines);
            OnPropertyChanged("Medicines");
        }

        public void SelectionChange()
        {
            List<String> ingredients = new List<String>();
            if (selectedMedicineName != null)
            {
                foreach (Ingredient ingredient in _medicineIngredientsController.GetMedicineIngredients(_medicineController.GetOneByname(selectedMedicineName)))
                {
                    ingredients.Add(ingredient.Name);
                }

                Ingredients = new ObservableCollection<String>(ingredients);
                OnPropertyChanged("Ingredients");
            }
        }
    }
}
