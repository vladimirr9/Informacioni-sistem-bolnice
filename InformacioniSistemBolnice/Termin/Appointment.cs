// File:    Appointment.cs
// Author:  User
// Created: Monday, March 22, 2021 6:42:32 PM
// Purpose: Definition of Class Appointment

using System;
using System.Collections.Generic;
using InformacioniSistemBolnice.Secretary_ns;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class Appointment
{
    public int AppointmentID { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DurationInMinutes { get; set; }
    public AppointmentType Type { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public AppointmentStatus AppointmentStatus { get; set; }

    [JsonIgnore]
    private Patient _patient;
    [JsonIgnore]
    private Room _room;

    public int RoomID;
    public string DoctorUsername;
    public string PatientUsername;

    [JsonIgnore]
    public Patient Patient
    {
        get
        {
            PatientFileRepository patientFileRepository = new PatientFileRepository();
            return patientFileRepository.GetOne(PatientUsername);
        }
        set
        {
            if (this._patient == null || !this._patient.Equals(value))
            {
                if (this._patient != null)
                {
                    Patient oldPatient = this._patient;
                    this._patient = null;
                }
                if (value != null)
                {
                    this._patient = value;
                    PatientUsername = this._patient.Username;
                }
            }
        }
    }
    [JsonIgnore]
    private Doctor _doctor;

    public DateTime AppointmentEnd { 
    get
        {
            return AppointmentDate.AddMinutes(DurationInMinutes);
        } 
    }


    public Appointment(int appointmentId, DateTime appointmentDate, int durationInMinutes, AppointmentType type, AppointmentStatus appointmentStatus, Patient patient, Doctor doctor, Room room)
    {
        this.AppointmentID = appointmentId;
        this.AppointmentDate = appointmentDate;
        this.DurationInMinutes = durationInMinutes;
        this.Type = type;
        this.AppointmentStatus = appointmentStatus;
        Patient = patient;
        Doctor = doctor;
        Room = room;
        RoomID = room.RoomId;
        DoctorUsername = doctor.Username;
        PatientUsername = patient.Username;
    }

    [JsonConstructor]
    public Appointment(int appointmentId, DateTime appointmentDate, int durationInMinutes, AppointmentType type, AppointmentStatus appointmentStatus,  int roomId, string doctorUsername, string patientUsername)
    {
        this.AppointmentID = appointmentId;
        this.AppointmentDate = appointmentDate;
        this.DurationInMinutes = durationInMinutes;
        this.Type = type;
        this.AppointmentStatus = appointmentStatus;
        this.RoomID = roomId;
        this.DoctorUsername = doctorUsername;
        this.PatientUsername = patientUsername;
    }



    [JsonIgnore]
    public Doctor Doctor
    {
        get
        {
            DoctorFileRepository doctorFileRepository = new DoctorFileRepository();
            return doctorFileRepository.GetOne(DoctorUsername);
        }
        set
        {
            if (this._doctor == null || !this._doctor.Equals(value))
            {
                if (this._doctor != null)
                {
                    Doctor oldDoctor = this._doctor;
                    this._doctor = null;
                }
                if (value != null)
                {
                    this._doctor = value;
                    DoctorUsername = this._doctor.Username;
                }
            }
        }
    }
    [JsonIgnore]
    public Room Room
    {
        get
        {
            return RoomFileRepository.GetOne(RoomID);
        }
        set
        {
            if (this._room == null || !this._room.Equals(value))
            {
                if (this._room != null)
                {
                    Room oldRoom = this._room;
                    this._room = null;
                }
                if (value != null)
                {
                    this._room = value;
                    RoomID = this._room.RoomId;
                }
            }
        }
    }
    [JsonIgnore]
    public int PostponementDuration { get; set; }
    public static int SortByPostponementDurationAscending(Appointment x, Appointment y)
    {

        return x.PostponementDuration.CompareTo(y.PostponementDuration);
    }


    public bool OccursOn(DateTime date)
    {
        return AppointmentDate.Date.Equals(date.Date);
    }
    public bool InvolvesEither(Patient patient, Doctor doctor)
    {
        if (patient == null && doctor == null)
            return false;
        else if (patient == null)
            return (Doctor.Equals(doctor));
        else if (doctor == null)
            return (Patient.Equals(patient));
        else
            return (Doctor.Equals(doctor)) || (Patient.Equals(patient));
    }
    public bool AreAllEntitiesAvailable(List<Appointment> appointmentsToCheck = null)
    {

        bool retVal = true;
        List<Appointment> termini;
        if (appointmentsToCheck == null)
            termini = AppointmentFileRepository.GetAll();
        else
            termini = appointmentsToCheck;
        foreach (Appointment termin in termini)
        {
            if (termin.AppointmentStatus != AppointmentStatus.scheduled)
                continue;
            if (termin.Patient.Equals(this.Patient))
            {
                if (this.AppointmentDate >= termin.AppointmentDate && this.AppointmentDate <= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (this.AppointmentEnd >= termin.AppointmentDate && this.AppointmentEnd <= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (this.AppointmentDate <= termin.AppointmentDate && this.AppointmentEnd >= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
            }
            if (termin.Doctor.Equals(this.Doctor))
            {
                if (this.AppointmentDate >= termin.AppointmentDate && this.AppointmentDate <= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (this.AppointmentEnd >= termin.AppointmentDate && this.AppointmentEnd <= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (this.AppointmentDate <= termin.AppointmentDate && this.AppointmentEnd >= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
            }
            if (termin.Room.Equals(this.Room))
            {
                if (this.AppointmentDate >= termin.AppointmentDate && this.AppointmentDate <= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (this.AppointmentEnd >= termin.AppointmentDate && this.AppointmentEnd <= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (this.AppointmentDate <= termin.AppointmentDate && this.AppointmentEnd >= termin.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
            }
            
        }
        return retVal;
    }
}