using MD3SQLite.Views;
using System.Diagnostics;

namespace MD3SQLite
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //try
            //{
            //    // Registering the routes
            //    Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            //    Debug.WriteLine("MainPage route registered successfully.");
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"Error registering MainPage route: {ex.Message}");
            //}

            //try
            //{
            //    Routing.RegisterRoute(nameof(StudentPage), typeof(StudentPage));
            //    Debug.WriteLine("StudentPage route registered successfully.");
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"Error registering StudentPage route: {ex.Message}");
            //}

            try
            {
                Routing.RegisterRoute(nameof(StudentDetailPage), typeof(StudentDetailPage));
                Debug.WriteLine("StudentDetailPage route registered successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error registering StudentDetailPage route: {ex.Message}");
            }

            //try
            //{
            //    Routing.RegisterRoute(nameof(TeacherPage), typeof(TeacherPage));
            //    Debug.WriteLine("TeacherPage route registered successfully.");
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"Error registering TeacherPage route: {ex.Message}");
            //}

            try
            {
                Routing.RegisterRoute(nameof(TeacherDetailPage), typeof(TeacherDetailPage));
                Debug.WriteLine("TeacherDetailPage route registered successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error registering TeacherDetailPage route: {ex.Message}");
            }
        }
    }
}
