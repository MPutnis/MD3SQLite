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
        private readonly AssignmentService _assignmentService;

        [ObservableProperty]
        private Course? _course;

        [ObservableProperty]
        private ObservableCollection<Teacher>? _teachers;

        [ObservableProperty]
        private Teacher? _selectedTeacher;

        [ObservableProperty]
        private ObservableCollection<Assignment>? _assignments;

        public CourseDetailViewModel(
            CourseService courseService, TeacherService teacherService, AssignmentService assignmentService)
        {
            _courseService = courseService;
            _teacherService = teacherService;
            _assignmentService = assignmentService;
            SaveCommand = new AsyncRelayCommand(SaveCourseAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackAsync);
            LoadTeachersCommand = new AsyncRelayCommand(LoadTeachersAsync);
            LoadAssignmentsCommand = new AsyncRelayCommand(LoadAssignmentsAsync);
            LoadTeachersCommand.Execute(null);
        }

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand NavigateBackCommand { get; }
        public IAsyncRelayCommand LoadTeachersCommand { get; }
        public IAsyncRelayCommand LoadAssignmentsCommand { get; }

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
                await ToastService.ShowToastAsync("Error saving course. Please try again.");
            }
        }

        // Navigation from course details to course list
        private async Task NavigateBackAsync()
        {
            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating back: {ex.Message}");
                await ToastService.ShowToastAsync("Error navigating back. Please try again.");
            }
        }

        private async Task LoadTeachersAsync()
        {
            try
            {
                var teachers = await _teacherService.GetTeachersAsync();
                Teachers = new ObservableCollection<Teacher>(teachers);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading teachers: {ex.Message}");
                await ToastService.ShowToastAsync("Error loading teachers. Please try again.");
            }
        }

        private async Task LoadAssignmentsAsync()
        {
            try
            {
                if (Course != null)
                {
                    var assignments = await _assignmentService.GetAssignmentsByCourseIdAsync(Course.Id);
                    Assignments = new ObservableCollection<Assignment>(assignments);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading assignments: {ex.Message}");
                await ToastService.ShowToastAsync("Error loading assignments. Please try again.");
            }
        }

        public async void Initialize(Course course)
        {
            try
            {
                Course = course;
                await LoadTeachersAsync();
                SelectedTeacher = Teachers?.FirstOrDefault(t => t.Id == course.TeacherId);
                await LoadAssignmentsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing course details: {ex.Message}");
                await ToastService.ShowToastAsync("Error initializing course details. Please try again.");
            }
        }
    }
}
