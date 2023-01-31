
using Swole_Patrol.ViewModels;
using Swole_Patrol.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Swole_Patrol.Views
{
    public partial class LoginPage : ContentPage
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();

        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            (BindingContext as LoginViewModel).Username = Username;
            (BindingContext as LoginViewModel).Password = Password;
            
        }
    }
}