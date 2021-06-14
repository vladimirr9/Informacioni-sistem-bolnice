// File:    Lekar.cs
// Author:  User
// Created: Monday, March 22, 2021 6:32:18 PM
// Purpose: Definition of Class Lekar

using InformacioniSistemBolnice;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Doctor_ns;
using InformacioniSistemBolnice.Secretary_ns;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

public class Doctor : User
{
    public DoctorType doctorType { get; set; }
    public List<Vacation> Vacations;
    public int DaysOfVacation { get; set;}
    public WorkHours WorkHours { get; set; }
    [JsonIgnore]
    private AppointmentController _appointmentController = new AppointmentController();

    public Doctor(string name, string surname, string jmbg, char gender, string phoneNumber, string email, DateTime birthday, string username, string password, ResidentialAddress address, DoctorType doctorType, bool isDeleted = false) : base(name, surname, jmbg, gender, phoneNumber, email, birthday, username, password, address, isDeleted)
    {
        this.doctorType = doctorType;
        Vacations = new List<Vacation>();
        DaysOfVacation = 25;
        WorkHours = new WorkHours();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public bool IsWithinWorkHours(DateTime date)
    {
        if (WorkHours.AberrationExists(date))
        {
            WorkHourAberration aberration = WorkHours.GetAberrationByDate(date);
            if (date.TimeOfDay >= aberration.Start.TimeOfDay && date.TimeOfDay <= aberration.End.TimeOfDay)
                return true;
            else return false;
        }
        else
        {
            if (date.TimeOfDay >= WorkHours.Start.TimeOfDay && date.TimeOfDay <= WorkHours.End.TimeOfDay)
                return true;
            else return false;
        }
    }
    public bool IsWithinVacations(DateTime date)
    {
        foreach (Vacation vacation in Vacations)
        {
            if (vacation.IsDateWithinVacation(date))
                return true;
        }
        return false;
    }

    public bool IsAvailable(DateTime start, DateTime end) // proverava da li je DoctorComboBox slobodan izmedju neka dva trenutka u vremenu
    {

        if (start.Equals(end))
            return true;
        if (IsWithinVacations(start))
            return false;
        if (!IsWithinWorkHours(start))
            return false;

        foreach (Appointment appointment in _appointmentController.GetAll())
        {
            if (appointment.Doctor.Equals(this) && appointment.AppointmentStatus == AppointmentStatus.scheduled)
            {
                if (IsStartWithinAppointmentDuration(start, appointment))
                    return false;
                if (IsEndWithinAppointmentDuration(end, appointment))
                    return false;
                if (IsPeriodWithinApppointmentDuration(start, end, appointment))
                    return false;
            }
        }
        return true;

    }
    private bool IsStartWithinAppointmentDuration(DateTime start, Appointment appointment)
    {
        return (start >= appointment.AppointmentDate && start <= appointment.AppointmentEnd); 
    }
    private bool IsEndWithinAppointmentDuration(DateTime end, Appointment appointment)
    {
        return (end >= appointment.AppointmentDate && end <= appointment.AppointmentEnd);
    }
    private bool IsPeriodWithinApppointmentDuration(DateTime start, DateTime end, Appointment appointment)
    {
        return (start <= appointment.AppointmentDate && end >= appointment.AppointmentEnd);
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