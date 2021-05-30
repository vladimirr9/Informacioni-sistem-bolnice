using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    class AllergyCheck
    {
        internal bool IsAllergic(Medicine medicine, Patient patient)
        {
            foreach (Ingredient ingredient in medicine.IngredientsList)
            {
                if (patient.MedicalRecord.Allergens.Contains(ingredient))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
