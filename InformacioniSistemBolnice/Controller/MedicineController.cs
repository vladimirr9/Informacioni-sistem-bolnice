using InformacioniSistemBolnice.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Controller
{
    public class MedicineController
    {
        private MedicineService _medicineService = new MedicineService();
        private ValidatedMedicineService _validatedMedicineService = new ValidatedMedicineService();
        public void AddMedicine(Medicine medicine)
        {
            _medicineService.AddMedicine(medicine);
        }
        public void UpdateMedicine(Medicine medicine)
        {
            _medicineService.UpdateMedicine(medicine);
        }
        public void RemoveMedicine(Medicine medicine)
        {
            _medicineService.RemoveMedicine(medicine);
        }
        public Medicine GetOneByname(String name)
        {
            return _medicineService.GetOneByname(name);
        }
        public List<Medicine> GetAllMedicines()
        {
            return _medicineService.GetAllMedicines();
        }
        
        public void SendMedicineForRemovingValidation(Medicine medicine)
        {
            _medicineService.SendMedicineForRemovingValidation(medicine);
        }

        public List<Medicine> GetValidatedMedicines()
        {
            return _validatedMedicineService.GetValidatedMedicines();
        }

        
    }
}
