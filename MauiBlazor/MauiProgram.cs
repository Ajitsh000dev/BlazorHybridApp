using MauiBlazor.Services;
using SharedUI.Interface;


namespace MauiBlazor
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
				});

			builder.Services.AddMauiBlazorWebView();
#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
#endif

			//builder.Services.AddSingleton<WeatherForecast>();
			builder.Services.AddSingleton<HttpClient>();
			builder.Services.AddSingleton<ITesting, Testing>();
			builder.Services.AddSingleton<SharedUI.Notifications.INotificationService, NotificationService>();
			
			return builder.Build();
		}
	}
}