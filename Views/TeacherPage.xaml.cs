using MD3SQLite.Models;
using MD3SQLite.ViewModels;

namespace MD3SQLite.Views;

public partial class TeacherPage : ContentPage
{
	public TeacherPage(TeacherViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
    // Done: forrmat ContractDate to not show time part
    // refresh teacher list when navigating to Teacher page
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TeacherViewModel viewModel)
        {
            viewModel.LoadTeachersCommand.Execute(null);
        }
    }

    // navigate to TeacherDetailPage when a teacher is selected,
    // passing selected teacher to detail page
    //private async void OnTeacherSelected(object sender, SelectedItemChangedEventArgs e)
    //{
    //    if (e.SelectedItem is Teacher selectedTeacher)
    //    {
    //        var navigationParameter = new Dictionary<string, object>
    //    {
    //        { "Teacher", selectedTeacher }
    //    };
    //        await Shell.Current.GoToAsync(nameof(TeacherDetailPage), navigationParameter);
    //    }
    //}
}