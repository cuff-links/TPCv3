using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;

namespace TPCv3.Domain.Concrete{
    public class EfProjectRepository : IProjectRepository{
        #region Constants and Fields

        private readonly EfDbContext _context = MyModelDbContextSingleton.Instance;

        #endregion

<<<<<<< HEAD
        #region IProjectRepository Members

        public IQueryable<Project> ProjectsForCategory(string categorySlug, int pagesToSkip, int pageSize){
            IQueryable<Project> projects;
            if (categorySlug == "All")
            {
                projects = _context.Projects.OrderByDescending(pc => pc.Name).Skip
                    (pagesToSkip*pageSize).Take(pageSize);
            }
            else
            {
                projects =
                    _context.Projects.Include(pc => pc.Category).Where(
                        pc => pc.Category.Name.Equals(categorySlug)).OrderByDescending(pc => pc.Category.Name).Skip
                        (pagesToSkip*pageSize).Take(pageSize);
            }

            return projects;
        }

        public int TotalProjects(){
            int projectCount = _context.Projects.Count();
=======
        public IQueryable<Project> ProjectsForCategory(string categorySlug, int pageNo, int pageSize){
            var projectsForCategory = _context.Projects.Where(
                p => p.Category.Equals(categorySlug)).Skip
                (pageNo*pageSize).Take(pageSize);
            return projectsForCategory;
        }

        public int TotalProjects(){
            var projectCount = _context.Projects.Count();
>>>>>>> 59de76e6a20a87d878d82b2dd67d0cfdbd639233
            return projectCount;
        }

        public int TotalProjectsForCategory(string categorySlug){
<<<<<<< HEAD
            int projectCountForCategory = categorySlug == "All"
                                              ? TotalProjects()
                                              : _context.Projects.Count(p => p.Category.Name.Equals(categorySlug));
=======
            var projectCountForCategory = _context.Projects.Count(p => p.Category.Equals(categorySlug));
>>>>>>> 59de76e6a20a87d878d82b2dd67d0cfdbd639233
            return projectCountForCategory;
        }

        public IQueryable<Project> Projects(int pageNo, int pageSize){
            IQueryable<Project> projects = _context.Projects.OrderBy(c => c.Name).Skip(pageNo*pageSize).Take(pageSize);
            return projects;
        }

        public IEnumerable<Project> AllProjects{
            get { return _context.Projects; }
        }

        public IList<Project> Projects(){
            List<Project> projectList = _context.Projects.OrderBy(c => c.Name).ToList();
            return projectList;
        }

        public IList<ProjectCategory> Categories(){
            List<ProjectCategory> categoryList = _context.ProjectCategories.OrderBy(c => c.Name).ToList();
            return categoryList;
        }

        #endregion
    }
}