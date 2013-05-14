using System;
using System.Collections.Generic;
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

        public RedirectToRouteResult GoToPost(int id){
            Post post = _blogRepository.PostForId(id);
            int postMonth = post.PostedOn.Date.Month;
            int postYear = post.PostedOn.Date.Year;
            return RedirectToAction("Post", "Blog", new {month = postMonth, title = post.UrlSlug, year = postYear});
        }

        [HttpPost, ValidateInput(false)]
        public ContentResult AddPost(Post post){
            string json;

            ModelState.Clear();

            if (TryValidateModel(post)){
                int id = _blogRepository.AddPost(post);

                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id,
                                                           success = true,
                                                           message = "Post added successfully."
                                                       });
            }
            else{
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id = 0,
                                                           success = false,
                                                           message = "Failed to add the post."
                                                       });
            }

            return Content(json, "application/json");
        }

        [HttpPost, ValidateInput(false)]
        public ContentResult EditPost(Post post){
            string json;

            ModelState.Clear();

            if (TryValidateModel(post)){
                _blogRepository.EditPost(post);
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id = post.Id,
                                                           success = true,
                                                           message = "Changes saved successfully."
                                                       });
            }
            else{
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id = 0,
                                                           success = false,
                                                           message = "Failed to save the changes."
                                                       });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeletePost(int id){
            _blogRepository.DeletePost(id);

            string json = JsonConvert.SerializeObject(new
                                                          {
                                                              id = 0,
                                                              success = true,
                                                              message = "Post deleted successfully."
                                                          });

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

        public ActionResult Posts(JqInViewModel jqParams){
            IQueryable<Post> posts = _blogRepository.Posts(
                jqParams.page - 1, jqParams.rows, jqParams.sidx, jqParams.sord == "asc");

            int totalPosts = _blogRepository.TotalPosts(false);

            return
                Content(
                    JsonConvert.SerializeObject(
                        new
                            {
                                jqParams.page,
                                records = totalPosts,
                                rows = posts,
                                total = Math.Ceiling(Convert.ToDouble(totalPosts)/jqParams.rows)
                            },
                        new CustomDateTimeConverter()),
                    "application/json");
        }

        public ContentResult Categories(){
            IList<Category> categories = _blogRepository.Categories();

            return Content(JsonConvert.SerializeObject(new
                                                           {
                                                               page = 1,
                                                               records = categories.Count,
                                                               rows = categories,
                                                               total = 1
                                                           }), "application/json");
        }

        [HttpPost]
        public ContentResult AddCategory([Bind(Exclude = "Id")] Category category){
            string json;

            if (ModelState.IsValid){
                int id = _blogRepository.AddCategory(category);
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id,
                                                           success = true,
                                                           message = "Category added successfully."
                                                       });
            }
            else{
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id = 0,
                                                           success = false,
                                                           message = "Failed to add the category."
                                                       });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult EditCategory(Category category){
            string json;

            if (ModelState.IsValid){
                _blogRepository.EditCategory(category);
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id = category.Id,
                                                           success = true,
                                                           message = "Changes saved successfully."
                                                       });
            }
            else{
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id = 0,
                                                           success = false,
                                                           message = "Failed to save the changes."
                                                       });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeleteCategory(int id){
            _blogRepository.DeleteCategory(id);

            string json = JsonConvert.SerializeObject(new
                                                          {
                                                              id = 0,
                                                              success = true,
                                                              message = "Category deleted successfully."
                                                          });

            return Content(json, "application/json");
        }

        public ContentResult Tags(){
            IList<Tag> tags = _blogRepository.Tags();

            return Content(JsonConvert.SerializeObject(new
                                                           {
                                                               page = 1,
                                                               records = tags.Count,
                                                               rows = tags,
                                                               total = 1
                                                           }), "application/json");
        }

        [HttpPost]
        public ContentResult AddTag([Bind(Exclude = "Id")] Tag tag){
            string json;

            if (ModelState.IsValid){
                int id = _blogRepository.AddTag(tag);
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id,
                                                           success = true,
                                                           message = "Tag added successfully."
                                                       });
            }
            else{
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id = 0,
                                                           success = false,
                                                           message = "Failed to add the tag."
                                                       });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult EditTag(Tag tag){
            string json;

            if (ModelState.IsValid){
                _blogRepository.EditTag(tag);
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id = tag.Id,
                                                           success = true,
                                                           message = "Changes saved successfully."
                                                       });
            }
            else{
                json = JsonConvert.SerializeObject(new
                                                       {
                                                           id = 0,
                                                           success = false,
                                                           message = "Failed to save the changes."
                                                       });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeleteTag(int id){
            _blogRepository.DeleteTag(id);

            string json = JsonConvert.SerializeObject(new
                                                          {
                                                              id = 0,
                                                              success = true,
                                                              message = "Tag deleted successfully."
                                                          });

            return Content(json, "application/json");
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