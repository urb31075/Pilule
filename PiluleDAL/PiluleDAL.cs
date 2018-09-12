// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PiluleDAL.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Defines the PiluleDal type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
namespace PiluleDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Dapper;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// The pilule dal.
    /// </summary>
    public class PiluleDal
    {
        /// <summary>
        /// The connection string.
        /// </summary>
        private const string ConnectionString = @"User Id=developer; pwd=dupel; Host=192.168.168.7;Character Set=utf8; Database=Pilule; SslMode = none;";

        /// <summary>
        /// Gets the last error.
        /// </summary>
        public static string LastError { get; private set; }

        /// <summary>
        /// The test reading.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckVersionAdoNet()
        {
            try
         {
             var conn = new MySqlConnection(ConnectionString);
             conn.Open();
             var cmd = new MySqlCommand
             {
                 Connection = conn,
                 CommandText = "select * from Version"
             };

             var reader = cmd.ExecuteReader();
             var infoList = new List<string>();
             while (reader.Read())
             {
                 var ver = reader["info"].ToString();
                    infoList.Add(ver); 
             }

             conn.Close();
             return infoList.Contains("ver1.0");
         }
         catch (Exception)
         {
             return false;
         }
      }

        /// <summary>
        /// The check version daper orm.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckVersionDaperOrm()
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<string>("Select info From Version").ToList();
                    return infoList.Contains("ver1.0");
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public static dynamic GetDebugValue(string table)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query($"Select * From {table}").ToList();
                    return infoList;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        public static IEnumerable<GoodsDictionary> GetGoodsDictionary()
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<GoodsDictionary>("Select * From GoodsDictionary").ToList();
                    return infoList;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        public static IEnumerable<GoodsDictionary> GetGoodsDictionary(int id)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<GoodsDictionary>("Select * From GoodsDictionary where Id = @Id", new {Id = id}).ToList();
                    return infoList;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        public static IEnumerable<GoodsDictionary> GetGoodsDictionary(List<int> idlist)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<GoodsDictionary>("Select * From GoodsDictionary where Id in @idlist", new { idlist }).ToList();
                    return infoList;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        public static IEnumerable<GoodsDictionary> ExecuteStorageProc()
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<GoodsDictionary>("TestStorageProc", commandType:CommandType.StoredProcedure).ToList();
                    return infoList;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        public static int ExecuteNonSelectCommand()
        {
            try
            {
                const string Sql = "CREATE TABLE Pilule.XXX ( " 
                                   + "Id INT(11) NOT NULL AUTO_INCREMENT, " 
                                   + "PRIMARY KEY(Id)) "
                                   + "ENGINE = INNODB; ";
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var result = db.Execute(Sql);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }

        public static int DropDataBase()
        {
            try
            {
                const string Sql = "DROP DATABASE Dupel";
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var result = db.Execute(Sql);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }

        public static int ExecuteScript(string scriptName)
        {
            try
            {
                var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (string.IsNullOrEmpty(assemblyFolder))
                {
                    LastError = "ExecutingAssembly not found";
                    return -1;
                }

                var fileName = Path.Combine(assemblyFolder, "DbScripts", scriptName + ".txt");
                var lines = File.ReadAllLines(fileName);

                var sql = lines.Aggregate(string.Empty, (current, c) => current + c + " ");
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var result = db.Execute(sql);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }

        public static int InsertDataExecute(string scriptName)
        {
            try
            {
                var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (string.IsNullOrEmpty(assemblyFolder))
                {
                    LastError = "ExecutingAssembly not found";
                    return -1;
                }

                var fileName = Path.Combine(assemblyFolder, "DbScripts", scriptName + ".txt");
                var lines = File.ReadAllLines(fileName);
                var summaryAffect = 0;
                foreach (var sql in lines)
                {
                    if (string.IsNullOrEmpty(sql))
                    {
                        continue;
                    }

                    using (IDbConnection db = new MySqlConnection(ConnectionString))
                    {
                        var result = db.Execute(sql);
                        if (result == 1)
                        {
                            summaryAffect++;
                        }
                        else
                        {
                            LastError = "Affect != 1";
                            return -1;
                        }
                    }
                }

                return summaryAffect;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }

        public static int InsertDataExecuteBulk(IEnumerable<GoodsDictionary> goodsDictionary)
        {
            try
            {
                const string Sql = "INSERT INTO Dupel.GoodsDictionary(Name, Price, Comment) VALUES(@Name, @Price, @Comment)";
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                   {
                       var result = db.Execute(Sql, goodsDictionary);
                       return result;
                   }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// The goods dictionary.
        /// </summary>
        public class GoodsDictionary
        {
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the price.
            /// </summary>
            public decimal Price { get; set; }

            /// <summary>
            /// Gets or sets the comment.
            /// </summary>
            public string Comment { get; set; }

            public override string ToString()
            {
                return $"{this.Id} {this.Name} {this.Price} {this.Comment}";
            }
        }

        public class StockBalance
        {
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public int GoodsId { get; set; }

            /// <summary>
            /// Gets or sets the price.
            /// </summary>
            public decimal Amount { get; set; }

            public override string ToString()
            {
                return $"{this.Id} {this.GoodsId} {this.Amount}";
            }
        }
    }
}
