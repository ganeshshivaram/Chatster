using System;
using System.Collections.Generic;
using ChatsterApi.Models;

namespace ChatsterApi.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }

        public string Introduction { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastActive { get; set; }

        public string Interests { get; set; }

        public string LookingFor { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PhotoUrl { get; set; }

        public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
}