using Microsoft.Maui.Controls;
using MD3SQLite.ViewModels;
using MD3SQLite.Models;

namespace MD3SQLite.Views;

public partial class AssignmentPage : ContentPage
{
    public AssignmentPage(AssignmentViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // refresh assignment list when navigating to Assignment page
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AssignmentViewModel viewModel)
        {
            viewModel.LoadAssignmentsCommand.Execute(null);
        }
    }
    // navigate to AssignmentDetailPage when an assignment is selected,
    // passing selected assignment to detail page
    private async void OnAssignmentSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Assignment selectedAssignment)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Assignment", selectedAssignment }
            };
            await Shell.Current.GoToAsync(nameof(AssignmentDetailPage), navigationParameter);
        }
    }
}