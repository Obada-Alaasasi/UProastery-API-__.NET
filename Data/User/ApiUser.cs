using Microsoft.AspNetCore.Identity;
using UProastery.Data.DB;

namespace UProastery.Data.User {
    
    //create a base user for the model
    public class ApiUser : IdentityUser {

        public int _Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateEnrolled { get; set; }
        public int? age { get; set; }
        public int? Points { get; set; }

        public ICollection<Order>? MyOrders { get; set; }
    }
}
