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
    using System.Threading.Tasks;

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
        //private const string ConnectionString = @"User Id=developer; pwd=dupel; Host=192.168.168.7;Character Set=utf8; SslMode = none;";
        private const string ConnectionString = @"User Id=pilule; pwd=xxxxx; Host=urb31075.ru; Port=3306; Character Set=utf8; SslMode = none;";

        /// <summary>
        /// Gets the last error.
        /// </summary>
        public static string LastError { get; private set; }

        /// <summary>
        /// The test reading.
        /// </summary>
        /// <param name="marker">
        /// The marker.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckVersionAdoNet(string marker)
        {
            try
         {
             var conn = new MySqlConnection(ConnectionString);
             conn.Open();
             var cmd = new MySqlCommand
             {
                 Connection = conn,
                 CommandText = "select * from Dupel.Version"
             };

             var reader = cmd.ExecuteReader();
             var infoList = new List<string>();
             while (reader.Read())
             {
                 var ver = reader["info"].ToString();
                    infoList.Add(ver); 
             }

             conn.Close();
             return infoList.Contains(marker);
         }
         catch (Exception)
         {
             return false;
         }
      }

        /// <summary>
        /// The check version daper orm.
        /// </summary>
        /// <param name="marker">
        /// The marker.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckVersionDaperOrm(string marker)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<string>("Select info From Dupel.Version").ToList();
                    return infoList.Contains(marker);
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// The get debug value.
        /// </summary>
        /// <param name="table">
        /// The table.
        /// </param>
        /// <returns>
        /// Возврат тестового значения
        /// </returns>
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


        public static GoodsDictionary GetFirstGoodsByTemplate(string template)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var sql = $"Select * From Dupel.GoodsDictionary Where Name like '{template}%' order by Name";
                    var infoList = db.Query<GoodsDictionary>(sql).ToList();
                    if (infoList.Count > 0)
                    {
                        return infoList.First();
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// The get stock balance.
        /// </summary>
        /// <returns>
        /// The return StockBalance
        /// </returns>
        public static IEnumerable<StockBalance> GetStockBalance()
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<StockBalance>("Select * From Dupel.StockBalance").ToList();
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
        /// The get goods dictionary.
        /// </summary>
        /// <returns>
        /// The The return GoodsDictionary
        /// </returns>
        public static IEnumerable<GoodsDictionary> GetGoodsDictionary()
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<GoodsDictionary>("Select * From Dupel.GoodsDictionary").ToList();
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
        /// The get goods dictionary.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The The return GoodsDictionary
        /// </returns>
        public static GoodsDictionary GetGoodsDictionary(int id)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<GoodsDictionary>("Select * From Dupel.GoodsDictionary where Id = @Id", new { Id = id }).ToList();
                    if (infoList.Count > 0)
                    {
                        return infoList.First();
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// The get goods dictionary.
        /// </summary>
        /// <param name="idlist">
        /// The idlist.
        /// </param>
        /// <returns>
        /// The The return GoodsDictionary
        /// </returns>
        public static IEnumerable<GoodsDictionary> GetGoodsDictionary(List<int> idlist)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<GoodsDictionary>("Select * From Dupel.GoodsDictionary where Id in @idlist", new { idlist }).ToList();
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
        /// The execute storage proc.
        /// </summary>
        /// <param name="maxId">
        /// The max id.
        /// </param>
        /// <param name="cnt">
        /// The cnt.
        /// </param>
        /// <returns>
        /// The return GoodsDictionary
        /// </returns>
        public static IEnumerable<GoodsDictionary> ExecuteStorageProc(int maxId, out int cnt)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var p = new DynamicParameters();
                    p.Add("@maxId", maxId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@cnt", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var infoList = db.Query<GoodsDictionary>(
                        "Dupel.TestStorageProc",
                        p,
                        commandType: CommandType.StoredProcedure).ToList();
                    cnt = p.Get<int>("@cnt");
                    return infoList;
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                cnt = -1;
                return null;
            }
        }

        /// <summary>
        /// The execute non select command.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int ExecuteNonSelectCommand()
        {
            const string Sql = "CREATE TABLE Dupel.XXX ( "
                               + "Id INT(11) NOT NULL AUTO_INCREMENT, "
                               + "PRIMARY KEY(Id)) "
                               + "ENGINE = INNODB; ";
            try
            {
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

        /// <summary>
        /// The drop data base.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
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

        /// <summary>
        /// The drop data base async.
        /// </summary>
        /// <param name="outLog">
        /// The out log.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<int> DropDataBaseAsync(Action<string> outLog)
        {
            var result = -1;
            outLog?.Invoke(@"Execute DropDataBaseAsync");
            await Task.Run(() => { result = DropDataBase(); });
            outLog?.Invoke(result < 0 ? $"DropDataBase Error: {LastError}" : "DropDataBase Done!");
            return result;
        }

        /// <summary>
        /// The execute script.
        /// </summary>
        /// <param name="scriptName">
        /// The script name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
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

        /// <summary>
        /// The execute script async.
        /// </summary>
        /// <param name="scriptName">
        /// The script name.
        /// </param>
        /// <param name="outLog">
        /// The out log.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<int> ExecuteScriptAsync(string scriptName, Action<string> outLog)
        {
            var result = -1;
            outLog?.Invoke($"ExecuteScript: {scriptName}");
            await Task.Run(() => { result = ExecuteScript(scriptName); });
            outLog?.Invoke(result < 0 ? $"ExecuteScript Error: {LastError}" : "ExecuteScript Done!");
            return result;
        }

        /// <summary>
        /// The insert data execute.
        /// </summary>
        /// <param name="scriptName">
        /// The script name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
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

        /// <summary>
        /// The insert goods dictionary.
        /// </summary>
        /// <param name="goodsDictionary">
        /// The goods dictionary.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int InsertGoodsDictionary(IEnumerable<GoodsDictionary> goodsDictionary)
        {
            try
            {
                const string Sql = "INSERT INTO Dupel.GoodsDictionary(Code, Name, Price, Comment) VALUES(@Code, @Name, @Price, @Comment)";
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
        /// The insert goods dictionary async.
        /// </summary>
        /// <param name="goodsDictionary">
        /// The goods dictionary.
        /// </param>
        /// <param name="outLog">
        /// The out log.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<int> InsertGoodsDictionaryAsync(IEnumerable<GoodsDictionary> goodsDictionary, Action<string> outLog)
        {
            var result = -1;
            var goodsDictionaries = goodsDictionary as IList<GoodsDictionary> ?? goodsDictionary.ToList();
            outLog?.Invoke($"InsertGoodsDictionary: {goodsDictionaries.Count} Elements to Add");
            await Task.Run(() => { result = InsertGoodsDictionary(goodsDictionaries); });
            outLog?.Invoke(result < 0 ? $"InsertGoodsDictionary Error: {LastError}" : "InsertGoodsDictionary Done!");
            return result;
        }

        /// <summary>
        /// The insert stock balance.
        /// </summary>
        /// <param name="stockBalance">
        /// The stock balance.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int InsertStockBalance(IEnumerable<StockBalance> stockBalance)
        {
            try
            {
                const string Sql = "INSERT INTO Dupel.StockBalance(GoodsId, Amount) VALUES(@GoodsId, @Amount)";
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var result = db.Execute(Sql, stockBalance);
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
        /// The insert stock balance async.
        /// </summary>
        /// <param name="stockBalance">
        /// The stock balance.
        /// </param>
        /// <param name="outLog">
        /// The out log.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<int> InsertStockBalanceAsync(IEnumerable<StockBalance> stockBalance, Action<string> outLog)
        {
            var result = -1;
            var stockBalances = stockBalance as IList<StockBalance> ?? stockBalance.ToList();
            outLog?.Invoke($"InsertStockBalance: {stockBalances.Count} Elements to Add");
            await Task.Run(() => { result = InsertStockBalance(stockBalances); });
            outLog?.Invoke(result < 0 ? $"InsertStockBalance Error: {LastError}" : "InsertStockBalance Done!");
            return result;
        }

        /// <summary>
        /// The multi mapping.
        /// </summary>
        /// <returns>
        /// The The return GoodsDictionary and StockBalance
        /// </returns>
        public static IEnumerable<GoodsAndBalance> MultiMapping()
        {
            const string Sql = "select gd.Id As Id, gd.Name, gd.Price, gd.Comment, sb.Id as StockBalanceId, sb.Amount  "
                               + "from Dupel.GoodsDictionary gd "
                               + "left join Dupel.StockBalance sb on sb.GoodsId = gd.Id ";
            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    var infoList = db.Query<GoodsDictionary, StockBalance, GoodsAndBalance>(
                        Sql,
                        (goods, balance) =>
                            {
                                var gb = new GoodsAndBalance
                                    {
                                        Id = goods.Id,
                                        Name = goods.Name,
                                        Price = goods.Price,
                                        Comment = goods.Comment,
                                        StockBalanceId = balance.StockBalanceId,
                                        Amount = balance.Amount
                                    };
                                return gb;
                            },
                        splitOn: "StockBalanceId").ToList();
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
        /// The multi select.
        /// </summary>
        /// <returns>
        /// The <see cref="Tuple"/>.
        /// </returns>
        public static Tuple<IEnumerable<GoodsDictionary>, IEnumerable<StockBalance>> MultiSelect()
        {
            const string Sql = "Select * from Dupel.GoodsDictionary; Select * from Dupel.StockBalance";

            try
            {
                using (IDbConnection db = new MySqlConnection(ConnectionString))
                {
                    using (var multi = db.QueryMultiple(Sql))
                    {
                        var gd = multi.Read<GoodsDictionary>();
                        var sb = multi.Read<StockBalance>();
                        var infoList = new Tuple<IEnumerable<GoodsDictionary>, IEnumerable<StockBalance>>(gd, sb);
                        return infoList;
                    }
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
            /// Gets or sets the code.
            /// </summary>
            public string Code { get; set; }

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

            /// <summary>
            /// The to string.
            /// </summary>
            /// <returns>
            /// The <see cref="string"/>.
            /// </returns>
            public override string ToString()
            {
                return $"{this.Id} \"{this.Code}\" \"{this.Name}\" {this.Price} \"{this.Comment}\"";
            }
        }

        /// <summary>
        /// The stock balance.
        /// </summary>
        public class StockBalance
        {
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the stock balance id.
            /// </summary>
            public int StockBalanceId { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public int GoodsId { get; set; }

            /// <summary>
            /// Gets or sets the price.
            /// </summary>
            public decimal Amount { get; set; }

            /// <summary>
            /// The to string.
            /// </summary>
            /// <returns>
            /// The <see cref="string"/>.
            /// </returns>
            public override string ToString()
            {
                return $"{this.Id} {this.GoodsId} {this.Amount}";
            }
        }

        /// <summary>
        /// The goods and balance.
        /// </summary>
        public class GoodsAndBalance
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

            /// <summary>
            /// Gets or sets the stock balance id.
            /// </summary>
            public int StockBalanceId { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public decimal Amount { get; set; }

            /// <summary>
            /// The to string.
            /// </summary>
            /// <returns>
            /// The <see cref="string"/>.
            /// </returns>
            public override string ToString()
            {
                return $"{this.Id} \"{this.Name}\" {this.Price} \"{this.Comment}\" {this.StockBalanceId} {this.Amount}";
            }
        }
    }
}
