using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MD3SQLite.Models;
using MD3SQLite.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace MD3SQLite.ViewModels
{
    public partial class CourseDetailViewModel : ObservableObject
    {
        private readonly CourseService _courseService;

        [ObservableProperty]
        private Course? _course;

        public CourseDetailViewModel(CourseService courseService)
        {
            _courseService = courseService;
            SaveCommand = new AsyncRelayCommand(SaveCourseAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackAsync);
        }

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand NavigateBackCommand { get; }

        private async Task SaveCourseAsync()
        {
            try
            {
                if (Course != null)
                {
                    await _courseService.SaveCourseAsync(Course);
                    Debug.WriteLine($"Course saved: {Course.Name}");
                    await Shell.Current.GoToAsync(".."); // Go back to the previous page
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving course: {ex.Message}");
            }
        }

        // Navigation from course details to course list
        private async Task NavigateBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public void Initialize(Course course)
        {
            Course = course;
        }
    }
}
