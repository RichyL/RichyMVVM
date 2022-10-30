using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichyMVVM.Framework.ViewModels
{
    /// <summary>
    /// Interface for all ViewModels using RichyMVVM.  Contains a number of ViewModel life cycle methods that are called during navigation. It is not a requirement 
    /// that all these methods do something.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Called by the NavigationService prior to navigating to the ViewModel
        /// </summary>
        /// <param name="o">Information to be passed to the ViewModel</param>
        void NavigatingTo(object o);

        /// <summary>
        /// Called by the NavigationService after navigating to the ViewModel
        /// </summary>
        void NavigatedTo();

        /// <summary>
        /// Called by the NavigationService prior to navigating away from the ViewModel
        /// </summary>
        void NavigatingFrom();

        /// <summary>
        /// Called by the NavigationService prior after navigating away from the ViewModel
        /// </summary>
        void NavigatedFrom();
        
    }
}
