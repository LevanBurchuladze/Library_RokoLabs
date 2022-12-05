
using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class User
    {
        [RegularExpression(@"^[a-zA-Z]([a-zA-Z0-9]|_?)+[a-zA-Z0-9]$", ErrorMessage = "login spelled wrong")]
        public string Login { get; set; }

        [MinLength(3, ErrorMessage = "Length is small. Min lenght is 3")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
