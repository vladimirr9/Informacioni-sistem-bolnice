// File:    Lekar.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 6:32:18 PM
// Purpose: Definition of Class Lekar

using System;
using System.Collections.Generic;
using System.Windows.Documents;

public class Doctor : Korisnik
{
    public DoctorType doctorType { get; set; }
    //private int iDLekara;

    public Doctor(string name, string surname, string jmbg, char gender, string phoneNumber, string email, DateTime birthday, string username, string password, AdresaStanovanja address, DoctorType doctorType, bool isDeleted = false) : base(name, surname, jmbg, gender, phoneNumber, email, birthday, username, password, address, isDeleted)
    {
        this.doctorType = doctorType;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public bool IsAvailable(DateTime begin, DateTime end) // proverava da li je DoctorComboBox slobodan izmedju neka dva trenutka u vremenu
    {
        if (begin.Equals(end))
            return true;
        bool retVal = true;
        List<Termin> appointmens = TerminFileStorage.GetAll();
        foreach (Termin appointmen in appointmens)
        {
            if (appointmen.Doctor.Equals(this) && appointmen.status == StatusTermina.zakazan)
            {
                if (begin >= appointmen.datumZakazivanja && begin <= appointmen.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (end >= appointmen.datumZakazivanja && end <= appointmen.KrajTermina)
                {
                    retVal = false;
                    break;
                }
                if (begin <= appointmen.datumZakazivanja && end >= appointmen.KrajTermina)
                {
                    retVal = false;
                    break;
                }
            }
        }
        return retVal;

    }
    public static List<string> GetDoctorTypes()
    {
        List<string> types = new List<string>();
        types.Add("Opšte Prakse");
        types.Add("Kardiolog");
        types.Add("Hirurg");
        types.Add("Pedijatar");
        types.Add("Neurolog");
        return types;
    }
    public static DoctorType DoctorTypeFromString(string type)
    {
        if (type.Equals("Opšte Prakse"))
            return DoctorType.opstePrakse;
        else if (type.Equals("Kardiolog"))
            return DoctorType.kardiolog;
        else if (type.Equals("Hirurg"))
            return DoctorType.hirurg;
        else if (type.Equals("Pedijatar"))
            return DoctorType.pedijatar;
        else if (type.Equals("Opšte Prakse"))
            return DoctorType.neurolog;
        else return 0;
    }

}