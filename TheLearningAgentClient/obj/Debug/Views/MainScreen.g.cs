﻿#pragma checksum "..\..\..\Views\MainScreen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EE4B38C0862B306A48E9101CB1787E0CCEF4BE51"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TheLearningAgentClient;
using TheLearningAgentClient.ViewModels;
using TheLearningAgentClient.Views;


namespace TheLearningAgentClient.Views {
    
    
    /// <summary>
    /// MainScreen
    /// </summary>
    public partial class MainScreen : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Views\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TheLearningAgentClient.Views.MainScreen myPage;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Views\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TheLearningAgentClient.Views.ItemInCart ItemInCartViewControl;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Views\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbxDepartments;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\Views\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbxIngredients;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\Views\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TheLearningAgentClient.Views.ItemPreview ItemPreviewViewControl;
        
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
            System.Uri resourceLocater = new System.Uri("/TheLearningAgentClient;component/views/mainscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MainScreen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.myPage = ((TheLearningAgentClient.Views.MainScreen)(target));
            return;
            case 2:
            this.ItemInCartViewControl = ((TheLearningAgentClient.Views.ItemInCart)(target));
            return;
            case 3:
            this.cmbxDepartments = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.cmbxIngredients = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.ItemPreviewViewControl = ((TheLearningAgentClient.Views.ItemPreview)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
