using Microsoft.Maui.Controls;
using MD3SQLite.ViewModels;
using MD3SQLite.Models;

namespace MD3SQLite.Views;

public partial class StudentPage : ContentPage
{
	public StudentPage(StudentViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    // refresh student list when navigating to Student page
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is StudentViewModel viewModel)
        {
            viewModel.LoadStudentsCommand.Execute(null);
        }
    }
    // navigate to StudentDetailPage when a student is selected,
    // passing selected student to detail page
    private async void OnStudentSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Student selectedStudent)
        {
            var navigationParameter = new Dictionary<string, object>
        {
            { "Student", selectedStudent }
        };
            await Shell.Current.GoToAsync(nameof(StudentDetailPage), navigationParameter);
        }
    }
}