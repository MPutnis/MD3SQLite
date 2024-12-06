using System;
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
            var teachers = await _teacherService.GetTeachersAsync();
            // Bind the teachers to the view
            Teachers = new ObservableCollection<Teacher>(teachers);
        }

        private async Task AddTeacherAsync()
        {
            var newTeacher = new Teacher();
            await Shell.Current.GoToAsync(nameof(TeacherDetailPage), new Dictionary<string, object>
            {
                { "Teacher", newTeacher }
            });
        }

        private async Task UpdateTeacherAsync()
        {
            if (SelectedTeacher != null)
            {
                await Shell.Current.GoToAsync(nameof(TeacherDetailPage), new Dictionary<string, object>
                {
                    { "Teacher", SelectedTeacher }
                });
            }
        }

        private async Task DeleteTeacherAsync()
        {
            if (SelectedTeacher != null)
            {
                bool confirm = await App.Current.MainPage.DisplayAlert(
                    "Delete Teacher",
                    $"Are you sure you want to delete {SelectedTeacher.Name} {SelectedTeacher.Surname}?",
                    "Yes",
                    "No"
                    );
                if (confirm)
                {
                    try
                    {
                        await _teacherService.DeleteTeacherAsync(SelectedTeacher);
                        Debug.WriteLine($"Teacher deleted: {SelectedTeacher.Name} {SelectedTeacher.Surname}");
                        SelectedTeacher = null;
                        await LoadTeachersAsync();
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine($"Error deleting teacher: {SelectedTeacher.Name} {SelectedTeacher.Surname}");
                    }
                    
                }
                
            }

        }
    }
}
