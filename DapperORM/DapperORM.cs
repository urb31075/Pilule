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

    public partial class DapperOrm
    {
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
