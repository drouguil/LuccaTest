using LuccaDevises.Exceptions;
using LuccaDevises.Models.Data;
using System;
using System.Globalization;
using System.IO;

namespace LuccaDevises.Services
{
    /// <summary>
    /// 
    /// </summary>

    public class FileReaderService
    {
        #region Public methods

        /// <summary>
        /// Extract data from file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns></returns>

        public static Data ExtractData(string path)
        {
            StreamReader file = new StreamReader(path);

            DataToConvert dataToConvert = ExtractDataToConvert(file);

            ExchangeRate[] exchangeRates = ExtractExchangeRates(file);

            file.Close();

            return new Data(dataToConvert, exchangeRates);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Extract data to convert at the first line
        /// </summary>
        /// <param name="file">File</param>
        /// <returns></returns>

        private static DataToConvert ExtractDataToConvert(StreamReader file)
        {
            string line = file.ReadLine();

            if (line == null)
            {
                throw new DataFormatException("First line is empty");
            }

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
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>

        private static ExchangeRate[] ExtractExchangeRates(StreamReader file)
        {
            string line = file.ReadLine();

            if (line == null)
            {
                throw new DataFormatException("Second line is empty");
            }

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


            if (nbExRates == 0)
            {
                throw new DataFormatException("No exchange rate");
            }

            bool isCompleted = false;

            uint cptExRates = 0;

            ExchangeRate[] exchangeRates = new ExchangeRate[nbExRates];

            while(!isCompleted) {
                line = file.ReadLine();

                if(line == null)
                {
                    throw new DataFormatException("Number of exchange rates = " + nbExRates, "Line n°" + (cptExRates + 3) + " is empty");
                }
                exchangeRates[cptExRates] = ExtractExchangeRate(line);
                cptExRates++;
                isCompleted = cptExRates >= nbExRates;
            }

            line = file.ReadLine();

            if (line != null)
            {
                throw new DataFormatException("Number of exchange rates = " + nbExRates, "Line n°" + (cptExRates + 3) + " is not empty : " + line);
            }

            return exchangeRates;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>

        private static ExchangeRate ExtractExchangeRate(string line)
        {
            if(line.Length == 0)
            {
                throw new DataFormatException("An exchange rate line is empty");
            }

            string[] datas = line.Split(';');
            if (datas.Length == 3)
            {
                // Current device

                string dd = datas[0].Trim().ToUpper();
                if (dd.Length != 3)
                {
                    throw new DataFormatException("The initial currency (DD) have an invalid length", "DD = " + dd, "DD Length = " + dd.Length);
                }

                // Converted device

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

                if(t == 0)
                {
                    throw new DataFormatException("The conversion rate (T) is zero", "Line = " + line);
                }

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