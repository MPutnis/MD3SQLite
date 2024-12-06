using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using MD3SQLite.Services;
using MD3SQLite.ViewModels;
using MD3SQLite.Views;
using System.IO;
using Microsoft.Extensions.Logging;

namespace MD3SQLite
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registering services and ViewModels
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Study.db3");
            builder.Services.AddSingleton<DatabaseContext>(
                s => ActivatorUtilities.CreateInstance<DatabaseContext>(s, dbPath));

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
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
