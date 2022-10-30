using RichyMVVM.Framework.ViewModels;

namespace RichyMVVM.Framework.Services
{
    /// <summary>
    /// This interface defines the contract for a navigation service which is the service which will be responsible for navigating to a ViewModel and 
    /// creating the View and binding that View to the ViewModel. The interface is required so that during testing a navigation service mock can be created.
    /// It is not possible to apply an inteface to a static class and the navigation service implementation in this project uses static methods.
    /// </summary>
    /// <remarks>Not currently used in code - see NavigationService.cs for full documentation</remarks>
    public  interface INavigationService
    {
        /// <summary>
        /// Called by a ViewModel to navigate to another ViewModel. Information can be passed between the ViewModels via an Object.
        /// </summary>
        /// <param name="viewModel">Type of ViewModel to show in the application</param>
        /// <param name="o">Information to be passed to the ViewModel</param>
        void NavigateTo(Type viewModel, object o);

        /// <summary>
        /// Called by a ViewModel to show a Popup.
        /// </summary>
        /// <param name="parent">ViewModel which is launching the Popup</param>
        /// <param name="viewModel">The type of ViewModel associated with the Popup</param>
        /// <param name="o">Information to be passed to the Popup's ViewModel</param>
        void ShowPopup(IPopUpParent parent, Type viewModel, object o);

        /// <summary>
        /// This command is to be called by the Popup ViewModel to close the Popup.
        /// </summary>
        void ClosePopUp();
    }
}