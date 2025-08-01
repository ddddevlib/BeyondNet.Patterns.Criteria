﻿namespace BeyondNet.Patterns.Criteria.Test.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User(int id, string name, string username, string password, string email)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null.");
            Username = username ?? throw new ArgumentNullException(nameof(username), "Username cannot be null.");
            Password = password ?? throw new ArgumentNullException(nameof(password), "Password cannot be null.");
            Email = email ?? throw new ArgumentNullException(nameof(email), "Email cannot be null.");
        }
    }
}
