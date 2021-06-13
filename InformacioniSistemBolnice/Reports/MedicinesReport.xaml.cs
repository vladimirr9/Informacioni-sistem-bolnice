using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Reports
{
    /// <summary>
    /// Interaction logic for MedicinesReport.xaml
    /// </summary>
    public partial class MedicinesReport : Page
    {
        private MedicineController _medicineController = new MedicineController();
        /*private MedicinesCountReport _validatedMedicinesCount = new ValidatedMedicinesCount();
        private MedicinesCountReport _rejectedMedicinesCount = new RejectedMedicinesCount();
        private MedicinesCountReport _waitingMedicinesCount = new WaitingMedicinesCount();*/
        public MedicinesReport()
        {
            InitializeComponent();
            UpdateTable();
            this.DataContext = this;

            label1.Content = new ValidatedMedicinesCount().Calculate();
            label2.Content = new RejectedMedicinesCount().Calculate();
            label3.Content = new WaitingMedicinesCount().Calculate();
            label4.Content = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        public void UpdateTable()
        {
            dataGridMedicines.Items.Clear();
            foreach (Medicine med in _medicineController.GetAllMedicines())
            {
                if (!med.IsDeleted)
                    dataGridMedicines.Items.Add(med);
            }
        }
    }
}
