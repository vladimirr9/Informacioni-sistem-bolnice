using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class LekFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "lekovi.json";

    public static List<Lek> GetAll()
    {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Lek> lekovi;
        String procitano = File.ReadAllText(startupPath);
        if (procitano.Equals(""))
        {
            lekovi = new List<Lek>();
        }
        else
        {
            lekovi = JsonConvert.DeserializeObject<List<Lek>>(procitano);
        }
        return lekovi;
    }

    public static Lek GetOne(String sifra)
    {
        List<Lek> lekovi = GetAll();
        foreach (Lek l in lekovi)
        {
            if (l.Sifra.Equals(sifra))
            {
                return lekovi[lekovi.IndexOf(l)];
            }
        }
        return null;
    }

    public static Boolean RemoveLek(String sifra)
    {
        List<Lek> lekovi = GetAll();
        foreach (Lek l in lekovi)
        {
            if (l.Sifra.Equals(sifra))
            {
                lekovi[lekovi.IndexOf(l)].IsDeleted = true;
                Save(lekovi);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Lek> lekovi)
    {
        string upisivanje = JsonConvert.SerializeObject(lekovi);
        File.WriteAllText(startupPath, upisivanje);
    }

    public static Boolean AddLek(Lek noviLek)
    {
        List<Lek> lekovi = GetAll();
        lekovi.Add(noviLek);
        Save(lekovi);
        return true;
    }

    public static Boolean UpdateLek(String sifra, Lek noviLek)
    {
        List<Lek> lekovi = GetAll();
        foreach (Lek l in lekovi)
        {
            if (l.Sifra.Equals(sifra))
            {
                lekovi[lekovi.IndexOf(l)] = noviLek;
                Save(lekovi);
                return true;
            }
        }
        return false;
    }
}
