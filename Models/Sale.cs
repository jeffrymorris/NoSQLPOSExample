using System.Collections.Generic;

namespace NoSQLPOSExample.Models
{
    public class Sale
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int StoreId { get; set; }
        public double Total { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}