// File:    UpravnikFileStorage.cs
// Author:  User
// Created: Saturday, March 27, 2021 2:15:03 PM
// Purpose: Definition of Class UpravnikFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using InformacioniSistemBolnice.FileStorage;

public class ManagerFileRepository : GenericUserFileRepository<Manager> , IManagerRepository
{
    public ManagerFileRepository()
    {
        StartupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "managers.json";

    }
}