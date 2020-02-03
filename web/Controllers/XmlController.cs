using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Xml;

namespace web.Controllers
{
    public class PersonModel{
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string Notes {get;set;}
    }

    public class XmlController : Controller {
        [HttpGet]
        public ActionResult Index() {
            return View(viewName:"Xml");
        }

        [HttpPost]
        public ActionResult Preview(IFormFile file) {
            XmlReaderSettings setting = new XmlReaderSettings{
                CheckCharacters = false,
                IgnoreProcessingInstructions = false,
                MaxCharactersFromEntities = long.MaxValue,
                DtdProcessing = DtdProcessing.Parse,    
                XmlResolver = new XmlUrlResolver(),
            };
            using(MemoryStream m = new MemoryStream()) {
                file.CopyTo(m);
                m.Position = 0;
                using(XmlReader reader = XmlReader.Create(m, setting)){
                    var doc = XDocument.Load(reader);
                    XElement personElement = doc.Descendants("person").FirstOrDefault();
                    if(personElement == null) {
                        return View(null);
                    }
                    PersonModel model = new PersonModel {
                        FirstName = personElement.Attribute("FirstName")?.Value,
                        LastName = personElement.Attribute("LastName")?.Value,
                        Email = personElement.Attribute("Email")?.Value,
                        Notes = personElement.Descendants("notes")
                            .FirstOrDefault()?.Value,
                    };
                    return View(model);
                }
            }
        }
    }
}