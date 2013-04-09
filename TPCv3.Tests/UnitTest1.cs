using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TPCv3.Domain.Abstract;

namespace TPCv3.Tests{
    [TestClass]
    public class UnitTest1{
        #region Public Methods and Operators

        [TestMethod]
        public void TestMethod1(){
            //Arrange

            var mock = new Mock<IBlogRepository>();
        }

        #endregion
    }
}