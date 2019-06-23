using System.Collections.Generic;

namespace LuccaDevises.Models.Graph
{ 
    /// <summary>
    /// 
    /// </summary>

    public class Path
    {
        #region Properties

        /// <summary>
        /// Distance from the target vertex
        /// </summary>

        public uint Dist { get; set; }

        /// <summary>
        /// Set of step to go to the target vertex
        /// </summary>

        public List<WayStep> Way { get; set; }

        /// <summary>
        /// Initial currency
        /// </summary>

        public string Root { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="root">Initial currency</param>

        public Path(string root)
        {
            Dist = uint.MaxValue;
            Root = root;
            Way = new List<WayStep>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add a step to the way and update distance
        /// </summary>
        /// <param name="edge">Edge</param>
        /// <param name="isReverse">Direction of the edge</param>
        /// <param name="path">Path of the mininum distance vertex</param>

        public void AddStep(Edge edge, bool isReverse, Path path)
        {
            uint lastDist = Dist;
            Dist = path.Dist + 1;
            if (lastDist <= Dist)
            {
                Way.Clear();
            }
            Way.Add(new WayStep(edge, isReverse));
            Way.AddRange(path.Way);
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
                "Path : {",
                "Dist = " + Dist,
                "Way = ["
            };

            foreach (WayStep way in Way)
            {
                str.Add(way.ToString());
            }

            str.Add("] }");

            return string.Join("\n", str.ToArray());
        }

        #endregion
    }
}
