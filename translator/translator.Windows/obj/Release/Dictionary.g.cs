﻿

#pragma checksum "C:\Users\Kieran\Desktop\Translator\translator\translator\translator.Windows\Dictionary.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "439BBB1C7AC344BAB9F0DB0F582FC27F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace translator
{
    partial class Dictionary : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 12 "..\..\Dictionary.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.StackPanel_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 44 "..\..\Dictionary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Button_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 52 "..\..\Dictionary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AddBtn;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 61 "..\..\Dictionary.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SaveBtn_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

