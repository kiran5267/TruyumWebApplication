using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoTY.Models;

namespace DemoTY.Controllers
{
    [HandleError]
    public class MenuItemsController : Controller
    {
        truYumEntities db = new truYumEntities();
        // GET: MenuItems
        public ActionResult Index()
        {
            var menuItems = db.MenuItems.Where(m=>m.Active.Contains("Yes")).Where(m=>m.DateOfLaunch<DateTime.Today).ToList();
            return View(menuItems);
        }
        
        public ActionResult MenuList()
        {
            var menuItems = db.MenuItems.ToList();
            return View(menuItems);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            MenuItem menuItem = db.MenuItems.Find(id);
            return View(menuItem);
        }
        [HttpPost]
        public ActionResult Edit(MenuItem obj)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obj).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("MenuList");
            }
            return View(obj);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MenuItem m)
        {
            if (ModelState.IsValid)
            {
                db.MenuItems.Add(m);
                db.SaveChanges();
                return RedirectToAction("MenuList");
            }

            return View(m);
        }
        [Route("MenuItems/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            MenuItem m = db.MenuItems.Find(id);
            db.MenuItems.Remove(m);
            db.SaveChanges();
            return RedirectToAction("MenuList");
        }
        public ActionResult Cart()
        {
            var c = db.Carts.OrderBy(m=>m.Name).ToList();
            if (c==null)
            {
                return View();
                
            }
            else
            {
                return View(c);
            }
        }
        [Route("MenuItems/AddToCart/{id:int}")]
        public ActionResult AddToCart(int id)
        {
            try
            {
                MenuItem m = db.MenuItems.Find(id);
                Cart c = new Cart();
                c.Category = m.Category;
                c.Name = m.Name;
                c.FreeDelivery = m.FreeDelivery;
                c.Price = m.Price;
                c.ProductId = m.ProductId;
                if (ModelState.IsValid)
                {
                    db.Carts.Add(c);
                    db.SaveChanges();
                }
                return View(c);
            }
            catch(Exception e)
            {
                //return Content("Exception :  "+e.ToString());
                return RedirectToAction("Index");
            }
        }
        [Route("MenuItems/Remove/{id:int}")]
        public ActionResult Remove(int id)
        {
            Cart c = db.Carts.Find(id);
            if (c != null)
            {
                db.Carts.Remove(c);
                db.SaveChanges();
            }
            return RedirectToAction("Cart");
        }
        public ActionResult truYum()
        {
            return View();
        }
    }
}


/*
         * public class StudentController : Controller
    {
        StudentContext db = new StudentContext();
        // GET: Student
        public ActionResult Index()
        {
            return View(db.students.ToList());
        }
        public ActionResult Details(int id)
        {
            Student obj = db.students.Find(id);
            return View(obj);
        }
        public ActionResult Add()
        {
            return View();
        }
         [HttpPost]
        public ActionResult Add(Student s,HttpPostedFileBase ImageUpload)
        {
            
            string filename = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
            string extension = Path.GetExtension(ImageUpload.FileName);
            filename = filename + extension;
            s.sphoto = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            ImageUpload.SaveAs(filename);
            db.students.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        namespace photoexample.Models
    {
    [Table("Student")]
    public class Student
    {
        [Key]
        public int Sid { get; set; }
        public string sname { get; set; }
        public string sphoto { get; set; }
      
    }
    public class StudentContext : DbContext
    {
        public DbSet<Student> students { get; set; }
    }
}
        /// <summary>
        /// This Actionmethod is used to login by checking the existence of the email id and password in the database
        /// </summary>
        /// <returns>On successful validation it will redirect to welcome page</returns>
        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(Registration objUser, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Registrations.Where(a => a.Email_ID.Equals(objUser.Email_ID) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                if (obj != null)
                {
                    //Session["Email"] = obj.Email_ID;
                    Session["Email"] = obj.Email_ID.ToString();
                    Session["password"] = obj.Password.ToString();
        <form id="LoginForm">
                <div id="msg"><ul style="color:red">Invalid UserName or Password</ul></div>
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
                        <input class="form-control" type="email" name="Email" id="logEmail" placeholder="Email" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                        <input class="form-control" type="password" name="Password" id="lockPassword" placeholder="Password" />
                    </div>
                </div>
            </form>


https://www.c-sharpcorner.com/UploadFile/sourabh_mishra1/cascading-dropdownlist-in-Asp-Net-mvc/


        <h2>Details</h2>

<div>
    <h4>Student</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(model => model.sname)
        </dt>
        <dd>
            @Html.DisplayFor(model => model.sname)
        </dd>
        <dt>
            @Html.DisplayNameFor(model => model.sphoto)
        </dt>
        <dd>
            <img src="@Url.Content(Model.sphoto)"/>
        </dd>
    </dl>
</div>


Add.cshtml.....@using (Html.BeginForm("Add","Student",FormMethod.Post,new { enctype = "multipart/form-data" }))
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Student</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(model => model.sname, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.sname, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.sname, "", new { @class = "text-danger" })
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(model => model.sphoto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @*@Html.EditorFor(model => model.sphoto, new { htmlAttributes = ne
                <input type="image" name="ImageUpload">
 */