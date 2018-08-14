using AspRossko.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AspRossko.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;   
            Assert.Equal("Testing data", result?.ViewData["Message"]);
        }

        [Fact]
        public void IndexViewResultNotNull()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult; 
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;   
            Assert.Equal("Index", result?.ViewName);
        }
    }
}
