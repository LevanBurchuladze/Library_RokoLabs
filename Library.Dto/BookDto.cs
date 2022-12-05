
namespace Library.Dto
{
    public class BookDto : EditionDto
    {
        public int BookId { get; set; }
        public string PublicationHouse { get; set; }
        public string ISBN { get; set; }
    }
}
