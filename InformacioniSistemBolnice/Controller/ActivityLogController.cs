﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Patient_ns;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class ActivityLogController
    {
        private ActivityLogService _activityLogService = new ActivityLogService();

        public int CheckActivity(string username, TypeOfActivity type)
        {
            return _activityLogService.NumberOfActivity(username, type);
        }

        public void AddActivity(ActivityLog newActivity)
        {
            _activityLogService.AddActivity(newActivity);
        }


    }
}
