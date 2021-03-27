// File:    Pacijent.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:32:17 PM
// Purpose: Definition of Class Pacijent

using System;

public class Pacijent : Korisnik
{
   private Boolean isGuest;
   private int iDPacijenta;
   private String brojZdravstveneKartice;
   
   public System.Collections.Generic.List<Termin> termin;
   
   public System.Collections.Generic.List<Termin> Termin
   {
      get
      {
         if (termin == null)
            termin = new System.Collections.Generic.List<Termin>();
         return termin;
      }
      set
      {
         RemoveAllTermin();
         if (value != null)
         {
            foreach (Termin oTermin in value)
               AddTermin(oTermin);
         }
      }
   }
   
   
   public void AddTermin(Termin newTermin)
   {
      if (newTermin == null)
         return;
      if (this.termin == null)
         this.termin = new System.Collections.Generic.List<Termin>();
      if (!this.termin.Contains(newTermin))
      {
         this.termin.Add(newTermin);
         newTermin.Pacijent = this;
      }
   }
   
   
   public void RemoveTermin(Termin oldTermin)
   {
      if (oldTermin == null)
         return;
      if (this.termin != null)
         if (this.termin.Contains(oldTermin))
         {
            this.termin.Remove(oldTermin);
            oldTermin.Pacijent = null;
         }
   }
   
   
   public void RemoveAllTermin()
   {
      if (termin != null)
      {
         System.Collections.ArrayList tmpTermin = new System.Collections.ArrayList();
         foreach (Termin oldTermin in termin)
            tmpTermin.Add(oldTermin);
         termin.Clear();
         foreach (Termin oldTermin in tmpTermin)
            oldTermin.Pacijent = null;
         tmpTermin.Clear();
      }
   }
   public ZdravstveniKarton zdravstveniKarton;

}