// File:    ZdravstveniKarton.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:40:57 PM
// Purpose: Definition of Class ZdravstveniKarton

using System;

public class ZdravstveniKarton
{
    private String brojZdravstvenogKartona;

    public System.Collections.Generic.List<Alergen> alergen;

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
    public System.Collections.Generic.List<Terapija> terapija;

    public System.Collections.Generic.List<Terapija> Terapija
    {
        get
        {
            if (terapija == null)
                terapija = new System.Collections.Generic.List<Terapija>();
            return terapija;
        }
        set
        {
            RemoveAllTerapija();
            if (value != null)
            {
                foreach (Terapija oTerapija in value)
                    AddTerapija(oTerapija);
            }
        }
    }

    public void AddTerapija(Terapija newTerapija)
    {
        if (newTerapija == null)
            return;
        if (this.terapija == null)
            this.terapija = new System.Collections.Generic.List<Terapija>();
        if (!this.terapija.Contains(newTerapija))
            this.terapija.Add(newTerapija);
    }

    public void RemoveTerapija(Terapija oldTerapija)
    {
        if (oldTerapija == null)
            return;
        if (this.terapija != null)
            if (this.terapija.Contains(oldTerapija))
                this.terapija.Remove(oldTerapija);
    }

    public void RemoveAllTerapija()
    {
        if (terapija != null)
            terapija.Clear();
    }
    public System.Collections.Generic.List<Recept> recept;

    public System.Collections.Generic.List<Recept> Recept
    {
        get
        {
            if (recept == null)
                recept = new System.Collections.Generic.List<Recept>();
            return recept;
        }
        set
        {
            RemoveAllRecept();
            if (value != null)
            {
                foreach (Recept oRecept in value)
                    AddRecept(oRecept);
            }
        }
    }

    public void AddRecept(Recept newRecept)
    {
        if (newRecept == null)
            return;
        if (this.recept == null)
            this.recept = new System.Collections.Generic.List<Recept>();
        if (!this.recept.Contains(newRecept))
            this.recept.Add(newRecept);
    }

    public void RemoveRecept(Recept oldRecept)
    {
        if (oldRecept == null)
            return;
        if (this.recept != null)
            if (this.recept.Contains(oldRecept))
                this.recept.Remove(oldRecept);
    }

    public void RemoveAllRecept()
    {
        if (recept != null)
            recept.Clear();
    }

}