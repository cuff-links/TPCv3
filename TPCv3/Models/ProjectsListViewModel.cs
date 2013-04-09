using System.Linq;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;

namespace TPCv3.Models{
    public class ProjectsListViewModel{
        #region Public Properties

        private const int ItemsPerPage = 9;
        private readonly IProjectRepository _repository;

        public ProjectsListViewModel(IProjectRepository repository, int pageNo){
            _repository = repository;
            Projects = _repository.Projects(pageNo - 1, ItemsPerPage);
            TotalProjects = _repository.TotalProjects();
            PagingInfo = new PagingInfo
                             {
                                 CurrentPage = pageNo,
                                 ItemsPerPage = ItemsPerPage,
                                 TotalItems = TotalProjects
                             };
        }

        public string CurrentCategory { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public IQueryable<Project> Projects { get; set; }

        public int TotalProjects { get; private set; }

        #endregion
    }
}