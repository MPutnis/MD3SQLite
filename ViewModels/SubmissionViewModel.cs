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
    public partial class SubmissionViewModel : ObservableObject
    {
        private readonly SubmissionService _submissionService;
        private readonly AssignmentService _assignmentService;
        private readonly StudentService _studentService;

        [ObservableProperty]
        private ObservableCollection<Submission>? _submissions;

        [ObservableProperty]
        private Submission? _selectedSubmission;
        //done: refresh student list after navigating to student page
        public SubmissionViewModel(
            SubmissionService submissionsService, AssignmentService assignmentService, StudentService studentService)
        {
            _submissionService = submissionsService;
            _assignmentService = assignmentService;
            _studentService = studentService;
            LoadSubmissionsCommand = new AsyncRelayCommand(LoadSubmissionsAsync);
            AddSubmissionCommand = new AsyncRelayCommand(AddSubmissionAsync);
            UpdateSubmissionCommand = new AsyncRelayCommand(UpdateSubmissionAsync);
            DeleteSubmissionCommand = new AsyncRelayCommand(DeleteSubmissionAsync);
            // Load the submissions initially
            LoadSubmissionsCommand.Execute(null);
        }
        public IAsyncRelayCommand LoadSubmissionsCommand { get; }
        public IAsyncRelayCommand AddSubmissionCommand { get; }
        public IAsyncRelayCommand UpdateSubmissionCommand { get; }
        public IAsyncRelayCommand DeleteSubmissionCommand { get; }
        // done: can't unselct a student once selected, except by reentering the page
        // add new student works now, can live without unselcting a student
        private async Task LoadSubmissionsAsync()
        {
            var submissions = await _submissionService.GetSubmissionsAsync();
            foreach (var submission in submissions)
            {
                submission.Assignment = await _assignmentService.GetAssignmentAsync(submission.AssignmentId);
                submission.Student = await _studentService.GetStudentAsync(submission.StudentId);
            }
            // Bind the submissions to the view
            Submissions = new ObservableCollection<Submission>(submissions);
        }
        private async Task AddSubmissionAsync()
        {
            var newSubmission = new Submission();
            await Shell.Current.GoToAsync(nameof(SubmissionsDetailPage), new Dictionary<string, object>
            {
                { "Submission", newSubmission }
            });
        }

        private async Task UpdateSubmissionAsync()
        {
            if (SelectedSubmission != null)
            {
                await Shell.Current.GoToAsync(nameof(SubmissionsDetailPage), new Dictionary<string, object>
                {
                    { "Submission", SelectedSubmission }
                });
            }
        }

        private async Task DeleteSubmissionAsync()
        {
            if (SelectedSubmission != null)
            {
                var mainPage = Application.Current?.MainPage;
                if (mainPage != null)
                {
                    // TODO: display assignment description instead of ID
                    bool confirm = await mainPage.DisplayAlert(
                        "Confirm Delete",
                        $"Are you sure you want to delete {SelectedSubmission.AssignmentId} {SelectedSubmission.SubmissionTime}?",
                        "Yes",
                        "No"
                        );

                    if (confirm)
                    {
                        try
                        {
                            await _submissionService.DeleteSubmissionAsync(SelectedSubmission);
                            Debug.WriteLine($"Deleted student {SelectedSubmission.AssignmentId} {SelectedSubmission.SubmissionTime}");
                            SelectedSubmission = null;
                            await LoadSubmissionsAsync();
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
}
