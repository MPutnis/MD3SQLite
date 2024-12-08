using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MD3SQLite.Models;
using MD3SQLite.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MD3SQLite.ViewModels
{
    // TODO: data validation and error handling, when editing details: can save with empty fields
    // TODO: can delete student that has submission, submission stays with no student
    public partial class StudentDetailViewModel : ObservableObject
    {
        private readonly StudentService _studentService;

        [ObservableProperty]
        private Student? _student;

        public ObservableCollection<Genders> Genders { get; } =
        [
            Models.Genders.Male,
            Models.Genders.Female,
            Models.Genders.Other
        ];

        public StudentDetailViewModel(StudentService studentService)
        {
            _studentService = studentService;
            SaveCommand = new AsyncRelayCommand(SaveStudentAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackAsync);
        }

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand NavigateBackCommand { get; }

        private async Task SaveStudentAsync()
        {

            try
            {
                if (Student != null)
                {
                    await _studentService.SaveStudentAsync(Student);
                    Debug.WriteLine($"Student saved: {Student.Name} {Student.Surname} {Student.StudentIdNumber}");
                    await Shell.Current.GoToAsync(".."); // Go back to the previous page
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving student: {ex.Message}");
                await ToastService.ShowToastAsync("Error saving student. Please try again.");
            }
        }

        // Navigation from student details to student list
        private async Task NavigateBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public void Initialize(Student student)
        {
            Student = student;
        }
    }
}
