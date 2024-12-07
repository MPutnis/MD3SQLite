using MD3SQLite.Models;
using MD3SQLite.ViewModels;
using Microsoft.Maui.Controls;

namespace MD3SQLite.Views
{
	[QueryProperty(nameof(Submission), "Submission")]
    public partial class SubmissionsDetailPage : ContentPage
	{
        private readonly SubmissionDetailViewModel _viewModel;
        private Submission _submission;
        public SubmissionsDetailPage(SubmissionDetailViewModel viewModel)
		{
			InitializeComponent();
            BindingContext = _viewModel = viewModel;
            _submission = new Submission();
        }
        public Submission Submission
        {
            get => _submission;
            set
            {
                _submission = value;
                _viewModel.Initialize(_submission);
            }
        }
	}
}