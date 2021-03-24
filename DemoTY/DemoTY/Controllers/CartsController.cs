using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoTY.Models;

namespace DemoTY.Controllers
{
    public class CartController : Controller
    {
        truYumEntities db = new truYumEntities();
        // GET: Cart
        public ActionResult Index()
        {
            var c = db.Carts.ToList();
            if (c.Count() > 0)
            {
                return View(c);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            return Content(id.ToString());
        }

    }
}