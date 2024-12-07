using MD3SQLite.Models;
using MD3SQLite.ViewModels;
using Microsoft.Maui.Controls;

namespace MD3SQLite.Views
{
	[QueryProperty(nameof(Assignment), "Assignment")]
    public partial class AssignmentDetailPage : ContentPage
	{
        private readonly AssignmentDetailViewModel _viewModel;
        private Assignment _assignment;
        public AssignmentDetailPage(AssignmentDetailViewModel viewModel)
		{
			InitializeComponent();
            BindingContext = _viewModel = viewModel;
            _assignment = new Assignment();
        }
        public Assignment Assignment
        {
            get => _assignment;
            set
            {
                _assignment = value;
                _viewModel.Initialize(_assignment);
            }
        }
	}
}