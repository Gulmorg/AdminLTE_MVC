using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdminLTE_MVC.Models
{
    public class UserAccount
    {
        public ulong Id { get; set; }

        [DataType(DataType.Text)]
        public string? Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string? PasswordSalt { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? PasswordHash { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string? AuthorisationLevel { get; set; }
    }
}