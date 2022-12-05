
namespace Library.Dto
{
    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int? BookId { get; set; }
        public int? PatentId { get; set; }
    }
}
