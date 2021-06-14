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
        private MedicineController medicineController = new MedicineController();

        public MedicinesCountReport()
        {
            _medicines = medicineController.GetAllMedicines();
        }

        public abstract int Calculate();
    }

    public class ValidatedMedicinesCount : MedicinesCountReport
    {
        public ValidatedMedicinesCount() : base() { }

        public override int Calculate() => _medicines.Where(x => x.MedicineStatus == MedicineStatus.validated).Count();

    }

    public class RejectedMedicinesCount : MedicinesCountReport
    {
        public RejectedMedicinesCount() : base() { }

        public override int Calculate() => _medicines.Where(x => x.MedicineStatus == MedicineStatus.rejected).Count();
    }

    public class WaitingMedicinesCount : MedicinesCountReport
    {
        public WaitingMedicinesCount() : base() { }

        public override int Calculate() => _medicines.Where(x => x.MedicineStatus == MedicineStatus.waitingForValidation).Count();
    }
}
