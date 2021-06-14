using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class RatingsWrittenByPatientController
    {
        private RatingsWrittenByPatientService _ratingsWrittenByPatientService = new RatingsWrittenByPatientService();
        public Boolean IsCheckedCondition(Patient patient)
        {
            return _ratingsWrittenByPatientService.IsCheckedCondition(patient);
        }

        public int GetNumberOfRating(int number, Doctor doctor)
        {
            return _ratingsWrittenByPatientService.GetNumberOfRating(number, doctor);
        }
    }
}
