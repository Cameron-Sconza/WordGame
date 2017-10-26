using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Library.EFModels;

namespace Repository.Context
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Game> Game;
        public DbSet<UserProfile> UserProfile;
        public DbSet<WordBank> WordBank;
        public DbSet<Role> Roles;
        public DbSet<Dictionary> Dictionary;

        RepositoryContext() { }

        public RepositoryContext(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<UserProfile>().ToTable("UserProfile");
            modelBuilder.Entity<WordBank>().ToTable("WordBank");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Dictionary>().ToTable("Dictionary");
            
            modelBuilder.Entity<Game>().Property(g => g.PlayerOne).IsOptional();
            modelBuilder.Entity<Game>().Property(g => g.PlayerTwo).IsOptional();
            modelBuilder.Entity<Game>().Property(g => g.PlayerThree).IsOptional();
            modelBuilder.Entity<Game>().Property(g => g.PlayerFour).IsOptional();
            modelBuilder.Entity<Game>().Property(g => g.PlayerFive).IsOptional();
            modelBuilder.Entity<Game>().Property(g => g.PlayerTurn).IsOptional();
            modelBuilder.Entity<Game>().HasMany(g => g.UserProfile).WithMany();
            modelBuilder.Entity<Game>().HasMany(g => g.WordBank);
            
            modelBuilder.Entity<UserProfile>().HasRequired(u => u.Role);

            modelBuilder.Entity<WordBank>().Property(w => w.GameID).IsRequired();
        }
    }
}
