using System;
using InformacioniSistemBolnice;
using System;
using System.Collections.Generic;

public class Lek

{
    private String sifra;
    private String naziv;
    private bool isDeleted = false;
    private StatusLeka statusLeka;
    private List<Ingredient> listaSastojaka;

    #region Properties
    public String Sifra
    {
        get { return sifra; }
        set { sifra = value; }
    }

    public String Naziv
    {
        get { return naziv; }
        set { naziv = value; }
    }

    public bool IsDeleted
    {
        get { return isDeleted; }
        set { isDeleted = value; }
    }

    public StatusLeka StatusLeka
    {
        get { return statusLeka; }
        set { statusLeka = value; }
    }

    public List<Ingredient> ListaSastojaka
    {
        get { return listaSastojaka; }
        set { listaSastojaka = value; }
    }
    #endregion

    public Lek() { }

    public Lek(String sifra, String naziv, bool isDeleted, StatusLeka statusLeka, List<Ingredient> listaSastojaka)
    {
        Sifra = sifra;
        Naziv = naziv;
        IsDeleted = isDeleted;
        StatusLeka = statusLeka;
        ListaSastojaka = listaSastojaka;
    }
}
