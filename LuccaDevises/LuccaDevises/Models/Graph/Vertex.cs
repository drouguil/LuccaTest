using System.Collections.Generic;

namespace LuccaDevises.Models.Graph
{
    /// <summary>
    /// Vertex connected with edges
    /// </summary>

    public class Vertex
    {
        #region Properties

        /// <summary>
        /// Currency
        /// </summary>

        public string Currency { get; }

        /// <summary>
        /// Edges connected at the vertex
        /// </summary>

        public List<Edge> Edges { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currency">Currency</param>

        internal Vertex(string currency)
        {
            Currency = currency;
            Edges = new List<Edge>();
        }

        #endregion

        #region Equals

        /// <summary>
        /// Check if an object is equal to the edge
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns></returns>

        public override bool Equals(object obj)
        {
            if (!(obj is Vertex vertex))
                return false;
            else
            {
                return Currency != null && Currency.Equals(vertex.Currency);
            }
        }

        #endregion

        #region GetHashCode

        /// <summary>
        /// Get the hash code of the object
        /// </summary>
        /// <returns>The hash code of the object</returns>

        public override int GetHashCode()
        {
            var hashCode = -2020781480;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Currency);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Edge>>.Default.GetHashCode(Edges);
            return hashCode;
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
                "Vertex : {",
                "Currency = " + Currency,
                "Edges = ["
            };

            foreach (Edge edge in Edges)
            {
                str.Add(edge.ToString());
            }

            str.Add("] }");

            return string.Join("\n", str.ToArray());
        }

        #endregion
    }
}