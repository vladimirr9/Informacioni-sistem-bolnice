﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class MedicineFileRepository
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "lekovi.json";

    public static List<Medicine> GetAll()
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

    public static Medicine GetOne(String medicineId)
    {
        List<Medicine> medicines = GetAll();
        foreach (Medicine medicine in medicines)
        {
            if (medicine.Id.Equals(medicineId))
            {
                return medicines[medicines.IndexOf(medicine)];
            }
        }
        return null;
    }

    public static Boolean RemoveMedicine(String medicineId)
    {
        List<Medicine> medicines = GetAll();
        foreach (Medicine medicine in medicines)
        {
            if (medicine.Id.Equals(medicineId))
            {
                medicines[medicines.IndexOf(medicine)].IsDeleted = true;
                Save(medicines);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Medicine> medicines)
    {
        string write = JsonConvert.SerializeObject(medicines);
        File.WriteAllText(startupPath, write);
    }

    public static Boolean AddMedicine(Medicine newMedicine)
    {
        List<Medicine> medicines = GetAll();
        medicines.Add(newMedicine);
        Save(medicines);
        return true;
    }

    public static Boolean UpdateMedicine(String medicineId, Medicine newMedicine)
    {
        List<Medicine> medicines = GetAll();
        foreach (Medicine medicine in medicines)
        {
            if (medicine.Id.Equals(medicineId))
            {
                medicines[medicines.IndexOf(medicine)] = newMedicine;
                Save(medicines);
                return true;
            }
        }
        return false;
    }
}