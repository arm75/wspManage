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
    public class rentalsController : Controller
    {
        private WSPManageContext db = new WSPManageContext();

        // GET: rentals
        public async Task<ActionResult> Index()
        {
            // original code
            // return View(await db.rentals.ToListAsync());

            List<rental> rentalsList = await db.rentals.OrderBy(p => p.rentalID).ToListAsync();

            // set the Property NAME using the ID stored in the loan, for THIS instance of the list. it is NOT STORED.
            foreach (var listEntry in rentalsList)
            {
                property thisProperty = db.properties.Find(listEntry.propertyID);
                customer thisCustomer = db.customers.Find(listEntry.customerID);

                listEntry.propertyIDName = thisProperty != null ? thisProperty.PhysicalAddress : listEntry.propertyID.ToString();
                listEntry.customerIDName = thisCustomer != null ? thisCustomer.FullName : listEntry.customerID.ToString();

            }

            return View(rentalsList);

        }

        // GET: rentals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rental rental = await db.rentals.FindAsync(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: rentals/Create
        public ActionResult Create()
        {
            ViewBag.propertyIDSelectList = new SelectList(db.properties, "propertyID", "PhysicalAddress").OrderBy(p => p.Text);
            ViewBag.customerIDSelectList = new SelectList(db.customers, "customerID", "FullNameLastFirst").OrderBy(p => p.Text);

            return View();
        }

        // POST: rentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "rentalID,Active,customerID,propertyID,customerIDName,propertyIDName,RentalStartDate,RentalEndDate,RentalNotes,MonthlyRentAmount,LatePaymentAmount,Judgement,CourtDate,EvictionDate,RandPSentDate,WritSentDate,AgreeToVacateDate,CourtCosts,LegalFees,DateCreated,UserCreated,DateModified,UserModified")] rental rental)
        {
            if (ModelState.IsValid)
            {
                db.rentals.Add(rental);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rental);
        }

        // GET: rentals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rental rental = await db.rentals.FindAsync(id);

            ViewBag.propertyIDSelectList = new SelectList(db.properties, "propertyID", "PhysicalAddress").OrderBy(p => p.Text);
            ViewBag.customerIDSelectList = new SelectList(db.customers, "customerID", "FullNameLastFirst").OrderBy(p => p.Text);

            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: rentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "rentalID,Active,customerID,propertyID,customerIDName,propertyIDName,RentalStartDate,RentalEndDate,RentalNotes,MonthlyRentAmount,LatePaymentAmount,Judgement,CourtDate,EvictionDate,RandPSentDate,WritSentDate,AgreeToVacateDate,CourtCosts,LegalFees,DateCreated,UserCreated,DateModified,UserModified")] rental rental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rental).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rental);
        }

        // GET: rentals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rental rental = await db.rentals.FindAsync(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            rental rental = await db.rentals.FindAsync(id);
            db.rentals.Remove(rental);
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
