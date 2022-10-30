using CommunityToolkit.Maui.Views;
using RichyMVVM.Framework.Services;
using RichyMVVM.ApplicationCode.ViewModels;
using RichyMVVM.Framework.ViewModels;

namespace RichyMVVM;

/// <summary>
/// App is the default implementation of Application which is created by the Visual Studio blank Maui project. Some extra properties have been added (see code below)
/// </summary>
public partial class App : Application
{

    //public NavigationService Navigation { get; set; }

    /// <summary>
    /// Contains a reference to the currently shown ViewModel
    /// </summary>
    /// <remarks>New property added to the App class</remarks>
    public IViewModel MainViewModel { get; set; }

    /// <summary>
    /// Contains a reference to the currently shown Popup. This property will be set by the Navigation Service.
    /// </summary>
    /// <remarks>New property added to the App class</remarks>
    public IPopupViewModel PopupViewModel { get; set; }

    /// <summary>
    /// The Application class contains a MainPage property which is perhaps because the default AppShell implementation is ViewFirst.
    /// Whereas the MainPage property holds a reference to the currently displayed View then this property holds a reference to the currently displayed Popup.
    /// This property will be set by the Navigation Service. This reference is required as the NavigationService will use it to close the Popup. 
    /// </summary>
    /// <remarks>New property added to the App class</remarks>
    public Popup MainPopup { get; set; }

    public App()
	{
		InitializeComponent();

        //Required - the NavigationService is a Singleton class so this method must be called to give it a reference to the App object
        NavigationService.SetApp(this);

        //Navigate to the first page of the application. Easy!
        NavigationService.NavigateTo(typeof(FirstViewModel), null);
    }
}
