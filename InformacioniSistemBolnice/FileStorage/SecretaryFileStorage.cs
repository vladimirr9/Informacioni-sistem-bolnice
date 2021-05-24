// File:    SekretarFileStorage.cs
// Author:  Korisnik
// Created: Saturday, March 27, 2021 2:10:32 PM
// Purpose: Definition of Class SekretarFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using InformacioniSistemBolnice.Secretary_ns;

public class SecretaryFileStorage
{
    private static string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "sekretari.json";
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
            if (secretary.korisnickoIme.Equals(username))
                return secretary;
        }
        return null;
    }
   
   public static Boolean RemoveSekretar(string username)
   {
        List<Secretary> secretaries = GetAll();
        foreach (Secretary secretary in secretaries)
        {
            if (secretary.korisnickoIme.Equals(username))
            {
                secretaries[secretaries.IndexOf(secretary)].isDeleted = true;
                Save(secretaries);
                return true;
            }
        }
        return false;
    }
   
   public static Boolean AddSekretar(Secretary newSecretary)
   {
        List<Secretary> secretaries = GetAll();
        secretaries.Add(newSecretary);
        Save(secretaries);
        return true;
    }
   
   public static Boolean UpdateSekretar(string username, Secretary newSecretary)
   {
        List<Secretary> secretaries = GetAll();
        foreach (Secretary secretary in secretaries)
        {
            if (secretary.korisnickoIme.Equals(username))
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
        string contents = JsonConvert.SerializeObject(secretaries);
        File.WriteAllText(_startupPath, contents);
    }

}