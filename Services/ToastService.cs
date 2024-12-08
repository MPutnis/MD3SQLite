using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Threading.Tasks;

namespace MD3SQLite.Services
{
    public static class ToastService
    {
        public static async Task ShowToastAsync(
            string message, ToastDuration duration = ToastDuration.Long)
        {
            var toast = Toast.Make(message, duration);
            await toast.Show();
        }
    }
}
