using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MD3SQLite.Models;
using MD3SQLite.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MD3SQLite.ViewModels
{
    public partial class TeacherDetailViewModel : ObservableObject
    {
        private readonly TeacherService _teacherService;

        [ObservableProperty]
        private Teacher? _teacher;
        public ObservableCollection<Genders> Genders { get; } =
        [
            Models.Genders.Male,
            Models.Genders.Female,
            Models.Genders.Other
        ];

        public TeacherDetailViewModel(TeacherService teacherService)
        {
            _teacherService = teacherService;
            SaveCommand = new AsyncRelayCommand(SaveTeacherAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackAsync);
        }

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand NavigateBackCommand { get; }

        private async Task SaveTeacherAsync()
        {
            try
            {
                if (Teacher != null)
                {
                    await _teacherService.SaveTeacherAsync(Teacher);
                    Debug.WriteLine($"Teacher saved: {Teacher.Name} {Teacher.Surname}");
                    await Shell.Current.GoToAsync(".."); // Go back to the previous page
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving teacher: {ex.Message}");
            }
        }

        // Navigation from teacher details to teacher list
        private async Task NavigateBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public void Initialize(Teacher teacher)
        {
            Teacher = teacher;
        }
    }
}
