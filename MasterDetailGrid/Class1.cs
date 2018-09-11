using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetailGrid
{
    public class MasterData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public List<DetailData> DetailDataList { get; set; }

        public static List<MasterData> GetInstance()
        {
            var masterDataList = new List<MasterData>
                                    {
                                        new MasterData { Id = 1, Name = "xxx", Amount = 10, DetailDataList = new List<DetailData> { new DetailData(1, "aaa1", "OK"), new DetailData(1, "bbb1", "Error"), new DetailData(1, "ccc1", "NoData") } }, 
                                        new MasterData { Id = 2, Name = "yyy", Amount = 20, DetailDataList = new List<DetailData> { new DetailData(2, "aaa2", "OK"), new DetailData(2, "bbb2", "Error"), new DetailData(2, "ccc2", "NoData") } }, 
                                        new MasterData { Id = 3, Name = "zzz", Amount = 30, DetailDataList = new List<DetailData> { new DetailData(3, "aaa3", "OK"), new DetailData(3, "bbb3", "Error"), new DetailData(3, "ccc3", "NoData") } }
                                    };
            return masterDataList;

        }
    }

    public class DetailData
    {
        public DetailData(int Id, string comment, string status)
        {
            this.Id = Id;
            this.Comment = comment;
            this.Status = status;
        }

        public int Id { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }
    }
}
