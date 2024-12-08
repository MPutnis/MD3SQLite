using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MD3SQLite.Models;
using MD3SQLite.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MD3SQLite.ViewModels
{
    public partial class AssignmentDetailViewModel : ObservableObject
    {
        private readonly AssignmentService _assignmentService;
        private readonly CourseService _courseService;

        [ObservableProperty]
        private Assignment? _assignment;

        [ObservableProperty]
        private ObservableCollection<Course> _courses;

        [ObservableProperty]
        private Course? _selectedCourse;

        [ObservableProperty]
        private DateTime _deadLineDate;

        [ObservableProperty]
        private TimeSpan _deadLineTime;

        public AssignmentDetailViewModel(AssignmentService assignmentService, CourseService courseService)
        {
            _assignmentService = assignmentService;
            _courseService = courseService;
            SaveCommand = new AsyncRelayCommand(SaveAssignmentAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackAsync);
            LoadCoursesCommand = new AsyncRelayCommand(LoadCoursesAsync);
            LoadCoursesCommand.Execute(null);
        }
        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand NavigateBackCommand { get; }
        public IAsyncRelayCommand LoadCoursesCommand { get; }

        // Combine picked date and time into a single property
        public DateTime DeadLine
        { 
            get => DeadLineDate.Date + DeadLineTime;
            set
            {
                DeadLineDate = value.Date;
                DeadLineTime = value.TimeOfDay;
            }
        }
        private async Task SaveAssignmentAsync()
        {
            try
            {
                if (Assignment != null && SelectedCourse != null)
                {
                    Assignment.CourseId = SelectedCourse.Id;
                    Assignment.DeadLine = DeadLine;
                    await _assignmentService.SaveAssignmentAsync(Assignment);
                    Debug.WriteLine($"Assignment saved: {Assignment.Description} {Assignment.DeadLine} {SelectedCourse.Name}");
                    await Shell.Current.GoToAsync(".."); // Go back to the previous page
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving assignment: {ex.Message}");
            }
        }
        // Navigation from assignment details to assignment list
        private async Task NavigateBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task LoadCoursesAsync()
        {
            var courses = await _courseService.GetCoursesAsync();
            Courses = new ObservableCollection<Course>(courses);
        }
        public async void Initialize(Assignment assignment)
        {
            Assignment = assignment;
            DeadLine = assignment.DeadLine ?? DateTime.Now; // Initialize DeadLineDate and DeadLineTime
            await LoadCoursesAsync();
            SelectedCourse = Courses?.FirstOrDefault(c => c.Id == assignment.CourseId);
        }
    }
}
