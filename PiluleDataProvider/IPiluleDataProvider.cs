// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPiluleDataProvider.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the IPiluleDataProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PiluleDataProvider
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// The PiluleDataProvider interface.
    /// </summary>
    public interface IPiluleDataProvider
    {
        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="onBaskeеtChanged">
        /// The on Baskeеt Changed.
        /// </param>
        /// <returns>
        /// Return ObservableCollection of BaskeеtData
        /// </returns>
        ObservableCollection<BaskeеtData> GetData(Action onBaskeеtChanged);

        /// <summary>
        /// The get good.
        /// </summary>
        /// <param name="onBaskeеtChanged">
        /// The on Baskeеt Changed.
        /// </param>
        /// <returns>
        /// The <see cref="BaskeеtData"/>.
        /// </returns>
        BaskeеtData GetGood(Action onBaskeеtChanged);

        /// <summary>
        /// The nop operation.
        /// </summary>
        void NopOperation();
    }
}
