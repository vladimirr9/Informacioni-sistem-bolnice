using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Upravnik;
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
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Manager_ns
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        private UpravnikWindow _parent;
        private DoctorControler _doctorController = new DoctorControler();
        public EmployeesWindow(UpravnikWindow parent)
        {
            InitializeComponent();
            this._parent = parent;
            this.DataContext = this;
            UpdateTable();
        }
        public void UpdateTable()
        {
            dataGridZaposleni.Items.Clear();
            foreach (Doctor doc in _doctorController.GetAll())
            {
                if (!doc.IsDeleted)
                    dataGridZaposleni.Items.Add(doc);
            }
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
