// File:    UpravnikFileStorage.cs
// Author:  Korisnik
// Created: Saturday, March 27, 2021 2:15:03 PM
// Purpose: Definition of Class UpravnikFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class UpravnikFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "upravnik.json";

    public static List<Upravnik> GetAll()
   {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }

        List<Upravnik> upravnikLista;
        String procitano = File.ReadAllText(startupPath);
        if (procitano.Equals(""))
        {
            upravnikLista = new List<Upravnik>();
        }
        else
        {
            upravnikLista = JsonConvert.DeserializeObject<List<Upravnik>>(procitano);
        }
        return upravnikLista;
    }
   
   public static Upravnik GetOne(String korisnickoIme)
   {
        List<Upravnik> upravnikLista = GetAll();
        foreach (Upravnik u in upravnikLista)
        {
            if (u.korisnickoIme.Equals(korisnickoIme))
                return u;
        }
        return null;
    }
   
   public static Boolean RemoveUpravnik(int iDUpravnika)
   {
      throw new NotImplementedException();
   }
   
   public static Boolean AddUpravnik(Upravnik noviUpravnik)
   {
      throw new NotImplementedException();
   }
   
   public static Boolean UpdateUpravnik(int iDUpravnika, Upravnik noviUpravnik)
   {
      throw new NotImplementedException();
   }

}