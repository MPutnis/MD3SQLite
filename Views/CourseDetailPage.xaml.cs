using MD3SQLite.Models;
using MD3SQLite.ViewModels;
using Microsoft.Maui.Controls;

namespace MD3SQLite.Views
{
	[QueryProperty(nameof(Course), "Course")]
    public partial class CourseDetailPage : ContentPage
	{
        private readonly CourseDetailViewModel _viewModel;
        private Course _course;
        public CourseDetailPage(CourseDetailViewModel viewModel)
		{
			InitializeComponent();
            BindingContext = _viewModel = viewModel;
            _course = new Course();
        }
        public Course Course
        {
            get => _course;
            set
            {
                _course = value;
                _viewModel.Initialize(_course);
            }
        }

        // TODO: make Course.TeacherId a picker from existing teachers
    }
}