// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   An empty page that can be used on its own or navigated to within a Frame.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MordaUWP
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;

    using PiluleDataProvider;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        /// <summary>
        /// The pilule data provider.
        /// </summary>
        private readonly IPiluleDataProvider piluleDataProvider;

        /// <summary>
        /// The baskeеt.
        /// </summary>
        private readonly ObservableCollection<BaskeеtData> baskeеt;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            this.baskeеt = new ObservableCollection<BaskeеtData>();
            this.piluleDataProvider = new WorkDataProvider();
            this.baskeеt = this.piluleDataProvider.GetData(this.OnBaskeеtChanged);
            this.MyGrid.ItemsSource = this.baskeеt;
            this.baskeеt.CollectionChanged += this.OnCollectionChanged;
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
                this.PositionAmount.Text = $"Позиций: {this.baskeеt.Count}";
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
            this.InfoLabelBottom.Text = item.Name;
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
            this.baskeеt.Add(this.piluleDataProvider.GetGood(this.OnBaskeеtChanged));
        }

        /// <summary>
        /// The on baskeеt changed.
        /// </summary>
        private void OnBaskeеtChanged()
        {
            this.TotalSumm.Text = this.baskeеt.Sum(c => c.Summa).ToString(CultureInfo.InvariantCulture);
        }
    }
}
