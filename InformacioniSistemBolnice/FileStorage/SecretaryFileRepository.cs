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

    /*
    private string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "secretaries.json";
    public List<Secretary> GetAll()
   {
        if (!File.Exists(_startupPath))
        {
            var tmp = File.OpenWrite(_startupPath);
            tmp.Close();
        }
        List<Secretary> secretaries;
        String allText = File.ReadAllText(_startupPath);
        if (allText.Equals(""))
        {
            secretaries = new List<Secretary>();
        }
        else
        {
            secretaries = JsonConvert.DeserializeObject<List<Secretary>>(allText);
        }
        return secretaries;
    }
   
   public Secretary GetOne(string username)
   {
        List<Secretary> secretaries = GetAll();
        foreach (Secretary secretary in secretaries)
        {
            if (secretary.Username.Equals(username))
                return secretary;
        }
        return null;
    }
   
   public Boolean Remove(string username)
   {
        List<Secretary> secretaries = GetAll();
        foreach (Secretary secretary in secretaries)
        {
            if (secretary.Username.Equals(username))
            {
                secretaries[secretaries.IndexOf(secretary)].IsDeleted = true;
                Save(secretaries);
                return true;
            }
        }
        return false;
    }
   
   public Boolean Add(Secretary newSecretary)
   {
        List<Secretary> secretaries = GetAll();
        secretaries.Add(newSecretary);
        Save(secretaries);
        return true;
    }
   
   public Boolean Update(string username, Secretary newSecretary)
   {
        List<Secretary> secretaries = GetAll();
        foreach (Secretary secretary in secretaries)
        {
            if (secretary.Username.Equals(username))
            {
                secretaries[secretaries.IndexOf(secretary)] = newSecretary;
                Save(secretaries);
                return true;
            }
        }
        return false;
    }
    private void Save(List<Secretary> secretaries)
    {
        string serializeObject = JsonConvert.SerializeObject(secretaries);
        File.WriteAllText(_startupPath, serializeObject);
    }
    */

}