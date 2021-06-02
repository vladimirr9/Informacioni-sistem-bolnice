using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformacioniSistemBolnice.Commands
{
    public static class RoutedCommands
    {
        
        public static RoutedUICommand StartingPage = new RoutedUICommand(
            "Starting Page",
            "StartingPage",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F1, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Patients = new RoutedUICommand(
            "Patients",
            "Patients",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F2, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Doctors = new RoutedUICommand(
            "Doctors",
            "Doctors",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F3, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Appointments = new RoutedUICommand(
            "Appointments",
            "Appointments",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F4, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Reports = new RoutedUICommand(
            "Reports",
            "Reports",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F5, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Account = new RoutedUICommand(
            "Account",
            "Account",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F6, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Help = new RoutedUICommand(
            "Help",
            "Help",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F9, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Back = new RoutedUICommand(
            "Back",
            "Back",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
            }
            );
        public static RoutedUICommand Next = new RoutedUICommand(
            "Next",
            "Next",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
            }
            );
        public static RoutedUICommand Cancel = new RoutedUICommand(
            "Cancel",
            "Cancel",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
            }
            );
        public static RoutedUICommand Finish = new RoutedUICommand(
            "Finish",
            "Finish",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
            }
            );


    }
}
