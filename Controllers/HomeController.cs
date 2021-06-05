using E_Market.Models;
using E_Market.Products;
using E_Market.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Market.Controllers
{
    public class HomeController : Controller
    {
        private ISubject _ISubject;
        private IObserver _IObserver;
        private storeEntities db = new storeEntities();
        public HomeController()
        {
            String productName = "samsung";
            float productPrice = 1200;
            String avail = "Available";
            String userName = "abdo";
            Subjects sub = new Subjects(productName,productPrice, avail);
            Observer ob = new Observer(userName , sub);
        }
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            Session["username"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "username,password")]User user)
        {
            var rec = db.Users.Where(x => x.username == user.username & x.password == user.password).ToList().FirstOrDefault();
            if (rec != null)
            {
                Session["username"] = rec.username;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Invalid User";
                return View(user);
            }
        }
    }
}