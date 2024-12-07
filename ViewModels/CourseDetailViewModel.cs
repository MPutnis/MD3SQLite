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
        private readonly TeacherService _teacherService;

        [ObservableProperty]
        private Course? _course;

        [ObservableProperty]
        private ObservableCollection<Teacher>? _teachers;

        [ObservableProperty]
        private Teacher? _selectedTeacher;

        public CourseDetailViewModel(CourseService courseService, TeacherService teacherService)
        {
            _courseService = courseService;
            _teacherService = teacherService;
            SaveCommand = new AsyncRelayCommand(SaveCourseAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackAsync);
            LoadTeachersCommand = new AsyncRelayCommand(LoadTeachersAsync);
            LoadTeachersCommand.Execute(null);
        }

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand NavigateBackCommand { get; }
        public IAsyncRelayCommand LoadTeachersCommand { get; }

        private async Task SaveCourseAsync()
        {
            try
            {
                if (Course != null && SelectedTeacher != null)
                {
                    Course.TeacherId = SelectedTeacher.Id;
                    await _courseService.SaveCourseAsync(Course);
                    Debug.WriteLine($"Course saved: {Course.Name} teacher: {SelectedTeacher.FullName}");
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


        private async Task LoadTeachersAsync()
        {
            var teachers = await _teacherService.GetTeachersAsync();
            Teachers = new ObservableCollection<Teacher>(teachers);
        }
        public async void Initialize(Course course)
        {
            Course = course;
            await LoadTeachersAsync();
            SelectedTeacher = Teachers?.FirstOrDefault(t => t.Id == course.TeacherId);
        }
    }
}
