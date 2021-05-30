// File:    DoctorFileRepository.cs
// Author:  User
// Created: Saturday, March 27, 2021 2:13:31 PM
// Purpose: Definition of Class DoctorFileRepository

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class DoctorFileRepository
{
    private static string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "doctors.json";
    public static List<Doctor> GetAll()
    {
        if (!File.Exists(_startupPath))
        {
            var tmp = File.OpenWrite(_startupPath);
            tmp.Close();
        }
        List<Doctor> doctors;
        String allText = File.ReadAllText(_startupPath);
        if (allText.Equals(""))
        {
            doctors = new List<Doctor>();
        }
        else
        {
            doctors = JsonConvert.DeserializeObject<List<Doctor>>(allText);
        }
        return doctors;
    }

    public static Doctor GetOne(string username)
    {
        List<Doctor> doctors = GetAll();
        foreach (Doctor doctor in doctors)
        {
            if (doctor.Username.Equals(username))
                return doctor;
        }
        return null;
    }

    public static Boolean RemoveDoctor(string username)
    {
        List<Doctor> doctors = GetAll();
        foreach (Doctor doctor in doctors)
        {
            if (doctor.Username.Equals(username))
            {
                doctors[doctors.IndexOf(doctor)].IsDeleted = true;
                Save(doctors);
                return true;
            }
        }
        return false;
    }

    public static Boolean AddDoctor(Doctor newDoctor)
    {
        List<Doctor> doctors = GetAll();
        doctors.Add(newDoctor);
        Save(doctors);
        return true;
    }

    public static Boolean UpdateDoctor(string username, Doctor newDoctor)
    {
        List<Doctor> doctors = GetAll();
        foreach (Doctor doctor in doctors)
        {
            if (doctor.Username.Equals(username))
            {
                doctors[doctors.IndexOf(doctor)] = newDoctor;
                Save(doctors);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Doctor> doctors)
    {
        string serializeObject = JsonConvert.SerializeObject(doctors);
        File.WriteAllText(_startupPath, serializeObject);
    }

}