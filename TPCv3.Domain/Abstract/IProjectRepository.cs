using System.Linq;

using TPCv3.Domain.Entities;

namespace TPCv3.Domain.Abstract{
    public interface IProjectRepository{
        #region Public Methods and Operators

        IQueryable<Project> Projects(int pageNo, int pageSize);

        IQueryable<Project> ProjectsForCategory(string categorySlug, int pageNo, int pageSize);

        int TotalProjects();

        int TotalProjectsForCategory(string categorySlug);

        #endregion
    }
}