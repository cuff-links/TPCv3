using System.Data.Entity.Migrations;
using TPCv3.Domain.Concrete;

namespace TPCv3.Domain.Migrations{
    internal sealed class Configuration : DbMigrationsConfiguration<EfDbContext>{
        #region Constructors and Destructors

        public Configuration(){
            AutomaticMigrationsEnabled = true;
        }

        #endregion

        #region Methods

        protected override void Seed(EfDbContext context){
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        #endregion
    }
}