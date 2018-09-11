using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DapperORM
{
    using System.Data.SQLite;

    using Dapper;

    public class Catalog
    {
        public int id { get; set; }
        public string author { get; set; }
        public string book { get; set; }
    }

    public partial class DapperForm : Form
    {
        public DapperForm()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var сatalog = new Catalog { id = 1, author = "Dupel", book = "Hren" };
            this.Update(сatalog);
            var list = this.ReadAll();
            foreach (var item in list)
            {
                this.listBox1.Items.Add($"{item.id} {item.book} {item.author}");
            }

            var z = this.Find(1);
            this.listBox1.Items.Add($"{z.id} {z.book} {z.author}");
        }

        public List<Catalog> ReadAll()
        {
            using (IDbConnection db = new SQLiteConnection("Data Source=d:\\config.sqlite; Version=3;"))
            {
                return db.Query<Catalog>("Select * From Catalog").ToList();
            }
        }

        public Catalog Find(int id)
        {
            using (IDbConnection db = new SQLiteConnection("Data Source=d:\\config.sqlite; Version=3;"))
            {
                return db.Query<Catalog>("Select * From Catalog WHERE Id = @id", new { id }).SingleOrDefault();
            }
        }

        public int Update(Catalog сatalog)
        {
            using (IDbConnection db = new SQLiteConnection("Data Source=d:\\config.sqlite; Version=3;"))
            {
                string sqlQuery = "UPDATE Catalog SET author = @author, book = @book WHERE id = @id";
                int rowsAffected = db.Execute(sqlQuery, сatalog);
                return rowsAffected;
            }
        }
    }
}
