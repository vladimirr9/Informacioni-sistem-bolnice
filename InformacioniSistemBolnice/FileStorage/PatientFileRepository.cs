// File:    PatientFileRepository.cs
// Author:  User
// Created: Saturday, March 27, 2021 1:58:44 PM
// Purpose: Definition of Class PatientFileRepository

using InformacioniSistemBolnice.FileStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class PatientFileRepository
{
    private string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "patients.json";

    public List<Patient> GetAll()
    {
        if (!File.Exists(_startupPath))
        {
            var tmp = File.OpenWrite(_startupPath);
            tmp.Close();
        }
        List<Patient> patients;
        String allText = File.ReadAllText(_startupPath);
        if (allText.Equals(""))
        {
            patients = new List<Patient>();
        }
        else
        {
            patients = JsonConvert.DeserializeObject<List<Patient>>(allText);
        }
        return patients;
    }

    public Patient GetOne(string username)
    {
        List<Patient> patients = GetAll();
        foreach (Patient patient in patients)
        {
            if (patient.Username.Equals(username))
                return patients[patients.IndexOf(patient)];
        }
        return null;
    }

    public Patient GetOneByJMBG(string jmbg)
    {
        List<Patient> patients = GetAll();
        foreach (Patient patient in patients)
        {
            if (patient.JMBG.Equals(jmbg))
                return patients[patients.IndexOf(patient)];
        }
        return null;
    }

    public Boolean RemovePatient(string username)
    {
        List<Patient> patients = GetAll();
        foreach (Patient patient in patients)
        {
            if (patient.Username.Equals(username))
            {
                patients[patients.IndexOf(patient)].IsDeleted = true;
                Save(patients);
                return true;
            }
        }
        return false;
    }

    public Boolean AddPatient(Patient newPatient)
    {
        List<Patient> patients = GetAll();
        patients.Add(newPatient);
        Save(patients);
        return true;
    }

    public Boolean UpdatePatient(string username, Patient newPatient)
    {
        List<Patient> patients = GetAll();
        foreach (Patient patient in patients)
        {
            if (patient.Username.Equals(username))
            {
                patients[patients.IndexOf(patient)] = newPatient;
                Save(patients);
                return true;
            }
        }
        return false;
    }

    private void Save(List<Patient> patients)
    {
        string serializeObject = JsonConvert.SerializeObject(patients);
        File.WriteAllText(_startupPath, serializeObject);
    }
}
