using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolRecordsHW2.Models;
using PagedList;
using PagedList.Mvc;
using System.Drawing.Drawing2D;
using System.Security.Policy;

namespace SchoolRecordsHW2.Controllers
{
    public class HomeController : Controller
    {
        db_RecordsEntities db = new db_RecordsEntities();        

        public ActionResult school_Records(string op, string srch, int? pageNum)
        {
            if (op == "Subject")
            {
                return View(db.schoolRecords.Where(x => x.Subject == srch || srch == null).ToList().ToPagedList(pageNum ?? 1, 5));
            }
            else if (op == "Gender")
            {
                return View(db.schoolRecords.Where(x => x.Gender == srch || srch == null).ToList().ToPagedList(pageNum ?? 1, 5));
            }
            else if (op == "Email")
            {
                return View(db.schoolRecords.Where(x => x.Email == srch || srch == null).ToList().ToPagedList(pageNum ?? 1, 5));
            }
            else
            {
                return View(db.schoolRecords.Where(x => x.InstructorName.StartsWith(srch) || srch == null).ToList().ToPagedList(pageNum ?? 1,5));
            }
        }
        //Start of Add Functionality

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(schoolRecord add)
        {
            try
            {
                if (add != null)
                {
                      schoolRecord addRecord = new schoolRecord();
                    addRecord.InstructorName = add.InstructorName;
                            addRecord.Gender = add.Gender;
                           addRecord.Subject = add.Subject;
                             addRecord.Email = add.Email;
                       addRecord.PhoneNumber = add.PhoneNumber;

                    db.schoolRecords.Add(addRecord);
                    db.SaveChanges();
                }
                return RedirectToAction("school_Records");
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred.";
                return RedirectToAction("school_Records");
            }
        }

        //Start of Edit Functionality

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                if (id != 0)
                {
                    schoolRecord school_recor = db.schoolRecords.SingleOrDefault(x => x.Id == id);
                    return PartialView("Edit", school_recor);
                }
                else
                {
                    return RedirectToAction("school_Records");
                }
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred";
                return RedirectToAction("school_Records");
            }
        }
        [HttpPost]
        public ActionResult Edit(schoolRecord recordMod)
        {
            try
            {
                if (recordMod != null)
                {
                      schoolRecord updateRecord = db.schoolRecords.SingleOrDefault(x => x.Id == recordMod.Id);
                    updateRecord.InstructorName = recordMod.InstructorName;
                            updateRecord.Gender = recordMod.Gender;
                             updateRecord.Email = recordMod.Email;
                       updateRecord.PhoneNumber = recordMod.PhoneNumber;
                    db.SaveChanges();
                }
                return RedirectToAction("school_Records");
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred";
                return RedirectToAction("school_Records");
            }
        }

        //Start of Read Functionality
        [HttpGet]
        public ActionResult Read(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            schoolRecord read = db.schoolRecords.Find(id);
            if (read == null)
            {
                return HttpNotFound();
            }
            return View(read);
        }

        //Start of Delete Functionality
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                schoolRecord schoolData = db.schoolRecords.SingleOrDefault(x => x.Id == id);
                if (schoolData != null)
                {
                    db.schoolRecords.Remove(schoolData);
                    db.SaveChanges();
                }
                return RedirectToAction("school_Records");
            }
            catch (Exception)
            {
                ViewBag.msg = "Some error occurred.";
                return RedirectToAction("school_Records");
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
    }
}