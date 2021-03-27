// File:    Termin.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:42:32 PM
// Purpose: Definition of Class Termin

using System;

public class Termin
{
   private int iDTermina;
   private DateTime datumZakazivanja;
   private int trajanjeUMinutima;
   private TipTermina tipTermina;
   private StatusTermina status = StatusTermina.zakazan;
   
   public Pacijent pacijent;
   
   public Pacijent Pacijent
   {
      get
      {
         return pacijent;
      }
      set
      {
         if (this.pacijent == null || !this.pacijent.Equals(value))
         {
            if (this.pacijent != null)
            {
               Pacijent oldPacijent = this.pacijent;
               this.pacijent = null;
               oldPacijent.RemoveTermin(this);
            }
            if (value != null)
            {
               this.pacijent = value;
               this.pacijent.AddTermin(this);
            }
         }
      }
   }
   public Lekar lekar;
   
   public Lekar Lekar
   {
      get
      {
         return lekar;
      }
      set
      {
         this.lekar = value;
      }
   }

}