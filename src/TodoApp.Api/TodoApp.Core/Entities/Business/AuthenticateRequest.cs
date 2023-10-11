using System.ComponentModel.DataAnnotations;
namespace TodoApp.Core.Entities.Business
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
