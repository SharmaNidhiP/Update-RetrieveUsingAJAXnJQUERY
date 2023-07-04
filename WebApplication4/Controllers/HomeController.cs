
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    //ea("Admin")]
    public class HomeController : Controller
    {
        DemoTableContext db = new DemoTableContext();
        // GET: Home
        public ActionResult Index(string search )
        {
            if(!string.IsNullOrEmpty(search))
            {
                var data=db.Table.Where(model=>model.Textbox1==search).ToList();
                return View(data);
            }
            else 
            {
                var data= db.Table.ToList();
                return View(data);
            }
        }

        //using BeginForm

        public string PostUsingParameters(string firstvalue ,string lastvalue)
        {
            return  "from parameters" + firstvalue + " : " +lastvalue;
        }



       /* public string GetData()
        {
           var stuList = db.Table.ToList();
           ViewBag.StudentTb1=new SelectList(stuList, "Textbox1", "Textbox1");
            return View();
       }*/
        

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DemoTable s)
        {
            if (ModelState.IsValid==true)
            {
                db.Table.Add(s);
                int a = db.SaveChanges();
                if (a>0)
                {
                    TempData["InsertMessage"]="<script>alert('data inserted')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["InsertMessage"]="<script>alert('data not inserted')</script>";
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var row= db.Table.Where(model=>model.Id==id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(DemoTable s)
        {
            db.Entry(s).State=EntityState.Modified;
           int a= db.SaveChanges();
            if (a>0)
            {
                TempData["UpdateMessage"]="<script>alert('Data updated')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag["UpdateMessage"]="<script>alert('Data not updated')</script>";
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (id>0)
            {
                var Studentrow = db.Table.Where(model => model.Id==id).FirstOrDefault();
                if(Studentrow!=null)
                {
                    db.Entry(Studentrow).State=EntityState.Deleted;
                   int a= db.SaveChanges();
                    if(a>0)
                    {
                        TempData["DeleteMessage"]="<script>alert('Data Deleted')</script>";
                    }
                    else
                    {
                        ViewBag["DeleteMessage"]="<script>alert('Data not Deleted')</script>";
                    }
                }
                //return View(Studentrow);
            }
            return RedirectToAction("Index");
        }
        /*
        [HttpPost]
        public ActionResult Delete(DemoTable s)
        {
            db.Entry(s).State=EntityState.Deleted;
            int a = db.SaveChanges();
            if (a>0)
            {

                TempData["DeleteMessage"]="<script>alert('Data Deleted')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag["DeleteMessage"]="<script>alert('Data not Deleted')</script>";
            }
            return RedirectToAction("Index");
        }
        */
    }
}