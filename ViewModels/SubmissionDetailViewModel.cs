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
        [ObservableProperty]
        private Submission? _submission;
        public SubmissionDetailViewModel(SubmissionService submissionService)
        {
            _submissionService = submissionService;
            SaveCommand = new AsyncRelayCommand(SaveSubmissionAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackAsync);
        }
        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand NavigateBackCommand { get; }
        private async Task SaveSubmissionAsync()
        {
            try
            {
                if (Submission != null)
                {
                    await _submissionService.SaveSubmissionAsync((Submission)Submission);
                    Debug.WriteLine($"Submission saved: {Submission.Score} {Submission.SubmissionTime} {Submission.AssignmentId} {Submission.StudentId}");
                    await Shell.Current.GoToAsync(".."); // Go back to the previous page
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving submission: {ex.Message}");
            }
        }
        // Navigation from submission details to submission list
        private async Task NavigateBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
        public void Initialize(Submission submission)
        {
            Submission = submission;
        }
    }
}
