using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichyMVVM.ApplicationCode.Services
{
    /// <summary>
    /// Simple service which returns the current time for use in ViewModels
    /// </summary>
    public class TimeService
    {
        /// <summary>
        /// Returns the current time. Amazingly it doesn't feel the need to do this as a string!
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
