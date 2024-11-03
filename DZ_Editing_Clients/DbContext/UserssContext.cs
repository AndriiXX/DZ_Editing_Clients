using Microsoft.EntityFrameworkCore;
using DZ_Editing_Clients.Models;

public class UserssContext : DbContext
{
        public UserssContext(DbContextOptions<UserssContext> options)
        : base(options) { }
        public DbSet<User> Users { get; set; }
    }

