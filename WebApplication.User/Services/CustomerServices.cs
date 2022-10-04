using WebApplication.CRUDUser.DbContexts;
using WebApplication.CRUDUser.Dto;
using WebApplication.CRUDUser.Entities;

namespace WebApplication.CRUDUser.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _Context;
        //private static List<Customer> _Customers = new();
        private static List<Customer> _FilteCustomers = new();
        private static List<user> _ListUser = new();

        public CustomerServices(ILogger<CustomerServices> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _Context = dbContext;
        }


        public void CreateCustomer(CreateCustomerDto input)
        {
            var find = _Context.Customers.FirstOrDefault(s => s.CCCD == input.CCCD);

            if (find != null)
            {
                throw new Exception("Khách hàng đã tồn tại");
            }

            _Context.Customers.Add(new Customer
            {
                FullName = input.FullName,
                DateOfBirth = input.DateOfBirth,
                CCCD = input.CCCD,
                Address = input.Address,
                //Subjects = Customer.Subjects

            });
            _Context.SaveChanges();
        }
        public PageResultCustomerDto GetAllCustomer(CustomerFilterDto input)
        {
            //var Customers = _Context.Customers.ToList();
            //return Customers;

            var Customerss = _Context.Customers;

            if (input.Keyword != null) //nếu keyword khác null
            {
                _FilteCustomers = _Context.Customers
                    //kiểm tra nếu Name khác null, nếu Name có chứa ký tự trong keyword
                    .Where(s => s.FullName != null && s.FullName.Contains(input.Keyword))
                    .ToList();
            }
            int totalItem = Customerss.Count();

            _FilteCustomers = Customerss
                .Skip(input.PageSize * (input.PageIndex - 1))
                .Take(input.PageSize)
                .ToList();

            return new PageResultCustomerDto
            {
                Customers = _FilteCustomers,
                TotalCustomer = totalItem
            };
        }

        public Customer GetByIdCustomer(int id)
        {
            var findCustomer = _Context.Customers.FirstOrDefault(s => s.CustomerId == id);
            if (findCustomer == null)
            {
                throw new Exception("Khách hàng không tồn tại ");
            }
            return findCustomer;
        }
        public Customer UpdateByIdCustomer(int id, UpdateCustomerDto Customer)
        {
            var findCustomer = _Context.Customers.FirstOrDefault(s => s.CustomerId == id);
            if (findCustomer == null)
            {
                throw new Exception("Khách hàng không tồn tại ");
            }
            findCustomer.FullName = Customer.FullName;
            findCustomer.DateOfBirth = Customer.DateOfBirth;
            findCustomer.CCCD = Customer.CCCD;
            findCustomer.Address = Customer.Address;
            _Context.SaveChanges();
            return findCustomer;

        }
        public void DeleteByIdCustomer(int id)
        {
            var findCustomer = _Context.Customers.FirstOrDefault(s => s.CustomerId == id);
            if (findCustomer == null)
            {
                throw new Exception("Khách hàng không tồn tại ");
            }
            _Context.Customers.Remove(findCustomer);
            _Context.SaveChanges();
            //return findCustomer;
        }
        public List<user> ListUserOfCustomer(int id)
        {
            _ListUser = _Context.Users
                    //kiểm tra nếu Name khác null, nếu Name có chứa ký tự trong keyword
                    .Where(s => s.CustomerId == id)
                    .ToList();
            if (_ListUser == null)
            {
                throw new Exception("Không có tài khoản nào ");
            }
            return _ListUser;

        }

    }
}
