using System;
using System.Collections.Generic;

namespace MysqlEntity.Core.Model
{
    public partial class Billstoreretail
    {
        public int BillId { get; set; }
        public string BillNo { get; set; }
        public DateTime? RetailDate { get; set; }
        public int? ZfbQty { get; set; }
        public int? WxQty { get; set; }
        public int? OfflineQty { get; set; }
        public decimal? ZfbAmount { get; set; }
        public decimal? WxAmount { get; set; }
        public decimal? OfflineAmount { get; set; }
    }
}
