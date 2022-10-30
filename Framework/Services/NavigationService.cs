using RichyMVVM.Framework.ViewModels;
using CommunityToolkit.Maui.Views;


namespace RichyMVVM.Framework.Services
{
    /// <summary>
    /// Class responsible for ViewModel-first navigation. This class is a Singleton class uses implemntation 6 from https://csharpindepth.com/articles/singleton
    /// This class does the following:
    /// 1. handles the navigating from ViewModel to ViewModel and creating the relevant Views and binding the two together
    /// 2. handles the displaying and closing of popups
    /// </summary>
    public class NavigationService
    {
        private static NavigationService Instance { get { return lazy.Value; } }

        private static readonly Lazy<NavigationService> lazy = new Lazy<NavigationService>(() => new NavigationService());

        /// <summary>
        /// Private constructor as per Singleton pattern
        /// </summary>
        private NavigationService()
        {

        }


        /// <summary>
        /// Reference to the Application class which has the MainPage, MainViewModel, PopupViewModel and MainPopup attributes
        /// </summary>
        private static App _app;

        /// <summary>
        /// As this class implements the Singleton pattern then the constructor cannot have any arguments. This method is used to set provide this class
        /// with a reference to the App class. See the App class code for more information.
        /// </summary>
        /// <param name="app"></param>
        public static void SetApp(App app)
        {
            _app = app;
        }

        /// <summary>
        /// Creates a new ViewModel and as part of this process automatically finds the View, instantiates it and binds it to the newly created ViewModel.
        /// As part of this process information can be sent to the new ViewModel as an object
        /// </summary>
        /// <param name="viewModel">The type of ViewModel to navigate to</param>
        /// <param name="o">Information to be sent to the soon to be created ViewModel</param>
        /// <exception cref="NullReferenceException">Thrown if no App reference has been passed to the NavigationService class</exception>
        public static void NavigateTo( Type viewModel, object o)
        {
            if (_app is null) throw new NullReferenceException("The NavigationService object has not been provided with an App object to work on");

            //*************************** Make new ViewModel

            //Make the new ViewModel using the Microsoft IoC container
            IViewModel newViewModel = (IViewModel) ServiceHelper.GetService(viewModel);


            //*************************** Make new View
            
            //The View is created automatically and some assumptions are made as to where the View class will be found
            //As it is made automatically then start with the Type name of the ViewModel
            string viewModelTypeName = viewModel.AssemblyQualifiedName;

            //The value for viewModelTypeName will look something like = "RichyMVVM.ViewModels.FirstViewModel, RichyMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            //The View is expected to be in the Views folder and for the class to end with the name View

            //Change the typename to account for the expected folder name of Views
            string viewTypeName = viewModelTypeName.Replace(".ViewModels", ".Views");

            //Use the fact that the class name is followed by a comma to change the name of the class from ViewName to View without affecting the folder name already set
            viewTypeName = viewTypeName.Replace("ViewModel,", "View,");

            //This will be the new View type
            Type newViewType = Type.GetType(viewTypeName);

            //Create the View using the Microsoft IoC container
            ContentPage newView = (ContentPage) ServiceHelper.GetService(newViewType);

            //Set new viewmodel as the binding context for the new view
            newView.BindingContext=newViewModel;

            //call the ViewModel lifecycle methods 
            newViewModel.NavigatingTo(o);
            newViewModel.NavigatedTo();

            //call the ViewModel lifecycle methods on the previous ViewModel (if it exists)
            if(_app.MainViewModel!=null)
            {
                _app.MainViewModel.NavigatingFrom();
                _app.MainViewModel.NavigatedFrom();
            }

            // Navigation complete!
            _app.MainViewModel = newViewModel;
            _app.MainPage = (Page)newView;
        }


        /// <summary>
        /// Shows a Popup
        /// </summary>
        /// <param name="parent">This is the ViewModel which is launching the Popup. The Popup will call the  parents PopupClosed closed method and by doing so pass information from the Popup ViewModel back to the parent ViewModel</param>
        /// <param name="popupViewModel">The type of ViewModel used by the Popup</param>
        /// <param name="o">Information to be passed to the Popup</param>
        /// <exception cref="NullReferenceException">Thrown if no App reference has been passed to the NavigationService class</exception>
        public static void ShowPopup(IPopUpParent parent, Type popupViewModel, object o)
        {
            if (_app is null) throw new NullReferenceException("The NavigationService object has not been provided with an App object to work on");

            //*************************** Make new ViewModel

            //Make the new popup ViewModel using the Microsoft IoC container
            IPopupViewModel newViewModel = (IPopupViewModel)ServiceHelper.GetService(popupViewModel);

            //*************************** Make new View

            //The Popup is created automatically and some assumptions are made as to where the Popup class will be found
            //As it is made automatically then start with the Type name of the ViewModel
            string viewModelTypeName = popupViewModel.AssemblyQualifiedName;

            //The value for viewModelTypeName will look something like = "RichyMVVM.ViewModels.FirstViewModel, RichyMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            //The Popup is expected to be in the Popup folder and for the class to end with the name Popup

            //Change the typename to account for the expected folder name of Popups
            string viewTypeName = viewModelTypeName.Replace(".ViewModels", ".Popups");

            //Use the fact that the class name is followed by a comma to change the name of the class from ViewName to Popup without affecting the folder name already set
            viewTypeName = viewTypeName.Replace("ViewModel,", "Popup,");

            //This will be the new Popup type
            Type newViewType = Type.GetType(viewTypeName);

            //Create the Popup using the Microsoft IoC container
            Popup newView = (Popup)ServiceHelper.GetService(newViewType);

            //Set new viewmodel as new popup binding context
            newView.BindingContext = newViewModel;

            _app.MainPopup = newView;
            _app.PopupViewModel = newViewModel;

            //The SetPopupParent method is used so that the Popup can be created with the IoC container. If the Popup had a reference to the parent ViewModel then 
            //it would get this from the IoC container.
            newViewModel.SetPopupParent(parent);

            //Popup is shown!
            _app.MainPage.ShowPopup(newView);
        }

        /// <summary>
        /// Close the currently diplayed popup. This method must be called from the Popup ViewModel which handles the close command. 
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if either no App reference has been passed to the NavigationService class or no Popup is currently being shown</exception>
        public static void ClosePopUp()
        {
            if (_app is null) throw new NullReferenceException("The NavigationService object has not been provided with an App object to work on");

            if (_app.PopupViewModel is null) throw new NullReferenceException("There is no popup is currently showing");

            //In order to have a ViewModel first approach then to close the Popup this cannot be done from the Popup XAML object
            //Instead the App class has a reference to the Popup being displayed. Close is called on this which is the only way to allow ViewModel to close the View without
            //having code in the XAML code behind.
            _app.MainPopup.Close();

            //As the Popup has been closed then get rid of the PopupViewModel
            _app.PopupViewModel = null;

        }
    }
}
