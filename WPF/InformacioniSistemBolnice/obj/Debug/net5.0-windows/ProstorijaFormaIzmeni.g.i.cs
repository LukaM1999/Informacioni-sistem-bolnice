﻿#pragma checksum "..\..\..\ProstorijaFormaIzmeni.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9D45BC7E9DBC7AFA6955CA1B14B7BA310A0929DB"
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
using System.Windows.Controls.Ribbon;
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
    /// ProstorijaFormaIzmeni
    /// </summary>
    public partial class ProstorijaFormaIzmeni : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\ProstorijaFormaIzmeni.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox tipIzmena;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\ProstorijaFormaIzmeni.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb1;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\ProstorijaFormaIzmeni.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb2;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\ProstorijaFormaIzmeni.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button potvrdaIzmeneDugme;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\ProstorijaFormaIzmeni.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb1;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\ProstorijaFormaIzmeni.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/InformacioniSistemBolnice;V1.0.0.0;component/prostorijaformaizmeni.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ProstorijaFormaIzmeni.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tipIzmena = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.tb1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tb2 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.potvrdaIzmeneDugme = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\ProstorijaFormaIzmeni.xaml"
            this.potvrdaIzmeneDugme.Click += new System.Windows.RoutedEventHandler(this.potvrdaIzmeneDugme_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rb1 = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.rb2 = ((System.Windows.Controls.RadioButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

