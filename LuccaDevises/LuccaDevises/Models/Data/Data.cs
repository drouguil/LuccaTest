using System.Collections.Generic;

namespace LuccaDevises.Models.Data
{
    /// <summary>
    /// Data from the data file
    /// </summary>

    public struct Data
    {
        #region Properties

        /// <summary>
        /// Data to convert (first line)
        /// </summary>

        public DataToConvert DataToConvert { get; }

        /// <summary>
        /// Exchanges rates for conversion (second line until the end of file)
        /// </summary>

        public ExchangeRate[] ExchangeRates { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataToConvert">Data to convert</param>
        /// <param name="exchangeRates">Exchange rates</param>

        public Data(DataToConvert dataToConvert, ExchangeRate[] exchangeRates)
        {
            DataToConvert = dataToConvert;
            ExchangeRates = exchangeRates;
        }

        #endregion

        #region ToString

        /// <summary>
        /// Get the string description of the object
        /// </summary>
        /// <returns>String description of the object</returns>

        public override string ToString()
        {
            List<string> str = new List<string>
            {
                "Data : {",
                "DataToConvert = " + DataToConvert,
                "ExchangeRates = ["
            };

            foreach (ExchangeRate exchangeRate in ExchangeRates)
            {
                str.Add(exchangeRate.ToString());
            }

            str.Add("] }");

            return string.Join("\n", str.ToArray());
        }

        #endregion
    }
}