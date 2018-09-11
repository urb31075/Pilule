namespace EntityFrameworkORM
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
            this.InfoListBox = new System.Windows.Forms.ListBox();
            this.DebugListBox = new System.Windows.Forms.ListBox();
            this.DebugButton = new System.Windows.Forms.Button();
            this.RoundByControlButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InfoListBox
            // 
            this.InfoListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoListBox.FormattingEnabled = true;
            this.InfoListBox.Location = new System.Drawing.Point(12, 41);
            this.InfoListBox.Name = "InfoListBox";
            this.InfoListBox.Size = new System.Drawing.Size(1041, 290);
            this.InfoListBox.TabIndex = 5;
            // 
            // DebugListBox
            // 
            this.DebugListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DebugListBox.FormattingEnabled = true;
            this.DebugListBox.Location = new System.Drawing.Point(12, 346);
            this.DebugListBox.Name = "DebugListBox";
            this.DebugListBox.Size = new System.Drawing.Size(1041, 381);
            this.DebugListBox.TabIndex = 4;
            // 
            // DebugButton
            // 
            this.DebugButton.Location = new System.Drawing.Point(12, 12);
            this.DebugButton.Name = "DebugButton";
            this.DebugButton.Size = new System.Drawing.Size(75, 23);
            this.DebugButton.TabIndex = 3;
            this.DebugButton.Text = "Debug";
            this.DebugButton.UseVisualStyleBackColor = true;
            this.DebugButton.Click += new System.EventHandler(this.DebugButtonClick);
            // 
            // RoundByControlButton
            // 
            this.RoundByControlButton.Location = new System.Drawing.Point(93, 12);
            this.RoundByControlButton.Name = "RoundByControlButton";
            this.RoundByControlButton.Size = new System.Drawing.Size(145, 23);
            this.RoundByControlButton.TabIndex = 6;
            this.RoundByControlButton.Text = "RoundByControl";
            this.RoundByControlButton.UseVisualStyleBackColor = true;
            this.RoundByControlButton.Click += new System.EventHandler(this.RoundByControlButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 739);
            this.Controls.Add(this.RoundByControlButton);
            this.Controls.Add(this.InfoListBox);
            this.Controls.Add(this.DebugListBox);
            this.Controls.Add(this.DebugButton);
            this.Name = "MainForm";
            this.Text = "EntityFrameworkORM";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox InfoListBox;
        private System.Windows.Forms.ListBox DebugListBox;
        private System.Windows.Forms.Button DebugButton;
        private System.Windows.Forms.Button RoundByControlButton;
    }
}

