﻿namespace CarDealer.Models.EntityModels
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Logins = new HashSet<Login>();
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<Login> Logins { get; set; }
    }
}