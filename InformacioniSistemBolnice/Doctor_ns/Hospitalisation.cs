using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public class Hospitalisation
    {
        public int HospitalisationId { get; set; }
        public String PatientUsername { get; set; }
        public int RoomId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Bed { get; set; }
        public Boolean IsDeleted { get; set; }

        public Hospitalisation(int hospitalisationId,String patientUsername, int roomId, DateTime beginDate, DateTime endDate, int bed)
        {
            this.HospitalisationId = hospitalisationId;
            this.PatientUsername = patientUsername;
            this.RoomId = roomId;
            this.BeginDate = beginDate;
            this.EndDate = endDate;
            this.Bed = bed;
            this.IsDeleted = false;
        }
    }
}
