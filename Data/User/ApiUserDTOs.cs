namespace UProastery.Data.User {
    
    // A register DTO for ApiIUser
    public class ApiUserDTO {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? DateEnrolled { get; set; }
        public int? age { get; set; }
        public int? Points { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
       
    }

    // A login DTO for ApiUser
    public class LoginDTO {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // a DTO for login and register op responses
    public class AuthResponse {
        public int UserId { get; set; }
        public string? Token { get; set; }
    }
}
