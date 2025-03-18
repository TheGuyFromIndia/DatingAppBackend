namespace DatingApp.Domain.Dto
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public bool IsMain { get; set; }
    }
}