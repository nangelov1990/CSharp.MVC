﻿namespace RazorEngineHw.Data
{
    using System.Collections.Generic;
    using Models;

    public class Db
    {
        public static readonly Person[] People = new Person[]
        {
            new Person()
            {
                Name = "John Doe",
                Age = 40,
                Email = "john@office.com",
                IsSubscribed = true
            },
            new Person()
            {
                Name = "John Doe Jr",
                Email = "john@office.com"
            },
            new Person()
            {
                Name = "Mickey Mouse",
                Age = 20,
                IsSubscribed = false
            },
        };
    }
}
