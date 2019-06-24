using LuccaDevises.Exceptions;
using LuccaDevises.Models.Data;
using System;
using System.Globalization;
using System.IO;

namespace LuccaDevises.Services
{
    /// <summary>
    /// Service for extract data from the data file
    /// </summary>

    public class FileReaderService
    {
        #region Public methods

        /// <summary>
        /// Extract data from file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Data form data file</returns>

        public static Data ExtractData(string path)
        {
            // Get file

            StreamReader file = new StreamReader(path);

            // Get data to convert

            DataToConvert dataToConvert = ExtractDataToConvert(file);

            // Get exchange rates

            ExchangeRate[] exchangeRates = ExtractExchangeRates(file);

            // Close file reading

            file.Close();

            // Return data from file

            return new Data(dataToConvert, exchangeRates);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Extract data to convert from data file at the first line
        /// </summary>
        /// <param name="file">Data file</param>
        /// <returns>Data to convert</returns>

        private static DataToConvert ExtractDataToConvert(StreamReader file)
        {
            // Get the first line

            string line = file.ReadLine();

            // If the line is empty

            if (line == null)
            {
                throw new DataFormatException("First line is empty");
            }

            // Browse line for extract data if the data length is 3

            string[] datas = line.Split(';');
            if(datas.Length == 3)
            {
                // Initial currency

                string d1 = datas[0].Trim().ToUpper();
                if(d1.Length != 3)
                {
                    throw new DataFormatException("The initial currency (D1) have an invalid length", "D1 = " + d1, "D1 Length = " + d1.Length);
                }

                // Amount to convert

                uint m;
                string mText = datas[1].Trim();
                try
                {
                    m = uint.Parse(mText);
                }
                catch (Exception e)
                {
                    throw new DataFormatException("The amount to convert (M) have an invalid format", "M = " + mText, "Error : " + e.Message);
                }

                if (m == 0)
                {
                    throw new DataFormatException("The amount to convert (M) is zero", "Line = " + line);
                }

                // Target currency

                string d2 = datas[2].Trim().ToUpper();
                if (d2.Length != 3)
                {
                    throw new DataFormatException("The target currency (D2) have an invalid length", "D2 = " + d2, "D2 Length = " + d2.Length);
                }
                return new DataToConvert(d1, m, d2);
            }
            else
            {
                throw new DataFormatException("First line have an invalid format or an invalid data length", "Line = " + line, "Data Length = " + datas.Length);
            }
        }

        /// <summary>
        /// Extract exchange rates from data file
        /// </summary>
        /// <param name="file">Data file</param>
        /// <returns>Exchange rates</returns>

        private static ExchangeRate[] ExtractExchangeRates(StreamReader file)
        {
            // Get the second line

            string line = file.ReadLine();

            // If the line is empty

            if (line == null)
            {
                throw new DataFormatException("Second line is empty");
            }

            // Get number of exchange rates

            uint nbExRates;
            string nbExRatesText = line.Trim();

            try
            {
                nbExRates = uint.Parse(nbExRatesText);
            }
            catch (Exception e)
            {
                throw new DataFormatException("The number of exchange rates have an invalid format", "nbExRates = " + nbExRatesText, "Error : " + e.Message);
            }

            // If there is no exchange rate

            if (nbExRates == 0)
            {
                throw new DataFormatException("No exchange rate");
            }

            // Initialize browsing data

            uint cptExRates = 0;
            bool isCompleted = false;

            // Initialize the array of exchange rates

            ExchangeRate[] exchangeRates = new ExchangeRate[nbExRates];

            // Browse exchange rates lines

            while(!isCompleted) {
                line = file.ReadLine();

                // if line is empty

                if(line == null)
                {
                    throw new DataFormatException("Number of exchange rates = " + nbExRates, "Line n°" + (cptExRates + 3) + " is empty");
                }

                // Extract exchange rate data

                exchangeRates[cptExRates] = ExtractExchangeRate(line);
                
                // Update browsing data

                cptExRates++;
                isCompleted = cptExRates >= nbExRates;
            }

            // Check if the file have extra lines

            line = file.ReadLine();

            if (line != null)
            {
                throw new DataFormatException("Number of exchange rates = " + nbExRates, "Line n°" + (cptExRates + 3) + " is not empty : " + line);
            }

            return exchangeRates;
        }

        /// <summary>
        /// Extract exchange rate from a line of data file
        /// </summary>
        /// <param name="line">Line of data file with the exchange rate</param>
        /// <returns>Exchange rate</returns>

        private static ExchangeRate ExtractExchangeRate(string line)
        {
            // If line is empty

            if(line.Length == 0)
            {
                throw new DataFormatException("An exchange rate line is empty");
            }

            string[] datas = line.Split(';');

            // If data have the correct length

            if (datas.Length == 3)
            {
                // Initial currency

                string dd = datas[0].Trim().ToUpper();
                if (dd.Length != 3)
                {
                    throw new DataFormatException("The initial currency (DD) have an invalid length", "DD = " + dd, "DD Length = " + dd.Length);
                }

                // Target currency

                string da = datas[1].Trim().ToUpper();
                if (da.Length != 3)
                {
                    throw new DataFormatException("The target currency (DA) have an invalid length", "DA = " + da, "DA Length = " + da.Length);
                }

                // Amount to convert

                decimal t;
                string tText = datas[2].Trim();
                try
                {
                    t = decimal.Parse(tText, NumberStyles.Any, new CultureInfo("en-US"));
                }
                catch (Exception e)
                {
                    throw new DataFormatException("The conversion rate (T) have an invalid format", "T = " + tText, "Error : " + e.Message);
                }

                // If amount is zero

                if(t == 0)
                {
                    throw new DataFormatException("The conversion rate (T) is zero", "Line = " + line);
                }

                // If amount is negative

                if(t < 0)
                {
                    throw new DataFormatException("The conversion rate (T) is negative", "Line = " + line);
                }

                return new ExchangeRate(dd, da, t);
            }
            else
            {
                throw new DataFormatException("Exchange rate have an invalid format or an invalid data length", "Line = " + line, "Data Length = " + datas.Length);
            }
        }

        #endregion
    }
}