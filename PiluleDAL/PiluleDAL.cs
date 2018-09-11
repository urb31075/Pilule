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
    using System.Linq;
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

        /// <summary>
        /// The get goods dictionary.
        /// </summary>
        /// <returns>
        /// Возврат списка товаров
        /// </returns>
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
    }
}
