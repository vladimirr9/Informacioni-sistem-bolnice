// File:    DoctorFileRepository.cs
// Author:  User
// Created: Saturday, March 27, 2021 2:13:31 PM
// Purpose: Definition of Class DoctorFileRepository

using System;
using System.Collections.Generic;
using System.IO;
using InformacioniSistemBolnice.FileStorage;
using Newtonsoft.Json;

public class DoctorFileRepository : GenericUserFileRepository<Doctor> , IDoctorRepository
{
    public DoctorFileRepository()
    {
        StartupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "doctors.json";
    }
}