﻿#pragma checksum "..\..\PacijentZakazuje.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FACCEEA959983260D6B661A3576AB83C941D173819359B028BC5EAA0668E457E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using InformacioniSistemBolnice;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace InformacioniSistemBolnice {
    
    
    /// <summary>
    /// PacijentZakazuje
    /// </summary>
    public partial class PacijentZakazuje : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\PacijentZakazuje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox lekar;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\PacijentZakazuje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker date;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\PacijentZakazuje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox time;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\PacijentZakazuje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button dugmePotvrdi;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\PacijentZakazuje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock vrijeme;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\PacijentZakazuje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\PacijentZakazuje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\PacijentZakazuje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/InformacioniSistemBolnice;component/pacijentzakazuje.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PacijentZakazuje.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lekar = ((System.Windows.Controls.ComboBox)(target));
            
            #line 11 "..\..\PacijentZakazuje.xaml"
            this.lekar.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lekar_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.date = ((System.Windows.Controls.DatePicker)(target));
            
            #line 28 "..\..\PacijentZakazuje.xaml"
            this.date.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.date_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.time = ((System.Windows.Controls.ComboBox)(target));
            
            #line 29 "..\..\PacijentZakazuje.xaml"
            this.time.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.time_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dugmePotvrdi = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\PacijentZakazuje.xaml"
            this.dugmePotvrdi.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 34 "..\..\PacijentZakazuje.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.vrijeme = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.button = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\PacijentZakazuje.xaml"
            this.button.Click += new System.Windows.RoutedEventHandler(this.button_Click_2);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

