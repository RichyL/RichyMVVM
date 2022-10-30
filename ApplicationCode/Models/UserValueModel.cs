using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichyMVVM.ApplicationCode.Models
{
    /// <summary>
    /// Model used to pass selection information from the FirstViewModel to SecondViewModel. Note - it's just a POCO and nothing special at all.
    /// Using private sets means it can be passed to the SecondViewModel as an immutable object and so could not be changed.
    /// </summary>
    public class UserValueModel
    {
        public int UserValue { get; private set; }

        public DateTime SelectionTime { get; private set; }

        public UserValueModel(int userValue, DateTime selectionTime)
        {
            UserValue = userValue;
            SelectionTime = selectionTime;
        }


    }
}
