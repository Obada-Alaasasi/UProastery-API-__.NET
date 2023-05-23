using System.Text.Json.Serialization;
using UProastery.Data.DB;

namespace UProastery.Data {

    public class OrderItemDTO {
        public int ItemId { get; set; }
        [JsonIgnore]
        public Item? Item { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderItemDTO_OUT {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDTO_OUT {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        public int UserId { get; set; }
        public List<OrderItemDTO_OUT> OrderItems { get; set; }
    }
}
