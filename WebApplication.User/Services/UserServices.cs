using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication.CRUDUser.Constants;
using WebApplication.CRUDUser.DbContexts;
using WebApplication.CRUDUser.Dto;
using WebApplication.CRUDUser.Entities;
using WebApplication.CRUDUser.Exceptions;
using WebApplication.CRUDUser.Utils;

namespace WebApplication.CRUDUser.Services
{
    public class UserServices : IUserServices
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _Context;
        //private static List<User> _Users = new();
        private static List<user> _FilteUsers = new();
        private readonly IConfiguration _configuration;

        public UserServices(
            ILogger<CustomerServices> logger,
            ApplicationDbContext dbContext,
            IConfiguration configuration)
        {
            _logger = logger;
            _Context = dbContext;
            _configuration = configuration;
        }


        public void CreateUser(CreateUserDto input)
        {
            var find = _Context.Users.FirstOrDefault(s => s.Username == input.Username);

            if (find != null)
            {
                throw new Exception("Tài khoản đã tồn tại");
            }

            _Context.Users.Add(new user
            {
                Username = input.Username,
                Password = CommonUtils.CreateMD5(input.Password),
                Email = input.Email,
                Phone = input.Phone,
                UserType = input.UserType,
                CustomerId = input.CustomerId,
                Status = StatusTypes.Open,
                //Subjects = User.Subjects

            });
            _Context.SaveChanges();
        }
        public PageResultUserDto GetAllUser(CustomerFilterDto input)
        {
            //var Users = _Context.Users.ToList();
            //return Users;

            var Userss = _Context.Users;

            if (input.Keyword != null) //nếu keyword khác null
            {
                _FilteUsers = _Context.Users
                    //kiểm tra nếu Name khác null, nếu Name có chứa ký tự trong keyword
                    .Where(s => s.Username != null && s.Username.Contains(input.Keyword))
                    .ToList();
            }
            int totalItem = Userss.Count();

            _FilteUsers = Userss
                .Skip(input.PageSize * (input.PageIndex - 1))
                .Take(input.PageSize)
                .ToList();

            return new PageResultUserDto
            {
                Users = _FilteUsers,
                TotalUser = totalItem
            };
        }

        public user GetByIdUser(int id)
        {
            var findUser = _Context.Users.FirstOrDefault(u => u.UserId == id);
            if (findUser == null)
            {
                throw new Exception("Tài khoản không tồn tại ");
            }
            return findUser;
        }
        public user UpdateByIdUser(int id, UpdateUserDto User)
        {
            var findUser = _Context.Users.FirstOrDefault(s => s.UserId == id);
            if (findUser == null)
            {
                throw new Exception("Tài khoản không tồn tại ");
            }
            findUser.Username = User.Username;
            findUser.Password = User.Password;
            findUser.Phone = User.Phone;
            findUser.Email = User.Email;
            findUser.UserType = User.UserType;
            findUser.CustomerId = User.CustomerId;
            _Context.SaveChanges();
            return findUser;

        }
        public void UpdateByIdUserStatus(int id, bool Status)
        {
            var findUser = _Context.Users.FirstOrDefault(s => s.UserId == id);
            if (findUser == null)
            {
                throw new Exception("Tài khoản không tồn tại ");
            }
            findUser.Status = Status;
            _Context.SaveChanges();
        }

        public void DeleteByIdUser(int id)
        {
            var findUser = _Context.Users.FirstOrDefault(s => s.UserId == id);
            if (findUser == null)
            {
                throw new Exception("Tài khoản không tồn tại ");
            }
            _Context.Users.Remove(findUser);
            _Context.SaveChanges();
            //return findUser;
        }

        public string Login(LoginDto input)
        {
            var user = _Context.Users.FirstOrDefault(u => u.Username == input.Username);
            if (user == null)
            {
                throw new UserFriendlyException($"Tài khoản \"{input.Username}\" không tồn tại");
            }
            if (user.Status == StatusTypes.Block)
            {
                throw new UserFriendlyException($"Tài khoản \"{input.Username}\" đã bị khóa");
            }


            if (CommonUtils.CreateMD5(input.Password) == user.Password)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.Username),
                    new Claim(CustomClaimTypes.UserType, user.UserType.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddSeconds(_configuration.GetValue<int>("JWT:Expires")),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                throw new UserFriendlyException($"Mật khẩu không chính xác");
            }
        }
    }
}
