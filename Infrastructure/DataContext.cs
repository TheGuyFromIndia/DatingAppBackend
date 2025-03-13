﻿using DatingApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } 
    }
}
