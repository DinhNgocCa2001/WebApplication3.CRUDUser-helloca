using WebApplication.CRUDUser.Entities;

namespace WebApplication.CRUDUser.Dto
{
    public class PageResultCustomerDto
    {
        public List<Customer> Customers { get; set; }
        public int TotalCustomer { get; set; }
    }
}
