// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
//<DataGridTextColumn Binding="{Binding Name}" Header="Наименование" Width="*"/>
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
        private readonly ObservableCollection<BaskeеtData> baskeеt;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            //https://habr.com/post/270413/
            //https://www.codeproject.com/Articles/139629/A-Numeric-Up-Down-Control-for-WPF
            //https://docs.microsoft.com/en-us/dotnet/framework/wpf/data/how-to-implement-property-change-notification

            this.InitializeComponent();
            this.baskeеt = new ObservableCollection<BaskeеtData>();
            this.MyGrid.ItemsSource = this.baskeеt;
            this.baskeеt.CollectionChanged += this.OnCollectionChanged;

            this.baskeеt.Add(new BaskeеtData(this.OnBaskeеtChanged) { Id = 0, Name = "xxx", Amount = 1, Price = 2 });
            this.baskeеt.Add(new BaskeеtData(this.OnBaskeеtChanged) { Id = 1, Name = "yyy", Amount = 2, Price = 4 });
            this.baskeеt.Add(new BaskeеtData(this.OnBaskeеtChanged) { Id = 2, Name = "zzz", Amount = 3, Price = 8 });
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var action = e.Action;
            if ((action == NotifyCollectionChangedAction.Add) || (action == NotifyCollectionChangedAction.Remove))
            {
                this.PositionAmount.Content = $"Позиций: {this.baskeеt.Count}";
            }

            this.TotalSumm.Content = this.baskeеt.Sum(c => c.Summa).ToString(CultureInfo.InvariantCulture);
        }

        private void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = this.MyGrid.SelectedItem as BaskeеtData;
            if (item == null)
            {
                return;
            }

            this.InfoLabelTop.Content = item.ToString();
            this.InfoLabelBottom.Content = item.Name.ToString();

        }

        private void RecalculateAmount(bool up)
        {
            var item = this.MyGrid.SelectedItem as BaskeеtData;
            if (item == null)
            {
                return;
            }

            if (up)
                item.Amount++;
            else
                item.Amount--;
        }

        private void AmountUpClick(object sender, RoutedEventArgs e)
        {
            this.RecalculateAmount(true);
        }

        private void AmountDownClick(object sender, RoutedEventArgs e)
        {
            this.RecalculateAmount(false);
        }


        private void DebugButtonClick(object sender, RoutedEventArgs e)
        {
            this.baskeеt.Add(new BaskeеtData(this.OnBaskeеtChanged) { Id = 2, Name = "zzz", Amount = 3, Price = 8});
        }

        private void OnBaskeеtChanged()
        {
            this.TotalSumm.Content = this.baskeеt.Sum(c => c.Summa).ToString(CultureInfo.InvariantCulture);
        }

        private class BaskeеtData : INotifyPropertyChanged
        {
            private readonly Action baskeеChenged;
            public BaskeеtData(Action baskeеChenged)
            {
                this.baskeеChenged = baskeеChenged;
            }

            public int Id { private get; set; }

            public string Name { get; set; }

            private int amount;
            public int Amount
            {
                get
                {
                    return this.amount;
                }
                set
                {
                    if (value >= 0)
                    {
                        this.amount = value;
                        this.OnPropertyChanged("Amount");
                        this.OnPropertyChanged("Summa");
                        this.baskeеChenged?.Invoke();
                    }
                }
            }

            public decimal Price { private get; set; }

            public decimal Summa
            {
                set
                {

                } 

                get
                {
                    return this.Amount * this.Price;
                }
            }

            public override string ToString()
            {
                return $"{nameof(this.Id)}={this.Id}  {nameof(this.Name)}={this.Name} {nameof(this.Amount)}={this.Amount}";
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(string name)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
