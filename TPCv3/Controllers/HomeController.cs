using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;
using TPCv3.Models;

namespace TPCv3.Controllers{
    public class HomeController : Controller{
        #region Constants and Fields

        private const int itemsPerPage = 6;
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

        #region Public Methods

        public virtual ActionResult About(){
            return View();
        }

        //
        // GET: /Contact/
        public virtual ActionResult Contact(){
            return View();
        }

        [HttpPost]
        public ViewResult Contact(Contact contact){
            if (ModelState.IsValid){
                using (var client = new SmtpClient()){
                    string adminEmail = ConfigurationManager.AppSettings["AdminEmail"];
                    var from = new MailAddress(adminEmail, "JustBlog Messenger");
                    var to = new MailAddress(adminEmail, "JustBlog Admin");

                    using (var message = new MailMessage(from, to)){
                        message.Body = contact.Body;
                        message.IsBodyHtml = true;
                        message.BodyEncoding = Encoding.UTF8;

                        message.Subject = contact.Subject;
                        message.SubjectEncoding = Encoding.UTF8;

                        message.ReplyToList.Add(new MailAddress(contact.Email));
                        client.Send(message);
                    }
                }

                return View("Thanks");
            }

            return View();
        }

        public virtual ActionResult Index(){
            return View();
        }

        public virtual ActionResult Portfolio(int portfolioPage = 1){
            var model = new ProjectsListViewModel(_projectRepository, portfolioPage, itemsPerPage);
            return View(model);
        }

        public virtual ViewResult ProjectCategory(string selectedProjectCategory, int projectCategoryPage = 1){
            var viewModel = new ProjectsListViewModel(_projectRepository, selectedProjectCategory, projectCategoryPage,
                                                      itemsPerPage);
            ViewBag.Title = String.Format(@"Latest posts on category ""{0}""", viewModel.CurrentCategory);
            viewModel.Projects = viewModel.Projects.Take(itemsPerPage);
            return View("Portfolio", viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult PortfolioSideBar(){
            var model = new PortfolioSideBarViewModel(_projectRepository);
            return PartialView("_PortfolioSideBar", model);
        }

        #endregion
    }
}