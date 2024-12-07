using MD3SQLite.Models;
using MD3SQLite.ViewModels;
using Microsoft.Maui.Controls;

namespace MD3SQLite.Views
{
    [QueryProperty(nameof(Student), "Student")]
    public partial class StudentDetailPage : ContentPage
    {
        private readonly StudentDetailViewModel _viewModel;
        private Student _student;
        public StudentDetailPage(StudentDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            _student = new Student();
        }


       
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
