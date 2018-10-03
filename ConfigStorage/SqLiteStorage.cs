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
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Dapper;

    /// <summary>
    /// The sq lite storage.
    /// </summary>
    public class SqLiteStorage
    {
        /// <summary>
        /// The сonfig file name.
        /// </summary>
        private readonly string сonfigFileName;

        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqLiteStorage"/> class.
        /// </summary>
        /// <param name="configFileName">
        /// The config file name.
        /// </param>
        public SqLiteStorage(string configFileName)
        {
            this.сonfigFileName = configFileName;
            this.connectionString = $"Data Source={configFileName}; Version=3;"; // Data Source=d:\\config.sqlite; Version=3;
        }

        /// <summary>
        /// Gets the last error.
        /// </summary>
        public string LastError { get; private set; }

        /// <summary>
        /// The create config storage dapper.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CreateConfigStorage()
        {
            this.LastError = string.Empty;
            try
            {
                if (!File.Exists(this.сonfigFileName))
                {
                    SQLiteConnection.CreateFile(this.сonfigFileName);
                }

                using (IDbConnection db = new SQLiteConnection(this.connectionString))
                {
                    return db.Execute("CREATE TABLE IF NOT EXISTS Config (id INTEGER PRIMARY KEY AUTOINCREMENT, key TEXT, value TEXT)") == 0;
                }
            }
            catch (Exception ex)
            {
                this.LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// The read config value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ReadConfigValue(string key, out string value)
        {
            this.LastError = string.Empty;
            value = string.Empty;
            try
            {
                using (IDbConnection db = new SQLiteConnection(this.connectionString))
                {
                    var result = db.Query<string>("SELECT value FROM Config WHERE key = @key", new { key }).ToList();
                    if (result.Count != 1)
                    {
                        return false;
                    }

                    value = result.First();
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// The add config value dapper.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AddConfigValue(string key, string value)
        {
            this.LastError = string.Empty;
            try
            {
                using (var db = new SQLiteConnection(this.connectionString))
                {
                    const string SqlQueryUpdate = "UPDATE Config SET value = @value WHERE key = @key";
                    const string SqlQueryInsert = "INSERT INTO Config ('key', 'value') values (@key, @value)";
                    string probeValue;
                    if (this.ReadConfigValue(key, out probeValue))
                    {
                        return db.Execute(SqlQueryUpdate, new { key, value }) == 1;

                    }

                    return db.Execute(SqlQueryInsert, new { key, value }) == 1;
                }
            }
            catch (Exception ex)
            {
                this.LastError = ex.Message;
                return false;
            }
        }
    }
}
