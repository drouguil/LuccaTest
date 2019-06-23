using LuccaDevises.Exceptions;
using LuccaDevises.Models.Data;
using LuccaDevises.Models.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuccaDevises.Services
{
    /// <summary>
    /// 
    /// </summary>

    public static class CurrencyConverterService
    {
        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public static uint Convert(Data data)
        {
            List<Vertex> vertices = InitGraph(data.ExchangeRates);
            Path shortestConvertPath = GetShortestConvertPath(vertices, data.DataToConvert);
            return GetConvertedValue(shortestConvertPath, data.DataToConvert);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeRates"></param>
        /// <returns></returns>

        private static List<Vertex> InitGraph(ExchangeRate[] exchangeRates)
        {
            List<Vertex> vertices = new List<Vertex>();

            foreach(ExchangeRate exchangeRate in exchangeRates)
            {
                Vertex dd = vertices.FirstOrDefault(x => x.Currency.Equals(exchangeRate.DD));

                if(dd == null)
                {
                    dd = new Vertex(exchangeRate.DD);
                    vertices.Add(dd);
                }

                Vertex da = vertices.FirstOrDefault(x => x.Currency.Equals(exchangeRate.DA));

                if (da == null)
                {
                    da = new Vertex(exchangeRate.DA);
                    vertices.Add(da);
                }

                Edge edge = new Edge(dd, da, exchangeRate.T);

                if(!dd.Edges.Exists(x => x.Equals(edge)))
                {
                    dd.Edges.Add(edge);
                }
                else
                {
                    throw new DataFormatException("Already existing edge for this vertex", dd.ToString(), "New = " + edge.ToString());
                }

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
        /// 
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="dataToConvert"></param>

        private static Path GetShortestConvertPath(List<Vertex> vertices, DataToConvert dataToConvert)
        {

            Dictionary<string, Path> paths = new Dictionary<string, Path>();
            Dictionary<string, bool> marks = new Dictionary<string, bool>();

            foreach (Vertex vertex in vertices)
            {
                paths.Add(vertex.Currency, new Path(vertex.Currency));
                marks.Add(vertex.Currency, false);
            }

            paths[dataToConvert.D1].Dist = 0;

            bool isFound = false;

            for (uint i = 0; i < vertices.Count && !isFound; i++)
            {
                Vertex vertexMin = GetMinDist(vertices, paths, marks);

                if (vertexMin.Currency.Equals(dataToConvert.D2))
                {
                    isFound = true;
                    break;
                }

                marks[vertexMin.Currency] = true;

                paths.TryGetValue(vertexMin.Currency, out Path path);

                foreach (Edge edge in vertexMin.Edges)
                {
                    bool isReverse = edge.VertexEnd.Equals(vertexMin);
                    Vertex neighbour = isReverse ? edge.VertexStart : edge.VertexEnd;

                    marks.TryGetValue(neighbour.Currency, out bool isMarked);
                    paths.TryGetValue(neighbour.Currency, out Path pathNeighbour);

                    if (!isMarked && path.Dist < pathNeighbour.Dist)
                    {
                        pathNeighbour.AddStep(edge, isReverse, path);
                    }
                }
            }

            if(!isFound)
            {
                throw new AlgorithmException("Unreached devise : " + dataToConvert.D2);
            }

            return paths[dataToConvert.D2];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="paths"></param>
        /// <param name="marks"></param>
        /// <returns></returns>

        private static Vertex GetMinDist(List<Vertex> vertices, Dictionary<string, Path> paths, Dictionary<string, bool> marks)
        {
            uint min = int.MaxValue;
            Vertex vertexMin = null;

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

            if(vertexMin == null)
            {
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
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dataToConvert"></param>
        /// <returns></returns>

        private static uint GetConvertedValue(Path path, DataToConvert dataToConvert)
        {
            decimal amount = dataToConvert.M;
            foreach (WayStep way in path.Way.AsEnumerable().Reverse())
            {
                decimal multiplier = way.IsReverse ? Math.Round(1 / way.Step.T, 4) : Math.Round(way.Step.T, 4);
                amount *= multiplier;
                amount = Math.Round(amount, 4);
            }

            return decimal.ToUInt32(Math.Round(amount));
        }

        #endregion
    }
}