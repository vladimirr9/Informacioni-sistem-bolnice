// File:    Recept.cs
// Author:  Korisnik
// Created: Monday, April 12, 2021 8:25:35 PM
// Purpose: Definition of Class Recept

using System;

public class Recept
{
   private String lek;
   private DateTime datumIzdavanja;
   
   public Lekar lekar;

    public Recept(String lek, DateTime datum, Lekar lekar)
    {
        this.lek = lek;
        this.datumIzdavanja = datum;
        this.lekar = lekar;
    }

}