using Formula1App.Views;
using Formula1App.ViewModels;
using Microsoft.Extensions.Logging;

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
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<SignUpView>();
            builder.Services.AddSingleton<SignUpViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
