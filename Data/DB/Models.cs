using UProastery.Data.User;

namespace UProastery.Data.DB {

    // Item table resmebling Products 
    public class Item {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int? Stock { get; set; }
        public DateTime Added { get; set; }
        public int TotalOrders;        

        public List<OrderItem> OrderItems { get; set; }
    }

    // A table that connects between orders and items (M2M)
    public class OrderItem {

        public int Id { get; set; }

        public int Quantity { get; set; }

        // Foreign key to Item
        public int ItemId { get; set; }
        public Item Item { get; set; }

        // Foreign key to Order
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }

    // Orders table
    public class Order {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
        
        // Foreign key to ApiUser : IdentityUser
        public int? UserId { get; set; }
        public ApiUser? User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }


}
