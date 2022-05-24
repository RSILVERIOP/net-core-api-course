
using System;
using Api.Data.Mapping;
using Api.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } 

        public MyContext (DbContextOptions<MyContext> options) : base (options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    Email = "Admin@gmail.com",
                    CreateAt= DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                }
            );
        }
    }
}