using Amazon.DynamoDBv2.DataModel;

namespace StockPicker.Lambda.Persistence.Models
{
    [DynamoDBTable("StockInfo")]
    public class StockInfo
    {
        [DynamoDBHashKey]
        public required string Uuid { get; set; }

        [DynamoDBProperty]
        public string StockCode { get; set; }

        [DynamoDBProperty]
        public decimal CurrentPrice { get; set; }

        [DynamoDBProperty]
        public decimal Change { get; set; }

        [DynamoDBProperty]
        public decimal OpenPrice { get; set; }

        [DynamoDBProperty]
        public decimal ClosePrice { get; set; }        

        [DynamoDBProperty]
        public DateTime DateInserted { get; set; }
    }
}
