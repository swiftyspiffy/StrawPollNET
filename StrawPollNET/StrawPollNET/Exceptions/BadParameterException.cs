using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrawPollNET.Exceptions
{
    /// <summary>Exception representing an invalid parameter</summary>
    public class BadParameterException : Exception
    {
        /// <summary>Exception constructor</summary>
        public BadParameterException(string apiData)
            : base(apiData)
        {
        }
    }
}
