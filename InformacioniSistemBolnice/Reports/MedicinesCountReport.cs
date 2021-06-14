using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Reports
{
    public abstract class MedicinesCountReport
    {
        protected readonly List<Medicine> _medicines;
        protected int _sum;
        private MedicineController medicineController = new MedicineController();

        public MedicinesCountReport()
        {
            _medicines = medicineController.GetAllMedicines();
            _sum = 0;
        }

        public abstract int Calculate();
    }

    public class ValidatedMedicinesCount : MedicinesCountReport
    {
        public ValidatedMedicinesCount() : base() { }

        public override int Calculate()
        {
            foreach(Medicine medicine in _medicines)
            {
                if(medicine.MedicineStatus == MedicineStatus.validated)
                {
                    _sum += medicine.Quantity;
                }
            }
            return _sum;
        }

    }

    public class RejectedMedicinesCount : MedicinesCountReport
    {
        public RejectedMedicinesCount() : base() { }

        public override int Calculate()
        {
            foreach (Medicine medicine in _medicines)
            {
                if (medicine.MedicineStatus == MedicineStatus.rejected)
                {
                    _sum += medicine.Quantity;
                }
            }
            return _sum;
        }       
    }
    public class WaitingMedicinesCount : MedicinesCountReport
    {
        public WaitingMedicinesCount() : base() { }

        public override int Calculate()
        {
            foreach (Medicine medicine in _medicines)
            {
                if (medicine.MedicineStatus == MedicineStatus.waitingForValidation)
                {
                    _sum += medicine.Quantity;
                }
            }
            return _sum;
        }
    }
}
