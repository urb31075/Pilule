// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugLogModeAttribute.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the DebugLogModeAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PiluleDAL
{
    using System;

    /// <summary>
    /// The debug log mode attribute.
    /// </summary>
    public class DebugLogModeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugLogModeAttribute"/> class. 
        /// </summary>
        /// <param name="debugLogMode">
        /// The dubug log file name.
        /// </param>
        public DebugLogModeAttribute(bool debugLogMode)
        {
            this.DebugLogMode = debugLogMode;
        }

        /// <summary>
        /// Gets or sets a value indicating whether debug log mode.
        /// </summary>
        public bool DebugLogMode { get; set; }
    }
}
