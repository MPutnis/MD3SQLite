using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MD3SQLite.Models;
using MD3SQLite.Services;
using MD3SQLite.Views;

namespace MD3SQLite.ViewModels
{
    public partial class CourseViewModel : ObservableObject
    {
        private readonly CourseService _courseService;
        private readonly TeacherService _teacherService;

        [ObservableProperty]
        private ObservableCollection<Course>? _courses;

        [ObservableProperty]
        private Course? _selectedCourse;

        //done: refresh course list after navigating to course page
        public CourseViewModel(CourseService courseService, TeacherService teacherService)
        {
            _courseService = courseService;
            _teacherService = teacherService;

            LoadCoursesCommand = new AsyncRelayCommand(LoadCoursesAsync);
            AddCourseCommand = new AsyncRelayCommand(AddCourseAsync);
            UpdateCourseCommand = new AsyncRelayCommand(UpdateCourseAsync);
            DeleteCourseCommand = new AsyncRelayCommand(DeleteCourseAsync);
            // Load the courses initially
            LoadCoursesCommand.Execute(null);
            
        }
        public IAsyncRelayCommand LoadCoursesCommand { get; }
        public IAsyncRelayCommand AddCourseCommand { get; }
        public IAsyncRelayCommand UpdateCourseCommand { get; }
        public IAsyncRelayCommand DeleteCourseCommand { get; }
        // done: can't unselct a course once selected, except by reentering the page
        // add new course works now, can live without unselcting a course
        private async Task LoadCoursesAsync()
        {
            try
            {
                var courses = await _courseService.GetCoursesAsync();
                foreach (var course in courses)
                {
                    course.Teacher = await _teacherService.GetTeacherAsync(course.TeacherId);
                }
                // Bind the courses to the view
                Courses = new ObservableCollection<Course>(courses);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading courses: {ex.Message}");
                await ToastService.ShowToastAsync("Error loading courses. Please try again.");
            }
        }
        private async Task AddCourseAsync()
        {
            try
            {
                var newCourse = new Course();
                await Shell.Current.GoToAsync(nameof(CourseDetailPage), new Dictionary<string, object>
                {
                    { "Course", newCourse }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding course: {ex.Message}");
                await ToastService.ShowToastAsync("Error adding course. Please try again.");
            }
        }
        private async Task UpdateCourseAsync()
        {
            try
            {
                if (SelectedCourse != null)
                {
                    await Shell.Current.GoToAsync(nameof(CourseDetailPage), new Dictionary<string, object>
                    {
                        { "Course", SelectedCourse }
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating course: {ex.Message}");
                await ToastService.ShowToastAsync("Error updating course. Please try again.");
            }
        }
        private async Task DeleteCourseAsync()
        {
            try
            {
                if (SelectedCourse != null)
                {
                    var mainPage = Application.Current?.MainPage;
                    if (mainPage != null)
                    {
                        bool confirm = await mainPage.DisplayAlert(
                            "Delete Course",
                            $"Are you sure you want to delete {SelectedCourse.Name}?",
                            "Yes",
                            "No"
                        );
                        if (confirm)
                        {
                            await _courseService.DeleteCourseAsync(SelectedCourse);
                            Debug.WriteLine($"Course deleted: {SelectedCourse.Name}");
                            SelectedCourse = null;
                            await LoadCoursesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting course: {ex.Message}");
                await ToastService.ShowToastAsync("Error deleting course. Please try again.");
            }
        }
    }
    
}
