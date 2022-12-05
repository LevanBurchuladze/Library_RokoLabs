
using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Author
    {
        private string _firstName;
        private string _secondName;

        public int AuthorId { get; set; }

        [StringLength(50,ErrorMessage = "Length is big. Max lenght is 50")]
        [RegularExpression(@"(^([A-Z])([a-z])*(-([A-Z]))?([a-z])*)|(^([А-Я])([а-я])*(-([А-Я]))?([а-я])*)", ErrorMessage = "Invalid text")]
        [Required]
        public string FirstName 
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            } 
        }

        [StringLength(200)]
        [RegularExpression(@"((([A-Z])([a-z])*\s)?([A-Z])([a-z])*(-([A-Z]))?([a-z])*)|((([А-Я])([а-я])*\s)?([А-Я])([а-я])*(-([А-Я]))?([а-я])*)", ErrorMessage = "Invalid text")]
        [Required]
        public string SecondName 
        {
            get
            {
                return _secondName;
            }
            set
            {
                _secondName = value;
            } 
        }

        public bool Equals(Author other)
        {
            if (other is null)
                return false;

            return this.FirstName == other.FirstName && this.SecondName == other.SecondName;
        }

        public override int GetHashCode() => (FirstName, SecondName).GetHashCode();

        public override string ToString()
        {
            return $"{FirstName} {SecondName} ";
        }
    }

}
