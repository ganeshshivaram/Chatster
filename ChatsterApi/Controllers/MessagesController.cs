using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ChatsterApi.Data;
using ChatsterApi.Dtos;
using ChatsterApi.Helpers;
using ChatsterApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatsterApi.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/users/{userId}/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        public MessagesController(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var message = await _messageRepository.GetMessage(id);

            if (message == null)
                return NotFound();

            return Ok(message);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messagesFromRepo = await _messageRepository.GetMessageThread(userId, recipientId);

            var messagesToReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messagesToReturn);

        }
        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery]MessageParams messageParams)
        {

            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            messageParams.UserId = userId;

            var messagesFromRepo = await _messageRepository.GetMessagesForUser(messageParams);

            this.HttpContext.Response.AddPaginationHeaders(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize,
                        messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);

            var messagestoReturn = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messagestoReturn);

        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDto messageForCreationDto)
        {
            var sender = await _userRepository.GetUser(userId);

            if (sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            messageForCreationDto.SenderId = userId;

            var recipientUser = await _userRepository.GetUser(messageForCreationDto.RecipientId);

            if (recipientUser == null)
                return BadRequest("Count not find user");

            var message = _mapper.Map<Message>(messageForCreationDto);

            _userRepository.Add<Message>(message);

            var messageToReturn = _mapper.Map<MessageToReturnDto>(message);

            return CreatedAtRoute("GetMessage", new { userId, id = message.Id }, messageToReturn);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _messageRepository.GetMessage(id);

            if (messageFromRepo.SenderId == userId)
                messageFromRepo.SenderDeleted = true;

            if (messageFromRepo.RecipientId == userId)
                messageFromRepo.RecipientDeleted = true;

            if (messageFromRepo.SenderDeleted && messageFromRepo.RecipientDeleted)
                _userRepository.Delete<Message>(messageFromRepo);

            if (await _userRepository.SaveAll())
                return NoContent();

            return BadRequest("Unable to delete the message");
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkMessageAdRead(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _messageRepository.GetMessage(id);

            if (messageFromRepo.RecipientId != userId)
                return Unauthorized();

            messageFromRepo.IsRead = true;
            messageFromRepo.DateRead = DateTime.Now;

            await _userRepository.SaveAll();

            return NoContent();
        }
    }
}