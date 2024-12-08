using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MD3SQLite.Models;
using MD3SQLite.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MD3SQLite.ViewModels
{
    public partial class SubmissionDetailViewModel : ObservableObject
    {
        private readonly SubmissionService _submissionService;
        private readonly AssignmentService _assignmentService;
        private readonly StudentService _studentService;

        [ObservableProperty]
        private Submission? _submission;

        [ObservableProperty]
        private ObservableCollection<Assignment>? _assignments;

        [ObservableProperty]
        private Assignment? _selectedAssignment;

        [ObservableProperty]
        private ObservableCollection<Student>? _students;

        [ObservableProperty]
        private Student? _selectedStudent;

        [ObservableProperty]
        private DateTime _submissionDate;

        [ObservableProperty]
        private TimeSpan _submissionTime;

        public SubmissionDetailViewModel(
            SubmissionService submissionService, AssignmentService assignmentService, StudentService studentService)
        {
            _submissionService = submissionService;
            _assignmentService = assignmentService;
            _studentService = studentService;
            SaveCommand = new AsyncRelayCommand(SaveSubmissionAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackAsync);
            LoadAssignmentsCommand = new AsyncRelayCommand(LoadAssignmentsAsync);
            LoadStudentsCommand = new AsyncRelayCommand(LoadStudentsAsync);
            LoadAssignmentsCommand.Execute(null);
            LoadStudentsCommand.Execute(null);
        }
        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand NavigateBackCommand { get; }
        public IAsyncRelayCommand LoadAssignmentsCommand { get; }
        public IAsyncRelayCommand LoadStudentsCommand { get; }

        // Combine picked date and time into a single property
        public DateTime SubmissionDateTime
        {
            get => SubmissionDate.Date + SubmissionTime;
            set
            {
                SubmissionDate = value.Date;
                SubmissionTime = value.TimeOfDay;
            }
        }

        private async Task SaveSubmissionAsync()
        {
            try
            {
                if (Submission != null && SelectedAssignment != null && SelectedStudent != null)
                {
                    Submission.AssignmentId = SelectedAssignment.Id;
                    Submission.StudentId = SelectedStudent.Id;
                    Submission.SubmissionTime = SubmissionDateTime;
                    await _submissionService.SaveSubmissionAsync((Submission)Submission);
                    Debug.WriteLine($"Submission saved: " +
                        $"{Submission.Score} {Submission.SubmissionTime} {SelectedAssignment.Description} {SelectedStudent.FullName}");
                    await Shell.Current.GoToAsync(".."); // Go back to the previous page
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving submission: {ex.Message}");
                await ToastService.ShowToastAsync("Error saving submission. Please try again.");
            }
        }

        // Navigation from submission details to submission list
        private async Task NavigateBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task LoadAssignmentsAsync()
        {
            try
            {
                var assignments = await _assignmentService.GetAssignmentsAsync();
                Assignments = new ObservableCollection<Assignment>(assignments);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading assignments: {ex.Message}");
                await ToastService.ShowToastAsync("Error loading assignments. Please try again.");
            }
        }

        private async Task LoadStudentsAsync()
        {
            try
            {
                var students = await _studentService.GetStudentsAsync();
                Students = new ObservableCollection<Student>(students);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading students: {ex.Message}");
                await ToastService.ShowToastAsync("Error loading students. Please try again.");
            }
        }
        public async void Initialize(Submission submission)
        {
            try
            {
                Submission = submission;
                SubmissionDateTime = submission.SubmissionTime ?? DateTime.Now; // Initialize SubmissionDate and SubmissionTime
                await LoadAssignmentsAsync();
                await LoadStudentsAsync();
                SelectedAssignment = Assignments?.FirstOrDefault(a => a.Id == submission.AssignmentId);
                SelectedStudent = Students?.FirstOrDefault(s => s.Id == submission.StudentId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing submission details: {ex.Message}");
                await ToastService.ShowToastAsync("Error initializing submission details. Please try again.");
            }
        }
    }
}
