namespace PiluleDebug
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateDbButton = new System.Windows.Forms.Button();
            this.InfoListBox = new System.Windows.Forms.ListBox();
            this.GetDebugValueButton = new System.Windows.Forms.Button();
            this.GetVersionButton = new System.Windows.Forms.Button();
            this.InsertDataButton = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.CreateDbPage = new System.Windows.Forms.TabPage();
            this.ReportButton = new System.Windows.Forms.Button();
            this.ConfigureButton = new System.Windows.Forms.Button();
            this.DataPage = new System.Windows.Forms.TabPage();
            this.TestStorageProcButton = new System.Windows.Forms.Button();
            this.GetMultiMappingButton = new System.Windows.Forms.Button();
            this.GetStockBalanceButton = new System.Windows.Forms.Button();
            this.MainDataGridView = new System.Windows.Forms.DataGridView();
            this.GetGoodsDictionaryButton = new System.Windows.Forms.Button();
            this.DesignModeCheckBox = new System.Windows.Forms.CheckBox();
            this.MainTabControl.SuspendLayout();
            this.CreateDbPage.SuspendLayout();
            this.DataPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateDbButton
            // 
            this.CreateDbButton.Location = new System.Drawing.Point(6, 32);
            this.CreateDbButton.Name = "CreateDbButton";
            this.CreateDbButton.Size = new System.Drawing.Size(140, 23);
            this.CreateDbButton.TabIndex = 0;
            this.CreateDbButton.Text = "Создать базу данных";
            this.CreateDbButton.UseVisualStyleBackColor = true;
            this.CreateDbButton.Click += new System.EventHandler(this.CreateDbButtonClick);
            // 
            // InfoListBox
            // 
            this.InfoListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoListBox.FormattingEnabled = true;
            this.InfoListBox.Location = new System.Drawing.Point(152, 6);
            this.InfoListBox.Name = "InfoListBox";
            this.InfoListBox.ScrollAlwaysVisible = true;
            this.InfoListBox.Size = new System.Drawing.Size(711, 654);
            this.InfoListBox.TabIndex = 1;
            // 
            // GetDebugValueButton
            // 
            this.GetDebugValueButton.Location = new System.Drawing.Point(6, 3);
            this.GetDebugValueButton.Name = "GetDebugValueButton";
            this.GetDebugValueButton.Size = new System.Drawing.Size(140, 23);
            this.GetDebugValueButton.TabIndex = 2;
            this.GetDebugValueButton.Text = "GetDebugValue";
            this.GetDebugValueButton.UseVisualStyleBackColor = true;
            this.GetDebugValueButton.Click += new System.EventHandler(this.GetDebugValueButtonClick);
            // 
            // GetVersionButton
            // 
            this.GetVersionButton.Location = new System.Drawing.Point(6, 61);
            this.GetVersionButton.Name = "GetVersionButton";
            this.GetVersionButton.Size = new System.Drawing.Size(140, 23);
            this.GetVersionButton.TabIndex = 3;
            this.GetVersionButton.Text = "Версия базы";
            this.GetVersionButton.UseVisualStyleBackColor = true;
            this.GetVersionButton.Click += new System.EventHandler(this.GetVersionButtonClick);
            // 
            // InsertDataButton
            // 
            this.InsertDataButton.Location = new System.Drawing.Point(6, 90);
            this.InsertDataButton.Name = "InsertDataButton";
            this.InsertDataButton.Size = new System.Drawing.Size(140, 23);
            this.InsertDataButton.TabIndex = 4;
            this.InsertDataButton.Text = "Вставить данные";
            this.InsertDataButton.UseVisualStyleBackColor = true;
            this.InsertDataButton.Click += new System.EventHandler(this.InsertDataButtonClick);
            // 
            // TestButton
            // 
            this.TestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TestButton.Location = new System.Drawing.Point(6, 636);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(140, 23);
            this.TestButton.TabIndex = 5;
            this.TestButton.Text = "TestButton";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButtonClick);
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Controls.Add(this.CreateDbPage);
            this.MainTabControl.Controls.Add(this.DataPage);
            this.MainTabControl.Location = new System.Drawing.Point(5, 5);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.Padding = new System.Drawing.Point(3, 3);
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(877, 689);
            this.MainTabControl.TabIndex = 6;
            // 
            // CreateDbPage
            // 
            this.CreateDbPage.Controls.Add(this.DesignModeCheckBox);
            this.CreateDbPage.Controls.Add(this.ReportButton);
            this.CreateDbPage.Controls.Add(this.ConfigureButton);
            this.CreateDbPage.Controls.Add(this.InfoListBox);
            this.CreateDbPage.Controls.Add(this.TestButton);
            this.CreateDbPage.Controls.Add(this.GetDebugValueButton);
            this.CreateDbPage.Controls.Add(this.InsertDataButton);
            this.CreateDbPage.Controls.Add(this.CreateDbButton);
            this.CreateDbPage.Controls.Add(this.GetVersionButton);
            this.CreateDbPage.Location = new System.Drawing.Point(4, 22);
            this.CreateDbPage.Name = "CreateDbPage";
            this.CreateDbPage.Padding = new System.Windows.Forms.Padding(3);
            this.CreateDbPage.Size = new System.Drawing.Size(869, 663);
            this.CreateDbPage.TabIndex = 0;
            this.CreateDbPage.Text = "CreteDB";
            this.CreateDbPage.UseVisualStyleBackColor = true;
            // 
            // ReportButton
            // 
            this.ReportButton.Location = new System.Drawing.Point(6, 271);
            this.ReportButton.Name = "ReportButton";
            this.ReportButton.Size = new System.Drawing.Size(140, 23);
            this.ReportButton.TabIndex = 7;
            this.ReportButton.Text = "Чек";
            this.ReportButton.UseVisualStyleBackColor = true;
            this.ReportButton.Click += new System.EventHandler(this.ReportButtonClick);
            // 
            // ConfigureButton
            // 
            this.ConfigureButton.Location = new System.Drawing.Point(6, 171);
            this.ConfigureButton.Name = "ConfigureButton";
            this.ConfigureButton.Size = new System.Drawing.Size(140, 23);
            this.ConfigureButton.TabIndex = 6;
            this.ConfigureButton.Text = "Сконфигурировать";
            this.ConfigureButton.UseVisualStyleBackColor = true;
            this.ConfigureButton.Click += new System.EventHandler(this.ConfigureButtonClick);
            // 
            // DataPage
            // 
            this.DataPage.Controls.Add(this.TestStorageProcButton);
            this.DataPage.Controls.Add(this.GetMultiMappingButton);
            this.DataPage.Controls.Add(this.GetStockBalanceButton);
            this.DataPage.Controls.Add(this.MainDataGridView);
            this.DataPage.Controls.Add(this.GetGoodsDictionaryButton);
            this.DataPage.Location = new System.Drawing.Point(4, 22);
            this.DataPage.Name = "DataPage";
            this.DataPage.Padding = new System.Windows.Forms.Padding(3);
            this.DataPage.Size = new System.Drawing.Size(869, 663);
            this.DataPage.TabIndex = 1;
            this.DataPage.Text = "Data";
            this.DataPage.UseVisualStyleBackColor = true;
            // 
            // TestStorageProcButton
            // 
            this.TestStorageProcButton.Location = new System.Drawing.Point(358, 7);
            this.TestStorageProcButton.Name = "TestStorageProcButton";
            this.TestStorageProcButton.Size = new System.Drawing.Size(109, 23);
            this.TestStorageProcButton.TabIndex = 4;
            this.TestStorageProcButton.Text = "TestStorageProc";
            this.TestStorageProcButton.UseVisualStyleBackColor = true;
            this.TestStorageProcButton.Click += new System.EventHandler(this.TestStorageProcButtonClick);
            // 
            // GetMultiMappingButton
            // 
            this.GetMultiMappingButton.Location = new System.Drawing.Point(247, 6);
            this.GetMultiMappingButton.Name = "GetMultiMappingButton";
            this.GetMultiMappingButton.Size = new System.Drawing.Size(105, 23);
            this.GetMultiMappingButton.TabIndex = 3;
            this.GetMultiMappingButton.Text = "GetMultiMapping";
            this.GetMultiMappingButton.UseVisualStyleBackColor = true;
            this.GetMultiMappingButton.Click += new System.EventHandler(this.GetMultiMappingButtonClick);
            // 
            // GetStockBalanceButton
            // 
            this.GetStockBalanceButton.Location = new System.Drawing.Point(134, 6);
            this.GetStockBalanceButton.Name = "GetStockBalanceButton";
            this.GetStockBalanceButton.Size = new System.Drawing.Size(107, 23);
            this.GetStockBalanceButton.TabIndex = 2;
            this.GetStockBalanceButton.Text = "GetStockBalance";
            this.GetStockBalanceButton.UseVisualStyleBackColor = true;
            this.GetStockBalanceButton.Click += new System.EventHandler(this.GetStockBalanceButtonClick);
            // 
            // MainDataGridView
            // 
            this.MainDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainDataGridView.Location = new System.Drawing.Point(7, 36);
            this.MainDataGridView.Name = "MainDataGridView";
            this.MainDataGridView.Size = new System.Drawing.Size(856, 621);
            this.MainDataGridView.TabIndex = 1;
            // 
            // GetGoodsDictionaryButton
            // 
            this.GetGoodsDictionaryButton.Location = new System.Drawing.Point(6, 6);
            this.GetGoodsDictionaryButton.Name = "GetGoodsDictionaryButton";
            this.GetGoodsDictionaryButton.Size = new System.Drawing.Size(122, 23);
            this.GetGoodsDictionaryButton.TabIndex = 0;
            this.GetGoodsDictionaryButton.Text = "GetGoodsDictionary";
            this.GetGoodsDictionaryButton.UseVisualStyleBackColor = true;
            this.GetGoodsDictionaryButton.Click += new System.EventHandler(this.GetGoodsDictionaryButtonClick);
            // 
            // DesignModeCheckBox
            // 
            this.DesignModeCheckBox.AutoSize = true;
            this.DesignModeCheckBox.Checked = true;
            this.DesignModeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DesignModeCheckBox.Location = new System.Drawing.Point(11, 248);
            this.DesignModeCheckBox.Name = "DesignModeCheckBox";
            this.DesignModeCheckBox.Size = new System.Drawing.Size(118, 17);
            this.DesignModeCheckBox.TabIndex = 8;
            this.DesignModeCheckBox.Text = "Режим дизайнера";
            this.DesignModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 700);
            this.Controls.Add(this.MainTabControl);
            this.Name = "MainForm";
            this.Text = "PiluleDebug";
            this.MainTabControl.ResumeLayout(false);
            this.CreateDbPage.ResumeLayout(false);
            this.CreateDbPage.PerformLayout();
            this.DataPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreateDbButton;
        private System.Windows.Forms.ListBox InfoListBox;
        private System.Windows.Forms.Button GetDebugValueButton;
        private System.Windows.Forms.Button GetVersionButton;
        private System.Windows.Forms.Button InsertDataButton;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage CreateDbPage;
        private System.Windows.Forms.TabPage DataPage;
        private System.Windows.Forms.DataGridView MainDataGridView;
        private System.Windows.Forms.Button GetGoodsDictionaryButton;
        private System.Windows.Forms.Button GetStockBalanceButton;
        private System.Windows.Forms.Button GetMultiMappingButton;
        private System.Windows.Forms.Button TestStorageProcButton;
        private System.Windows.Forms.Button ConfigureButton;
        private System.Windows.Forms.Button ReportButton;
        private System.Windows.Forms.CheckBox DesignModeCheckBox;
    }
}

