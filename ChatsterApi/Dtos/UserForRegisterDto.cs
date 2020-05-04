using System;
using System.ComponentModel.DataAnnotations;

public class UserForRegisterDto
{
    private const int MINIMUM_LENGTH = 4;

    [Required(ErrorMessage = "Username cannot be empty")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password cannot be empty")]
    [StringLength(8, MinimumLength = MINIMUM_LENGTH, ErrorMessage = "Password must be between 4 to 8 characters")]
    public string Password { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string KnownAs { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }

    public UserForRegisterDto()
    {
        this.Created = DateTime.Now;
        this.LastActive = DateTime.Now;
    }

}