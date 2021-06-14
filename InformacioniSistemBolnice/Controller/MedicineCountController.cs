using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class MedicineCountController
    {
        private MedicineCountService _medicineCountService = new MedicineCountService();
        public int ValidatedMedicinesCount()
        {
            return _medicineCountService.ValidatedMedicinesCount();
        }
        public int RejectedMedicinesCount()
        {
            return _medicineCountService.RejectedMedicinesCount();
        }
        public int WaitingMedicinesCount()
        {
            return _medicineCountService.WaitingMedicinesCount();
        }
    }
}
