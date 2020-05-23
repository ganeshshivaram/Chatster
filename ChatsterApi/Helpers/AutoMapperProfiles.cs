using System.Linq;
using AutoMapper;
using ChatsterApi.Dtos;
using ChatsterApi.Models;

namespace ChatsterApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListingDto>()
            .ForMember(
                dest => dest.PhotoUrl, opt => opt.MapFrom(
                    src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(
                dest => dest.Age, opt => opt.MapFrom(
                    src => src.DateOfBirth.CalculateAge()
                )
            );

            CreateMap<User, UserForDetailedDto>()
            .ForMember(
                dest => dest.PhotoUrl, opt => opt.MapFrom(
                    src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(
                dest => dest.Age, opt => opt.MapFrom(
                    src => src.DateOfBirth.CalculateAge()
                )
            );
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<Message, MessageForCreationDto>().ReverseMap();

            CreateMap<MessageToReturnDto, Message>().ReverseMap()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(
                    src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url
                )
                )
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(
                    src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url
                )
            );
        }
    }
}