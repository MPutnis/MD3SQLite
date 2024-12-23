using MD3SQLite.Models;
using MD3SQLite.ViewModels;
using Microsoft.Maui.Controls;

namespace MD3SQLite.Views
{ 
	[QueryProperty(nameof(Teacher), "Teacher")]
	public partial class TeacherDetailPage : ContentPage
	{
        private readonly TeacherDetailViewModel _viewModel;
        private Teacher _teacher;
        public TeacherDetailPage(TeacherDetailViewModel viewModel)
		{
			InitializeComponent();
            BindingContext = _viewModel = viewModel;
            _teacher = new Teacher();
        }
        
        public Teacher Teacher
        {
            get => _teacher;
            set
            {
                _teacher = value;
                _viewModel.Initialize(_teacher);
            }

        }
	}
}