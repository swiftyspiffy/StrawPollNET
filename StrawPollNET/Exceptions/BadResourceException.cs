using System;
using System.Collections.Generic;
using System.Text;

namespace StrawPollNET.Exceptions
{
    /// <summary>Exception representing an invalid resource</summary>
    public class BadResourceException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadResourceException(string apiData)
            : base(apiData)
        {
        }
    }
}
