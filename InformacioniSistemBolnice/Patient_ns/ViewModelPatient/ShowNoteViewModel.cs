using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice.Patient_ns.ViewModelPatient
{
    class ShowNoteViewModel : BindableBase
    {
        private PatientMedicalRecordPage karton;
        private Anamnesis _selectedAnamnesis;
        private AnamnesisController _anamnesisController = new AnamnesisController();
        private Patient _loggedInPatient;
        private ObservableCollection<string> _notesListView;
        private List<string> notes = new List<string>();

        public ObservableCollection<string> NotesListView
        {
            get { return _notesListView; }
            set
            {
                _notesListView = value;
                OnPropertyChanged("showNotesListView");
            }
        }

        public ShowNoteViewModel(PatientMedicalRecordPage pkp, Anamnesis selected)
        {
            _selectedAnamnesis = selected;
            _loggedInPatient = pkp.Patient;
            karton = pkp;
            UpdateListView();
            _notesListView = new ObservableCollection<string>(notes);
        }

        private void UpdateListView()
        {
            foreach (Note n in _anamnesisController.NotesForAnamnesis(_selectedAnamnesis, _loggedInPatient))
            {
                notes.Add(n.DescriptionOfNote);
            }
        }
    }
}
