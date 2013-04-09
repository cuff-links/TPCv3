using System.Data.Entity;
using TPCv3.Domain.Entities;

namespace TPCv3.Domain.Concrete{
    public class EfDbContext : DbContext{
        #region Public Properties

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Tag> Tags { get; set; }

        #endregion
    }
}