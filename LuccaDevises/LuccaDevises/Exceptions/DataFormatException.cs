using System;

namespace LuccaDevises.Exceptions
{
    /// <summary>
    /// Exception for errors occured by invalid format of data
    /// </summary>

    [Serializable]
    public class DataFormatException : Exception
    {
        #region Constructor

        /// <summary>
        /// Constructor with multiples messages
        /// </summary>
        /// <param name="message">Logs</param>

        internal DataFormatException(params string[] message): base(string.Join("\n", message))
        {
        }

        #endregion
    }
}