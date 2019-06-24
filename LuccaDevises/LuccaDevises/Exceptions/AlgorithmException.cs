using System;

namespace LuccaDevises.Exceptions
{
    /// <summary>
    /// Exception for errors occured by algorithm process
    /// </summary>

    [Serializable]
    public class AlgorithmException : Exception
    {
        #region Constructor

        /// <summary>
        /// Constructor with multiples messages
        /// </summary>
        /// <param name="message">Logs</param>

        public AlgorithmException(params string[] message) : base(string.Join("\n", message))
        {
        }

        #endregion
    }
}