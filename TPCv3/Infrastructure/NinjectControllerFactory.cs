using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Concrete;
using TPCv3.Providers;

namespace TPCv3.Infrastructure{
    public class NinjectControllerFactory : DefaultControllerFactory{
        #region Constants and Fields

        private readonly IKernel _ninjectKernel;

        #endregion

        #region Constructors and Destructors

        public NinjectControllerFactory(){
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        #endregion

        #region Methods

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType){
            return controllerType == null ? null : (IController) _ninjectKernel.Get(controllerType);
        }

        private void AddBindings(){
            //Bind Interface to the consuming Entities. 
            _ninjectKernel.Bind<IProjectRepository>().To<EfProjectRepository>();
            _ninjectKernel.Bind<IBlogRepository>().To<EfBlogRepository>();
            _ninjectKernel.Bind<IAuthProvider>().To<AuthProvider>();
        }

        #endregion
    }
}