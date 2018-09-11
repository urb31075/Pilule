using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EntityFrameworkORM
{
    using System.Drawing;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
        }

        private void DebugButtonClick(object sender, EventArgs e)
        {
            var staffIqTestEntities = new StaffIQTestEntities();

            var logger = new Logger();
            staffIqTestEntities.Database.Log = logger.Log;
            var ages = staffIqTestEntities.Age.Where(c => c.id > 10).Select(c => c.Name).ToArray();
            foreach (var age in ages)
            {
                this.InfoListBox.Items.Add(age);
            }

            foreach (var item in logger.Items)
            {
                this.DebugListBox.Items.Add(item);
            }
        }

        /// <summary>
        /// The logger.
        /// </summary>
        public class Logger
        {
            public Logger()
            {
                this.messages = new List<string>();
            }

            /// <summary>
            /// Gets the Items.
            /// </summary>
            public string[] Items => this.messages.ToArray();

            private readonly List<string> messages;

            public void Log(string message)
            {
                this.messages.Add(message);
            }
        }

        private void RoundByControlButtonClick(object sender, EventArgs e)
        {
            SetFontSizeForControlTree(this.Controls, 9);
        }

        private static void SetFontSizeForControlTree(Control.ControlCollection controlCollection, float fontSize)
        {
            foreach (Control control in controlCollection)
            {
                control.Font = new Font(control.Font.Name, fontSize, control.Font.Style, GraphicsUnit.Point);
                if (control.Controls.Count > 0)
                {
                    SetFontSizeForControlTree(control.Controls, fontSize);
                }
            }
        }
    }
}
