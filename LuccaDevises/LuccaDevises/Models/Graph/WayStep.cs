namespace LuccaDevises.Models.Graph
{
    /// <summary>
    /// Step of the way for the initial vertex to the target
    /// </summary>

    public class WayStep
    {
        #region Properties

        /// <summary>
        /// Edge
        /// </summary>

        public Edge Step { get; }

        /// <summary>
        /// Direction of the edge
        /// </summary>

        public bool IsReverse { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isReverse">Direction of the edge</param>
        /// <param name="step"></param>

        public WayStep(Edge step, bool isReverse)
        {
            Step = step;
            IsReverse = isReverse;
        }

        #endregion

        #region ToString

        /// <summary>
        /// Get the string description of the object
        /// </summary>
        /// <returns>String description of the object</returns>

        public override string ToString()
        {
            return "WayStep : { Step = " + Step + ", IsReverse = " + IsReverse + " }";
        }

        #endregion
    }
}
