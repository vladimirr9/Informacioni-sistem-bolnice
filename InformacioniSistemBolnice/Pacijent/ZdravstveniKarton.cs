// File:    ZdravstveniKarton.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:40:57 PM
// Purpose: Definition of Class ZdravstveniKarton

using System;

public class ZdravstveniKarton
{
   private String brojZdravstvenogKartona;
   
   public Pacijent pacijent;

    public ZdravstveniKarton(string brojZdravstvenogKartona, Pacijent pacijent)
    {
        this.brojZdravstvenogKartona = brojZdravstvenogKartona;
        this.pacijent = pacijent;
    }
}