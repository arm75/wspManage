using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WSPManage.Models;

namespace WSPManage.Controllers
{
    public class rentalPaymentsController : Controller
    {
        private WSPManageContext db = new WSPManageContext();

        // GET: rentalPayments
        public async Task<ActionResult> Index()
        {
            return View(await db.rentalPayments.ToListAsync());
        }

        // GET: rentalPayments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rentalPayment rentalPayment = await db.rentalPayments.FindAsync(id);
            if (rentalPayment == null)
            {
                return HttpNotFound();
            }
            return View(rentalPayment);
        }

        // GET: rentalPayments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: rentalPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "rentalPaymentID,customerID,propertyID,rentalID,DateCreated,UserCreated,DateModified,UserModified")] rentalPayment rentalPayment)
        {
            if (ModelState.IsValid)
            {
                db.rentalPayments.Add(rentalPayment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rentalPayment);
        }

        // GET: rentalPayments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rentalPayment rentalPayment = await db.rentalPayments.FindAsync(id);
            if (rentalPayment == null)
            {
                return HttpNotFound();
            }
            return View(rentalPayment);
        }

        // POST: rentalPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "rentalPaymentID,customerID,propertyID,rentalID,DateCreated,UserCreated,DateModified,UserModified")] rentalPayment rentalPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalPayment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rentalPayment);
        }

        // GET: rentalPayments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rentalPayment rentalPayment = await db.rentalPayments.FindAsync(id);
            if (rentalPayment == null)
            {
                return HttpNotFound();
            }
            return View(rentalPayment);
        }

        // POST: rentalPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            rentalPayment rentalPayment = await db.rentalPayments.FindAsync(id);
            db.rentalPayments.Remove(rentalPayment);
            await db.SaveChangesAsync();
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
