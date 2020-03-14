using System.ComponentModel.DataAnnotations;

namespace Csi.LookMeUp.Models
{
    public class AccountViewModel
    {
        [Required]
        [Display(Name="Username")]
        public string Username { get;set;}

        [Display(Name="Given name")]
        public string GivenName {get;set;}

    }

    public class RegisterAccountViewModel : AccountViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password1 { get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password2 { get;set;}
    }
}