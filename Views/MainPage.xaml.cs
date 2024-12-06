﻿using Microsoft.Maui.Controls;
using MD3SQLite.ViewModels;
using System.Diagnostics;

namespace MD3SQLite.Views
{
    public partial class MainPage : ContentPage
    {
        //public MainPage()
        //{
        //    InitializeComponent();
        //    Debug.WriteLine("MainPage constructor called");
        //}

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Debug.WriteLine("MainPage with ViewModel constructor called");
        }
    }
}