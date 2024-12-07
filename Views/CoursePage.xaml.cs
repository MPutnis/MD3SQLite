using Microsoft.Maui.Controls;
using MD3SQLite.ViewModels;
using MD3SQLite.Models;

namespace MD3SQLite.Views;

public partial class CoursePage : ContentPage
{
	public CoursePage(CourseViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    // refresh course list when navigating to Course page
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CourseViewModel viewModel)
        {
            viewModel.LoadCoursesCommand.Execute(null);
        }
    }
    // navigate to CourseDetailPage when a course is selected,
    // passing selected course to detail page
    private async void OnCourseSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Course selectedCourse)
        {
            var navigationParameter = new Dictionary<string, object>
        {
            { "Course", selectedCourse }
        };
            await Shell.Current.GoToAsync(nameof(CourseDetailPage), navigationParameter);
        }
    }

    // TODO: Display teacher name instead of teacher id
}