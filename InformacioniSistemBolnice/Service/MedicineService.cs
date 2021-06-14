using InformacioniSistemBolnice.FileStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Service
{
    public class MedicineService
    {
        private IMedicineRepository _medicineRepository = new MedicineFileRepository();
        

        public void AddMedicine(Medicine medicine)
        {
            if (!IsIdunique(medicine.MedicineId))
            {
                MessageBox.Show("Uneti ID leka već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!IsNameUnique(medicine.Name))
            {
                MessageBox.Show("Uneto ime leka već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            _medicineRepository.Add(medicine);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            _medicineRepository.Update(medicine.MedicineId, medicine);
        }

        public void RemoveMedicine(Medicine medicine)
        {
            _medicineRepository.Remove(medicine.MedicineId);
        }

        public void SendMedicineForRemovingValidation(Medicine medicine)
        {
            medicine.MedicineStatus = MedicineStatus.waitingForValidation;
            UpdateMedicine(medicine);
        }

        public Medicine GetOneByname(String name)
        {
            return _medicineRepository.GetOneByName(name);
        }
        public bool IsIdunique(String medicineId)
        {
            if (_medicineRepository.GetOne(medicineId) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsNameUnique(String name)
        {
            if (_medicineRepository.GetOne(name) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Medicine> GetAllMedicines()
        {
            return _medicineRepository.GetAll();
        }

        
        

        
       
    }
}
