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
    public partial class AssignmentViewModel : ObservableObject
    {
        private readonly AssignmentService _assignmentService;
        [ObservableProperty]
        private ObservableCollection<Assignment>? _assignments;
        [ObservableProperty]
        private Assignment? _selectedAssignment;
        //done: refresh assignment list after navigating to assignment page
        public AssignmentViewModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            LoadAssignmentsCommand = new AsyncRelayCommand(LoadAssignmentsAsync);
            AddAssignmentCommand = new AsyncRelayCommand(AddAssignmentAsync);
            UpdateAssignmentCommand = new AsyncRelayCommand(UpdateAssignmentAsync);
            DeleteAssignmentCommand = new AsyncRelayCommand(DeleteAssignmentAsync);
            // Load the assignments initially
            LoadAssignmentsCommand.Execute(null);
        }
        public IAsyncRelayCommand LoadAssignmentsCommand { get; }
        public IAsyncRelayCommand AddAssignmentCommand { get; }
        public IAsyncRelayCommand UpdateAssignmentCommand { get; }
        public IAsyncRelayCommand DeleteAssignmentCommand { get; }
        // done: can't unselct a assignment once selected, except by reentering the page
        // add new assignment works now, can live without unselcting a assignment
        private async Task LoadAssignmentsAsync()
        {
            var assignments = await _assignmentService.GetAssignmentsAsync();
            // Bind the assignments to the view
            Assignments = new ObservableCollection<Assignment>(assignments);
        }
        private async Task AddAssignmentAsync()
        {
            var newAssignment = new Assignment();
            await Shell.Current.GoToAsync(nameof(AssignmentDetailPage), new Dictionary<string, object>
            {
                { "Assignment", newAssignment }
            });
        }

        private async Task UpdateAssignmentAsync()
        {
            if (SelectedAssignment != null)
            {
                await Shell.Current.GoToAsync(nameof(AssignmentDetailPage), new Dictionary<string, object>
                {
                    {"Assignment", SelectedAssignment }
                });
            }
        }

        private async Task DeleteAssignmentAsync()
        {
            if (SelectedAssignment != null)
            {
                var mainPage = Application.Current?.MainPage;
                if (mainPage != null)
                {

                    bool confirm = await mainPage.DisplayAlert(
                        "Delete Assignment",
                        $"Are you sure you want to delete {SelectedAssignment.Description}?",
                        "Yes",
                        "No"
                        );
                    if (confirm)
                    {
                        try
                        {
                            await _assignmentService.DeleteAssignmentAsync(SelectedAssignment);
                            Debug.WriteLine($"Assignment {SelectedAssignment.Description} deleted.");
                            SelectedAssignment = null;
                            await LoadAssignmentsAsync();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error deleting assignment: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
