using System.Web.Mvc;

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

        #endregion
    }
}