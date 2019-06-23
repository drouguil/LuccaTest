using LuccaDevises.Exceptions;
using LuccaDevises.Models.Data;
using LuccaDevises.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace LuccaDevises
{
    /// <summary>
    /// 
    /// </summary>

    class Program
    {
        #region Main

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>

        static void Main(string[] args)
        {
            if (args.Length > 0) {
                try
                {
                    Data data = FileReaderService.ExtractData(args[0]);
                    uint amountConverted = CurrencyConverterService.Convert(data);
                    Console.Write(amountConverted);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("An error has occured during the file reading !");
                }
                catch (DataFormatException e)
                {
                    Console.WriteLine(e.Message);
                    List<string> log = new List<string>
                    {
                        "Invalid data format !",
                        "",
                        "Reminder of the correct format :",
                        "",
                        "XXX;M;YYY",
                        "N",
                        "AAA;BBB;T.TTTT",
                        "CCC;DDD;U.UUUU",
                        "EEE;FFF;V.VVVV",
                        "... N times"
                    };

                    Console.WriteLine(string.Join("\n", log.ToArray()));
                }
                catch (AlgorithmException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("An error has occured during the dijkstra algorithm !");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Uncaught error !");
                }

                Console.ReadLine();

            } else {
                throw new FileNotFoundException("File name missed !");
            }
        }

        #endregion
    }
}
