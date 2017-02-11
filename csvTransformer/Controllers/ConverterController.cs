using System.IO;
using System.Web.Mvc;

namespace csvTransformer.Controllers
{
    public class ConverterController : Controller
    {
        public ActionResult Xls()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                }

                return View("Download", file);
            }

            return View("Download");
        }
    }
}