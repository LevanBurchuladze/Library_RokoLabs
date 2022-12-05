
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public abstract class Edition
    {
        private string _title;
        private int _publicationYear;
        private string _publicationPlace;
        private int _countPages;
        private string _description;

        public int EditionId { get; set; }

        [Required(ErrorMessage = "Length is small. Min lenght is 1")]
        [StringLength(300, ErrorMessage = "Length is big. Max lenght is 300")]
        public string Title 
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public int Type { get; set; }

        [StringLength(200, ErrorMessage = "Length is big. Max lenght is 200")]
        [RegularExpression(@"((([A-Z])([a-z])*\s)?([A-Z])([a-z])*(-([A-Z]))?([a-z])*)|((([А-Я])([а-я])*\s)?([А-Я])([а-я])*(-([А-Я]))?([а-я])*)",
            ErrorMessage = "This city format is not available")]
        public string PublicationPlace 
        {
            get
            {
                return _publicationPlace;
            }
            set
            {
                _publicationPlace = value;
            }
        }

        public int PublicationYear 
        {
            get
            {
                return _publicationYear;
            }
            set
            {
                if (value > DateTime.Now.Year)
                {
                    throw new WrongData("The year of publication cannot exceed the current year.");
                }
                _publicationYear = value;
            } 
        }

        [Range(0, int.MaxValue, ErrorMessage = "The number of pages cannot be lower than 0")]
        public int CountPages 
        {
            get
            {
                return _countPages;
            }
            set
            {
                _countPages = value;
            } 
        }

        [StringLength(2000, ErrorMessage = "Length is big. Max lenght is 2000")]
        public string Description 
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            } 
        }

        public virtual string GetTitle()
        {
            return null;
        }

        public virtual string GetIdentifier()
        {
            return null;
        }

        public virtual string GetCount()
        {
            return _countPages.ToString();
        }
    }
}
