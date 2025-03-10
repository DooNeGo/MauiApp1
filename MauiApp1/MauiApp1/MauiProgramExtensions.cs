using Microsoft.Extensions.Logging;
using MPowerKit.Navigation;
using MPowerKit.Navigation.Popups;
using MPowerKit.Navigation.Utilities;
using MPowerKit.Regions;

namespace MauiApp1;

public static class MauiProgramExtensions
{
	public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
	{
		builder
			.UseMauiApp<App>()
			.UseMPowerKitNavigation(mvvmBuilder => mvvmBuilder
				.ConfigureServices(collection => collection
					.RegisterForNavigation<MainPage>()
					.RegisterForNavigation<NewPage1>()
					.RegisterForNavigation<NewContent1>()
					.RegisterForNavigation<NewContent2>()
					.RegisterForNavigation<PopupPageTest>()
					.RegisterForNavigation<MainFlyoutPage>())
				.UsePopupNavigation()
				.UsePageEventsInRegions()
				.OnAppStart("/MainFlyoutPage/NavigationPage/MainPage"))
			.UseMPowerKitRegions()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder;
	}
}