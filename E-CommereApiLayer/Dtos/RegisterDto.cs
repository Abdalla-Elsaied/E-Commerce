using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
    public class RegisterDto
    {
        public string dispalyName { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
    }
}
