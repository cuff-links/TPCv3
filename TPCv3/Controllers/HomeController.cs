using System.Web.Mvc;
using TPCv3.Domain.Abstract;
using TPCv3.Models;

namespace TPCv3.Controllers{
    public class HomeController : Controller{
        #region Constants and Fields

        private readonly IProjectRepository _projectRepository;

        #endregion

        #region Constructors and Destructors

        public HomeController(IProjectRepository projectRepository){
            _projectRepository = projectRepository;
        }

        #endregion

        #region Public Properties

        public int PageSize { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual ActionResult About(){
            return View();
        }

        //
        // GET: /Contact/
        public virtual ActionResult Contact(){
            return View();
        }

        public virtual ActionResult Index(){
            return View();
        }

        public virtual ActionResult Portfolio(string category, int page = 1){
            var model = new ProjectsListViewModel(_projectRepository, page);
            return View(model);
        }

        #endregion
    }
}