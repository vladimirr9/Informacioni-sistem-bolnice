using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using InformacioniSistemBolnice.FileStorage;

public class MedicineFileRepository : IMedicineRepository
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "medicines.json";

    public List<Medicine> GetAll()
    {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Medicine> medicines;
        String read = File.ReadAllText(startupPath);
        if (read.Equals(""))
        {
            medicines = new List<Medicine>();
        }
        else
        {
            medicines = JsonConvert.DeserializeObject<List<Medicine>>(read);
        }
        return medicines;
    }

    public Medicine GetOne(String medicineId)
    {
        List<Medicine> medicines = GetAll();
        foreach (Medicine med in medicines)
        {
            if (med.MedicineId.Equals(medicineId))
            {
                return medicines[medicines.IndexOf(med)];
            }
        }
        return null;
    }

    public Boolean Remove(String medicineId)
    {
        List<Medicine> medicines = GetAll();
        foreach (Medicine med in medicines)
        {
            if (med.MedicineId.Equals(medicineId))
            {
                medicines[medicines.IndexOf(med)].IsDeleted = true;
                Save(medicines);
                return true;
            }
        }
        return false;
    }

    private void Save(List<Medicine> medicines)
    {
        string write = JsonConvert.SerializeObject(medicines);
        File.WriteAllText(startupPath, write);
    }

    public Boolean Add(Medicine newMedicine)
    {
        List<Medicine> medicines = GetAll();
        medicines.Add(newMedicine);
        Save(medicines);
        return true;
    }

    public Boolean Update(String medicineId, Medicine newMedicine)
    {
        List<Medicine> medicines = GetAll();
        foreach (Medicine med in medicines)
        {
            if (med.MedicineId.Equals(medicineId))
            {
                medicines[medicines.IndexOf(med)] = newMedicine;
                Save(medicines);
                return true;
            }
        }
        return false;
    }

    public Medicine GetOneByName(String name)
    {
        List<Medicine> medicines = GetAll();
        foreach (Medicine med in medicines)
        {
            if (med.Name.Equals(name))
            {
                return medicines[medicines.IndexOf(med)];
            }
        }
        return null;
    }
}
