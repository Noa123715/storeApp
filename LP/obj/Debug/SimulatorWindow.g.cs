﻿#pragma checksum "..\..\SimulatorWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3362CC298344C2C94E97A6E7009D5D4D2AB37754"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL;
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


namespace PL {
    
    
    /// <summary>
    /// SimulatorWindow
    /// </summary>
    public partial class SimulatorWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\SimulatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.StatusBar SBar;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\SimulatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OrderIDText;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\SimulatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nextStateTxt;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\SimulatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox prevStateTxt;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\SimulatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox estimatedTimeTxt;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\SimulatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button endOfSimulator;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\SimulatorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label timerTextBlock;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;component/simulatorwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SimulatorWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\SimulatorWindow.xaml"
            ((PL.SimulatorWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\SimulatorWindow.xaml"
            ((PL.SimulatorWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SBar = ((System.Windows.Controls.Primitives.StatusBar)(target));
            return;
            case 3:
            this.OrderIDText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.nextStateTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.prevStateTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.estimatedTimeTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.endOfSimulator = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\SimulatorWindow.xaml"
            this.endOfSimulator.Click += new System.Windows.RoutedEventHandler(this.EndOfSimulator_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.timerTextBlock = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

