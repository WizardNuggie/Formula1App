using Formula1App.Views;
using Formula1App.ViewModels;
using Microsoft.Extensions.Logging;
using Formula1App.Services;

namespace Formula1App
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
            builder.Services.AddSingleton<SignPage>();
            #region Views
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<SignUpView>();
            #endregion
            #region View Models
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<SignUpViewModel>();
            #endregion
            #region Services
            builder.Services.AddSingleton<F1ExtService>();
            builder.Services.AddSingleton<F1IntService>();
            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
