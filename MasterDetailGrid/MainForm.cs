using System.Collections.Generic;

namespace MasterDetailGrid
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using DevExpress.Utils;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraGrid.Views.Card;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraPrinting.Native;

    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>


        public MainForm()
        {
            this.InitializeComponent();
        }

        private void DataSetButton_Click(object sender, EventArgs e)
        {
            this.FillDasaSource();
        }


        private void FillDasaSource()
        {
            this.AddMasterData(1, "AAA", 0, "xxx1");
            this.AddMasterData(2, "BBB", 1, "xxx2");
            this.AddMasterData(3, "CCC", 2, "xxx3");

            this.AddDetailData(1, "aaa1", "OK!", 1);
            this.AddDetailData(1, "aaa2", "OK!", 2);

            this.AddDetailData(2, "bbb1", "OK!", 1);
            this.AddDetailData(2, "bbb2", "ERROR!", 2);
            this.AddDetailData(2, "bbb3", "OK!", 2);

            this.AddDetailData(3, "ccc1", "OK!", 0);
            this.AddDetailData(3, "ccc2", "OK!", 0);
        }

        private void AddMasterData(int id, string name, int imageIndex, string dupel)
        {
            var mr = this.myDataSet.MainData.NewMainDataRow();
            mr.Id = id;
            mr.Name = name;
            mr.ImageIndex = imageIndex;
            mr.DupelIndex = 0;
            mr.MyDupel = dupel;
            this.myDataSet.MainData.Rows.Add(mr);
        }

        private void AddDetailData(int mainDataId, string message, string status, int image)
        {
            var dr = this.myDataSet.DetailData.NewDetailDataRow();
            dr.MainDataId = mainDataId;
            dr.Message = message;
            dr.Status = status;
            dr.Image = image;
            this.myDataSet.DetailData.Rows.Add(dr);            
        }

        /// <summary>
        /// The main form_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            this.repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem(@"X0", (string)"xxx0", 0));
            this.repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem(@"X1", (string)"xxx1", 1));
            this.repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem(@"X2", (string)"xxx2", 2));
            this.repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem(@"X3", (string)"xxx3", 3));
            this.repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem(@"X4", (string)"xxx4", 4));
            this.repositoryItemImageComboBox1.GlyphAlignment = HorzAlignment.Default;
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "MyImage" && e.IsGetData)
            {
                var imageIndex = this.myDataSet.MainData[e.ListSourceRowIndex].ImageIndex;
                e.Value = this.imageList.Images[imageIndex]; 
            }
        }
    }
}