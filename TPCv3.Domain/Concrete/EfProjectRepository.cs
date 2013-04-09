using System.Linq;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;

namespace TPCv3.Domain.Concrete{
    public class EfProjectRepository : IProjectRepository{
        #region Constants and Fields

        private readonly EfDbContext _context = new EfDbContext();

        #endregion

        public IQueryable<Project> ProjectsForCategory(string categorySlug, int pageNo, int pageSize){
            var projectsForCategory =  _context.Projects.Where(
                p => p.Category.Equals(categorySlug)).Skip
                (pageNo*pageSize).Take(pageSize);
            return projectsForCategory;
        }

        public int TotalProjects(){
            var projectCount =  _context.Projects.Count();
            return projectCount;
        }

        public int TotalProjectsForCategory(string categorySlug){
            var projectCountForCategory =  _context.Projects.Count(p => p.Category.Equals(categorySlug));
            return projectCountForCategory;
        }

        public IQueryable<Project> Projects(int pageNo, int pageSize){
            var projects = _context.Projects.Skip(pageNo*pageSize).Take(pageSize);
            return projects;
        }
    }
}