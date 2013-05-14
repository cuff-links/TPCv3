using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TPCv3.Infrastructure;

namespace TPCv3{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication{
        #region Public Methods and Operators

        public static void RegisterGlobalFilters(GlobalFilterCollection filters){
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes){
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Manage",
                //Route Name
                "Manage",
                //Url with Paramteres
                new {controller = "Admin", action = "Manage"} //Parameter Defaults
                );

            routes.MapRoute(
                "AdminAction",
                //Route Name
                "Admin/{action}",
                //Url with Paramteres
                new {controller = "Admin", action = "Login"} //Parameter Defaults
                );
            routes.MapRoute(
                "Login",
                //Route Name
                "Login",
                //Url with Paramteres
                new {controller = "Admin", action = "Login"} //Parameter Defaults
                );

            routes.MapRoute(
                "Post",
                //Route Name
                "Archive/{year}/{month}/{title}",
                //URL with Parameters
                new {controller = "Blog", action = "Post"} //Parameter defaults
                );
            routes.MapRoute(
                "Search",
                //Route Name
                "Search/Search_For_{txtSearch}/Page_{page}",
                //URL with Parameters
                new {controller = "Blog", action = "Search", page = 1} //Parameter defaults
                );

            routes.MapRoute(
                "Posts",
                //Route Name
                "Posts/{category}/Page_{page}",
                //URL with Parametersa
                new {controller = "Blog", action = "List", category = "All", page = 1}
                //Parameter defaults
                );

            routes.MapRoute(
                "Category",
                //Route Name
                "Category/{selectedCategory}/Page_{categoryPage}",
                //URL with Parameters
                new {controller = "Blog", action = "Category", categoryPage = 1}
                //Parameter defaults
                );
            routes.MapRoute(
                "Tag",
                //Route Name
                "Tag/{tag}/Page_{tagPage}",
                //URL with parameters
                new {controller = "Blog", action = "Tag", tagPage = 1});
            routes.MapRoute(
                "Default",
                // Route name
                "{controller}/{action}/{id}",
                // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        #endregion

        #region Methods

        protected void Application_Start(){
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        #endregion
    }
}