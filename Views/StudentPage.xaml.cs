using Microsoft.Maui.Controls;
using MD3SQLite.ViewModels;

namespace MD3SQLite.Views;

public partial class StudentPage : ContentPage
{
	public StudentPage(StudentViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}