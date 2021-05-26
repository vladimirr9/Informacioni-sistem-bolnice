using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ShowNotesPage.xaml
    /// </summary>
    public partial class ShowNotesPage : Page
    {
        private PacijentKartonPage karton;
        private Anamnesis selectedAnamnesis;
        public ShowNotesPage(PacijentKartonPage pkp,Anamnesis selected)
        {
            selectedAnamnesis = selected;
            Debug.WriteLine(selected.IdOfAppointment.ToString());
            karton = pkp;
            InitializeComponent();
            this.DataContext = this;
            UpdateListView();
        }

        private void UpdateListView()
        {
            List<Anamnesis> anamneses = AnamnesisFileRepository.GetAll();
            foreach (Anamnesis a in anamneses)
            {
                if (a.IdOfAnamnesis.Equals(selectedAnamnesis.IdOfAnamnesis))
                {
                    if (a.NotesForAnamnesis != null) 
                    
                    {
                        foreach (Note n in a.NotesForAnamnesis)
                        {
                            showNotesListView.Items.Add(n.DescriptionOfNote);
                        }
                    }
                }
            }
        }
    }
}
