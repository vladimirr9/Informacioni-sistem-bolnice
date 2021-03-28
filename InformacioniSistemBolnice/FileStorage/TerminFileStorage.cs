// File:    TerminFileStorage.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 8:27:36 PM
// Purpose: Definition of Class TerminFileStorage

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class TerminFileStorage
{
    private static string putanja = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "termini.json";
    public static List<Termin> GetAll()
    {
        if (!File.Exists(putanja))
        {
            var tmp = File.OpenWrite(putanja);
            tmp.Close();
        }
        List<Termin> termini;
        String procitano = File.ReadAllText(putanja);
        if (procitano.Equals(""))
        {
            termini = new List<Termin>();
        }
        else
        {
            termini = JsonConvert.DeserializeObject<List<Termin>>(procitano);
        }
        return termini;
    }

    public static Termin GetOne(int iDTermina)
    {
        List<Termin> termini = GetAll();
        foreach (Termin t in termini)
        {
            if (t.iDTermina.Equals(iDTermina))
                return t;
        }
        return null;
    }

    public static Boolean RemoveTermin(int iDTermina)
    {
        List<Termin> termini = GetAll();
        foreach (Termin t in termini)
        {
            if (t.iDTermina.Equals(iDTermina))
            {
                termini[termini.IndexOf(t)].status = StatusTermina.otkazan;
                Save(termini);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Termin> termini)
    {
        string upis = JsonConvert.SerializeObject(termini);
        File.WriteAllText(putanja, upis);
    }

    public static Boolean AddTermin(Termin noviTermin)
    {
        List<Termin> termini = GetAll();
        termini.Add(noviTermin);
        Save(termini);
        return true;

    }

    public static Boolean UpdateTermin(int iDTermina, Termin noviTermin)
    {
        List<Termin> termini = GetAll();
        foreach (Termin t in termini)
        {
            if (t.iDTermina.Equals(iDTermina))
            {
                termini[termini.IndexOf(t)] = noviTermin;
                Save(termini);
                return true;
            }
        }
        return false;

    }

}