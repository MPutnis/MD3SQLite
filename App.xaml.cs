using Microsoft.Maui.Controls;
using MD3SQLite.Views;
using MD3SQLite.Services;
using MD3SQLite.Models;
using MD3SQLite.ViewModels;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MD3SQLite
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set a temporary MainPage
            MainPage = new AppShell();
            Debug.WriteLine("AppShell set as MainPage.");

            // Initialize the database and set the MainPage
            InitializeDatabaseAndSetMainPageAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Debug.WriteLine($"Error initializing database: {task.Exception.Message}");
                }
            });
        }

        private static async Task InitializeDatabaseAndSetMainPageAsync()
        {
            try
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Study.db3");
                var databaseContext = new DatabaseContext(dbPath);

                // Ensure tables are created
                await databaseContext.CreateTableAsync<Student>();
                await databaseContext.CreateTableAsync<Teacher>();
                await databaseContext.CreateTableAsync<Course>();
                await databaseContext.CreateTableAsync<Assignment>();
                await databaseContext.CreateTableAsync<Submission>();  

                // Create the MainViewModel
                //var mainViewModel = new MainViewModel(databaseContext);

                // Create an instance of MainPage and set its BindingContext
                //var mainPage = new MainPage(mainViewModel);

                // Set the MainPage to the mainPage
                await Shell.Current.GoToAsync("//MainPage");
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing database: {ex.Message}");
            }
        }
    }
}
