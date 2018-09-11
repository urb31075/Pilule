// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the Default type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ElasticGrid
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;

    /// <summary>
    /// The default.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
            }
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
        protected void DebugButtonClick(object sender, EventArgs e)
        {
            var testDataList = Enumerable.Range(1, 10).Select(i => new ViewModel.TestData { Name = $"name{i}", Amount = i, Summa = i * 10, Comment = $"comment{i}" }).ToList();
            var viewModel = new ViewModel();
            this.InfoGridView.RowDataBound += (infoGridSender, infoGridEventArgs) =>
                {
                    if (infoGridEventArgs.Row.RowType != DataControlRowType.Header)
                    {
                        return;
                    }

                    foreach (var viewColumn in viewModel.ColumnHeaderDictionary)
                    {
                        infoGridEventArgs.Row.Cells[viewColumn.Key].Text = viewColumn.Value.Header;
                    }
                };

            this.InfoGridView.DataSource = ViewModel.PrepareForBinding(viewModel.ColumnHeaderDictionary, testDataList);
            this.InfoGridView.DataBind();

            this.InfoLabel.Text = $"Ok {testDataList.Count}";
        }
    }
}