using WebApplication.CRUDUser.Dto;
using WebApplication.CRUDUser.Entities;

namespace WebApplication.CRUDUser.Services
{
    public interface IUserServices
    {
        public void CreateUser(CreateUserDto input);
        PageResultUserDto GetAllUser(CustomerFilterDto input);
        public user GetByIdUser(int id);
        public user UpdateByIdUser(int id, UpdateUserDto input);
        public void UpdateByIdUserStatus(int id, bool Status);
        public void DeleteByIdUser(int id);
        public string Login(LoginDto input);
    }
}
