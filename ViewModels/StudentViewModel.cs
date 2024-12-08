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
    public partial class StudentViewModel : ObservableObject
    {
        private readonly StudentService _studentService;

        [ObservableProperty]
        private ObservableCollection<Student>? _students;

        [ObservableProperty]
        private Student? _selectedStudent;
        //done: refresh student list after navigating to student page
        public StudentViewModel(StudentService studentService)
        {
            _studentService = studentService;
            LoadStudentsCommand = new AsyncRelayCommand(LoadStudentsAsync);
            AddStudentCommand = new AsyncRelayCommand(AddStudentAsync);
            UpdateStudentCommand = new AsyncRelayCommand( UpdateStudentAsync);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudentAsync);

            // Load the students initially
            LoadStudentsCommand.Execute(null);
        }

        public IAsyncRelayCommand LoadStudentsCommand { get; }
        public IAsyncRelayCommand AddStudentCommand { get; }
        public IAsyncRelayCommand UpdateStudentCommand { get; }
        public IAsyncRelayCommand DeleteStudentCommand { get; }

        // done: can't unselct a student once selected, except by reentering the page
        // add new student works now, can live without unselcting a student
        private async Task LoadStudentsAsync()
        {
            try
            {
                var students = await _studentService.GetStudentsAsync();
                // Bind the students to the view
                Students = new ObservableCollection<Student>(students);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading students: {ex.Message}");
                await ToastService.ShowToastAsync("Error loading students. Please try again.");
            }
        }

        private async Task AddStudentAsync()
        {
            try
            {
                var newStudent = new Student();
                await Shell.Current.GoToAsync(nameof(StudentDetailPage), new Dictionary<string, object>
                {
                    { "Student", newStudent }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding student: {ex.Message}");
                await ToastService.ShowToastAsync("Error adding student. Please try again.");
            }
        }

        private async Task UpdateStudentAsync()
        {
            try
            {
                if (SelectedStudent != null)
                {
                    await Shell.Current.GoToAsync(nameof(StudentDetailPage), new Dictionary<string, object>
                    {
                        { "Student", SelectedStudent }
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating student: {ex.Message}");
                await ToastService.ShowToastAsync("Error updating student. Please try again.");
            }
        }

        private async Task DeleteStudentAsync()
        {
            try
            {
                if (SelectedStudent != null)
                {
                    var mainPage = Application.Current?.MainPage;
                    if (mainPage != null)
                    {
                        bool confirm = await mainPage.DisplayAlert(
                            "Confirm Delete",
                            $"Are you sure you want to delete {SelectedStudent.FullName}?",
                            "Yes",
                            "No"
                        );

                        if (confirm)
                        {
                            await _studentService.DeleteStudentAsync(SelectedStudent);
                            Debug.WriteLine($"Deleted student {SelectedStudent.FullName}");
                            SelectedStudent = null;
                            await LoadStudentsAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting student: {ex.Message}");
                await ToastService.ShowToastAsync("Error deleting student. Please try again.");
            }
        }
    }
}
