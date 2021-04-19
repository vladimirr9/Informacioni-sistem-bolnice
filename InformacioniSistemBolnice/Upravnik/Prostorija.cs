// File:    Prostorija.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:44:09 PM
// Purpose: Definition of Class Prostorija

using System;
using System.Collections.Generic;

public class Prostorija
{
   public String naziv { get; set; }
   private int iDprostorije;
   private TipProstorije tipProstorije;
   private Boolean isDeleted = false;
   private Boolean isActive;
   private Double kvadratura;
   private int brSprata;
   private int brSobe;
   private List<Oprema> opremaLista;

    #region Properties
    public String Naziv
    {
        get { return naziv; }
        set { naziv = value; }
    }
    public int IDprostorije
    {
        get { return iDprostorije; }
        set { iDprostorije = value; }
    }
    public TipProstorije TipProstorije
    {
        get { return tipProstorije; }
        set { tipProstorije = value; }
    }
    public Boolean IsDeleted
    {
        get { return isDeleted; }
        set { isDeleted = value; }
    }
    public Boolean IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
    public Double Kvadratura
    {
        get { return kvadratura; }
        set { kvadratura = value; }
    }
    public int BrSprata
    {
        get { return brSprata; }
        set { brSprata = value; }
    }
    public int BrSobe
    {
        get { return brSobe; }
        set { brSobe = value; }
    }

    public List<Oprema> OpremaLista
    {
        get { return opremaLista; }
        set { opremaLista = value; }
    }
    #endregion

    public Oprema GetOne(string SifraOpreme)
    {
        foreach (Oprema o in opremaLista)
        {
            if (o.Sifra.Equals(SifraOpreme))
            {
                return opremaLista[opremaLista.IndexOf(o)];
            }
        }
        return null;
    }

    public Prostorija() { }

    public Prostorija(String naziv, int iDprostorije, TipProstorije tipProstorije, Boolean isDeleted, Boolean isActive, Double kvadratura, int brSprata, int brSobe, List<Oprema> opremaLista)
    {
        Naziv = naziv;
        IDprostorije = iDprostorije;
        TipProstorije = tipProstorije;
        this.isDeleted = false;
        IsActive = isActive;
        Kvadratura = kvadratura;
        BrSprata = brSprata;
        BrSobe = brSobe;
        OpremaLista = opremaLista;
    }

    /*public Prostorija(String naziv, int iDprostorije, TipProstorije tipProstorije, Boolean isDeleted, Boolean isActive, Double kvadratura, int brSprata, int brSobe)
    {
        Naziv = naziv;
        IDprostorije = iDprostorije;
        TipProstorije = tipProstorije;
        this.isDeleted = false;
        IsActive = isActive;
        Kvadratura = kvadratura;
        BrSprata = brSprata;
        BrSobe = brSobe;
    }*/
}