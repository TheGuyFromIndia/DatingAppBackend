using DatingApp.Domain.Dto;
using DatingApp.Domain.Entity;

namespace DatingApp.Domain.Extensions
{
    public static class DomainExtensions
    {
        public static Member ToMember(this User user)
        {
            return new Member
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
                PhotoUrl = user.Photos.FirstOrDefault()?.Url // Assuming Photo has a Url property
            };
        }

        public static Dto.Photo ToPhoto(this Entity.Photo photo)
        {
            return new Dto.Photo
            {
                Id = photo.Id,
                Url = photo.Url,
                IsMain = photo.IsMain
            };
        }
    }
}
