namespace RichyMVVM.Framework.ViewModels
{
    /// <summary>
    /// Implemented by ViewModels which can launch a Popup. 
    /// </summary>
    public interface IPopUpParent
    {
        /// <summary>
        /// Called by the PopupViewModel on the IPopupParent in order to pass information from the Popup to the parent ViewModel.
        /// Code in this method should react to any information return by the Popup ViewModel.
        /// </summary>
        /// <param name="o"></param>
        void PopupClosed(object o);
    }
}
