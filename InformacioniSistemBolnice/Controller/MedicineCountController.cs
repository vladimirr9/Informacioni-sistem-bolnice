using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Controller
{
    public class MedicineCountController
    {
        private MedicineCountController _medicineCountController = new MedicineCountController();
        public int ValidatedMedicinesCount()
        {
            return _medicineCountController.ValidatedMedicinesCount();
        }
        public int RejectedMedicinesCount()
        {
            return _medicineCountController.RejectedMedicinesCount();
        }
        public int WaitingMedicinesCount()
        {
            return _medicineCountController.WaitingMedicinesCount();
        }
    }
}
