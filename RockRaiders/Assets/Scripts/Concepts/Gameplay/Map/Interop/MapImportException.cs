using System;

namespace Assets.Scripts
{
    public class MapImportException : Exception
    {
        public MapImportException(string message) : base(message)
        {
        }

        public MapImportException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}