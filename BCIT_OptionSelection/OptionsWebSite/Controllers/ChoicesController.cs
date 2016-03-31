using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.Models;
using Microsoft.AspNet.Identity;
using OptionsWebSite.Models;
using System.Collections;

namespace OptionsWebSite.Controllers
{
    public class ChoicesController : Controller
    {
        private DiplomaContext db = new DiplomaContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();


        // GET: Choices
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.termName = new Dictionary<int, string>()
            {
                {10, "Winter"},
                {20, "Spring/Summer"},
                {30, "Fall"}
            };

            return View(db.Choices.ToList());
        }

        //GET: Choices/MyChoices
        [Authorize(Roles = "Admin, Student")]
        public ActionResult MyChoices()
        {
            //shows the student their choices
            //This action method will check to see if the user has already created a record
            //if there is no record for the student they will be redirected to the 'create' view
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db2.Users.FirstOrDefault(x => x.Id == currentUserId);

            List<Choice> choiceExists = db.Choices.Where(c => c.StudentId == currentUser.UserName).ToList();
            if (choiceExists.Count == 0)
            {
                //if student has no record, redirect them to Option registration page (CREATE)
                return RedirectToAction("Create");
            }

            ViewBag.termName = new Dictionary<int, string>()
            {
                {10, "Winter"},
                {20, "Spring/Summer"},
                {30, "Fall"}
            };

            return View(choiceExists.ToList());
        }

        // GET: Choices/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            int term = db.YearTerms.FirstOrDefault(y => y.YearTermId == choice.YearTermId).Term;
            ViewBag.termNum = term;
            ViewBag.termName = new Dictionary<int, string>()
            {
                {10, "Winter"},
                {20, "Spring/Summer"},
                {30, "Fall"}
            };
            return View(choice);
        }

        // GET: Choices/Create
        [Authorize(Roles = "Admin, Student")]
        public ActionResult Create()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db2.Users.FirstOrDefault(x => x.Id == currentUserId);
            YearTerm currentDefault = db.YearTerms.FirstOrDefault(t => t.IsDefault == true);

            //if currentUserId exists in the Choice table
            if (!User.IsInRole("Admin"))
            {
                //Join the 'Choices' and 'YearTerms' table
                //Check if current student username exists AND whether the student record is for the current term OR current school year
                //if yes redirect to 'MyChoices' view otherwise continue with option registration
                var stdLookUp = db.Choices.Join(db.YearTerms, c => c.YearTermId, t => t.YearTermId, (c, t) => new { c, t })
                    .FirstOrDefault(m => m.c.StudentId == currentUser.UserName
                    && (m.t.IsDefault == true || m.c.SelecionDate.Year == m.t.Year));

                //Convert.ToDateTime(m.c.SelecionDate).Yea
                if (stdLookUp != null)
                {
                    return RedirectToAction("MyChoices");
                }
            }

            //redirect to a new view


            Choice newChoice = new Choice();
            newChoice.StudentId = currentUser.UserName;//automatically set the current logged in user
            newChoice.YearTermId = currentDefault.YearTermId;//set the default termID

            ViewBag.Options = new SelectList(
                db.Options.Where(o => o.IsActive == true).OrderBy(o => o.Title),
                "OptionId", "Title");

            return View(newChoice);
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Student")]
        public ActionResult Create([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                choice.SelecionDate = DateTime.Now;
            }
            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();


                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else {
                    return RedirectToAction("MyChoices");
                }

            }
            ViewBag.Options = new SelectList(
                db.Options.Where(o => o.IsActive == true).OrderBy(o => o.Title),
                "OptionId", "Title");
            return View(choice);
        }

        // GET: Choices/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstChoiceOptionId = new SelectList(
                db.Options.OrderBy(o => o.Title),
                "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(
                db.Options.OrderBy(o => o.Title),
                "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(
                db.Options.OrderBy(o => o.Title),
                "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(
                db.Options.OrderBy(o => o.Title),
                "OptionId", "Title", choice.FourthChoiceOptionId);

            ViewBag.termNum = db.YearTerms.FirstOrDefault(y => y.YearTermId == choice.YearTermId).Term;
            ViewBag.termName = new Dictionary<int, string>()
            {
                {10, "Winter"},
                {20, "Spring/Summer"},
                {30, "Fall"}
            };

            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.Entry(choice).Property(c => c.SelecionDate).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(choice);
        }

        // GET: Choices/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);

            db.Choices.Remove(choice);
            db.SaveChanges();
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
