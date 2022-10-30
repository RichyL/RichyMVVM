using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichyMVVM.Framework.ViewModels
{
    /// <summary>
    /// Implemented by any ViewModel used in a Popup. 
    /// </summary>
    public interface IPopupViewModel
    {

        /// <summary>
        /// As the Microsoft IoC Container is used to create the ViewModel then the parent ViewModel cannot be passed in the constructor otherwise the parent
        /// ViewModel will be resolved by the IoC container. Therefore this method is used to set the parent ViewModel on the Popup ViewModel.
        /// </summary>
        /// <param name="parent">Parent ViewModel launching the Popup</param>
        void SetPopupParent(IPopUpParent parent);
    }
}
