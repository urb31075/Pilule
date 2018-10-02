// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeDataProvider.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   The fake data provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PiluleDataProvider
{
    using System;
    using System.Collections.ObjectModel;

    /// <inheritdoc />
    /// <summary>
    /// The fake data provider.
    /// </summary>
    public class FakeDataProvider : IPiluleDataProvider
    {
        /// <inheritdoc />
        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="onBaskeеtChanged">
        /// The on baskeеt changed.
        /// </param>
        /// <returns>
        /// Return Fake ObservableCollection of BaskeеtData
        /// </returns>
        public ObservableCollection<BaskeеtData> GetData(Action onBaskeеtChanged)
        {
            var baskeеt = new ObservableCollection<BaskeеtData>
                 {
                     new BaskeеtData(onBaskeеtChanged) { Id = 0, Code = "0001", Name = "Fake Good №1", Amount = 1, Price = 2 },
                     new BaskeеtData(onBaskeеtChanged) { Id = 1, Code = "0002", Name = "Fake Good №2", Amount = 2, Price = 4 },
                     new BaskeеtData(onBaskeеtChanged) { Id = 2, Code = "0003", Name = "Fake Good №3", Amount = 3, Price = 8 },
                     new BaskeеtData(onBaskeеtChanged) { Id = 3, Code = "0004", Name = "Fake Good №4", Amount = 4, Price = 16 }
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
            return new BaskeеtData(onBaskeеtChanged) { Id = 4, Name = "Fake Good №5", Amount = 5, Price = 100 };
        }

        /// <summary>
        /// The nop operation.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// Nop operation not Implemented in FakeDataPrivider
        /// </exception>
        public void NopOperation()
        {
            throw new NotImplementedException("Nop operation not Implemented in FakeDataPrivider");
        }
    }
}
