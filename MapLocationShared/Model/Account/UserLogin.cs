using System.ComponentModel.DataAnnotations;

namespace MapLocationShared.Model.Account
{
    public class UserLogin
    {
        [Required(ErrorMessage = "O LOGIN é obrigatória.")]
        [Display(Name = "Login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A SENHA é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembre-me?")]
        public bool RememberMe { get; set; }

    }
}
