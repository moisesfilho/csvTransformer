using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Web.Mvc;
using csvTransformer.Container;
using csvTransformer.Models.Business;
using csvTransformer.Models.Enum;

namespace csvTransformer.Controllers
{
    public class ConverterController : Controller
    {
        private readonly IConverter _converter;

        public ConverterController() : this (IocContainer.Container.Resolve<IConverter>()) {}

        public ConverterController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet]
        public ActionResult XlsToCsv()
        {
            return View();
        }
        
        [HttpPost]
        [ActionName("XlsToCsv")]
        public ActionResult Execute()
        {
            try
            {
                if (Request.Files.Count < 0)
                    throw new Exception("Nenhum arquivo foi enviado.");

                if (Request.Files.Count > 1)
                    throw new Exception("Mais de um arquivo foi enviado.");

                var file = Request.Files[0];

                if (file == null || file.ContentLength <= 0)
                    throw new Exception("Arquivo com conteudo inválido.");

                var extensoesValidas = new List<string> {".xls", ".xlsx"};

                if (!extensoesValidas.Contains(Path.GetExtension(file.FileName)))
                    throw new Exception("Arquivo com extensão diferente de XLS ou XLSX.");

                var result = _converter.File(file.InputStream).To(FileType.csv).From(FileType.xls).Result();

                return File(result, MediaTypeNames.Application.Octet, file.FileName.Replace("xlsx", "csv"));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("Exception", exception.Message);
                return View("XlsToCsv");
            }
        }
    }
}