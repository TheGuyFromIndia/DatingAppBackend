using DatingApp.Domain.Entity;
using DatingApp.Domain.Extensions;
using DatingApp.Domain.Dto;
using DatingApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatingApp.AppExtensions;

namespace DatingApp.Controllers
{
    [Authorize]
    public class UsersController(IUserRepository userRepository, IPhotoService photoService) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetUsersAsync()
        {
            return Ok((await userRepository.GetUsersAsync()).Select(x => x.ToMember()));
        }

        
        //[HttpGet]
        //[Route("{id:int}")]
        //public async Task<ActionResult<Member>> GetUserAsync(int id)
        //{
        //    var user = await userRepository.GetByIdAsync(id);

        //    if (user == null) { 
        //        return NotFound();
        //    }

        //    return Ok(user.ToMember());
        //}

        [HttpGet("{username}", Name = "GetUserByUsername")]
        public async Task<ActionResult<Member>> GetUserAsync(string username)
        {
            var user = await userRepository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToMember());
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync(MemberUpdate memberUpdate)
        {
            var user = await GetUser();

            if(CheckIfUserValid(user))
            {
                user.LookingFor = memberUpdate.LookingFor;
                user.Introduction = memberUpdate.Introduction;
                user.Interests = memberUpdate.Interests;
                user.City = memberUpdate.City;
                user.Country = memberUpdate.Country;

                await userRepository.SaveAllAsync();

                return NoContent();
            }

            return BadRequest("Invalid user");
        }

        

        [HttpPost("add-photo")]
        public async Task<ActionResult<Domain.Dto.Photo>> UploadPhotoAsync(IFormFile file)
        {
            var user = await GetUser();

            if (CheckIfUserValid(user))
            {
                var result = await photoService.UploadImageAsync(file);

                if (result.Error == null)
                {
                    var photo = new Domain.Dto.Photo
                    {
                        PublicId = result.PublicId,
                        Url = result.SecureUrl.AbsoluteUri,
                    };

                    user.Photos.Add(photo.ToPhoto());
                    await userRepository.SaveAllAsync();
                        return CreatedAtRoute("GetUserByUsername", new { username = user.Name }, photo.ToPhoto());
                }

                return BadRequest(result.Error.Message);
            }

            return BadRequest("Invalid user");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhotoAsync(int photoId)
        {
            var user = await GetUser();

            if (CheckIfUserValid(user))
            {
                var oldMainPhoto = user.Photos.Where(x => x.IsMain).FirstOrDefault();
                var newMainPhoto = user.Photos.Where(x => x.Id == photoId).FirstOrDefault();

                if(newMainPhoto != null)
                {
                    newMainPhoto.IsMain = true;
                    if (oldMainPhoto != null)
                    {
                        oldMainPhoto.IsMain = false;
                    }
                    await userRepository.SaveAllAsync();

                    return NoContent();
                }

                return BadRequest("Invalid request, photo dosent exist");
            }

            return BadRequest("No such user");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await GetUser();

            if (CheckIfUserValid(user))
            {
                var photo = user.Photos.First(x => x.Id == photoId);
                if(photo == null || photo.IsMain)
                {
                    return BadRequest("This photo cannot be deleted");
                }

                if (photo.PublicId != null) 
                { 
                    var result = await photoService.DeleteImageAsync(photo.PublicId);
                    if(result.Error != null)
                    {
                        return BadRequest(result.Error.Message);
                    }
                }

                user.Photos.Remove(photo);

                await userRepository.SaveAllAsync();
                return Ok();
            }

            return BadRequest("No such user");
        }

        #region private function
        private async Task<User> GetUser()
        {
            var userName = User.GetUserName();

            var user = await userRepository.GetUserByUserNameAsync(userName);
            return user;
        }

        private bool CheckIfUserValid(User user)
        {
            return user != null;
        }
        #endregion
    }

}
