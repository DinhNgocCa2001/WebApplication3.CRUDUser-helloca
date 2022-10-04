using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace WebApplication.CRUDUser.Dto
{
    public class CreateCustomerDto
    {
        private string _FullName;
        [StringLength(10, ErrorMessage ="Max thi duoc 10 ki tu")]
        public string FullName
        {
            get => _FullName;
            set => _FullName = value?.Trim();
        }
        public DateTime DateOfBirth { get; set; }
        private string _CCCD;
        [Required]
        public String CCCD
        {
            get => _CCCD;
            set => _CCCD = value?.Trim();
        }
        private string _Address;
        public string Address
        {
            get => _Address; 
            set => _Address = value?.Trim();
        }
    }
}
