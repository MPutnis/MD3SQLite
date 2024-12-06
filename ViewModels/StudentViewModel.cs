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

namespace MD3SQLite.ViewModels
{
    public partial class StudentViewModel : ObservableObject
    {
        private readonly StudentService _studentService;

        [ObservableProperty]
        private ObservableCollection<Student>? _students;

        [ObservableProperty]
        private Student? _selectedStudent;
        //TODO: refresh student list after navigating to student page
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


        private async Task LoadStudentsAsync()
        {
            var students = await _studentService.GetStudentsAsync();
            // Bind the students to the view
            Students = new ObservableCollection<Student>(students);
        }

        private async Task AddStudentAsync()
        {
            if (SelectedStudent != null)
            {
                await _studentService.SaveStudentAsync(SelectedStudent);
                await LoadStudentsAsync();
            }
        }

        private async Task UpdateStudentAsync()
        {
            if (SelectedStudent != null)
            {
                
                await _studentService.SaveStudentAsync(SelectedStudent);
                await LoadStudentsAsync();
            }
        }

        private async Task DeleteStudentAsync()
        {
            if (SelectedStudent != null)
            { 
                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    "Confirm Delete",
                    $"Are you sure you want to delete {SelectedStudent.FullName}?",
                    "Yes",
                    "No"
                    );

                if (confirm)
                {
                    try
                    {
                        await _studentService.DeleteStudentAsync(SelectedStudent);
                        Debug.WriteLine($"Deleted student {SelectedStudent.FullName}");
                        SelectedStudent = null;
                        await LoadStudentsAsync();
                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine($"Error deleting student: {ex.Message}");
                    }
                }  
            }
        }
    }
}
