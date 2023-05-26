using Microsoft.AspNetCore.Identity;
using UProastery.Data.DB;

namespace UProastery.Data.User {
    
    //create a base user for the model with int-type primary key
    public class ApiUser : IdentityUser<int> {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateEnrolled { get; set; }
        public int? age { get; set; }
        public int? Points { get; set; }

        public ICollection<Order>? MyOrders { get; set; }
    }
    //create a base role for the model with int-type primary key
    public class ApiRole : IdentityRole<int> {

    }
}
