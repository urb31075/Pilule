// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkDataProvider.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   The work data provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PiluleDataProvider
{
    using System;
    using System.Collections.ObjectModel;

    /// <inheritdoc />
    /// <summary>
    /// The work data provider.
    /// </summary>
    public class WorkDataProvider : IPiluleDataProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="onBaskeеtChanged">
        /// The on baskeеt changed.
        /// </param>
        /// <returns>
        /// Return Work ObservableCollection of BaskeеtData
        /// </returns>
        public ObservableCollection<BaskeеtData> GetData(Action onBaskeеtChanged)
        {
            var baskeеt = new ObservableCollection<BaskeеtData>
                              {
                                  new BaskeеtData(onBaskeеtChanged) { Id = 0, Name = "Work Good №1", Amount = 1, Price = 2 },
                                  new BaskeеtData(onBaskeеtChanged) { Id = 1, Name = "Work Good №2", Amount = 2, Price = 4 },
                                  new BaskeеtData(onBaskeеtChanged) { Id = 2, Name = "Work Good №3", Amount = 3, Price = 8 },
                                  new BaskeеtData(onBaskeеtChanged) { Id = 3, Name = "Work Good №4", Amount = 4, Price = 16 }
                              };
            return baskeеt;
        }

        /// <inheritdoc />
        /// <summary>
        /// The get good.
        /// </summary>
        /// <param name="onBaskeеtChanged">
        /// The on baskeеt changed.
        /// </param>
        /// <returns>
        /// The <see cref="T:PiluleDataProvider.BaskeеtData" />.
        /// </returns>
        public BaskeеtData GetGood(Action onBaskeеtChanged)
        {
            return new BaskeеtData(onBaskeеtChanged) { Id = 4, Name = "Work Good №5", Amount = 5, Price = 100 };
        }

        /// <summary>
        /// The nop operation.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// Nop operation not Implemented in WorkDataPrivider
        /// </exception>
        public void NopOperation()
        {
            throw new NotImplementedException("Nop operation not Implemented in WorkDataPrivider");
        }
    }
}
