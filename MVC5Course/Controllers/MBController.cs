﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.ViewModel;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        public ActionResult Index()
        {
            ViewData.Model = db.Client.Find(3);

            ViewData["product"] = db.Product.Find(100);
            return View();
        }
        public ActionResult DataTrans()
        {
            ViewData["MSG1"] = "MSG IN ViewData";
            TempData["MSG2"] = "MSG IN TempData";
            Session["MSG3"] = "MSG IN Session";

            return RedirectToAction("DataTransResult");
        }
        public ActionResult DataTransResult()
        {
            ViewBag.MSG1 = ViewData["MSG1"];
            ViewBag.MSG2 = TempData["MSG2"];
            ViewBag.MSG3 = Session["MSG3"];
            return View();
        }

        public ActionResult Simple1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Simple1(string username,string password)
        {
            return Content(String.Format("UserName:[{0}] Password:[{1}]", username, password));
        }
        public ActionResult Simple2(){
        
            return View("Simple1");
        }

        [HttpPost]
        public ActionResult Simple2(FormCollection frm1)
        {
            return Content(String.Format("UserName:[{0}] Password:[{1}]", frm1["username"], frm1["password"]));
        }
        [HttpPost]
        public ActionResult Complex1(Simple1MBViewModel frm)
        {
            return Content(String.Format("UserName:[{0}] Password:[{1}]", frm.username, frm.password));
        }
        public ActionResult Complex1()
        {
            return View("Simple1");
        }

        public ActionResult Complex2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Complex2(Simple1MBViewModel item1, Simple1MBViewModel item2)
        {
            return Content("Complex2: "
                 + item1.username + ":" + item1.password
                 + " | "
                 + item2.username + ":" + item2.password);
        }
        public ActionResult Complex3()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Complex3(
            [Bind(Prefix="item1")]
            Simple1MBViewModel frm)
        {
            return Content(String.Format("UserName:[{0}] Password:[{1}]", frm.username, frm.password));
        }

        public ActionResult Complex4()
        {
            var data = from p in db.Client
                       select new Simple1MBViewModel()
                       {
                           username = p.FirstName,
                           password = p.LastName,
                           age = 18
                       };
            return View(data.Take(10));
        }

        [HttpPost]
        public ActionResult Complex4(IList<Simple1MBViewModel> item)
        {
            //由中斷點檢查所接到的資料內容
            return Content("請由中斷點檢查所接到的資料內容");
        }
        public ActionResult Complex5()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Complex5(FormCollection frm)
        {
            Simple1MBViewModel item = new Simple1MBViewModel();
            if(TryUpdateModel<Simple1MBViewModel>(item)){
                return RedirectToAction("Complex5");
            }
            return View(item);
        }
    }
}