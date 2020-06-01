using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ChatsterApi.Data;
using ChatsterApi.Dtos;
using ChatsterApi.Helpers;
using ChatsterApi.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ChatsterApi.Controllers
{
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IUserRepository _repo;

        private readonly IPhotoRepository _photoRepo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public PhotosController(IUserRepository repo, IPhotoRepository photoRepo, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;
            _photoRepo = photoRepo;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _photoRepo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);
            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if (!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

            userFromRepo.Photos.Add(photo);

            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { userId = userId, id = photo.Id }, photoToReturn);
            };

            return BadRequest("Could not add the photo");
        }

        [HttpPost("{id}/setMainPhoto")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {

            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userId);

            if (!user.Photos.Any(x => x.Id == id))
                return Unauthorized();

            var photoFromRepo = await _photoRepo.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("This photo is already the main photo");

            var currentMainPhoto = await _repo.GetMainPhoto(userId);

            if (currentMainPhoto != null)
                currentMainPhoto.IsMain = false;

            photoFromRepo.IsMain = true;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Unable to set the main photo");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await _repo.GetUser(userId);

            if (!user.Photos.Any(x => x.Id == id))
            {
                return Unauthorized();
            }

            var photoFromRepo = await _photoRepo.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("Cannot delete the main photo");

            if (photoFromRepo.PublicId != null)
            {
                var deleteParam = new DeletionParams(photoFromRepo.PublicId);
                var result = _cloudinary.Destroy(deleteParam);
                if (result.Result == "ok")
                {
                    _repo.Delete(photoFromRepo);
                }
            }
            else
            {
                _repo.Delete(photoFromRepo);
            }

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Photo cannot be deleted");
        }
    }
}