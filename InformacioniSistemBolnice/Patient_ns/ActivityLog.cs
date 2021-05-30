using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice
{
    public class ActivityLog
    {

        public DateTime DateOfActivity { get; set; }
        public string UsernameOfPatient { get; set; }
        public TypeOfActivity Type { get; set; }
        

        

        public ActivityLog()
        {
        }

        public ActivityLog(DateTime dateOfActivity, string usernameOfPatient, TypeOfActivity type)
        {
            DateOfActivity = dateOfActivity;
            UsernameOfPatient = usernameOfPatient;
            Type = type;
        }
    }
}
