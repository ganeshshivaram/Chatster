using Microsoft.AspNetCore.Identity;

namespace ChatsterApi.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}