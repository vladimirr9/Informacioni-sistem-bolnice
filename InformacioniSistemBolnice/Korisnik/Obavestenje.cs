using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Korisnik
{

    public class Obavestenje
    {
        public int idObavestenja { get; set; }
        public String naslov { get; set; }
        public String tekst { get; set; }
        public DateTime datumNastanka { get; set; }
        public Boolean procitano { get; set; }
        public Boolean isDeleted { get; set; }
        public List<string> Recipients { get; set; }   // ALL_USERS, EMPLOYED_USERS, PATIENT_USERS or username as values
        public Obavestenje(int idObavestenja, string naslov, string tekst, DateTime datumNastanka, bool procitano = false, bool isDeleted = false)
        {
            this.idObavestenja = idObavestenja;
            this.naslov = naslov;
            this.tekst = tekst;
            this.datumNastanka = datumNastanka;
            this.procitano = procitano;
            this.isDeleted = isDeleted;
            Recipients = new List<string>();
        }


        [JsonIgnore]
        public String Header
        {
            get
            {
                if (Recipients.Contains("ALL_USERS"))
                    return datumNastanka.ToString("dd/MM/yyyy HH:mm  - ") + naslov + " GLOBAL";
                else
                    return datumNastanka.ToString("dd/MM/yyyy HH:mm  - ") + naslov;
            }   
        }

        public bool IsDirectedTo(string username)
        {
            if (Recipients.Contains("ALL_USERS"))
                return true;
            if (PacijentFileStorage.GetOne(username) != null)
            {
                if (Recipients.Contains("PATIENT_USERS") || Recipients.Contains(username))
                    return true;
            }
            else if (SekretarFileStorage.GetOne(username) != null || LekarFileStorage.GetOne(username) != null || UpravnikFileStorage.GetOne(username) != null)
            {
                if (Recipients.Contains("EMPLOYED_USERS") || Recipients.Contains(username))
                    return true;
            }
            return false;
        }


    }
}
