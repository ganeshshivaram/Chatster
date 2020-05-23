using ChatsterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatsterApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Message { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

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