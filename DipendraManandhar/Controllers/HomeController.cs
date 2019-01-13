using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DipendraManandhar.Models;
using DipendraManandhar.ViewModel;

namespace DipendraManandhar.Controllers
{
    public class HomeController : Controller
    {
        private Entities1 db = new Entities1();

        public ActionResult Index()
        {
            return View();
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
        public ActionResult InterviewIndex()
        {
            InterviewIndexVM Vm = new InterviewIndexVM();
            Vm.indexlist = db.Interviews.ToList() ;
            Vm.droplist = db.InterviewDrops.ToList();
            return View(Vm);
        }
        //public ActionResult InterviewAdd()
        //{
        //    try
        //    {
        //        InterviewAddEdit Vm = new InterviewAddEdit();
        //        Vm.Droplist = db.InterviewDrops.ToList();
        //        return View(Vm);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.message = ex.Message;
        //        return View(new InterviewAddEdit());
        //    }
        //}

        public ActionResult InterviewAdd(int Id)
        {
            try
            {
                InterviewAddEdit Vm = new InterviewAddEdit();
                Vm.Droplist = db.InterviewDrops.ToList();

                if (Id == 0)
                    return View(Vm);
                else
                {
                    Vm.InterviewObj = db.Interviews.Where(x => x.Id == Id).FirstOrDefault();
                    return View(Vm);
                }
            }
            catch(Exception ex)
            {
                ViewBag.message = ex.Message;
                return View(new InterviewAddEdit());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InterviewAdd(InterviewAddEdit Submit)
        {
            Submit.Droplist = db.InterviewDrops.ToList();
            try
            { 
                if(!ModelState.IsValid)
                {
                    return View(Submit);
                }
                else
                {
                    if(Submit.InterviewObj.Id==0)
                    {
                        db.Interviews.Add(Submit.InterviewObj);
                        db.SaveChanges();
                        ViewBag.message = "added sucessfully";
                        return RedirectToAction("InterviewIndex");
                    }
                    else
                    {
                        db.Interviews.Attach(Submit.InterviewObj);
                        var entry = db.Entry(Submit.InterviewObj);
                        entry.Property(e => e.FirstName).IsModified = true;
                        entry.Property(e => e.LastName).IsModified = true;
                        entry.Property(e => e.Message).IsModified = true;
                        entry.Property(e => e.DropDown).IsModified = true;
                        entry.Property(e => e.Email).IsModified = true;
                        entry.Property(e => e.MobileNo).IsModified = true;
                        entry.Property(e => e.Hobby).IsModified = true;
                        db.SaveChanges();
                        ViewBag.message = "modified sucessfully";
                        return RedirectToAction("InterviewIndex");
                    }
                }
            }
            catch (Exception ex) { ViewBag.message = ex.Message+"!!error occured!!"; return View(Submit); }
        }
        //for delete posting garne ho but for fast hope u understand
        public ActionResult delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("InterviewIndex");
            }
            try
            {
                Interview del = db.Interviews.Where(x => x.Id == id).FirstOrDefault();
                db.Interviews.Remove(del);
                db.SaveChanges();
                ViewBag.message = "Successfully deleted";
                return RedirectToAction("InterviewIndex");
            }
            catch (Exception Ex)
            {
                ViewBag.message = Ex.Message;
                return RedirectToAction("InterviewIndex");
            }
        }
        public ActionResult ViewInterview(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("InterviewIndex");
            }
            try
            {
                InterviewAddEdit Submit=new InterviewAddEdit();
                Interview del = db.Interviews.Where(x => x.Id == id).FirstOrDefault();
                Submit.InterviewObj = del;
                Submit.Droplist = db.InterviewDrops.ToList();
                return View(Submit);
            }
            catch (Exception Ex)
            {
                ViewBag.message = Ex.Message;
                return RedirectToAction("InterviewIndex");
            }
        }
    }
}