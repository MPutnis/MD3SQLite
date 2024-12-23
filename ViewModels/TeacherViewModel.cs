﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MD3SQLite.Models;
using MD3SQLite.Services;
using MD3SQLite.Views;

namespace MD3SQLite.ViewModels
{
    public partial class TeacherViewModel : ObservableObject
    {
        private readonly TeacherService _teacherService;
        [ObservableProperty]
        private ObservableCollection<Teacher>? _teachers;
        [ObservableProperty]
        private Teacher? _selectedTeacher;
        //done: refresh teacher list after navigating to teacher page
        public TeacherViewModel(TeacherService teacherService)
        {
            _teacherService = teacherService;
            LoadTeachersCommand = new AsyncRelayCommand(LoadTeachersAsync);
            AddTeacherCommand = new AsyncRelayCommand(AddTeacherAsync);
            UpdateTeacherCommand = new AsyncRelayCommand(UpdateTeacherAsync);
            DeleteTeacherCommand = new AsyncRelayCommand(DeleteTeacherAsync);
            // Load the teachers initially
            LoadTeachersCommand.Execute(null);
        }
        public IAsyncRelayCommand LoadTeachersCommand { get; }
        public IAsyncRelayCommand AddTeacherCommand { get; }
        public IAsyncRelayCommand UpdateTeacherCommand { get; }
        public IAsyncRelayCommand DeleteTeacherCommand { get; }

        private async Task LoadTeachersAsync()
        {
            try
            {
                var teachers = await _teacherService.GetTeachersAsync();
                // Bind the teachers to the view
                Teachers = new ObservableCollection<Teacher>(teachers);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading teachers: {ex.Message}");
                await ToastService.ShowToastAsync("Error loading teachers. Please try again.");
            }
        }

        private async Task AddTeacherAsync()
        {
            try
            {
                var newTeacher = new Teacher();
                await Shell.Current.GoToAsync(nameof(TeacherDetailPage), new Dictionary<string, object>
                {
                    { "Teacher", newTeacher }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding teacher: {ex.Message}");
                await ToastService.ShowToastAsync("Error adding teacher. Please try again.");
            }
        }

        private async Task UpdateTeacherAsync()
        {
            try
            {
                if (SelectedTeacher != null)
                {
                    await Shell.Current.GoToAsync(nameof(TeacherDetailPage), new Dictionary<string, object>
                    {
                        { "Teacher", SelectedTeacher }
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating teacher: {ex.Message}");
                await ToastService.ShowToastAsync("Error updating teacher. Please try again.");
            }
        }

        private async Task DeleteTeacherAsync()
        {
            try
            {
                if (SelectedTeacher != null)
                {
                    var mainPage = Application.Current?.MainPage;
                    if (mainPage != null)
                    {
                        bool confirm = await mainPage.DisplayAlert(
                            "Delete Teacher",
                            $"Are you sure you want to delete {SelectedTeacher.Name} {SelectedTeacher.Surname}?",
                            "Yes",
                            "No"
                        );
                        if (confirm)
                        {
                            await _teacherService.DeleteTeacherAsync(SelectedTeacher);
                            Debug.WriteLine($"Teacher deleted: {SelectedTeacher.Name} {SelectedTeacher.Surname}");
                            SelectedTeacher = null;
                            await LoadTeachersAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting teacher: {ex.Message}");
                await ToastService.ShowToastAsync("Error deleting teacher. Please try again.");
            }
        }
    }
}
