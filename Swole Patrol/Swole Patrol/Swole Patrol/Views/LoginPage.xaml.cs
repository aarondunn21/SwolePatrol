﻿using Swole_Patrol.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Swole_Patrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}