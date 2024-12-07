using Microsoft.Maui.Controls;
using MD3SQLite.ViewModels;
using MD3SQLite.Models;

namespace MD3SQLite.Views;

public partial class SubmissionPage : ContentPage
{
	public SubmissionPage(SubmissionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    // refresh submission list when navigating to Submissions page
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SubmissionViewModel viewModel)
        {
            viewModel.LoadSubmissionsCommand.Execute(null);
        }
    }

    // navigate to SubmissionsDetailPage when a submission is selected,
    // passing selected submission to detail page
    private async void OnSubmissionSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Submission selectedSubmission)
        {
            var navigationParameter = new Dictionary<string, object>
        {
            { "Submission", selectedSubmission }
        };
            await Shell.Current.GoToAsync(nameof(SubmissionsDetailPage), navigationParameter);
        }
    }
}