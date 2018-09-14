// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MordaWPF
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// The baskeеt.
        /// </summary>
        private readonly ObservableCollection<BaskeеtData> baskeеt;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.baskeеt = new ObservableCollection<BaskeеtData>();
            this.MyGrid.ItemsSource = this.baskeеt;
            this.baskeеt.CollectionChanged += this.OnCollectionChanged;

            this.baskeеt.Add(new BaskeеtData(this.OnBaskeеtChanged) { Id = 0, Name = "xxx", Amount = 1, Price = 2 });
            this.baskeеt.Add(new BaskeеtData(this.OnBaskeеtChanged) { Id = 1, Name = "yyy", Amount = 2, Price = 4 });
            this.baskeеt.Add(new BaskeеtData(this.OnBaskeеtChanged) { Id = 2, Name = "zzz", Amount = 3, Price = 8 });
        }

        /// <summary>
        /// The on collection changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var action = e.Action;
            if ((action == NotifyCollectionChangedAction.Add) || (action == NotifyCollectionChangedAction.Remove))
            {
                this.PositionAmount.Content = $"Позиций: {this.baskeеt.Count}";
            }

            this.OnBaskeеtChanged();
        }

        /// <summary>
        /// The data grid selection changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(this.MyGrid.SelectedItem is BaskeеtData item))
            {
                return;
            }

            this.InfoTextBox.Text = item.ToString();
            this.InfoLabelBottom.Content = item.Name;
        }

        /// <summary>
        /// The recalculate amount.
        /// </summary>
        /// <param name="up">
        /// The up.
        /// </param>
        private void RecalculateAmount(bool up)
        {
            if (!(this.MyGrid.SelectedItem is BaskeеtData item))
            {
                return;
            }

            if (up)
            {
                item.Amount++;
            }
            else
            {
                item.Amount--;
            }
        }

        /// <summary>
        /// The amount up click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AmountUpClick(object sender, RoutedEventArgs e)
        {
            this.RecalculateAmount(true);
        }

        /// <summary>
        /// The amount down click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AmountDownClick(object sender, RoutedEventArgs e)
        {
            this.RecalculateAmount(false);
        }

        /// <summary>
        /// The debug button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DebugButtonClick(object sender, RoutedEventArgs e)
        {
            this.baskeеt.Add(new BaskeеtData(this.OnBaskeеtChanged) { Id = 2, Name = "zzz", Amount = 3, Price = 8 });
        }

        /// <summary>
        /// The on baskeеt changed.
        /// </summary>
        private void OnBaskeеtChanged()
        {
            this.TotalSumm.Text = this.baskeеt.Sum(c => c.Summa).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The baskeеt data.
        /// </summary>
        private class BaskeеtData : INotifyPropertyChanged
        {
            /// <summary>
            /// The baskeе chenged.
            /// </summary>
            private readonly Action baskeеChenged;

            /// <summary>
            /// The amount.
            /// </summary>
            private int amount;

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

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the amount.
            /// </summary>
            public int Amount
            {
                get => this.amount;
                set
                {
                    if (value < 0)
                    {
                        return;
                    }

                    this.amount = value;
                    this.OnPropertyChanged("Amount");
                    this.OnPropertyChanged("Summa");
                    this.baskeеChenged?.Invoke();
                }
            }

            /// <summary>
            /// Gets or sets the price.
            /// </summary>
            public decimal Price { private get; set; }

            /// <summary>
            /// Gets or sets the summa.
            /// </summary>
            public decimal Summa
            {
                get => this.Amount * this.Price;
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
                return $"{nameof(this.Id)}={this.Id}  {nameof(this.Name)}={this.Name} {nameof(this.Amount)}={this.Amount}";
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
}
