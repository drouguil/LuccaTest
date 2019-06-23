using System.Collections.Generic;

namespace LuccaDevises.Models.Graph
{
    /// <summary>
    /// Edge connecting two vertex
    /// </summary>

    public class Edge
    {
        #region Properties

        /// <summary>
        /// Initial vertex
        /// </summary>

        public Vertex VertexStart { get; }

        /// <summary>
        /// Target vertex
        /// </summary>

        public Vertex VertexEnd { get; }

        /// <summary>
        /// Weight of the edge
        /// </summary>

        public decimal T { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vertexStart">Initial vertex</param>
        /// <param name="vertexEnd">Target vertex</param>
        /// <param name="t">Weight of the edge</param>

        public Edge(Vertex vertexStart, Vertex vertexEnd, decimal t)
        {
            VertexStart = vertexStart;
            VertexEnd = vertexEnd;
            T = t;
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
            if (!(obj is Edge edge))
                return false;
            else
            {
                bool equalSS = VertexStart != null && VertexStart.Equals(edge.VertexStart);
                bool equalSE = VertexStart != null && VertexStart.Equals(edge.VertexEnd);
                bool equalEE = VertexEnd != null && VertexEnd.Equals(edge.VertexEnd);
                bool equalES = VertexEnd != null && VertexEnd.Equals(edge.VertexStart);

                return (equalSS && equalEE) || (equalSE && equalES);
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
            var hashCode = -578806220;
            hashCode = hashCode * -1521134295 + EqualityComparer<Vertex>.Default.GetHashCode(VertexStart);
            hashCode = hashCode * -1521134295 + EqualityComparer<Vertex>.Default.GetHashCode(VertexEnd);
            hashCode = hashCode * -1521134295 + T.GetHashCode();
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
            return "Edge : { VertexStart = " + VertexStart.Currency + ", VertexEnd = " + VertexEnd.Currency + ", T = " + T + " }";
        }

        #endregion
    }
}