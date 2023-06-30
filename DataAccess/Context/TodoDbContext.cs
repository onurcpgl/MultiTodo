using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext>options) : base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Todos)
                .WithOne(t => t.User)
                .HasForeignKey(userTodos => userTodos.Userid);
             
                modelBuilder.Entity<Team>()
                    .HasMany(u => u.memberList)
                    .WithMany(t => t.Teams);

            modelBuilder.Entity<Media>()
                .HasOne(media => media.user)
                .WithOne(user => user.media)
                .HasForeignKey<Media>(mediaUser => mediaUser.userId);

            modelBuilder.Entity<Media>()
                .HasOne(media => media.team)
                .WithOne(team => team.media)
                .HasForeignKey<Media>(mt => mt.teamId);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.owner)
                .WithOne(u => u.teamOwner)
                .HasForeignKey<Team>(tu => tu.ownerId);
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Request> Requests { get; set; }


    }
}
