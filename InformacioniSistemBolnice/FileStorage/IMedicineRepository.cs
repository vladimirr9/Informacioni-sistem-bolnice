using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IMedicineRepository
    {
        List<Medicine> GetAll();

        Medicine GetOne(String medicineId);

        Boolean Remove(String medicineId);

        Boolean Add(Medicine newMedicine);

        Boolean Update(String medicineId, Medicine newMedicine);

        Medicine GetOneByName(String name);
    }
}
