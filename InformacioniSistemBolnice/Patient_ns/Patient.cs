// File:    Patient.cs
// Author:  User
// Created: Monday, March 22, 2021 6:32:17 PM
// Purpose: Definition of Class Patient

using System;
using System.Collections.Generic;

public class Patient : User
{
    public Boolean IsGuest { get; set; }
    public String SocialSecurityNumber { get; set; }

    public Boolean Banned { get; set; } = false;

    public DateTime TimeOfBan { get; set; }

    public MedicalRecord MedicalRecord { get; set; }


    public Patient(
      string name,
      string surname,
      string jmbg,
      char gender,
      string phoneNumber,
      string email,
      DateTime dateOfBirth,
      string username,
      string password,
      ResidentialAddress residentialAddress, bool isGuest, string socialSecurityNumber, MedicalRecord medicalRecord,
      bool isDeleted = false) : base(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, username, password, residentialAddress, isDeleted)
    {
        this.IsGuest = isGuest;
        this.SocialSecurityNumber = socialSecurityNumber;
        this.MedicalRecord = medicalRecord;
    }


    public bool IsAvailable(DateTime start, DateTime end) // proverava da li je PatientComboBox slobodan izmedju neka dva trenutka u vremenu
    {
        if (start.Equals(end))
            return true;
        bool retVal = true;

        AppointmentFileRepository appointmentFileRepository = new AppointmentFileRepository();
        List<Appointment> appointments = appointmentFileRepository.GetAll();
        foreach (Appointment appointment in appointments)
        {
            if (appointment.Patient.Equals(this) && appointment.AppointmentStatus == AppointmentStatus.scheduled)
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

    public override bool Equals(object obj)
    {
        return obj is Patient patient &&
               base.Equals(obj);
    }
}