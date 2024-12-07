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
        [ObservableProperty]
        private Assignment? _assignment;
        
        public AssignmentDetailViewModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            SaveCommand = new AsyncRelayCommand(SaveAssignmentAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackAsync);
        }
        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand NavigateBackCommand { get; }
        private async Task SaveAssignmentAsync()
        {
            try
            {
                if (Assignment != null)
                {
                    await _assignmentService.SaveAssignmentAsync(Assignment);
                    Debug.WriteLine($"Assignment saved: {Assignment.Description} {Assignment.DeadLine}");
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
        public void Initialize(Assignment assignment)
        {
            Assignment = assignment;
        }
    }
}
