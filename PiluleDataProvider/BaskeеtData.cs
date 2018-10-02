// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaskeеtData.cs" company="urb31075">
//   All Right Reserved
// </copyright>
// <summary>
//   Defines the BaskeеtData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PiluleDataProvider
{
    using System;
    using System.ComponentModel;

    /// <inheritdoc />
    /// <summary>
    /// The baskeеt data.
    /// </summary>
    public class BaskeеtData : INotifyPropertyChanged
    {
        /// <summary>
        /// The baskeе chenged.
        /// </summary>
        private readonly Action baskeеChenged;

        /// <summary>
        /// The amount.
        /// </summary>
        private decimal amount;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaskeеtData"/> class.
        /// </summary>
        /// <param name="baskeеChenged">
        /// The baskeе chenged.
        /// </param>
        public BaskeеtData(Action baskeеChenged)
        {
            this.baskeеChenged = baskeеChenged;
        }

        /// <inheritdoc />
        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { private get; set; }

        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount
        {
            get
            {
                return this.amount;
            } 
            set
            {
                decimal x = value;
                this.amount = Math.Round(x, 2);
                this.OnPropertyChanged("Amount");
                this.OnPropertyChanged("Summa");
                this.baskeеChenged?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the summa.
        /// </summary>
        public decimal Summa
        {
            get
            {
                return Convert.ToDecimal(this.Amount) * this.Price;
            } 
            // ReSharper disable once ValueParameterNotUsed
            set { } // Не удалять! Без это хрени биндинг датагрида выдает эксепшен
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(this.Id)}={this.Id} {nameof(this.Code)}={this.Code} {nameof(this.Name)}={this.Name} {nameof(this.Amount)}={this.Amount}";
        }

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        private void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
