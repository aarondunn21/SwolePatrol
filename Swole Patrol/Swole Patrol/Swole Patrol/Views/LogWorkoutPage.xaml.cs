﻿using Swole_Patrol.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Swole_Patrol.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogWorkoutPage : ContentPage
    {
        public LogWorkoutPage()
        {
            InitializeComponent();
            BindingContext = new LogWorkoutViewModel();
        }
    }
}