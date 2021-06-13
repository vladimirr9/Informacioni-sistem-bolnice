using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    class ValidatedMedicineService
    {
        private IMedicineRepository _medicineRepository = new MedicineFileRepository();
        public List<Medicine> GetValidatedMedicines()
        {
            List<Medicine> validated = new List<Medicine>();
            foreach (Medicine medicine in _medicineRepository.GetAll())
            {
                if (!medicine.IsDeleted && medicine.MedicineStatus.Equals(MedicineStatus.validated))
                {
                    validated.Add(medicine);
                }
            }

            return validated;
        }

        public List<Medicine> GetUnvalidatedMedicines()
        {
            List<Medicine> unvalidated = new List<Medicine>();
            foreach (Medicine medicine in _medicineRepository.GetAll())
            {
                if (!medicine.IsDeleted && medicine.MedicineStatus.Equals(MedicineStatus.waitingForValidation))
                {
                    unvalidated.Add(medicine);
                }
            }

            return unvalidated;
        }
    }
}
