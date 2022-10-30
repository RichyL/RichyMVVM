using CommunityToolkit.Maui;

using RichyMVVM.ApplicationCode.Popups;
using RichyMVVM.ApplicationCode.ViewModels;
using RichyMVVM.ApplicationCode.Views;
using RichyMVVM.ApplicationCode.Services;

namespace RichyMVVM;

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

		/*
		 * The code below registers the object types with the Microsoft IoC container. The Services property is an IService
		 * and there is an implementation that will return an object using types or generics.
		 * 
		 * Note - services would often be Singletons as the same instance of the object could be used around the application. Hence the TimeService has been 
		 * specified as a Singleton (i.e. only one instance of that object in the application) and the Views and ViewModels have been specified as Transients (i.e. an 
		 * instance is made when required).
		 */
		builder.Services.AddSingleton<TimeService>();
			
        builder.Services.AddTransient<FirstViewModel>();
        builder.Services.AddTransient<FirstView>();

        builder.Services.AddTransient<SecondViewModel>();
        builder.Services.AddTransient<SecondView>();

        builder.Services.AddTransient<GetNumberViewModel>();
        builder.Services.AddTransient<GetNumberPopup>();

        return builder.Build();
	}
}
