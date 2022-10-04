using System.Numerics;

namespace WebApplication.CRUDUser.Entities
{
    public class user
    {
        public int UserId { get; set; }
        
        public string Username { get; set;}
        
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int UserType { get; set; }
        public bool Status { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
