using WebApplication.CRUDUser.Dto;
using WebApplication.CRUDUser.Entities;

namespace WebApplication.CRUDUser.Services
{
    public interface ICustomerServices
    {
        public void CreateCustomer(CreateCustomerDto input);
        PageResultCustomerDto GetAllCustomer(CustomerFilterDto input);
        public Customer GetByIdCustomer(int id);
        public Customer UpdateByIdCustomer(int id, UpdateCustomerDto input);
        public void DeleteByIdCustomer(int id);
        public List<user> ListUserOfCustomer(int CustomerId);

    }
}
