using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    class ValidatedMedicineService
    {
        public List<Medicine> GetValidatedMedicines()
        {
            List<Medicine> validated = new List<Medicine>();
            foreach (Medicine medicine in MedicineFileRepository.GetAll())
            {
                if (!medicine.IsDeleted && medicine.MedicineStatus.Equals(MedicineStatus.validated))
                {
                    validated.Add(medicine);
                }
            }

            return validated;
        }
    }
}
