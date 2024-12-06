using MD3SQLite.Models;
using MD3SQLite.ViewModels;
using Microsoft.Maui.Controls;

namespace MD3SQLite.Views
{
    [QueryProperty(nameof(Student), "Student")]
    public partial class StudentDetailPage : ContentPage
    {
        private StudentDetailViewModel _viewModel;
        public StudentDetailPage(StudentDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }


        private Student _student;
        public Student Student
        {
            get => _student;
            set
            {
                _student = value;
                _viewModel.Initialize(_student);
            }
        }
    }
}
