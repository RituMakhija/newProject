using SLKPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SLKPortal.Controllers
{
    public class UserController : Controller
    {
        SLKDataBaseEntities sde = new SLKDataBaseEntities();

        [HttpGet]

        public ActionResult SignUp()

        {

            return View();

        }



        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult SignUp(registration r)

        {

            if (ModelState.IsValid)

            {

                sde.registrations.Add(r);

                sde.SaveChanges();

                return RedirectToAction("Login", "User");

            }

            return View();

        }



        [HttpGet]

        public ActionResult Login()

        {

            return View();

        }



        [HttpPost]

        public ActionResult Login(registration r, detail det)

        {

            var v = sde.registrations.Where(m => m.EmailId == r.EmailId && m.Password == r.Password).FirstOrDefault();

            var d = sde.details.Where(m => m.EmailID == det.EmailID).FirstOrDefault();

            string Email = Request.Form["EmailId"].ToString();

            string Password = Request.Form["Password"].ToString();           

            if (Email == "admin" && Password == "asdfg")

            {                

                return RedirectToAction("AdminPage1", "User");

            }

            else if (v != null)

            {               
                if (d != null)

                {
                    Session["LogedUserFullname"] = v.UserName.ToString();
                    return RedirectToAction("ViewPage", "User");

                }

                else

                {

                    return RedirectToAction("RegistrationPage", "User");

                }

            }

            else

            {

                @ViewBag.Message = "Please enter the correct credentials";

            }

            return View();

        }



        public ActionResult HomePage()

        {

            return View();

        }



        public ActionResult RegistrationPage(detail d)

        {

            if (ModelState.IsValid)

            {

                sde.details.Add(d);

                sde.SaveChanges();

                return RedirectToAction("ViewPage", "User");

            }

            return View();

        }



        public ActionResult AdminPage()

        {

            detail model = new detail();

            var data = sde.details.ToList();

            List<detail> jobpos = new List<detail>();

            foreach (var item in data)

            {

                detail obj = new detail();

                obj.UserId = item.UserId;

                obj.FirstName = item.FirstName;

                obj.MiddleName = item.MiddleName;

                obj.LastName = item.LastName;

                obj.PhoneNumber = item.PhoneNumber;

                obj.Gender = item.Gender;

                obj.DateOfBirth = item.DateOfBirth;

                obj.EmailID = item.EmailID;

                obj.Address = item.Address;

                obj.City = item.City;

                obj.ZipCode = item.ZipCode;

                obj.State = item.State;

                obj.Country = item.Country;

                obj.Department = item.Department;

                obj.TenthBoard = item.TenthBoard;

                obj.TenthMarks = item.TenthMarks;

                obj.TwelfthBoard = item.TwelfthBoard;

                obj.TwelfthMarks = item.TwelfthMarks;

                jobpos.Add(obj);

            }

            ViewBag.list = jobpos;

            return View();

        }

        public ActionResult Create()

        {

            return View();

        }



        [HttpPost]

        public ActionResult Create(detail slk)

        {

            try

            {

                sde.details.Add(slk);

                sde.SaveChanges();

                return RedirectToAction("AdminPage", "User");

            }

            catch

            {

                //@ViewBag.Message = "Please create a valid account";

                return View();

            }

        }



        public ActionResult Edit(int UserId)

        {

            detail Post = new detail();

            Post = sde.details.Where(r => r.UserId == UserId).FirstOrDefault();

            return View(Post);

        }



        [HttpPost]

        public ActionResult Edit(detail Post)

        {

            if (ModelState.IsValid)

            {

                sde.Entry(Post).State = System.Data.Entity.EntityState.Modified;

                sde.SaveChanges();

                return RedirectToAction("AdminPage", "User");

            }



            return View(Post);

        }



        public ActionResult Delete(int UserId)

        {

            detail Post = new detail();

            Post = sde.details.Where(r => r.UserId == UserId).FirstOrDefault();

            return View(Post);

        }



        [HttpPost]

        public ActionResult Delete(detail Post)

        {

            if (ModelState.IsValid)

            {

                sde.Entry(Post).State = System.Data.Entity.EntityState.Deleted;

                sde.SaveChanges();

                return RedirectToAction("AdminPage", "User");

            }

            return View(Post);

        }

        public ActionResult AdminPage1(string searchBy, string search)

        {

            registration reg = new registration();

            var d = sde.registrations.ToList();

            List<registration> job = new List<registration>();

            foreach (var item in d)

            {

                registration obj = new registration();

                obj.SlNo = item.SlNo;

                obj.UserName = item.UserName;

                obj.EmailId = item.EmailId;

                obj.Password = item.Password;

                obj.ConfirmPassword = item.ConfirmPassword;

                job.Add(obj);

            }

            ViewBag.list = job;
        
            return View();

        }

        public ActionResult Create1()

        {

            return View();

        }



        [HttpPost]

        public ActionResult Create1(registration r)

        {

            try

            {

                sde.registrations.Add(r);

                sde.SaveChanges();

                return RedirectToAction("AdminPage1", "User");

            }

            catch

            {

                //@ViewBag.Message = "Please create a valid account";

                return View();

            }

        }



        public ActionResult Edit1(int SlNo)

        {

            registration Post = new registration();

            Post = sde.registrations.Where(r => r.SlNo == SlNo).FirstOrDefault();

            return View(Post);

        }



        [HttpPost]

        public ActionResult Edit1(registration Post)

        {

            if (ModelState.IsValid)

            {

                sde.Entry(Post).State = System.Data.Entity.EntityState.Modified;

                sde.SaveChanges();

                return RedirectToAction("AdminPage1", "User");

            }



            return View(Post);

        }



        public ActionResult Delete1(int SlNo)

        {

            registration Post = new registration();

            Post = sde.registrations.Where(r => r.SlNo == SlNo).FirstOrDefault();

            return View(Post);

        }



        [HttpPost]

        public ActionResult Delete1(registration Post)

        {

            if (ModelState.IsValid)

            {

                sde.Entry(Post).State = System.Data.Entity.EntityState.Deleted;

                sde.SaveChanges();

                return RedirectToAction("AdminPage1");

            }

            return View(Post);

        }


        [HttpPost]
        public ActionResult ViewOrUpdatePage(FormCollection Col)  
                          
        {
            return View();     
            //return PartialView("ViewOrUpdatePage", new detail { EmailID = Session["EmailID"].ToString() });

        }

        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "SlNo")
            {
                return View(sde.registrations.Where(x => x.SlNo.ToString() == search || search == null).ToList());
            }
            else
            {
                return View(sde.registrations.Where(x => x.UserName.StartsWith(search) || search == null).ToList());
            }
        }

        public ActionResult ViewPage(string UserName)
        {

            // detail model = new detail();

            // var data = sde.details.ToList();

            // List<detail> jobpos = new List<detail>();

            // foreach (var item in data)

            //{

            //     detail obj = new detail();

            //     obj.UserId = item.UserId;

            //     obj.FirstName = item.FirstName;

            //     obj.MiddleName = item.MiddleName;

            //     obj.LastName = item.LastName;

            //     obj.PhoneNumber = item.PhoneNumber;

            //     obj.Gender = item.Gender;

            //     obj.DateOfBirth = item.DateOfBirth;

            //     obj.EmailID = item.EmailID;

            //     obj.Address = item.Address;

            //     obj.City = item.City;

            //     obj.ZipCode = item.ZipCode;

            //     obj.State = item.State;

            //     obj.Country = item.Country;

            //     obj.Department = item.Department;

            //     obj.TenthBoard = item.TenthBoard;

            //     obj.TenthMarks = item.TenthMarks;

            //     obj.TwelfthBoard = item.TwelfthBoard;

            //     obj.TwelfthMarks = item.TwelfthMarks;

            //     jobpos.Add(obj);

            // }

            // ViewBag.list = jobpos;
            //var v = sde.details.Where(m => m.EmailID == d.EmailID).FirstOrDefault();
            var user = sde.details.Single(u => u.FirstName == UserName);

            return View(user);

            //return PartialView(sde.details.Single(d.EmailID==));                               
        }
    }
}