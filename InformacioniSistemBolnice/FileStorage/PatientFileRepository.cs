// File:    PatientFileRepository.cs
// Author:  User
// Created: Saturday, March 27, 2021 1:58:44 PM
// Purpose: Definition of Class PatientFileRepository

using InformacioniSistemBolnice.FileStorage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class PatientFileRepository : GenericUserFileRepository<Patient> , IPatientRepository
{
    public PatientFileRepository()
    {
        StartupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "patients.json";
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
}
