using WebApplication.CRUDUser.Entities;

namespace WebApplication.CRUDUser.Dto
{
    public class PageResultUserDto
    {
        public List<user> Users { get; set; }
        public int TotalUser { get; set; }
    }
}
