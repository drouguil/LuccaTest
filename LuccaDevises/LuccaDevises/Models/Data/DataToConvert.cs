namespace LuccaDevises.Models.Data
{
    /// <summary>
    /// Data to convert
    /// </summary>

    public struct DataToConvert
    {
        #region Properties

        /// <summary>
        /// Initial currency
        /// </summary>

        public string D1 { get; }

        /// <summary>
        /// Amount of the initial currency
        /// </summary>

        public uint M { get; }

        /// <summary>
        /// Target currency
        /// </summary>

        public string D2 { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="d1">Initial currency</param>
        /// <param name="m">Amount of the initial currency</param>
        /// <param name="d2">Target currency</param>

        public DataToConvert(string d1, uint m, string d2)
        {
            D1 = d1;
            M = m;
            D2 = d2;
        }

        #endregion

        #region ToString

        /// <summary>
        /// Get the string description of the object
        /// </summary>
        /// <returns>String description of the object</returns>

        public override string ToString()
        {
            return "Data to convert : { D1 = " + D1 + ", M = " + M + ", D2 = " + D2 + " }";
        }

        #endregion
    }
}