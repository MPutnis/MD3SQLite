using MD3SQLite.Views;
using System.Diagnostics;

namespace MD3SQLite
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            try
            {
                Routing.RegisterRoute(nameof(StudentDetailPage), typeof(StudentDetailPage));
                Debug.WriteLine("StudentDetailPage route registered successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error registering StudentDetailPage route: {ex.Message}");
            }
                        
            try
            {
                Routing.RegisterRoute(nameof(TeacherDetailPage), typeof(TeacherDetailPage));
                Debug.WriteLine("TeacherDetailPage route registered successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error registering TeacherDetailPage route: {ex.Message}");
            }

            try
            {
                Routing.RegisterRoute(nameof(CourseDetailPage), typeof(CourseDetailPage));
                Debug.WriteLine("CourseDetailPage route registered successfully.");
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Error registering CourseDetailPage route: {ex.Message}");
            }

            try
            {
                Routing.RegisterRoute(nameof(AssignmentDetailPage), typeof(AssignmentDetailPage));
                Debug.WriteLine("AssignmentDetailPage route registered successfully.");
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Error registering AssignmentDetailPage route: {ex.Message}");
            }

            try
            {
                Routing.RegisterRoute(nameof(SubmissionsDetailPage), typeof(SubmissionsDetailPage));
                Debug.WriteLine("SubmissionsDetailPage route registered successfully.");
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Error registering SubmissionsDetailPage route: {ex.Message}");
            }
        }
    }
}
