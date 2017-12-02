using System.Collections.Generic;
using Couchbase;

namespace NoSQLPOSExample.Models
{
    public class Sale : Document<Sale>
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int StoreId { get; set; }
        public double Total { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}