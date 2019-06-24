using LuccaDevises.Exceptions;
using LuccaDevises.Models.Data;
using LuccaDevises.Models.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuccaDevises.Services
{
    /// <summary>
    /// Service for convert a currency from another
    /// </summary>

    public static class CurrencyConverterService
    {
        #region Public methods

        /// <summary>
        /// Convert a currency to another
        /// </summary>
        /// <param name="data">All data from data file</param>
        /// <returns>Converted value</returns>

        public static uint Convert(Data data)
        {
            // Initialize the graph

            List<Vertex> vertices = InitGraph(data.ExchangeRates);

            // Get the shortest path for conversion

            Path shortestConvertPath = GetShortestConvertPath(vertices, data.DataToConvert);

            // Return the converted value

            return GetConvertedValue(shortestConvertPath, data.DataToConvert);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initialize the graph with data
        /// </summary>
        /// <param name="exchangeRates">All exchange rates from data file for represent the graph</param>
        /// <returns>List of all vertices</returns>

        private static List<Vertex> InitGraph(ExchangeRate[] exchangeRates)
        {
            List<Vertex> vertices = new List<Vertex>();

            // Browse exchange rates for create edges

            foreach(ExchangeRate exchangeRate in exchangeRates)
            {
                // Initial vertex

                Vertex dd = vertices.FirstOrDefault(x => x.Currency.Equals(exchangeRate.DD));

                if(dd == null)
                {
                    dd = new Vertex(exchangeRate.DD);
                    vertices.Add(dd);
                }

                // Target vertex

                Vertex da = vertices.FirstOrDefault(x => x.Currency.Equals(exchangeRate.DA));

                if (da == null)
                {
                    da = new Vertex(exchangeRate.DA);
                    vertices.Add(da);
                }

                // Create Edge

                Edge edge = new Edge(dd, da, exchangeRate.T);

                // If doesn't already exit add edge to the initial vertex

                if(!dd.Edges.Exists(x => x.Equals(edge)))
                {
                    dd.Edges.Add(edge);
                }
                else
                {
                    throw new DataFormatException("Already existing edge for this vertex", dd.ToString(), "New = " + edge.ToString());
                }

                // If doesn't already exit add edge to the target vertex

                if (!da.Edges.Exists(x => x.Equals(edge)))
                {
                    da.Edges.Add(edge);
                }
                else
                {
                throw new DataFormatException("Already existing edge for this vertex", da.ToString(), "New = " + edge.ToString());
                }
            }

            return vertices;
        }

        /// <summary>
        /// Apply dijkstra algorithm for get the shortest path from the initial currency to the target currency 
        /// </summary>
        /// <param name="vertices">All currencies</param>
        /// <param name="dataToConvert">Data to convert</param>
        /// <returns>Shortest path from the initial currency to the target currency</returns>

        private static Path GetShortestConvertPath(List<Vertex> vertices, DataToConvert dataToConvert)
        {
            // Store distance from the initial vertex of all vertices

            Dictionary<string, Path> paths = new Dictionary<string, Path>();

            // Store if vertices are visited or not

            Dictionary<string, bool> marks = new Dictionary<string, bool>();

            // Initialize all vertex as unmarked and infinite distance value

            foreach (Vertex vertex in vertices)
            {
                paths.Add(vertex.Currency, new Path(vertex.Currency));
                marks.Add(vertex.Currency, false);
            }

            // Initialize the initial currency distance to 0

            paths[dataToConvert.D1].Dist = 0;

            // Initialize a stop condition if no vertex with minimal distance was found

            bool isFound = false;

            // Browse all vertices

            for (uint i = 0; i < vertices.Count && !isFound; i++)
            {
                // Get the vertex with the minimal distance and unmarked

                Vertex vertexMin = GetMinDist(vertices, paths, marks);

                // If the target currency was found stop browsing

                if (vertexMin.Currency.Equals(dataToConvert.D2))
                {
                    isFound = true;
                    break;
                }

                // Marks the currency

                marks[vertexMin.Currency] = true;

                // Get the path of the minimum distance vertex

                paths.TryGetValue(vertexMin.Currency, out Path path);

                // Browse neighbours of the minimun distance vertex

                foreach (Edge edge in vertexMin.Edges)
                {
                    // Get a neighbour of the minimum distance vertex

                    bool isReverse = edge.VertexEnd.Equals(vertexMin);
                    Vertex neighbour = isReverse ? edge.VertexStart : edge.VertexEnd;

                    // Get path and if he's marked

                    marks.TryGetValue(neighbour.Currency, out bool isMarked);
                    paths.TryGetValue(neighbour.Currency, out Path pathNeighbour);

                    // Add step on the path of the neighbour only if is unmarked and the minimun vertex path distance is lower than the path of the neighbour path distance

                    if (!isMarked && path.Dist < pathNeighbour.Dist)
                    {
                        pathNeighbour.AddStep(edge, isReverse, path);
                    }
                }
            }

            // If the target vertex is not found

            if(!isFound)
            {
                throw new AlgorithmException("Unreached currency : " + dataToConvert.D2);
            }

            return paths[dataToConvert.D2];
        }

        /// <summary>
        /// Get the vertex with the minimal distance and unmarked
        /// </summary>
        /// <param name="vertices">Vertices to browse</param>
        /// <param name="paths">Distances</param>
        /// <param name="marks">Marks</param>
        /// <returns>Vertex with minimal distance and unmarked</returns>

        private static Vertex GetMinDist(List<Vertex> vertices, Dictionary<string, Path> paths, Dictionary<string, bool> marks)
        {
            // Initialize the minimum distance with infinite

            uint min = int.MaxValue;
            Vertex vertexMin = null;

            // Search minimum distance vertex

            foreach (Vertex vertex in vertices)
            {
                marks.TryGetValue(vertex.Currency, out bool isMarked);
                paths.TryGetValue(vertex.Currency, out Path path);
                if (!isMarked && path.Dist <= min)
                {
                    min = path.Dist;
                    vertexMin = vertex;
                }
            }

            // If he's not found

            if(vertexMin == null)
            {
                // Logs if no vertex was found

                List<string> distsLog = new List<string>
                {
                    "Dists : ["
                };

                foreach (KeyValuePair<string, Path> entry in paths)
                {
                    distsLog.Add("[" + entry.Key + "] = " + entry.Value);
                }

                distsLog.Add("]");

                List<string> marksLog = new List<string>
                {
                    "Marks : ["
                };

                foreach (KeyValuePair<string, bool> entry in marks)
                {
                    marksLog.Add("[" + entry.Key + "] = " + entry.Value);
                }

                marksLog.Add("]");

                throw new AlgorithmException("Minimum not found", string.Join("\n", distsLog), string.Join("\n", marksLog));
            }
            
            return vertexMin;
        }

        /// <summary>
        /// Browse the vertices of the shortest path for convert value
        /// </summary>
        /// <param name="path">Shortest path</param>
        /// <param name="dataToConvert">Data to convert</param>
        /// <returns>Converted value</returns>

        private static uint GetConvertedValue(Path path, DataToConvert dataToConvert)
        {
            // Get amount to convert

            decimal amount = dataToConvert.M;

            // Browse all step of the shortest path 

            foreach (WayStep way in path.Way.AsEnumerable().Reverse())
            {
                // If the direction is reversed

                bool isReverse = way.IsReverse;

                // Get the start and the end vertex currencies

                string startCurrency = way.Step.VertexStart.Currency;
                string endCurrency = way.Step.VertexEnd.Currency;

                // Get the rate of the exchange rate

                decimal rate = Math.Round(way.Step.T, 4);

                // Calculates the multiplier

                decimal multiplier = isReverse ? Math.Round(1 / way.Step.T, 4) : rate;

                // Logs

                if (isReverse)
                {
                    Console.Write(endCurrency + " --> " + startCurrency + " : " + amount + " * (1/" + rate + ") = ");
                }
                else
                {
                    Console.Write(startCurrency + " --> " + endCurrency + " : " + amount + " * " + rate + " = ");
                }

                // Calculates the new amount

                amount *= multiplier;
                amount = Math.Round(amount, 4);

                Console.WriteLine(amount);
            }

            // Returns the amount converted

            return decimal.ToUInt32(Math.Round(amount));
        }

        #endregion
    }
}