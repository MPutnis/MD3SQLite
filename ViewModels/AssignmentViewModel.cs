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
        private readonly CourseService _courseService;

        [ObservableProperty]
        private ObservableCollection<Assignment>? _assignments;

        [ObservableProperty]
        private Assignment? _selectedAssignment;

        //done: refresh assignment list after navigating to assignment page
        public AssignmentViewModel(AssignmentService assignmentService, CourseService courseService)
        {
            _assignmentService = assignmentService;
            _courseService = courseService;
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
            try
            {
                var assignments = await _assignmentService.GetAssignmentsAsync();
                foreach (var assignment in assignments)
                {
                    assignment.Course = await _courseService.GetCourseAsync(assignment.CourseId);
                }
                // Bind the assignments to the view
                Assignments = new ObservableCollection<Assignment>(assignments);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading assignments: {ex.Message}");
                await ToastService.ShowToastAsync("Error loading assignments. Please try again.");
            }
        }
        private async Task AddAssignmentAsync()
        {
            try
            {
                var newAssignment = new Assignment();
                await Shell.Current.GoToAsync(nameof(AssignmentDetailPage), new Dictionary<string, object>
                {
                    { "Assignment", newAssignment }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding assignment: {ex.Message}");
                await ToastService.ShowToastAsync("Error adding assignment. Please try again.");
            }
        }

        private async Task UpdateAssignmentAsync()
        {
            try
            {
                if (SelectedAssignment != null)
                {
                    await Shell.Current.GoToAsync(nameof(AssignmentDetailPage), new Dictionary<string, object>
                    {
                        { "Assignment", SelectedAssignment }
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating assignment: {ex.Message}");
                await ToastService.ShowToastAsync("Error updating assignment. Please try again.");
            }
        }

        private async Task DeleteAssignmentAsync()
        {
            try
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
                            await _assignmentService.DeleteAssignmentAsync(SelectedAssignment);
                            Debug.WriteLine($"Assignment {SelectedAssignment.Description} deleted.");
                            SelectedAssignment = null;
                            await LoadAssignmentsAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting assignment: {ex.Message}");
                await ToastService.ShowToastAsync("Error deleting assignment. Please try again.");
            }
        }
    }
}
