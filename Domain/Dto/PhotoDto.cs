﻿namespace DatingApp.Domain.Dto
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; } = null!;
        public bool IsMain { get; set; }
    }
}