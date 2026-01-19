using System.ComponentModel.DataAnnotations;

namespace Simulation_42.ViewModels.AccountViewModels
{
    public class RegisterVM
    {
        [Required, MaxLength(256), MinLength(3)]
        public string Username { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(3), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(6), DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    
}
}
