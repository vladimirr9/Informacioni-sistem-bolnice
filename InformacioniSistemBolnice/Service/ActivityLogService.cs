using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class ActivityLogService
    {
        public  int NumberOfActivity(string username, TypeOfActivity type)
        {
            int counterOfActivity = 0;
            List<ActivityLog> informacije = ActivityLogFileRepository.GetAll();
            foreach (ActivityLog i in informacije)
            {
                if (i.UsernameOfPatient.Equals(username) && i.Type.Equals(type))
                {
                    if (i.DateOfActivity > DateTime.Now.AddDays(-10))

                    {
                        ++counterOfActivity;
                    }
                }

            }

            return counterOfActivity;

        }
    }
}
