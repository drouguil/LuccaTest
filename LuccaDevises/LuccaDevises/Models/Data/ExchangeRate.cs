namespace LuccaDevises.Models.Data
{
    /// <summary>
    /// Conversion from an currency to another
    /// </summary>

    public struct ExchangeRate
    {
        #region Properties

        /// <summary>
        /// Initial currency
        /// </summary>

        public string DD { get; }

        /// <summary>
        /// Target currency
        /// </summary>

        public string DA { get; }

        /// <summary>
        /// Conversion rate
        /// </summary>

        public decimal T { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dd">Initial currency</param>
        /// <param name="da">Target currency</param>
        /// <param name="t">Conversion rate</param>

        public ExchangeRate(string dd, string da, decimal t)
        {
            DD = dd;
            DA = da;
            T = t;
        }

        #endregion

        #region ToString

        /// <summary>
        /// Get the string description of the object
        /// </summary>
        /// <returns>String description of the object</returns>

        public override string ToString()
        {
            return "Exchange rate : { DD = " + DD + ", DA = " + DA + ", T = " + T + " }";
        }

        #endregion
    }
}