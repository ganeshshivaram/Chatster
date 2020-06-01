using ChatsterApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatsterApi.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                UserRole, IdentityUserLogin<int>,
                                IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Message { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                userRole.HasOne(ur => ur.User)
                     .WithMany(r => r.UserRoles)
                     .HasForeignKey(ur => ur.UserId)
                     .IsRequired();
            });


            builder.Entity<Like>()
                   .HasKey(x => new { x.LikerId, x.LikeeId });

            builder.Entity<Like>()
                    .HasOne(x => x.Likee)
                    .WithMany(x => x.Likers)
                    .HasForeignKey(x => x.LikeeId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                     .HasOne(x => x.Liker)
                     .WithMany(x => x.Likees)
                     .HasForeignKey(x => x.LikerId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                    .HasOne(u => u.Sender)
                    .WithMany(m => m.MessagesSent)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                    .HasOne(u => u.Recipient)
                    .WithMany(m => m.MessagesReceived)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}