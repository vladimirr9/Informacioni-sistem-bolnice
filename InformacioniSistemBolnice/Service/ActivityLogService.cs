using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.Patient_ns;

namespace InformacioniSistemBolnice.Service
{
    public class ActivityLogService
    {
        private IActivityLogRepository _activityLogFileRepository = new ActivityLogFileRepository();
        public  int NumberOfActivity(string username, TypeOfActivity type)
        {
            int counterOfActivity = 0;
            List<ActivityLog> activities = _activityLogFileRepository.GetAll();
            foreach (ActivityLog activity in activities)
            {
                if (activity.UsernameOfPatient.Equals(username) && activity.Type.Equals(type))
                {
                    if (activity.DateOfActivity > DateTime.Now.AddDays(-10))

                    {
                        ++counterOfActivity;
                    }
                }

            }

            return counterOfActivity;

        }

        public void AddActivity(ActivityLog newActivity)
        {
            _activityLogFileRepository.Add(newActivity);
        }
    }
}
