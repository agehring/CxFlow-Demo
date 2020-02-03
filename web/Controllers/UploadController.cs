using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;

namespace web.Controllers
{
    public class ImageUploadedModel
    {
        public string Path { get; set; }
    }

    public class UploadController : Controller
    {
        [HttpGet]
        public ActionResult Index() {
            return View(viewName:"Upload");
        }

        [HttpPost]
        public ActionResult UploadImage(IFormFile file ) {
            string path = "wwwroot/uploads/" + file.FileName;
            FileInfo info = new FileInfo(path);
            Console.Error.WriteLine(info.FullName);
            using(var stream = info.OpenWrite()) {
                file.CopyTo(stream);
                return View(new ImageUploadedModel{Path = "/uploads/" + file.FileName});
            }
        }

        [HttpGet]
        public ActionResult Profile() {
            return View();
        }
    }
}