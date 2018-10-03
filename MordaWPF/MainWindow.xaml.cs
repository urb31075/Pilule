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
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using ConfigStorage;

    using FastReport;

    using PiluleDataProvider;

    using PiluleDAL;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// The pilule data provider.
        /// </summary>
        private readonly IPiluleDataProvider piluleDataProvider;

        /// <summary>
        /// The baskeеt.
        /// </summary>
        private readonly ObservableCollection<BaskeеtData> baskeеt;

        public int CurrentGoodsId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MordaWPF.MainWindow" /> class.
        /// </summary>
        /// <param name="piluleDataProvider">
        /// The pilule Data Provider.
        /// </param>
        public MainWindow(IPiluleDataProvider piluleDataProvider)
        {
            this.piluleDataProvider = piluleDataProvider;

            this.InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.baskeеt = new ObservableCollection<BaskeеtData>(); // this.piluleDataProvider.GetData(this.OnBaskeеtChanged);

            this.MyGrid.ItemsSource = this.baskeеt;
            this.baskeеt.CollectionChanged += this.OnCollectionChanged;
            this.OnBaskeеtChanged();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.PaymentButton.IsEnabled = false;
            this.AddBaskedButton.IsEnabled = false;
            var applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var sqliteStorage = new SqLiteStorage(Path.Combine(applicationDataPath, "Pilule", "config.sqlite"));
            string user;
            var result = sqliteStorage.ReadConfigValue("user", out user);
            this.InfoTextBox.Text = result ? user : "Error read config!";
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
            if (action == NotifyCollectionChangedAction.Add || (action == NotifyCollectionChangedAction.Remove))
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
            var item = this.MyGrid.SelectedItem as BaskeеtData;
            if (item == null)
            {
                this.InfoTextBox.Text = string.Empty;
                this.CodLabel.Content = "Код:";
                this.PriceLabel.Content = "Цена:";
                this.AmountLabel.Content = "Кол-во:";
                this.SummaLabel.Content = "Сумма:";
                this.NameLabel.Content = string.Empty;
                return;
            }

            this.InfoTextBox.Text = item.ToString();
            this.CodLabel.Content = $"Код: {item.Code}";
            this.PriceLabel.Content = $"Цена: {item.Price.ToString("#######.##").Replace(",",".")}";
            this.AmountLabel.Content = $"Кол-во: {item.Amount.ToString("#######.##").Replace(",", ".")}"; 
            this.SummaLabel.Content = $"Сумма: {(Convert.ToDecimal(item.Amount) * item.Price).ToString("#####.##").Replace(",", ".")}";
            this.NameLabel.Content = $"{item.Name}";
        }

        /// <summary>
        /// The recalculate amount.
        /// </summary>
        /// <param name="up">
        /// The up.
        /// </param>
        private void RecalculateAmount(bool up)
        {
            var item = this.MyGrid.SelectedItem as BaskeеtData;
            if (item == null)
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
            if (this.CurrentGoodsId == -1)
            {
                this.baskeеt.Add(this.piluleDataProvider.GetGood(this.OnBaskeеtChanged));
            }
            else
            {
                var goods = PiluleDal.GetGoodsDictionary(this.CurrentGoodsId);
                if (goods == null)
                {
                    return;
                }

                var basketData = new BaskeеtData(this.OnBaskeеtChanged)
                                     {
                                         Id = goods.Id,
                                         Name = goods.Name,
                                         Amount = 1,
                                         Price = goods.Price
                                     };

                this.baskeеt.Add(basketData);
                this.DocVid.Content = "Вид документа: продажа (открыт)";
                this.PaymentButton.IsEnabled = true;
                this.PaymentButton.Background = new SolidColorBrush(Colors.DarkSeaGreen);
            }
        }

        private void PaymentButtonClick(object sender, RoutedEventArgs e)
        {
            var paymentWindow = new PaymentWindow(this.baskeеt.Sum(c => c.Summa)) {Owner = this};
            var result = paymentWindow.ShowDialog();
            if (result == true)
            {
                var applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var reportPath = Path.Combine(applicationDataPath, "Pilule", "PiluleReport.frx");
                if (!File.Exists(reportPath))
                {
                    this.InfoTextBox.Text = "Отсутствует шаблон чека!";
                }
                else
                {
                    var report = new Report();
                    report.Load(reportPath);
                    report.RegisterData(this.baskeеt, "PD");
                    report.Prepare();
                    report.ShowPrepared(true);
                }

                this.baskeеt.Clear();
                this.DocVid.Content = "Вид документа: продажа (закрыт)";
                this.GoodsFilter.Text = string.Empty;
                this.DataGridSelectionChanged(null, null);
                this.PaymentButton.IsEnabled = false;
                this.AddBaskedButton.Background = new SolidColorBrush(Colors.Gainsboro);
                this.PaymentButton.Background = new SolidColorBrush(Colors.Gainsboro);
            }
        }

        /// <summary>
        /// The on baskeеt changed.
        /// </summary>
        private void OnBaskeеtChanged()
        {
            if (this.baskeеt == null)
            {
                return;
            }

            this.DataGridSelectionChanged(null, null);

            this.TotalSumm.Text = this.baskeеt.Sum(c => c.Summa).ToString("#####.##").Replace(",",".");
            this.PositionAmount.Content =$"Позиций: {this.baskeеt.Count}";
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

        private void GoodsFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            this.AddBaskedButton.IsEnabled = false;
            this.AddBaskedButton.Background = new SolidColorBrush(Colors.Gainsboro);
            this.CurrentGoodsId = -1;
            if (this.GoodsFilter.Text.Length > 2)
            {
                var template = this.GoodsFilter.Text;
                var item = PiluleDal.GetFirstGoodsByTemplate(template);
                if (item == null)
                {
                    return;
                }
                this.CurrentGoodsId = item.Id;
                this.NameLabel.Content = $"{item.Code} {item.Name} ({item.Comment})";
                this.AddBaskedButton.IsEnabled = true;
                this.AddBaskedButton.Background = new SolidColorBrush(Colors.DarkSeaGreen);
            }
        }
    }
}
