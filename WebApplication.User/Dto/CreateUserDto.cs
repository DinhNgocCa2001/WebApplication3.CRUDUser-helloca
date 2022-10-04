using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace WebApplication.CRUDUser.Dto
{
    public class CreateUserDto
    {
        [Required]
        private string _Username;
        public string Username 
        { 
            get => _Username;
            set => _Username = value?.Trim();
        }
        private string _Password;
        public string Password 
        {
            get => _Password; 
            set => _Password = value?.Trim(); 
        }
        private string _Email;
        public string Email 
        { 
            get => _Email;
            set => _Email = value?.Trim();
        }
        private string _Phone;
        public String Phone 
        {
            get => _Phone;
            set => _Phone = value?.Trim();
        }
        public int UserType { get; set; }
        public int CustomerId { get; set; }
    }
}
