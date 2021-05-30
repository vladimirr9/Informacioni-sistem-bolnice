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
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for AddingNotesPage.xaml
    /// </summary>
    public partial class AddingNotesPage : Page
    {
        private static PatientMedicalRecordPage _medicalRecord;
        private static StartPatientWindow _pparent;
        private AnamnesisController _anamnesisController = new AnamnesisController();
        private Anamnesis _selectedAnamnesis;
        private List<String> _elementsInComboBox;
        private Boolean _isPressedReminderButton;
        private Note _newNote;
        public AddingNotesPage(StartPatientWindow pp, PatientMedicalRecordPage pkp, Anamnesis selected)
        {
            _isPressedReminderButton = false;
            _newNote = new Note();
            _selectedAnamnesis = selected;
            _medicalRecord = pkp;
            _pparent = pp;
            InitializeComponent();
            _elementsInComboBox = new List<String>();
            BlackOutDates();
            endDatePicker.IsEnabled = false;
            endMomentComboBox.IsEnabled = false;
            UpdateVisibilityOfComponents();
            FillStartMomentComboBox();
            addNote.IsEnabled = false;
        }

        private void UpdateVisibilityOfComponents()
        {
            startDatePicker.Visibility = Visibility.Hidden;
            endDatePicker.Visibility = Visibility.Hidden;
            startDateTextBlock.Visibility = Visibility.Hidden;
            endDateTextBlock.Visibility = Visibility.Hidden;
            startMomentComboBox.Visibility = Visibility.Hidden;
            startMomentTextBlock.Visibility = Visibility.Hidden;
            endMomentComboBox.Visibility = Visibility.Hidden;
            endMomentTextBlock.Visibility = Visibility.Hidden;
        }

        private void FillStartMomentComboBox()
        {
            DateTime today = DateTime.Today;
            for (DateTime tm = today.AddHours(0); tm < today.AddHours(24); tm = tm.AddHours(1))
            {
                _elementsInComboBox.Add(tm.ToString("HH:mm"));

            }
            startMomentComboBox.ItemsSource = _elementsInComboBox;
        }

        private void FillEndMomentComboBox()
        {
            _elementsInComboBox.Clear();
            DateTime today = DateTime.Today;
            String datum = today.ToShortDateString();
            DateTime pocetak = DateTime.Parse(datum + " " + startMomentComboBox.SelectedItem.ToString());
            for (DateTime tm = pocetak; tm < today.AddHours(24); tm = tm.AddHours(1))
            {
                _elementsInComboBox.Add(tm.ToString("HH:mm"));

            }
            endMomentComboBox.ItemsSource = _elementsInComboBox;

        }

        private void BlackOutDates()
        {
            CalendarDateRange calendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            startDatePicker.BlackoutDates.Add(calendar);
            endDatePicker.BlackoutDates.Add(calendar);


        }

        private void StartDatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CalendarDateRange calendar = new CalendarDateRange(DateTime.MinValue, (DateTime)startDatePicker.SelectedDate);
            endDatePicker.IsEnabled = true;
            endDatePicker.BlackoutDates.Add(calendar);
            SetEnabledButtonSubmit();
        }

        private void startMomentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillEndMomentComboBox();
            endMomentComboBox.IsEnabled = true;
            SetEnabledButtonSubmit();
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            _medicalRecord.borderWindow.Content = new PatientExamineAnamnesesPage(_pparent, _medicalRecord);
        }

        private void addNote_Click(object sender, RoutedEventArgs e)
        {
            String textOfNote = textBox.Text;
            if (_isPressedReminderButton == false)
            {
                AddingNoteWithoutReminder(textOfNote);
            }
            else
            {
                AddingNoteWithReminder(textOfNote);
            }
        }

        private void AddingNoteWithoutReminder(String text)
        {
            _newNote.DescriptionOfNote = text;
            _newNote.StartDate = DateTime.MinValue;
            _newNote.EndDate = DateTime.MinValue;
            _newNote.StartPeriodOfTime = DateTime.MinValue;
            _newNote.EndPeriodOfTime = DateTime.MinValue;
            if (_selectedAnamnesis.NotesForAnamnesis != null)
            {
                _selectedAnamnesis.NotesForAnamnesis.Add(_newNote);
            }
            else
            {
                List<Note> notes = new List<Note>();
                notes.Add(_newNote);
                _selectedAnamnesis.NotesForAnamnesis = notes;
            }


            _anamnesisController.UpdateAnamnesis(_selectedAnamnesis.IdOfAnamnesis, _selectedAnamnesis);
            _medicalRecord.borderWindow.Content = new PatientExamineAnamnesesPage(_pparent, _medicalRecord);
        }

        private void AddingNoteWithReminder(String text)
        {
            _newNote.DescriptionOfNote = text;
            _newNote.StartDate = (DateTime)startDatePicker.SelectedDate;
            _newNote.EndDate = (DateTime)endDatePicker.SelectedDate;
            _newNote.IsSetReminder = true;
            String t = startMomentComboBox.SelectedItem.ToString();
            String d = startDatePicker.Text;
            var dt = DateTime.Parse(d + " " + t);
            _newNote.StartPeriodOfTime = dt;
            String t1 = endMomentComboBox.SelectedItem.ToString();
            String d1 = startDatePicker.Text;
            DateTime dt1 = DateTime.Parse(d1 + " " + t1);
            _newNote.EndPeriodOfTime = dt1;
            if (_selectedAnamnesis.NotesForAnamnesis != null)
            {
                _selectedAnamnesis.NotesForAnamnesis.Add(_newNote);
            }
            else
            {
                List<Note> notes = new List<Note>();
                notes.Add(_newNote);
                _selectedAnamnesis.NotesForAnamnesis = notes;
            }


            _anamnesisController.UpdateAnamnesis(_selectedAnamnesis.IdOfAnamnesis, _selectedAnamnesis);
            _medicalRecord.borderWindow.Content = new PatientExamineAnamnesesPage(_pparent, _medicalRecord);
        }

        private void reminderButton_Click(object sender, RoutedEventArgs e)
        {
            _isPressedReminderButton = true;
            addNote.Visibility = Visibility.Hidden;
            startDatePicker.Visibility = Visibility.Visible;
            endDatePicker.Visibility = Visibility.Visible;
            startDateTextBlock.Visibility = Visibility.Visible;
            endDateTextBlock.Visibility = Visibility.Visible;
            startMomentComboBox.Visibility = Visibility.Visible;
            startMomentTextBlock.Visibility = Visibility.Visible;
            endMomentComboBox.Visibility = Visibility.Visible;
            endMomentTextBlock.Visibility = Visibility.Visible;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetEnabledButtonSubmit();
        }

        private void SetEnabledButtonSubmit()
        {
            if (_isPressedReminderButton == true)
            {
                if (startDatePicker.SelectedDate != null && endDatePicker.SelectedDate != null &&
                    startMomentComboBox.SelectedItem != null && endMomentComboBox.SelectedItem != null &&
                    textBox.Text != null)
                {
                    addNote.Visibility = Visibility.Visible;
                    addNote.IsEnabled = true;
                }
                else
                {
                    addNote.IsEnabled = false;
                }
            }
            else
            {
                if (textBox.Text != null)
                {
                    addNote.IsEnabled = true;
                }
                else
                {
                    addNote.IsEnabled = false;
                }
            }

        }

        private void endMomentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetEnabledButtonSubmit();
        }

        private void EndDatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetEnabledButtonSubmit();
        }
    }
}
