using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Newtonsoft.Json;

using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;
using TPCv3.Helpers;
using TPCv3.Models;
using TPCv3.Providers;

namespace TPCv3.Controllers{
    [Authorize]
    public class AdminController : Controller{
        #region Constants and Fields

        private readonly IAuthProvider _authProvider;

        private readonly IBlogRepository _blogRepository;

        #endregion

        #region Constructors and Destructors

        public AdminController(IAuthProvider authProvider, IBlogRepository blogRepository = null){
            _authProvider = authProvider;
            _blogRepository = blogRepository;
        }

        #endregion

        #region Public Methods and Operators

        [HttpPost]
        public ContentResult AddPost(Post post){
            string json;

            if (ModelState.IsValid){
                int id = _blogRepository.AddPost(post);

                json = JsonConvert.SerializeObject(new{ id, success = true, message = "Post added successfully." });
            }
            else{
                json = JsonConvert.SerializeObject(new{ id = 0, success = false, message = "Failed to add the post." });
            }

            return Content(json, "application/json");
        }

        public ContentResult GetCategoriesHtml(){
            IOrderedEnumerable<Category> categories = _blogRepository.Categories().OrderBy(s => s.Name);

            var sb = new StringBuilder();
            sb.AppendLine(@"<select>");

            foreach (Category category in categories){
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>", category.Id, category.Name));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }

        public ContentResult GetTagsHtml(){
            IOrderedEnumerable<Tag> tags = _blogRepository.Tags().OrderBy(s => s.Name);

            var sb = new StringBuilder();
            sb.AppendLine(@"<select multiple=""multiple"">");

            foreach (Tag tag in tags){
                sb.AppendLine(string.Format(@"<option value=""{0}"">{1}</option>", tag.Id, tag.Name));
            }

            sb.AppendLine("<select>");
            return Content(sb.ToString(), "text/html");
        }

        //
        // GET: /Admin/

        [AllowAnonymous]
        public ActionResult Login(string returnUrl){
            if (_authProvider.IsLoggedIn){
                return RedirectToUrl(returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl){
            if (ModelState.IsValid && _authProvider.Login(model.Username, model.Password)){
                return RedirectToUrl(returnUrl);
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult Logout(){
            _authProvider.Logout();

            return RedirectToAction("Login", "Admin");
        }

        public ActionResult Manage(){
            return View();
        }

        public ActionResult Posts(){
            // TODO: return posts based on the pagination parameters
            // and order them based upon the order parameter sent by jqgrid
            return View();
        }

        public ActionResult Posts(JqInViewModel jqParams){
            IQueryable<Post> posts = _blogRepository.Posts(
                jqParams.page - 1, jqParams.rows, jqParams.sidx, jqParams.sord == "asc");

            int totalPosts = _blogRepository.TotalPosts(false);

            return
                Content(
                    JsonConvert.SerializeObject(
                        new{
                               jqParams.page,
                               records = totalPosts,
                               rows = posts,
                               total = Math.Ceiling(Convert.ToDouble(totalPosts) / jqParams.rows)
                           },
                        new CustomDateTimeConverter()),
                    "application/json");
        }

        #endregion

        #region Methods

        private ActionResult RedirectToUrl(string returnUrl){
            bool isLocal = Url.IsLocalUrl(returnUrl);
            if (isLocal){
                return Redirect(returnUrl);
            }
            return RedirectToAction("Manage");
        }

        #endregion
    }
}