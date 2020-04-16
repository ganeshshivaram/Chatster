using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChatsterApi.Data;
using ChatsterApi.Models;
using Newtonsoft.Json;

public class Seed
{
    public static void SeedUsers(DataContext context)
    {
        if (!context.Users.Any())
        {
            var fileData = File.ReadAllText("Data/SeedData/userseed.json");
            var users = JsonConvert.DeserializeObject<List<User>>(fileData);
            byte[] passwordHash, passwordSalt;
            foreach (var user in users)
            {
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.Username = user.Username.ToLower();
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}