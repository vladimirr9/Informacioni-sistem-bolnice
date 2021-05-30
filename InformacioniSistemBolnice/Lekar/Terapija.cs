// File:    Terapija.cs
// Author:  Korisnik
// Created: Monday, April 12, 2021 8:15:39 PM
// Purpose: Definition of Class Terapija

using System;

public class Terapija
{
   public String Opis { get; set; }
   public DateTime PocetakTerapije { get; set; }
   public DateTime KrajTerapije { get; set; }
   public int Dan { get; set; }

    public Terapija()
    {
    }

    public Terapija(string opis, DateTime pocetakTerapije, DateTime krajTerapije, int dan)
    {
        Opis = opis;
        PocetakTerapije = pocetakTerapije;
        KrajTerapije = krajTerapije;
        Dan = dan;
    }
}