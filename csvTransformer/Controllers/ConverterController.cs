using System.IO;
using System.Web.Mvc;

namespace csvTransformer.Controllers
{
    public class ConverterController : Controller
    {
        public ActionResult Xls(    )
        {
            return View();
        }
        
        public ActionResult Upload()
        {
            if (Request.Files.Count < 0) return View("Download");

            var file = Request.Files[0];

            if (file != null && file.ContentLength > 0)
            {
                file.InputStream
            }

            return View("Download", file);
        }
    }
}