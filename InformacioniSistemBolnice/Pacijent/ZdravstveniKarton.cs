// File:    ZdravstveniKarton.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:40:57 PM
// Purpose: Definition of Class ZdravstveniKarton

using InformacioniSistemBolnice;
using System;

public class ZdravstveniKarton
{
    private String brojZdravstvenogKartona { get; set; }
   [Newtonsoft.Json.JsonIgnore]
    public Pacijent pacijent { get; set; }
    private System.Collections.Generic.List<Alergen> alergen;

    public System.Collections.Generic.List<Alergen> Alergen
    {
        get
        {
            if (alergen == null)
                alergen = new System.Collections.Generic.List<Alergen>();
            return alergen;
        }
        set
        {
            RemoveAllAlergen();
            if (value != null)
            {
                foreach (Alergen oAlergen in value)
                    AddAlergen(oAlergen);
            }
        }
    }


    public void AddAlergen(Alergen newAlergen)
    {
        if (newAlergen == null)
            return;
        if (this.alergen == null)
            this.alergen = new System.Collections.Generic.List<Alergen>();
        if (!this.alergen.Contains(newAlergen))
            this.alergen.Add(newAlergen);
    }


    public void RemoveAlergen(Alergen oldAlergen)
    {
        if (oldAlergen == null)
            return;
        if (this.alergen != null)
            if (this.alergen.Contains(oldAlergen))
                this.alergen.Remove(oldAlergen);
    }


    public void RemoveAllAlergen()
    {
        if (alergen != null)
            alergen.Clear();
    }
    

    public ZdravstveniKarton(string brojZdravstvenogKartona)
    {
        this.brojZdravstvenogKartona = brojZdravstvenogKartona;
    }
   
}