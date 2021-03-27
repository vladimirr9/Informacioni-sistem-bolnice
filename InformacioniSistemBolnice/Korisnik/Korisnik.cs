// File:    Korisnik.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 7:10:30 PM
// Purpose: Definition of Class Korisnik

using System;

public class Korisnik
{
   private String ime;
   private String prezime;
   private String jmbg;
   private Char pol;
   private String brojTelefona;
   private String email;
   private DateTime datumRodenja;
   public String korisnickoIme { get; set; }
   public String lozinka { get; set; }
   private Boolean isDeleted = false;
   
   public AdresaStanovanja adresaStanovanja;

}