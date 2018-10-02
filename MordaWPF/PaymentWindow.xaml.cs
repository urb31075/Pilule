// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaymentWindow.xaml.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Interaction logic for PaymentWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MordaWPF
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        /// <summary>
        /// The summa.
        /// </summary>
        private readonly decimal summa;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentWindow"/> class.
        /// </summary>
        /// <param name="summa">
        /// The summa.
        /// </param>
        public PaymentWindow(decimal summa)
        {
            this.InitializeComponent();
            this.summa = summa;
        }

        /// <summary>
        /// The close button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// The calculate short change button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CalculateShortChangeButtonClick(object sender, RoutedEventArgs e)
        {
            decimal cash;
            var result = decimal.TryParse(this.CashTextBox.Text, out cash);

            if (result && cash >= this.summa)
            {
                var shortChange = cash - this.summa;
                this.ShortChangeTextBox.Text = shortChange.ToString("########.##").Replace(",", ".");
            }
            else
            {
                MessageBox.Show("Ошибка ввода наличности", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
