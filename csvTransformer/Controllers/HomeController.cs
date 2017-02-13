﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace csvTransformer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Desenvolvido por Moisés Filho";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Informações para contato:";

            return View();
        }
    }
}