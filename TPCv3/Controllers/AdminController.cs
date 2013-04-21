using System.Web.Mvc;
using TPCv3.Models;
using TPCv3.Providers;

namespace TPCv3.Controllers{
    [Authorize]
    public class AdminController : Controller{
        #region Constants and Fields

        private readonly IAuthProvider _authProvider;

        #endregion

        #region Constructors and Destructors

        public AdminController(IAuthProvider authProvider){
            _authProvider = authProvider;
        }

        #endregion

        //
        // GET: /Admin/

        #region Public Methods and Operators

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