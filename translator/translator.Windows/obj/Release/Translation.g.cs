﻿

#pragma checksum "C:\Users\Kieran\Desktop\Translator\translator\translator\translator.Windows\Translation.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6A7261537B334707CB5334C5577E83B4"
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
    partial class Translation : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 15 "..\..\Translation.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.guessBtn_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 16 "..\..\Translation.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.starBtn_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 19 "..\..\Translation.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.GuessBox_TextChanged;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 20 "..\..\Translation.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBlock)(target)).SelectionChanged += this.correctTextBox_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 21 "..\..\Translation.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBlock)(target)).SelectionChanged += this.correctTextBox_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 22 "..\..\Translation.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.starBtn_Click;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 32 "..\..\Translation.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Button_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

