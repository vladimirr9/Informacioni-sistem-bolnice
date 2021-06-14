// File:    SekretarFileStorage.cs
// Author:  User
// Created: Saturday, March 27, 2021 2:10:32 PM
// Purpose: Definition of Class SekretarFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.Secretary_ns;

public class SecretaryFileRepository : GenericUserFileRepository<Secretary> , ISecretaryRepository
{
    public SecretaryFileRepository()
    {
        StartupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "secretaries.json";
    }
}