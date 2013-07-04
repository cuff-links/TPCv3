using System.Linq;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;

namespace TPCv3.Models{
    public class ProjectsListViewModel{
        #region Public Properties

        private readonly IProjectRepository _projectRepository;

        public ProjectsListViewModel(IProjectRepository projectRepository, int pageNo, int itemsPerPage){
            _projectRepository = projectRepository;
            Projects = _projectRepository.Projects(pageNo - 1, itemsPerPage);
            TotalProjects = _projectRepository.TotalProjects();
            PagingInfo = new PagingInfo
                             {
                                 CurrentPage = pageNo,
                                 ItemsPerPage = itemsPerPage,
                                 TotalItems = TotalProjects
                             };
        }

        public ProjectsListViewModel(IProjectRepository projectRepository, string category, int pageNo, int itemsPerPage){
            _projectRepository = projectRepository;
            Projects = _projectRepository.ProjectsForCategory(category, pageNo - 1, itemsPerPage);
            TotalProjects = _projectRepository.TotalProjectsForCategory(category);
            CurrentCategory = category;
            PagingInfo = new PagingInfo
                             {
                                 CurrentPage = pageNo,
                                 ItemsPerPage = itemsPerPage,
                                 TotalItems = TotalProjects,
                                 CurrentCategory = category
                             };
        }

        public string CurrentCategory { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public IQueryable<Project> Projects { get; set; }

        public int TotalProjects { get; private set; }

        #endregion
    }
}