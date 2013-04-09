using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Concrete;

namespace TPCv3.Infrastructure{
    public class NinjectControllerFactory : DefaultControllerFactory{
        #region Constants and Fields

        private readonly IKernel ninjectKernel;

        #endregion

        #region Constructors and Destructors

        public NinjectControllerFactory(){
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        #endregion

        #region Methods

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType){
            return controllerType == null ? null : (IController) ninjectKernel.Get(controllerType);
        }

        private void AddBindings(){
            //Bind Interface to the consuming Entities. 
            ninjectKernel.Bind<IProjectRepository>().To<EfProjectRepository>();
            ninjectKernel.Bind<IBlogRepository>().To<EfBlogRepository>();
        }

        #endregion
    }
}