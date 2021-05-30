// File:    SekretarFileStorage.cs
// Author:  User
// Created: Saturday, March 27, 2021 2:10:32 PM
// Purpose: Definition of Class SekretarFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using InformacioniSistemBolnice.Secretary_ns;

public class SecretaryFileRepository
{
    private static string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "secretaries.json";
    public static List<Secretary> GetAll()
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
   
   public static Secretary GetOne(string username)
   {
        List<Secretary> secretaries = GetAll();
        foreach (Secretary secretary in secretaries)
        {
            if (secretary.Username.Equals(username))
                return secretary;
        }
        return null;
    }
   
   public static Boolean RemoveSecretary(string username)
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
   
   public static Boolean AddSecretary(Secretary newSecretary)
   {
        List<Secretary> secretaries = GetAll();
        secretaries.Add(newSecretary);
        Save(secretaries);
        return true;
    }
   
   public static Boolean UpdateSecretary(string username, Secretary newSecretary)
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
    private static void Save(List<Secretary> secretaries)
    {
        string serializeObject = JsonConvert.SerializeObject(secretaries);
        File.WriteAllText(_startupPath, serializeObject);
    }

}