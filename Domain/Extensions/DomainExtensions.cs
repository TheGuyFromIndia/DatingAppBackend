using DatingApp.Domain.Dto;
using DatingApp.Domain.Entity;

namespace DatingApp.Domain.Extensions
{
    public static class DomainExtensions
    {
        public static MemberDto ToMember(this Entity.User user)
        {
            return new MemberDto
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.GetAge(),
                KnownAs = user.KnownAs,
                Created = user.Created,
                LastActive = user.LastActive,
                Gender = user.Gender,
                Introduction = user.Introduction,
                Interests = user.Interests,
                LookingFor = user.LookingFor,
                City = user.City,
                Country = user.Country,
                Photos = user.Photos.Select(p => p.ToPhoto()).ToList(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url // Assuming Photo has a Url property
            };
        }

        public static Dto.PhotoDto ToPhoto(this Entity.Photo photo)
        {
            return new Dto.PhotoDto
            {
                Id = photo.Id,
                Url = photo.Url,
                IsMain = photo.IsMain
            };
        }

        public static Entity.Photo ToPhoto(this Dto.PhotoDto photo)
        {
            return new Entity.Photo
            {
                PublicId = photo.PublicId,
                Url = photo.Url,
                IsMain = photo.IsMain
            };
        }
    }
}
