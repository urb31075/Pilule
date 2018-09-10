// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqLiteStorage.cs" company="urb31075">
// All Right Reserved 
// </copyright>
// <summary>
//   Defines the SqLiteStorage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ConfigStorage
{
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The sq lite storage.
    /// </summary>
    public class SqLiteStorage
    {
        /// <summary>
        /// The сonfig file name.
        /// </summary>
        private readonly string сonfigFileName;

        public SqLiteStorage(string configFileName)
        {
            this.сonfigFileName = configFileName;
        }

        /// <summary>
        /// The create config storage.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CreateConfigStorage()
        {
            this.LastError = string.Empty;
            if (!File.Exists(this.сonfigFileName))
            {
                SQLiteConnection.CreateFile(this.сonfigFileName);
            }

            try
            {
                var conn = new SQLiteConnection("Data Source=" + this.сonfigFileName + ";Version=3;");
                conn.Open();

                var cmd = new SQLiteCommand
                {
                    Connection = conn,
                    CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT)"
                };

                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (SQLiteException ex)
            {
                this.LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// The read config value.
        /// </summary>
        /// <param name="valueName">
        /// The value name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ReadConfigValue(string valueName, out string value)
        {
            this.LastError = string.Empty;
            value = string.Empty;
            var table = new DataTable();

            var conn = new SQLiteConnection("Data Source=" + this.сonfigFileName + ";Version=3;");
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                this.LastError = @"Eror open connection with database";
                return false;
            }

            try
            {
                const string SqlQuery = "SELECT * FROM Catalog";
                var adapter = new SQLiteDataAdapter(SqlQuery, conn);
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    for (var i = 0; i < table.Rows.Count; i++)
                    {
                        value = table.Rows[i].ItemArray.Aggregate(value, (current, item) => current + item.ToString() + " ");
                    }
                }
                else
                {
                    this.LastError = @"Database is empty";
                    return false;
                }

                conn.Close();
                return true;
            }
            catch (SQLiteException ex)
            {
                this.LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// The write config value.
        /// </summary>
        /// <param name="valueName">
        /// The value name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool WriteConfigValue(string valueName, string value)
        {
            this.LastError = string.Empty;
            try
            {
                const string Author = "111";
                const string Book = "ret111";

                var conn = new SQLiteConnection("Data Source=" + this.сonfigFileName + ";Version=3;");
                conn.Open();
                var cmd = new SQLiteCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO Catalog ('author', 'book') values ('" + Author + "' , '" + Book + "')"
                };
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (SQLiteException ex)
            {
                this.LastError = ex.Message;
                return false;
            }
        }

        public bool TestEf(out string value)
        {
            value = string.Empty;
            var x = new cfgEntities();
            foreach (var items in x.Catalog)
            {
                value= $"{items.id} {items.author}  {items.book}";
            }

            return true;
        }

        /// <summary>
        /// Gets the last error.
        /// </summary>
        public string LastError { get; private set; }
    }
}
