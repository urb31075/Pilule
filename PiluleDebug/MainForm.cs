// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
namespace PiluleDebug
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    using PiluleDAL;

    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The version marker.
        /// </summary>
        private const string VersionMarker = "ver1.5";

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The clear log.
        /// </summary>
        private void ClearLog()
        {
            this.InfoListBox.Items.Clear();
        }

        /// <summary>
        /// The out log.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        private void OutLog(string msg)
        {
            this.InfoListBox.Items.Add(msg);
        }

        /// <summary>
        /// The get debug value button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void GetDebugValueButtonClick(object sender, EventArgs e)
        {
            var debugValue = PiluleDal.GetDebugValue("GoodsDictionary");
            if (debugValue == null)
            {
                this.OutLog("NULL Value");
                return;
            }

            foreach (var items in debugValue)
            {
                foreach (var item in items)
                {
                    this.OutLog($"{item.Key}={item.Value} ");
                }
            }
        }

        /// <summary>
        /// The create db button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private async void CreateDbButtonClick(object sender, EventArgs e)
        {
            this.CreateDbButton.Enabled = false;
            this.ClearLog();
            await PiluleDal.DropDataBaseAsync(this.OutLog);
            await PiluleDal.ExecuteScriptAsync("CreateDB", this.OutLog);
            await PiluleDal.ExecuteScriptAsync("CreateTable_Version", this.OutLog);
            await PiluleDal.ExecuteScriptAsync("CreateTable_GoodsDictionary", this.OutLog);
            await PiluleDal.ExecuteScriptAsync("CreateTable_StockBalance", this.OutLog);
            await PiluleDal.ExecuteScriptAsync("InsertVersion", this.OutLog);
            await PiluleDal.ExecuteScriptAsync("CreateTestStorageProc", this.OutLog);
            this.CreateDbButton.Enabled = true;
        }

        /// <summary>
        /// The get version button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void GetVersionButtonClick(object sender, EventArgs e)
        {
            this.OutLog($"test ADO.NET   - {PiluleDal.CheckVersionAdoNet(VersionMarker)}");
            this.OutLog($"test DapperORM - {PiluleDal.CheckVersionDaperOrm(VersionMarker)}");
        }

        /// <summary>
        /// The insert data button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private async void InsertDataButtonClick(object sender, EventArgs e)
        {
            this.InsertDataButton.Enabled = false;
            this.ClearLog();
            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(assemblyFolder))
            {
                this.OutLog("ExecutingAssembly not found");
                return;
            }

            var fileName = Path.Combine(assemblyFolder, "Docs", "ProductList.txt");
            var lines = File.ReadAllLines(fileName);

            var goodsDictionaryList = new List<PiluleDal.GoodsDictionary>();
            var stockBalanceList = new List<PiluleDal.StockBalance>();
            var rnd = new Random();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var code = $"{goodsDictionaryList.Count + 1:000000}";
                var price = Convert.ToDecimal(rnd.Next(10000, 50000)) / 100;
                var splitResult = line.Split(new[] { ". " }, StringSplitOptions.None);
                PiluleDal.GoodsDictionary item = null;
                if (splitResult.Length == 1)
                {
                    item = new PiluleDal.GoodsDictionary { Name = splitResult[0], Code = code, Comment = string.Empty, Price = price };
                }

                if (splitResult.Length == 2)
                {
                    item = new PiluleDal.GoodsDictionary { Name = splitResult[0], Code = code, Comment = splitResult[1], Price = price };
                }

                if (item == null)
                {
                    continue;
                }

                goodsDictionaryList.Add(item);
                stockBalanceList.Add(new PiluleDal.StockBalance { GoodsId = stockBalanceList.Count + 1, Amount = rnd.Next(0, 100) });
            }

            await PiluleDal.InsertGoodsDictionaryAsync(goodsDictionaryList, this.OutLog);
           await PiluleDal.InsertStockBalanceAsync(stockBalanceList, this.OutLog);
        }

        /// <summary>
        /// The test button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TestButtonClick(object sender, EventArgs e)
        {
            var x = PiluleDal.GetFirstGoodsByTemplate("Ай");
            this.OutLog(x.Name);
        }

        /// <summary>
        /// The get goods dictionary button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void GetGoodsDictionaryButtonClick(object sender, EventArgs e)
        {
            var goodsDictionary = PiluleDal.GetGoodsDictionary();
            if (goodsDictionary == null)
            {
                return;
            }

            this.MainDataGridView.DataSource = goodsDictionary;
        }

        /// <summary>
        /// The get stock balance button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void GetStockBalanceButtonClick(object sender, EventArgs e)
        {
            var stockBalance = PiluleDal.GetStockBalance();
            if (stockBalance == null)
            {
                return;
            }

            this.MainDataGridView.DataSource = stockBalance;
        }

        /// <summary>
        /// The get multi mapping button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void GetMultiMappingButtonClick(object sender, EventArgs e)
        {
            var multiMapping = PiluleDal.MultiMapping();
            if (multiMapping == null)
            {
                return;
            }

            this.MainDataGridView.DataSource = multiMapping;
        }

        /// <summary>
        /// The test storage proc button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TestStorageProcButtonClick(object sender, EventArgs e)
        {
            int cnt;
            var goodsDictionary = PiluleDal.ExecuteStorageProc(13, out cnt);
            if (goodsDictionary == null)
            {
                return;
            }

            this.MainDataGridView.DataSource = goodsDictionary;
        }
    }
}
