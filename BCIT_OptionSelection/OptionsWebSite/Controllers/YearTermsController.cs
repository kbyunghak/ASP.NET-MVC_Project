using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.Models;

namespace OptionsWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class YearTermsController : Controller
    {
        private DiplomaContext db = new DiplomaContext();

        // GET: YearTerms
        public ActionResult Index()
        {
            ViewBag.Error = TempData["Error"];
            ViewBag.termName = new Dictionary<int, string>()
            {
                {10, "Winter"},
                {20, "Spring/Summer"},
                {30, "Fall"}
            };
            return View(db.YearTerms.ToList());
        }

        // GET: YearTerms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }
            ViewBag.termNum = yearTerm.Term;
            ViewBag.termName = new Dictionary<int, string>()
            {
                {10, "Winter"},
                {20, "Spring/Summer"},
                {30, "Fall"}
            };
            return View(yearTerm);
        }

        // GET: YearTerms/Create
        public ActionResult Create()
        {
            ViewBag.Terms = new List<SelectListItem>() {
                new SelectListItem {Value = "10", Text = "Winter", Selected = true},
                new SelectListItem {Value = "20", Text = "Spring/Summer"},
                new SelectListItem {Value = "30", Text = "Fall"}
            };
            return View();
        }

        // POST: YearTerms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YearTermId,Year,Term,IsDefault")] YearTerm yearTerm)
        {
            if (ModelState.IsValid)
            {
                db.YearTerms.Add(yearTerm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yearTerm);
        }
        //GET: YearTerms/MakeDefault/5
        public ActionResult MakeDefault(int? id)
        {
            YearTerm oldDefault = db.YearTerms.Where(t => t.IsDefault == true).FirstOrDefault();
            if (oldDefault != null)
            {
                oldDefault.IsDefault = false;
                db.Entry(oldDefault).State = EntityState.Modified;
                db.SaveChanges();
            }


            YearTerm newDefault = db.YearTerms.Find(id);
            if (newDefault == null)
            {
                return HttpNotFound();
            }
            newDefault.IsDefault = true;
            db.Entry(newDefault).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: YearTerms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> itemTerms = new List<SelectListItem>() {
                new SelectListItem {Value = "10", Text = "Winter"},
                new SelectListItem {Value = "20", Text = "Spring/Summer"},
                new SelectListItem {Value = "30", Text = "Fall"}
            };
            itemTerms.Find(t => t.Value == yearTerm.Term.ToString()).Selected = true;
            ViewBag.Terms = itemTerms;

            return View(yearTerm);
        }

        // POST: YearTerms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YearTermId,Year,Term,IsDefault")] YearTerm yearTerm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yearTerm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yearTerm);
        }

        // GET: YearTerms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }
            return View(yearTerm);
        }

        // POST: YearTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YearTerm yearTerm = db.YearTerms.Find(id);

            if (db.YearTerms.Count() == 1)
            {
                TempData["Error"] = "Must hava at least one year term.";
                //ViewBag.Error = "Must hava at least one year term.";
                return RedirectToAction("index");
            }

            db.YearTerms.Remove(yearTerm);
            db.SaveChanges();

            //Sets the default term to be the first on the list
            if (yearTerm.IsDefault == true)
            {
                YearTerm newTerm = db.YearTerms.First();
                newTerm.IsDefault = true;
                db.Entry(newTerm).State = EntityState.Modified;
                db.SaveChanges();
            }
          

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
