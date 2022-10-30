using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using RichyMVVM.Framework.ViewModels;
using RichyMVVM.Framework.Services;
using RichyMVVM.ApplicationCode.Services;

using RichyMVVM.ApplicationCode.Models;

using System.Timers;
using Timer = System.Timers.Timer;

namespace RichyMVVM.ApplicationCode.ViewModels
{
    /// <summary>
    /// This is the first ViewModel to be shown in the application. 
    /// </summary>
    public partial class FirstViewModel : ObservableObject, IViewModel, IPopUpParent
    {


        [ObservableProperty]
        public int userValue;

        /// <summary>
        /// Timer used to update the CurrentTime property every second
        /// </summary>
        Timer _timer;

        [ObservableProperty]
        public DateTime currentTime;

        /// <summary>
        /// Refererence to UserValueModel which is a Model object used to move information about
        /// </summary>
        private UserValueModel _userValueModel;        

        /// <summary>
        /// Reference to the TimeService which is injected into this ViewModel by the Microsoft IoC container.
        /// The Service is added to the IoC container in the MauiProgram class
        /// </summary>
        /// <see cref="MauiProgram" />
        private TimeService _timeService;

        /// <summary>
        /// Notice how the constructor takes in a reference to the TimeService and this will be created by the Microsoft IoC container
        /// </summary>
        /// <param name="timeService"></param>
        public FirstViewModel(TimeService timeService)
        {
            _timeService = timeService;

            //Set the timer up. It will be started in NavigatedTo()
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            _timer.AutoReset = true;
        }

        /// <summary>
        /// When the timer has completed (after a second) then update the CurrentTime property with the time from the TimeService
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CurrentTime = _timeService.GetCurrentTime();
        }

        #region IViewModel Methods

        /// <summary>
        /// One of the IViewModel methods which is not implemented in this ViewModel.  
        /// </summary>
        /// <param name="o"></param>
        public void NavigatingTo(object o){}

        /// <summary>
        /// One of the IViewModel methods which is implemented in this ViewModel. The ViewMode is created and then before it is shown then this method is shown.
        /// In this case the Timer object that was created in the constructor is enabled. This shows how the IViewModel lifecycle events can be used to set up a 
        /// ViewModel.
        /// </summary>
        public void NavigatedTo()
        {
            _timer.Enabled = true;
        }


        /// <summary>
        /// One of the IViewModel methods which is implemented in this ViewModel. 
        /// </summary>
        public void NavigatingFrom()
        {
            _timer.Enabled = false;
        }

        /// <summary>
        /// One of the IViewModel methods which is implemented in this ViewModel. This method unregisters the event code from the Timer prior to 
        /// navigating away and show how navigation events can be used to handle a ViewModel being closed (or navigated away from)
        /// </summary>
        public void NavigatedFrom()
        {
            _timer.Elapsed -= _timer_Elapsed;
        }

        #endregion


        /// <summary>
        /// The command object to navigate to another ViewModel. Note how information is sent to the other ViewModel using the object parameter of NavigateTo (in this 
        /// case a reference to the UserViewModel object created when the Popup is closed)
        /// </summary>
        [RelayCommand]
        public void ChangePage()
        {
            NavigationService.NavigateTo(typeof(SecondViewModel), _userValueModel);
        }

        /// <summary>
        /// Launches the Popup. The NavigationService is asked to launch the Popup by specifying the ViewModel type to be used by the Popup. Information could be sent to the 
        /// Popup if required via an object. In this case no information needs to be sent so null is passed to ShowPopup.
        /// </summary>
        [RelayCommand]
        public void GetUserValue()
        { 
            NavigationService.ShowPopup(this, typeof(GetNumberViewModel), null);
        }

        #region IPopupParent Methods

        /// <summary>
        /// This method is called by the Popup view model (remember this has a reference to its parent which in this case is this ViewModel). This is how
        /// information is returned from the Popup. In a more robust implementation some checks would be done on the type of object returned and the relevant expection raised.
        /// 
        /// It can be seen that this method makes a new <see cref="UserValueModel">UserValueModel</see> object which is then passed to the SecondViewModel in the ChangePage method.
        /// </summary>
        /// <param name="o">Information passed from the Popup to this ViewModel</param>
        public void PopupClosed(object o)
        {
            //this object is passed to the SecondViewModel
            _userValueModel = new UserValueModel((int)o, CurrentTime);

            //at the same time show this information on FirstView.xaml so set the value of the ObservableProperty here too
            UserValue = (int)o;
        }

        #endregion
    }
}
