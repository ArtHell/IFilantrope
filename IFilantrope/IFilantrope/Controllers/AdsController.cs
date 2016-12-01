using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IFilantrope.Models;
using Microsoft.AspNet.Identity;

namespace IFilantrope.Controllers
{
    /// <summary>
    /// Ads controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class AdsController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.UserId = HttpContext.User.Identity.GetUserId();
            var pages = db.Ads.Include(a => a.Author);
            return View(pages.ToList());
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the specified ad.
        /// </summary>
        /// <param name="ad">The ad.</param>
        /// <returns>View</returns>
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,AuthorId")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                ad.AuthorId = User.Identity.GetUserId();
                db.Ads.Add(ad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ad);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View</returns>
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ad ad = db.Ads.Find(id);

            if (ad == null)
            {
                return HttpNotFound();
            }
            else if (HttpContext.User.IsInRole("Admin") || HttpContext.User.Identity.GetUserId() == ad.AuthorId)
            {
                return View(ad);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Edits the specified ad.
        /// </summary>
        /// <param name="ad">The ad.</param>
        /// <returns>View</returns>
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,AuthorId")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                ad.AuthorId = User.Identity.GetUserId();
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ad);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View</returns>
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ad ad = db.Ads.Find(id);

            if (ad == null)
            {
                return HttpNotFound();
            }
            else if (HttpContext.User.IsInRole("Admin") || HttpContext.User.Identity.GetUserId() == ad.AuthorId)
            {
                return View(ad);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Ad ad = db.Ads.Find(id);
            if (HttpContext.User.IsInRole("Admin") || HttpContext.User.Identity.GetUserId() == ad.AuthorId)
            {
                db.Ads.Remove(ad);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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
