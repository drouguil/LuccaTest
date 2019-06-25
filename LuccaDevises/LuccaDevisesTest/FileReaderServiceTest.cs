using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using LuccaDevises.Exceptions;
using LuccaDevises.Models.Data;
using LuccaDevises.Services;
using LuccaDevisesTest.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LuccaDevisesTest
{
    /// <summary>
    /// Test for the file reader service
    /// </summary>

    [TestClass]
    public class FileReaderServiceTest
    {
        #region Private attributes

        /// <summary>
        /// Allow to call private static methods for testing
        /// </summary>

        private PrivateType _privateType;

        #endregion

        #region Test initialization

        /// <summary>
        /// Initialization for test methods
        /// </summary>

        [TestInitialize]
        public void InitialisationDesTests()
        {
            // Update culture in en-US for exception messages

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            _privateType = new PrivateType(typeof(FileReaderService));
        }

        #endregion

        #region Test 

        #region DataToConvert

        #region SUCCESS

        [TestMethod]
        [TestProperty("DataToConvert", "SUCCESS")]
        public void ExtractDataToConvertCorrectFormat()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR;550;JPY");

            DataToConvert dataToConvert = (DataToConvert) _privateType.InvokeStatic("ExtractDataToConvert", streamReader);

            Assert.AreEqual(dataToConvert.D1,"EUR");
            Assert.AreEqual(dataToConvert.M, (uint) 550);
            Assert.AreEqual(dataToConvert.D2, "JPY");
        }

        [TestMethod]
        [TestProperty("DataToConvert", "SUCCESS")]
        public void ExtractDataToConvertTrim()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile(" EUR   ;550   ; JPY ");

            DataToConvert dataToConvert = (DataToConvert) _privateType.InvokeStatic("ExtractDataToConvert", streamReader);

            Assert.AreEqual(dataToConvert.D1, "EUR");
            Assert.AreEqual(dataToConvert.M, (uint) 550);
            Assert.AreEqual(dataToConvert.D2, "JPY");
        }

        #endregion SUCCESS

        #region ERROR

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertInvalidFormatAmount()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR;INVALID;JPY");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The amount to convert (M) have an invalid format",
                "M = INVALID",
                "Error : Input string was not in a correct format."
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertEmpty()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            Assert.AreEqual(exception.InnerException.Message, "First line is empty");
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertWrongDelimiter()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR,550,JPY");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "First line have an invalid format or an invalid data length",
                "Line = EUR,550,JPY",
                "Data Length = 1"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertTooShortDataLength()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR;550");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "First line have an invalid format or an invalid data length",
                "Line = EUR;550",
                "Data Length = 2"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertTooLongDataLength()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR;550;JPY;USD");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "First line have an invalid format or an invalid data length",
                "Line = EUR;550;JPY;USD",
                "Data Length = 4"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertTooShortD1Length()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EU;550;JPY");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The initial currency (D1) have an invalid length",
                "D1 = EU",
                "D1 Length = 2"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertTooLongD1Length()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EURO;550;JPY");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The initial currency (D1) have an invalid length",
                "D1 = EURO",
                "D1 Length = 4"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertTooShortD2Length()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR;550;JY");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The target currency (D2) have an invalid length",
                "D2 = JY",
                "D2 Length = 2"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertTooLongD2Length()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR;550;JAPY");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The target currency (D2) have an invalid length",
                "D2 = JAPY",
                "D2 Length = 4"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertZeroAmount()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR;0;JPY");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The amount to convert (M) is zero",
                "Line = EUR;0;JPY"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertEmptyAmount()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR;;JPY");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The amount to convert (M) have an invalid format",
                "M = ",
                "Error : Input string was not in a correct format."
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("DataToConvert", "ERROR")]
        public void ExtractDataToConvertNegativeAmount()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("EUR;-1;JPY");

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractDataToConvert", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The amount to convert (M) have an invalid format",
                "M = -1",
                "Error : Value was either too large or too small for a UInt32."
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        #endregion

        #endregion

        #region ExtractExchangeRates

        #region SUCCESS

        [TestMethod]
        [TestProperty("ExtractExchangeRates", "SUCCESS")]
        public void ExtractExchangeRatesCorrectFormat()
        {
            List<string> contents = new List<string>()
            {
                "6",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571"
            };

            StreamReader streamReader = MockStreamReaderService.MockContentFile(string.Join("\n", contents));

            ExchangeRate[] exchangeRates = (ExchangeRate[]) _privateType.InvokeStatic("ExtractExchangeRates", streamReader);

            // AUD;CHF;0.9661

            Assert.AreEqual(exchangeRates[0].DD, "AUD");
            Assert.AreEqual(exchangeRates[0].DA, "CHF");
            Assert.AreEqual(exchangeRates[0].T, (decimal) 0.9661);

            // JPY;KRW;13.1151

            Assert.AreEqual(exchangeRates[1].DD, "JPY");
            Assert.AreEqual(exchangeRates[1].DA, "KRW");
            Assert.AreEqual(exchangeRates[1].T, (decimal) 13.1151);

            // EUR;CHF;1.2053

            Assert.AreEqual(exchangeRates[2].DD, "EUR");
            Assert.AreEqual(exchangeRates[2].DA, "CHF");
            Assert.AreEqual(exchangeRates[2].T, (decimal) 1.2053);

            // AUD;JPY;86.0305

            Assert.AreEqual(exchangeRates[3].DD, "AUD");
            Assert.AreEqual(exchangeRates[3].DA, "JPY");
            Assert.AreEqual(exchangeRates[3].T, (decimal) 86.0305);

            // EUR;USD;1.2989

            Assert.AreEqual(exchangeRates[4].DD, "EUR");
            Assert.AreEqual(exchangeRates[4].DA, "USD");
            Assert.AreEqual(exchangeRates[4].T, (decimal) 1.2989);

            // JPY;INR;0.6571

            Assert.AreEqual(exchangeRates[5].DD, "JPY");
            Assert.AreEqual(exchangeRates[5].DA, "INR");
            Assert.AreEqual(exchangeRates[5].T, (decimal) 0.6571);
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRates", "SUCCESS")]
        public void ExtractExchangeRatesTrim()
        {
            List<string> contents = new List<string>()
            {
                "  6  ",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571"
            };

            StreamReader streamReader = MockStreamReaderService.MockContentFile(string.Join("\n", contents));

            ExchangeRate[] exchangeRates = (ExchangeRate[]) _privateType.InvokeStatic("ExtractExchangeRates", streamReader);

            // AUD;CHF;0.9661

            Assert.AreEqual(exchangeRates[0].DD, "AUD");
            Assert.AreEqual(exchangeRates[0].DA, "CHF");
            Assert.AreEqual(exchangeRates[0].T, (decimal) 0.9661);

            // JPY;KRW;13.1151

            Assert.AreEqual(exchangeRates[1].DD, "JPY");
            Assert.AreEqual(exchangeRates[1].DA, "KRW");
            Assert.AreEqual(exchangeRates[1].T, (decimal) 13.1151);

            // EUR;CHF;1.2053

            Assert.AreEqual(exchangeRates[2].DD, "EUR");
            Assert.AreEqual(exchangeRates[2].DA, "CHF");
            Assert.AreEqual(exchangeRates[2].T, (decimal) 1.2053);

            // AUD;JPY;86.0305

            Assert.AreEqual(exchangeRates[3].DD, "AUD");
            Assert.AreEqual(exchangeRates[3].DA, "JPY");
            Assert.AreEqual(exchangeRates[3].T, (decimal) 86.0305);

            // EUR;USD;1.2989

            Assert.AreEqual(exchangeRates[4].DD, "EUR");
            Assert.AreEqual(exchangeRates[4].DA, "USD");
            Assert.AreEqual(exchangeRates[4].T, (decimal) 1.2989);

            // JPY;INR;0.6571

            Assert.AreEqual(exchangeRates[5].DD, "JPY");
            Assert.AreEqual(exchangeRates[5].DA, "INR");
            Assert.AreEqual(exchangeRates[5].T, (decimal) 0.6571);
        }

        #endregion

        #region ERROR

        [TestMethod]
        [TestProperty("ExtractExchangeRates", "ERROR")]
        public void ExtractExchangeRatesEmpty()
        {
            StreamReader streamReader = MockStreamReaderService.MockContentFile("");
            
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRates", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));
            Assert.AreEqual(exception.InnerException.Message, "Second line is empty");
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRates", "ERROR")]
        public void ExtractExchangeRatesTooShortDataLength()
        {
            List<string> contents = new List<string>()
            {
                "6",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989"
            };

            StreamReader streamReader = MockStreamReaderService.MockContentFile(string.Join("\n", contents));

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRates", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "Number of exchange rates = 6",
                "Line n°8 is empty"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRates", "ERROR")]
        public void ExtractExchangeRatesTooLongDataLength()
        {
            List<string> contents = new List<string>()
            {
                "6",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571",
                "EUR;ROU;79.2900"
            };

            StreamReader streamReader = MockStreamReaderService.MockContentFile(string.Join("\n", contents));

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRates", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "Number of exchange rates = 6",
                "Line n°9 is not empty : EUR;ROU;79.2900"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRates", "ERROR")]
        public void ExtractExchangeRatesZeroCount()
        {
            List<string> contents = new List<string>()
            {
                "0",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571"
            };

            StreamReader streamReader = MockStreamReaderService.MockContentFile(string.Join("\n", contents));

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRates", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));
            Assert.AreEqual(exception.InnerException.Message, "No exchange rate");
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRates", "ERROR")]
        public void ExtractExchangeRatesNegativeCount()
        {
            List<string> contents = new List<string>()
            {
                "-1",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571"
            };

            StreamReader streamReader = MockStreamReaderService.MockContentFile(string.Join("\n", contents));

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRates", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The number of exchange rates have an invalid format",
                "nbExRates = -1",
                "Error : Value was either too large or too small for a UInt32."
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRates", "ERROR")]
        public void ExtractExchangeRatesInvalidFormatCount()
        {
            List<string> contents = new List<string>()
            {
                "INVALID",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571"
            };

            StreamReader streamReader = MockStreamReaderService.MockContentFile(string.Join("\n", contents));

            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRates", streamReader));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The number of exchange rates have an invalid format",
                "nbExRates = INVALID",
                "Error : Input string was not in a correct format."
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        #endregion

        #endregion

        #region ExtractExchangeRate

        #region SUCCESS

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "SUCCESS")]
        public void ExtractExchangeRateCorrectFormat()
        {
            ExchangeRate exchangeRate = (ExchangeRate) _privateType.InvokeStatic("ExtractExchangeRate", "AUD;CHF;0.9661");

            Assert.AreEqual(exchangeRate.DD, "AUD");
            Assert.AreEqual(exchangeRate.DA, "CHF");
            Assert.AreEqual(exchangeRate.T, (decimal)0.9661);
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "SUCCESS")]
        public void ExtractExchangeRateTrim()
        {
            ExchangeRate exchangeRate = (ExchangeRate) _privateType.InvokeStatic("ExtractExchangeRate", " AUD  ;  CHF;0.9661  ");

            Assert.AreEqual(exchangeRate.DD, "AUD");
            Assert.AreEqual(exchangeRate.DA, "CHF");
            Assert.AreEqual(exchangeRate.T, (decimal)0.9661);
        }

        #endregion

        #region ERROR

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateEmpty()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", ""));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            Assert.AreEqual(exception.InnerException.Message, "An exchange rate line is empty");
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateTooShortDDLength()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AU;CHF;0.9661"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The initial currency (DD) have an invalid length",
                "DD = AU",
                "DD Length = 2"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateTooLongDDLength()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AUDI;CHF;0.9661"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The initial currency (DD) have an invalid length",
                "DD = AUDI",
                "DD Length = 4"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateTooShortDALength()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AUD;CH;0.9661"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The target currency (DA) have an invalid length",
                "DA = CH",
                "DA Length = 2"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateTooLongDALength()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AUD;CHEF;0.9661"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The target currency (DA) have an invalid length",
                "DA = CHEF",
                "DA Length = 4"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateZeroRate()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AUD;CHF;0"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The conversion rate (T) is zero",
                "Line = AUD;CHF;0"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateNegativeRate()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AUD;CHF;-1"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The conversion rate (T) is negative",
                "Line = AUD;CHF;-1"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateInvalidFormatRate()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AUD;CHF;INVALID"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The conversion rate (T) have an invalid format",
                "T = INVALID",
                "Error : Input string was not in a correct format."
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateEmptyRate()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AUD;CHF;"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "The conversion rate (T) have an invalid format",
                "T = ",
                "Error : Input string was not in a correct format."
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateTooShortDataLength()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AUD;0.9661"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "Exchange rate have an invalid format or an invalid data length",
                "Line = AUD;0.9661",
                "Data Length = 2"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        [TestMethod]
        [TestProperty("ExtractExchangeRate", "ERROR")]
        public void ExtractExchangeRateTooLongDataLength()
        {
            TargetInvocationException exception = Assert.ThrowsException<TargetInvocationException>(() => _privateType.InvokeStatic("ExtractExchangeRate", "AUD;CHF;JPY;0.9661"));
            Assert.IsInstanceOfType(exception.InnerException, typeof(DataFormatException));

            List<string> messages = new List<string>()
            {
                "Exchange rate have an invalid format or an invalid data length",
                "Line = AUD;CHF;JPY;0.9661",
                "Data Length = 4"
            };

            Assert.AreEqual(exception.InnerException.Message, string.Join("\n", messages));
        }

        #endregion

        #endregion

        #endregion
    }
}
