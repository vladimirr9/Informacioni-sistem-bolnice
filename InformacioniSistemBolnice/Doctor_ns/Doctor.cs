// File:    Lekar.cs
// Author:  User
// Created: Monday, March 22, 2021 6:32:18 PM
// Purpose: Definition of Class Lekar

using InformacioniSistemBolnice.Doctor_ns;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

public class Doctor : User
{
    public DoctorType doctorType { get; set; }
    public WorkShift Shift { get; set; }
    public List<Vacation> Vacations;
    public int DaysOfVacation { get; set;}
    //private int iDLekara;

    public Doctor(string name, string surname, string jmbg, char gender, string phoneNumber, string email, DateTime birthday, string username, string password, ResidentialAddress address, DoctorType doctorType, WorkShift shift, bool isDeleted = false) : base(name, surname, jmbg, gender, phoneNumber, email, birthday, username, password, address, isDeleted)
    {
        this.doctorType = doctorType;
        Shift = shift;
        Vacations = new List<Vacation>();
        DaysOfVacation = 25;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public bool IsAvailable(DateTime start, DateTime end) // proverava da li je DoctorComboBox slobodan izmedju neka dva trenutka u vremenu
    {

        if (start.Equals(end))
            return true;
        if (Shift == WorkShift.firstShift)
        {
            if (start >= start.Date.AddHours(14))
                return false;
        }
        else if (Shift == WorkShift.secondShift)
        {
            if (start <= start.Date.AddHours(14))
                return false;
        }
        bool retVal = true;
        List<Appointment> appointments = AppointmentFileRepository.GetAll();
        foreach (Appointment appointment in appointments)
        {
            if (appointment.Doctor.Equals(this) && appointment.AppointmentStatus == AppointmentStatus.scheduled)
            {
                if (start >= appointment.AppointmentDate && start <= appointment.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (end >= appointment.AppointmentDate && end <= appointment.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (start <= appointment.AppointmentDate && end >= appointment.AppointmentEnd)
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
            return DoctorType.generalPractitioner;
        else if (type.Equals("Kardiolog"))
            return DoctorType.cardiologist;
        else if (type.Equals("Hirurg"))
            return DoctorType.surgeon;
        else if (type.Equals("Pedijatar"))
            return DoctorType.pediatrician;
        else if (type.Equals("Opšte Prakse"))
            return DoctorType.neurologist;
        else return 0;
    }

}