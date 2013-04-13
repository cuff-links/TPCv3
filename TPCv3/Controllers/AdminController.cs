using System.Web.Mvc;
using System.Web.Security;

using TPCv3.Models;

namespace TPCv3.Controllers{
    [Authorize]
    public class AdminController : Controller{
        //
        // GET: /Admin/

        #region Public Methods and Operators

        [AllowAnonymous]
        public ActionResult Login(string returnUrl){
            if (this.User.Identity.IsAuthenticated){
                if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl)){
                    return this.Redirect(returnUrl);
                }
                return this.RedirectToAction("Manage");
            }

            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl){
            if (this.ModelState.IsValid){
                if (Membership.ValidateUser(model.Username, model.Password)){
                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    if (!string.IsNullOrEmpty(returnUrl) && this.Url.IsLocalUrl(returnUrl)){
                        return this.Redirect(returnUrl);
                    }
                    return this.RedirectToAction("Manage");
                }
                this.ModelState.AddModelError("", "The username or password provided is incorrect.");
            }
            return this.View();
        }

        public ActionResult Logout(){
            FormsAuthentication.SignOut();
            this.Session.Clear();
            return this.RedirectToAction("Login", "Admin");
        }

        #endregion
    }
}