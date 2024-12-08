using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MD3SQLite.Services;
using MD3SQLite.Views;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MD3SQLite.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DatabaseContext _databaseContext;

        [ObservableProperty]
        private string _databaseStatistics = string.Empty;

        public MainViewModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            LoadDatabaseStatistics();
        }
        // Done: when navigating to Main page, refresh Database statistics

        public async void LoadDatabaseStatistics()
        {
            try
            {
                // Load database statistics (e.g., table names and row counts)
                var statistics = await _databaseContext.GetDatabaseStatisticsAsync();
                DatabaseStatistics = statistics;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading database statistics: {ex.Message}");
                await ToastService.ShowToastAsync("Error loading database statistics. Please try again.");
            }
        }

        [RelayCommand]
        private static async Task NavigateToStudents()
        {
            try
            {
                // Navigate to StudentPage
                if (Shell.Current != null)
                {
                    // Navigate to StudentPage
                    await Shell.Current.GoToAsync("//StudentPage");
                }
                else
                {
                    // Handle the null case, maybe log an error or show a message to the user
                    Debug.WriteLine("Shell.Current is null");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating to students: {ex.Message}");
                await ToastService.ShowToastAsync("Error navigating to students. Please try again.");
            }
        }

        [RelayCommand]
        private static async Task NavigateToTeachers()
        {
            try
            {
                // Navigate to TeacherPage
                if (Shell.Current != null)
                    await Shell.Current.GoToAsync("//TeacherPage");
                else
                    Debug.WriteLine("Shell.Current is null");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating to teachers: {ex.Message}");
                await ToastService.ShowToastAsync("Error navigating to teachers. Please try again.");
            }
        }

        [RelayCommand]
        private static async Task NavigateToCourses()
        {
            try
            {
                // Navigate to CoursePage
                if (Shell.Current != null)
                    await Shell.Current.GoToAsync("//CoursePage");
                else
                    Debug.WriteLine("Shell.Current is null");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating to courses: {ex.Message}");
                await ToastService.ShowToastAsync("Error navigating to courses. Please try again.");
            }
        }

        [RelayCommand]
        private static async Task NavigateToAssignments()
        {
            try
            {
                // Navigate to AssignmentPage
                if (Shell.Current != null)
                    await Shell.Current.GoToAsync("//AssignmentPage");
                else
                    Debug.WriteLine("Shell.Current is null");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating to assignments: {ex.Message}");
                await ToastService.ShowToastAsync("Error navigating to assignments. Please try again.");
            }
        }

        [RelayCommand]
        private static async Task NavigateToSubmissions()
        {
            try
            {
                // Navigate to SubmissionsPage
                if (Shell.Current != null)
                    await Shell.Current.GoToAsync("//SubmissionsPage");
                else
                    Debug.WriteLine("Shell.Current is null");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating to submissions: {ex.Message}");
                await ToastService.ShowToastAsync("Error navigating to submissions. Please try again.");
            }
        }

        [RelayCommand]
        private async Task SeedDatabase()
        {
            try
            {
                // Seed the database
                await _databaseContext.SeedDataAsync();
                LoadDatabaseStatistics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error seeding database: {ex.Message}");
                await ToastService.ShowToastAsync("Error seeding database. Please try again.");
            }
        }

        [RelayCommand]
        private async Task ClearDatabase()
        {
            try
            {
                // Clear the database
                await _databaseContext.ClearDataAsync();
                LoadDatabaseStatistics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error clearing database: {ex.Message}");
                await ToastService.ShowToastAsync("Error clearing database. Please try again.");
            }
        }
    }
}

