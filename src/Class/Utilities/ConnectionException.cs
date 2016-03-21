using System;
using Microsoft.Phone.Net.NetworkInformation;

namespace Tap.Class.Utilities
{
    internal class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message)
        {
        }
    }
}