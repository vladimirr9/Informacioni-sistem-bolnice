using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    public class MedicineCountService
    {
        private MedicineService _medicineService = new MedicineService();
        public int ValidatedMedicinesCount()
        {
            int sum = 0;
            foreach (Medicine medicine in _medicineService.GetAllMedicines())
            {
                if (medicine.MedicineStatus == MedicineStatus.validated)
                {
                    sum += medicine.Quantity;
                }
            }
            return sum;
        }
        public int RejectedMedicinesCount()
        {
            int sum = 0;
            foreach (Medicine medicine in _medicineService.GetAllMedicines())
            {
                if (medicine.MedicineStatus == MedicineStatus.rejected)
                {
                    sum += medicine.Quantity;
                }
            }
            return sum;
        }
        public int WaitingMedicinesCount()
        {
            int sum = 0;
            foreach (Medicine medicine in _medicineService.GetAllMedicines())
            {
                if (medicine.MedicineStatus == MedicineStatus.waitingForValidation)
                {
                    sum += medicine.Quantity;
                }
            }
            return sum;
        }
    }
}
