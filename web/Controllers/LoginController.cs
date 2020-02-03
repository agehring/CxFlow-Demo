using Microsoft.AspNetCore.Mvc;
using web.Models.Login;

namespace web.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index() {
            Response.Headers.Add("X-XSS-Protection","0");
            return View(viewName:"Login", model: new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel input) {
            Response.Headers.Add("X-XSS-Protection","0");
            var result = new LoginViewModel();
            result.Message = $"<em>Sorry</em> either you username (<strong>{input.UserName}</strong>) or you password is incorrect.";
            return View("Login", result);
        }

    }
}
 