// File:    MedicalRecord.cs
// Author:  User
// Created: Monday, March 22, 2021 6:40:57 PM
// Purpose: Definition of Class MedicalRecord

using InformacioniSistemBolnice;
using Newtonsoft.Json;
using System;

public class MedicalRecord
{
    public String MedicalRecordNumber { get; set; }
   [Newtonsoft.Json.JsonIgnore]
    public Patient patient { get; set; }
    private System.Collections.Generic.List<Ingredient> allergens;

    public System.Collections.Generic.List<Ingredient> Allergens
    {
        get
        {
            if (allergens == null)
                allergens = new System.Collections.Generic.List<Ingredient>();
            return allergens;
        }
        set
        {
            RemoveAllAlergen();
            if (value != null)
            {
                foreach (Ingredient oAllergen in value)
                    AddAllergen(oAllergen);
            }
        }
    }


    public void AddAllergen(Ingredient newAlergen)
    {
        if (newAlergen == null)
            return;
        if (this.allergens == null)
            this.allergens = new System.Collections.Generic.List<Ingredient>();
        if (!this.allergens.Contains(newAlergen))
            this.allergens.Add(newAlergen);
    }


    public void RemoveAlergen(Ingredient oldAlergen)
    {
        if (oldAlergen == null)
            return;
        if (this.allergens != null)
            if (this.allergens.Contains(oldAlergen))
                this.allergens.Remove(oldAlergen);
    }


    public void RemoveAllAlergen()
    {
        if (allergens != null)
            allergens.Clear();
    }
    

    public MedicalRecord(string medicalRecordNumber)
    {
        this.MedicalRecordNumber = medicalRecordNumber;
        
    }

    public MedicalRecord()
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