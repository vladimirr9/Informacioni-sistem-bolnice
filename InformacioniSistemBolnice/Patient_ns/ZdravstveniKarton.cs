// File:    ZdravstveniKarton.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:40:57 PM
// Purpose: Definition of Class ZdravstveniKarton

using InformacioniSistemBolnice;
using Newtonsoft.Json;
using System;

public class ZdravstveniKarton
{
    public String brojZdravstvenogKartona { get; set; }
   [Newtonsoft.Json.JsonIgnore]
    public Pacijent pacijent { get; set; }
    private System.Collections.Generic.List<Ingredient> alergen;

    public System.Collections.Generic.List<Ingredient> Alergen
    {
        get
        {
            if (alergen == null)
                alergen = new System.Collections.Generic.List<Ingredient>();
            return alergen;
        }
        set
        {
            RemoveAllAlergen();
            if (value != null)
            {
                foreach (Ingredient oAlergen in value)
                    AddAllergen(oAlergen);
            }
        }
    }


    public void AddAllergen(Ingredient newAlergen)
    {
        if (newAlergen == null)
            return;
        if (this.alergen == null)
            this.alergen = new System.Collections.Generic.List<Ingredient>();
        if (!this.alergen.Contains(newAlergen))
            this.alergen.Add(newAlergen);
    }


    public void RemoveAlergen(Ingredient oldAlergen)
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

    public ZdravstveniKarton()
    {
    }

    [JsonIgnore]
    public System.Collections.Generic.List<Therapy> terapija;

    public System.Collections.Generic.List<Therapy> Terapija
    {
        get
        {
            if (terapija == null)
                terapija = new System.Collections.Generic.List<Therapy>();
            return terapija;
        }
        set
        {
            RemoveAllTerapija();
            if (value != null)
            {
                foreach (Therapy oTerapija in value)
                    AddTerapija(oTerapija);
            }
        }
    }

    public void AddTerapija(Therapy newTherapy)
    {
        if (newTherapy == null)
            return;
        if (this.terapija == null)
            this.terapija = new System.Collections.Generic.List<Therapy>();
        if (!this.terapija.Contains(newTherapy))
            this.terapija.Add(newTherapy);
    }

    public void RemoveTerapija(Therapy oldTherapy)
    {
        if (oldTherapy == null)
            return;
        if (this.terapija != null)
            if (this.terapija.Contains(oldTherapy))
                this.terapija.Remove(oldTherapy);
    }

    public void RemoveAllTerapija()
    {
        if (terapija != null)
            terapija.Clear();
    }
    public System.Collections.Generic.List<Prescription> recept;

    public System.Collections.Generic.List<Prescription> Recept
    {
        get
        {
            if (recept == null)
                recept = new System.Collections.Generic.List<Prescription>();
            return recept;
        }
        set
        {
            RemoveAllRecept();
            if (value != null)
            {
                foreach (Prescription oRecept in value)
                    AddRecept(oRecept);
            }
        }
    }

    public void AddRecept(Prescription newPrescription)
    {
        if (newPrescription == null)
            return;
        if (this.recept == null)
            this.recept = new System.Collections.Generic.List<Prescription>();
        if (!this.recept.Contains(newPrescription))
            this.recept.Add(newPrescription);
    }

    public void RemoveRecept(Prescription oldPrescription)
    {
        if (oldPrescription == null)
            return;
        if (this.recept != null)
            if (this.recept.Contains(oldPrescription))
                this.recept.Remove(oldPrescription);
    }

    public void RemoveAllRecept()
    {
        if (recept != null)
            recept.Clear();
    }
}