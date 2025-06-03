using Formula1App.Views;
using Formula1App.ViewModels;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
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
                //.UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Formula1-Bold_web_0.ttf", "F1Bold");
                    fonts.AddFont("Formula1-Regular_web_0.ttf", "F1Reg");
                    fonts.AddFont("Formula1-Wide_web_0.ttf", "F1Wide");
                });
            #region Syncfusion
            builder.ConfigureSyncfusionCore();
            #endregion
            builder.Services.AddTransient<SignPage>();
            #region Views
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<SignUpView>();
            builder.Services.AddTransient<DriverStandingsView>();
            builder.Services.AddTransient<ConstStandingsView>();
            builder.Services.AddTransient<NewsView>();
            builder.Services.AddTransient<AllNewsView>();
            builder.Services.AddTransient<ArticleView>();
            builder.Services.AddTransient<ManageUsersView>();
            builder.Services.AddTransient<AddArticlesView>();
            builder.Services.AddTransient<YourApprovedArticles>();
            builder.Services.AddTransient<YourPendingArticles>();
            builder.Services.AddTransient<YourDeclinedArticles>();
            builder.Services.AddTransient<DriverView>();
            builder.Services.AddTransient<ConstructorView>();
            builder.Services.AddTransient<UpcomingRacesView>();
            builder.Services.AddTransient<SeasonPastRacesView>();
            builder.Services.AddTransient<RaceResultsView>();
            builder.Services.AddTransient<ManageArticlesView>();
            builder.Services.AddTransient<PrevSeasonsView>();
            builder.Services.AddTransient<ProfileView>();
            #endregion
            #region View Models
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<SignUpViewModel>();
            builder.Services.AddTransient<DriverStandingsViewModel>();
            builder.Services.AddTransient<ConstStandingsViewModel>();
            builder.Services.AddTransient<NewsViewModel>();
            builder.Services.AddTransient<AllNewsViewModel>();
            builder.Services.AddTransient<ArticleViewModel>();
            builder.Services.AddTransient<ManageUsersViewModel>();
            builder.Services.AddTransient<AddArticlesViewModel>();
            builder.Services.AddTransient<YourApprovedArticlesViewModel>();
            builder.Services.AddTransient<YourPendingArticlesViewModel>();
            builder.Services.AddTransient<YourDeclinedArticlesViewModel>();
            builder.Services.AddTransient<DriverViewModel>();
            builder.Services.AddTransient<ConstructorViewModel>();
            builder.Services.AddTransient<CurrSeasonRacesViewModel>();
            builder.Services.AddTransient<ManageArticlesViewModel>();
            builder.Services.AddTransient<PrevSeasonsViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
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