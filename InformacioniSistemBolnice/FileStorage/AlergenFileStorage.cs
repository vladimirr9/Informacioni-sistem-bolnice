using InformacioniSistemBolnice;
using InformacioniSistemBolnice.Korisnik;
using InformacioniSistemBolnice.Lekar;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class AlergenFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "alergeni.json";

    public static List<Alergen> GetAll()
    {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Alergen> alergeni;
        String procitano = File.ReadAllText(startupPath);
        if (procitano.Equals(""))
        {
            alergeni = new List<Alergen>();
        }
        else
        {
            alergeni = JsonConvert.DeserializeObject<List<Alergen>>(procitano);
        }
        return alergeni;
    }

    public static Alergen GetOne(String naziv)
    {
        List<Alergen> alergeni = GetAll();
        foreach (Alergen a in alergeni)
        {
            if (a.naziv.Equals(naziv))
                return alergeni[alergeni.IndexOf(a)];
        }
        return null;
    }

    public static Boolean RemoveAlergen(String naziv)
    {
        List<Alergen> alergeni = GetAll();
        foreach (Alergen a in alergeni)
        {
            if (a.naziv.Equals(naziv))
            {
                alergeni[alergeni.IndexOf(a)].isDeleted = true;
                Save(alergeni);
                return true;
            }
        }
        return false;
    }

    public static Boolean AddAlergen(Alergen noviAlergen)
    {
        List<Alergen> alergeni = GetAll();
        alergeni.Add(noviAlergen);
        Save(alergeni);
        return true;
    }

    public static Boolean UpdateAlergen(String naziv, Alergen noviAlergen)
    {
        List<Alergen> alergeni = GetAll();
        foreach (Alergen a in alergeni)
        {
            if (a.naziv.Equals(naziv))
            {
                alergeni[alergeni.IndexOf(a)] = noviAlergen;
                Save(alergeni);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Alergen> lista)
    {
        string upis = JsonConvert.SerializeObject(lista);
        File.WriteAllText(startupPath, upis);
    }

}