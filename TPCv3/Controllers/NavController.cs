using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPCv3.Domain.Abstract;

namespace TPCv3.Controllers{
    public class NavController : Controller{
        #region Constants and Fields

        private readonly IBlogRepository _repository;

        #endregion

        #region Constructors and Destructors

        public NavController(IBlogRepository repository){
            _repository = repository;
        }

        #endregion

        #region Public Methods and Operators

        public PartialViewResult Menu(string category = null){
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories =
                _repository.AllPosts.Select(c => c.Category.ToString()).Distinct().OrderBy(c => c).AsEnumerable();
            return PartialView(categories);
        }

        #endregion
    }
}