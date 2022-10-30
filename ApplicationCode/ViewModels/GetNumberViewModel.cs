using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichyMVVM.Framework.Services;
using RichyMVVM.Framework.ViewModels;


namespace RichyMVVM.ApplicationCode.ViewModels
{
    /// <summary>
    /// The ViewModel for the Popup. It must implement IPopupViewModel as this essential to get the reference to the parent ViewModel (as IPopupParent).
    /// The IPopupParent.ClosePopUp method is essential to allow this ViewModel to call PopupClosed which is the mechanism that this Popup can pass information 
    /// back to the parent ViewModel.
    /// to called 
    /// </summary>
    public partial class GetNumberViewModel : ObservableObject, IPopupViewModel
    {
        /// <summary>
        /// Property to be edited in the Popup
        /// </summary>
        [ObservableProperty]
        public int userValue;

        /// <summary>
        /// Reference to the parent ViewModel which launched this one. PopupClosed is called on this instance to pass information from this IPopupViewModel to the parent.
        /// </summary>
        IPopUpParent parentViewModel;

        /// <summary>
        /// Each IPopupViewModel would need code similar to this. The Popup XAML would have a button that allows the popup to be closed. The code below is how the
        /// Popup can both be closed and to pass any information (if required) to the parent ViewModel.
        /// </summary>
        [RelayCommand]
        public void ClosePopup()
        {
            parentViewModel.PopupClosed(UserValue);
            NavigationService.ClosePopUp();
        }

        #region IPopupViewModel Methods

        /// <summary>
        /// As the ViewModel is created by the Microsoft IoC then the IPopupParent can be not specified in the constructor as the Microsoft IoC container would attempt
        /// to resolve that Type and make its own instance. Therefore this method is used to specify the instance of the parent for an object created by the IoC container.
        /// </summary>
        /// <param name="parent"></param>
        public void SetPopupParent(IPopUpParent parent)
        {
            parentViewModel = parent;
        }

        #endregion
    }
}
