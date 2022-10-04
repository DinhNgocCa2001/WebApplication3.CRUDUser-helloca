using System.Numerics;

namespace WebApplication.CRUDUser.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String CCCD { get; set; }
        public string Address { get; set; }
        public List<user> Users { get; set; }

    }
}
