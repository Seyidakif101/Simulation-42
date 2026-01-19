using System.ComponentModel.DataAnnotations;

namespace Simulation_42.ViewModels.AccountViewModels
{
    public class LoginVM
    {
        [Required, MaxLength(256), MinLength(3), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(6),DataType(DataType.Password) ]
        public string Password { get; set; } = string.Empty;
    }
}
