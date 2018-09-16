// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugLogFileNameAttribute.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the DebugLogFileNameAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PiluleDAL
{
    using System;

    /// <summary>
    /// The debug log file name attribute.
    /// </summary>
    public class DebugLogFileNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugLogFileNameAttribute"/> class.
        /// </summary>
        /// <param name="debugLogFileName">
        /// The dubug log file name.
        /// </param>
        public DebugLogFileNameAttribute(string debugLogFileName)
        {
            this.DebugLogFileName = debugLogFileName;
        }

        /// <summary>
        /// Gets or sets the dubug log file name.
        /// </summary>
        public string DebugLogFileName { get; set; }
    }
}
