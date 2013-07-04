using System.Collections.Generic;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;

namespace TPCv3.Models{
    public class PortfolioSideBarViewModel{
        private readonly IProjectRepository _projectRepository;

        public PortfolioSideBarViewModel(IProjectRepository projectRepository){
            _projectRepository = projectRepository;
            Categories = _projectRepository.Categories()
                ;
        }

        public IList<ProjectCategory> Categories { get; private set; }
    }
}