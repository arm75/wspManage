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
    public class propertyController : Controller
    {
        private WSPManageContext db = new WSPManageContext();

        // GET: properties
        public async Task<ActionResult> Index()
        {
            return View(await db.properties.ToListAsync());
        }

        // GET: properties/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = await db.properties.FindAsync(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: properties/Create
        public ActionResult Create()
        {
            ViewBag.hello = new SelectList(db.businessEntities, "businessID", "businessName");

            return View();
        }

        // POST: properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "propertyID,Active,PhysicalAddress,PhysicalUnit,PhysicalCity,PhysicalState,PhysicalZipcode,PhysicalCounty,OriginalCost,AcquireDate,MarketValue,RentalUnit,PropertySource,LegallyAvailable,PhysicallyAvailable,WSPOwned,InsuranceRequired,InsuranceCarrier,InsurancePolicy,InsurancePolicyExpiration,WSPLiabilityDateAdded,Lender,LoanNumber,WSPBalanceOwed,TaxIDNumber,LastYearPaid,DateCreated,UserCreated,DateModified,UserModified")] property property)
        {
            // var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
            //    ? HttpContext.User.Identity.Name
            //    : "Anonymous";

            if (ModelState.IsValid)
            {
             //   property.DateCreated = DateTime.Now;
             //   property.UserCreated = currentUsername;

                db.properties.Add(property);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(property);
        }

        // GET: properties/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = await db.properties.FindAsync(id);

            property.businessentitySelectList = new SelectList(db.businessEntities, "businessID", "businessName");
            
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "propertyID,Active,PhysicalAddress,PhysicalUnit,PhysicalCity,PhysicalState,PhysicalZipcode,PhysicalCounty,OriginalCost,AcquireDate,MarketValue,RentalUnit,PropertySource,LegallyAvailable,PhysicallyAvailable,WSPOwned,InsuranceRequired,InsuranceCarrier,InsurancePolicy,InsurancePolicyExpiration,WSPLiabilityDateAdded,Lender,LoanNumber,WSPBalanceOwed,TaxIDNumber,LastYearPaid,DateCreated,UserCreated,DateModified,UserModified")] property property)
        {
            //var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
            //    ? HttpContext.User.Identity.Name
            //    : "Anonymous";

            if (ModelState.IsValid)
            {
             //   property.DateModified = DateTime.Now;
             //   property.UserModified = currentUsername;

                db.Entry(property).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // GET: properties/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            property property = await db.properties.FindAsync(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            property property = await db.properties.FindAsync(id);
            db.properties.Remove(property);
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
