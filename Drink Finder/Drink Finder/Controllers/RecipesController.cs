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
    public class RecipesController : Controller
    {
        private RecipeContext db = new RecipeContext();

        // GET: Recipes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Recipes
        public ActionResult RecipeList(string nameString, string alcoholSearch, string glassSearch)
        {
            var alcohol_list = new List<string>();
            var alcohol_query = from alc in db.Recipes orderby alc.Alcohol select alc.Alcohol;

            alcohol_list.AddRange(alcohol_query.Distinct());
            ViewBag.found_alcohol = new SelectList(alcohol_list);

            var glass_list = new List<string>();
            var glass_query = from g in db.Recipes orderby g.Glass select g.Glass;

            glass_list.AddRange(glass_query.Distinct());
            ViewBag.found_glass = new SelectList(glass_list);

            var recipes = from recip in db.Recipes select recip;

            if (!String.IsNullOrEmpty(nameString)){
                recipes = recipes.Where(r => r.Name.Contains(nameString));
            }

            if (!String.IsNullOrEmpty(alcoholSearch))
            {
                recipes = recipes.Where(r => r.Alcohol.Contains(alcoholSearch));
            }

            if (!String.IsNullOrEmpty(glassSearch))
            {
                recipes = recipes.Where(r => r.Glass.Contains(glassSearch));
            }

            var new_recipes = recipes.OrderBy(recipe => recipe.Name).ToList();

            ModelState.Remove("nameString");
            ModelState.Remove("alcoholSearch");
            ModelState.Remove("glassSearch");

            return View(new_recipes);
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Alcohol,Mixers,Directions,Glass")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("RecipeList");
            }

            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Alcohol,Mixers,Directions,Glass")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RecipeList");
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("RecipeList");
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
