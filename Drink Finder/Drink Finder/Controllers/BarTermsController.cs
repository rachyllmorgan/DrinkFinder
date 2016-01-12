using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Drink_Finder.Models;

namespace Drink_Finder.Controllers
{
    public class BarTermsController : Controller
    {
        private RecipeContext db = new RecipeContext();

        // GET: BarTerms
        public ActionResult Index(string nameSearch)
        {
            var terms = from term in db.BarTerms select term;

            if (!String.IsNullOrEmpty(nameSearch))
            {
                terms = terms.Where(t => t.Name.Contains(nameSearch));
            }

            var new_terms = terms.OrderBy(term => term.Name).ToList();

            ModelState.Remove("nameSearch");

            return View(new_terms);
        }

        // GET: BarTerms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarTerms term = db.BarTerms.Find(id);
            if (term == null)
            {
                return HttpNotFound();
            }
            return View(term);
        }

        // GET: BarTerms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BarTerms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Definition")] BarTerms term)
        {
            if (ModelState.IsValid)
            {
                db.BarTerms.Add(term);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(term);
        }

        // GET: BarTerms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarTerms term = db.BarTerms.Find(id);
            if (term == null)
            {
                return HttpNotFound();
            }
            return View(term);
        }

        // POST: BarTerms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Definition")] BarTerms term)
        {
            if (ModelState.IsValid)
            {
                db.Entry(term).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(term);
        }

        // GET: BarTerms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BarTerms term = db.BarTerms.Find(id);
            if (term == null)
            {
                return HttpNotFound();
            }
            return View(term);
        }

        // POST: BarTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BarTerms term = db.BarTerms.Find(id);
            db.BarTerms.Remove(term);
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
