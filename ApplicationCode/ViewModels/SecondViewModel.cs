using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RichyMVVM.ApplicationCode.Models;
using RichyMVVM.Framework.Services;
using RichyMVVM.Framework.ViewModels;


namespace RichyMVVM.ApplicationCode.ViewModels
{
    /// <summary>
    /// This is the second ViewModel shown in the application and is used to demonstrate both ViewModel first navigation and the ability to pass
    /// information from one ViewModel to another as part of that navigation
    /// </summary>
    public partial class SecondViewModel : ObservableObject, IViewModel
    {
 
        /// <summary>
        /// Reference to the UserValueModel passed this ViewModel in the NavigatingTo method
        /// </summary>
        /// <see cref="UserValueModel"/>
        UserValueModel _userValueModel;

        public SecondViewModel()
        {

        }

        
        /// <summary>
        /// Property used to show information passed from FirstViewModel
        /// </summary>
        [ObservableProperty]
        public string userValue;

        #region IViewModel Methods

        /// <summary>
        /// An IViewModel method which is not implemented in this case.
        /// </summary>
        public void NavigatedFrom(){}

        /// <summary>
        /// An IViewModel method which is not implemented in this case.
        /// </summary>
        public void NavigatedTo(){}


        /// <summary>
        /// An IViewModel method which is not implemented in this case.
        /// </summary>
        public void NavigatingFrom(){}

        /// <summary>
        /// This would be the most likely implemented IViewModel method. In this case FirstViewModel has passed information to this ViewModel via the object.
        /// The value assigned to UserValue depends on the state of the object that is passed.
        /// </summary>
        /// <param name="o"></param>
        public void NavigatingTo(object o)
        {
            if (o is not null)
            { 
                _userValueModel = (UserValueModel)o;
                UserValue = $"Value of {_userValueModel.UserValue} was chosen at {_userValueModel.SelectionTime.ToLocalTime()}";
            }
            else
            {
                UserValue = "No selection was made";
            }

        }

        #endregion

        /// <summary>
        /// A Command that allows the user to navigate back to the FirstViewModel. There is no information passed to FirstViewModel as seen by the null argument value in NavigateTo.
        /// </summary>
        [RelayCommand]
        public void ChangePage()
        {
            NavigationService.NavigateTo(typeof(FirstViewModel), null);
        }
    }
}
