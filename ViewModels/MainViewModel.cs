using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MD3SQLite.Services;
using MD3SQLite.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

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
            // Load database statistics (e.g., table names and row counts)
            var statistics = await _databaseContext.GetDatabaseStatisticsAsync();
            DatabaseStatistics = statistics;
        }

        [RelayCommand]
        private static async Task NavigateToStudents()
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

        [RelayCommand]
        private static async Task NavigateToTeachers()
        {
            // Navigate to TeacherPage
            if (Shell.Current != null)
                await Shell.Current.GoToAsync("//TeacherPage");
            else
                Debug.WriteLine("Shell.Currrent is null");
        }

        [RelayCommand]
        private async Task SeedDatabase()
        {
            // Seed the database
            await _databaseContext.SeedDataAsync();
            LoadDatabaseStatistics();
        }

        [RelayCommand]
        private async Task ClearDatabase()
        {
            // Clear the database
            await _databaseContext.ClearDataAsync();
            LoadDatabaseStatistics();
        }
    }
}


