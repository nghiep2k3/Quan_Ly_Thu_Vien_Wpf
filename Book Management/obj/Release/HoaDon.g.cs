﻿#pragma checksum "..\..\HoaDon.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FCA853AA0F8547374F6BD32F0693D615FE689B2AC952B2CEABDBD47E75DCCD96"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Book_Management;
using Syncfusion;
using Syncfusion.Windows;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
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


namespace Book_Management {
    
    
    /// <summary>
    /// HoaDon
    /// </summary>
    public partial class HoaDon : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\HoaDon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvHoaDon;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\HoaDon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearch;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\HoaDon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btChiTiet;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\HoaDon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btThem;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\HoaDon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btBack;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\HoaDon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btXoa;
        
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
            System.Uri resourceLocater = new System.Uri("/Book Management;component/hoadon.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\HoaDon.xaml"
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
            this.lvHoaDon = ((System.Windows.Controls.ListView)(target));
            
            #line 12 "..\..\HoaDon.xaml"
            this.lvHoaDon.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lvHoaDon_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtSearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\HoaDon.xaml"
            this.txtSearch.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtSearch_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btChiTiet = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\HoaDon.xaml"
            this.btChiTiet.Click += new System.Windows.RoutedEventHandler(this.btChiTiet_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btThem = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\HoaDon.xaml"
            this.btThem.Click += new System.Windows.RoutedEventHandler(this.btThem_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btBack = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\HoaDon.xaml"
            this.btBack.Click += new System.Windows.RoutedEventHandler(this.btBack_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btXoa = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\HoaDon.xaml"
            this.btXoa.Click += new System.Windows.RoutedEventHandler(this.btXoa_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 121 "..\..\HoaDon.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

