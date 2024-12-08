using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using MD3SQLite.Services;
using MD3SQLite.ViewModels;
using MD3SQLite.Views;
using System.IO;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using System.Diagnostics;

namespace MD3SQLite
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registering services and ViewModels
            string dbPath =string.Empty;
            try
            {
                dbPath = Path.Combine(FileSystem.AppDataDirectory, "Study.db3");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting database path: {ex.Message}");
                Application.Current?.MainPage?.DisplayAlert("Error", "Error getting database path", "OK");
            }

            try
            {
                builder.Services.AddSingleton<DatabaseContext>(
                    s => ActivatorUtilities.CreateInstance<DatabaseContext>(s, dbPath));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating DatabaseContext instance: {ex.Message}");
                Application.Current?.MainPage?.DisplayAlert("Error", "Error creating DatabaseContext instance", "OK");
            }

            // MainPage registrations
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            // Student  registrations
            builder.Services.AddSingleton<StudentService>();
            builder.Services.AddSingleton<StudentViewModel>();
            builder.Services.AddSingleton<StudentPage>();
            builder.Services.AddSingleton<StudentDetailPage>();
            builder.Services.AddSingleton<StudentDetailViewModel>();

            // Teacher registrations
            builder.Services.AddSingleton<TeacherService>();
            builder.Services.AddSingleton<TeacherViewModel>();
            builder.Services.AddSingleton<TeacherPage>();
            builder.Services.AddSingleton<TeacherDetailPage>();
            builder.Services.AddSingleton<TeacherDetailViewModel>();

            // Course registrations
            builder.Services.AddSingleton<CourseService>();
            builder.Services.AddSingleton<CourseViewModel>();
            builder.Services.AddSingleton<CoursePage>();
            builder.Services.AddSingleton<CourseDetailPage>();
            builder.Services.AddSingleton<CourseDetailViewModel>();

            // Assignment registrations
            builder.Services.AddSingleton<AssignmentService>();
            builder.Services.AddSingleton<AssignmentViewModel>();
            builder.Services.AddSingleton<AssignmentPage>();
            builder.Services.AddSingleton<AssignmentDetailPage>();
            builder.Services.AddSingleton<AssignmentDetailViewModel>();

            // Submissions registrations
            builder.Services.AddSingleton<SubmissionService>();
            builder.Services.AddSingleton<SubmissionViewModel>();
            builder.Services.AddSingleton<SubmissionPage>();
            builder.Services.AddSingleton<SubmissionsDetailPage>();
            builder.Services.AddSingleton<SubmissionDetailViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
