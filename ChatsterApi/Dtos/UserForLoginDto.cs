using System.ComponentModel.DataAnnotations;

public class UserForLoginDto 
{
    private const int MINIMUM_LENGTH = 4;

    [Required(ErrorMessage="Username cannot be empty")]
    public string Username { get; set; }    

    [Required(ErrorMessage="Password cannot be empty")]
    [StringLength(10, MinimumLength = MINIMUM_LENGTH, ErrorMessage="Password must be between 4 to 10 characters")]
    public string Password { get; set; }
}