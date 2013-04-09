using System;
using System.Web;
using System.Web.Mvc;
using TPCv3.Domain.Abstract;
using TPCv3.Models;

namespace TPCv3.Controllers{
    public class BlogController : Controller{
        #region Constants and Fields

        private readonly IBlogRepository _blogRepository;

        #endregion

        #region Constructors and Destructors

        //Constructor injecting the blog repository
        public BlogController(IBlogRepository blogRepository){
            this._blogRepository = blogRepository;
        }

        #endregion

        #region Public Methods and Operators

        //Get Blog/Category Action
        public virtual ViewResult Category(string category, int pageNo = 1){
            var viewModel = new ListPostViewModel(this._blogRepository, category, "Category", pageNo);
            if (viewModel.Category == null){
                throw new HttpException(404, "Category not found.");
            }
            this.ViewBag.Title = String.Format(@"Latest posts on category ""{0}""", viewModel.Category.Name);
            return View("List", viewModel);
        }

        //Get Blog/List Action
        public virtual ActionResult List(int pageNo = 1){
            var viewModel = new ListPostViewModel(this._blogRepository, pageNo);

            this.ViewBag.Title = "Latest Posts";

            return View("List", viewModel);
        }

        //Get Blog/Tag Action
        public virtual ViewResult Tag(string tag, int pageNo = 1){
            var viewModel = new ListPostViewModel(this._blogRepository, tag, "Tag", pageNo);
            if (viewModel.Tag == null){
                throw new HttpException(404, "Tag not found.");
            }
            this.ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""", viewModel.Tag.Name);
            return View("List", viewModel);
        }

        //Get Blog/Search Action
        public virtual ViewResult Search(string txtSearch, int pageNo = 1){
            ViewBag.Title = String.Format(@"Lists of posts found 
                        for search text ""{0}""", txtSearch);

            var viewModel = new ListPostViewModel(_blogRepository, txtSearch, "Search", pageNo);
            return View("List", viewModel);
        }

        //Get Blog/Posts Action
        public virtual ViewResult Post(int month, int year, string title){
            var post = _blogRepository.Post(month, year, title);

            if (post == null){
                throw new HttpException(404, "Post not found.");
            }

            if (post.Published == false && User.Identity.IsAuthenticated == false){
                throw new HttpException(401, "The post is not published");
            }
            return View(post);
        }

        //Get PartialViewResult Action Blog/Sidebars
        [ChildActionOnly]
        public PartialViewResult SideBars(){
            var widgetModel = new WidgetViewModel(_blogRepository);
            return PartialView("_Sidebars", widgetModel);
        }

        #endregion
    }
}