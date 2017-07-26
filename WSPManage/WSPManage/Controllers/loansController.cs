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
    public class loansController : Controller
    {
        private WSPManageContext db = new WSPManageContext();

        // GET: loans
        public async Task<ActionResult> Index()
        {
            return View(await db.loans.ToListAsync());
        }

        // GET: loans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loan loan = await db.loans.FindAsync(id);

            ViewBag.thisAmortizationScheduleList = loan.createAmortizationSchedule(loan.FirstPaymentDate, loan.SalePrice, loan.DownPayment, loan.LoanAmount, loan.InterestRate, loan.Period);
            // Test code to pass an Amortization Schedule to my View
            // List<amortizationScheduleEntry> thisAmortizationScheduleList = new List<amortizationScheduleEntry>();
            // amortizationScheduleEntry thisEntry = new amortizationScheduleEntry();
            //    thisEntry.someInteger = 2;
            //    thisEntry.someDecimal = 3.3m;
            //    thisEntry.someString = "hello";
            // thisAmortizationScheduleList.Add(thisEntry);

            // ViewBag.thisAmortizationScheduleList = thisAmortizationScheduleList;
            // end Test code

            if (loan == null)
            {
                return HttpNotFound();
            }
            
            return View(loan);
        }

        // GET: loans/Create
        public ActionResult Create()
        {
            ViewBag.customerIDSelectList = new SelectList(db.customers, "customerID", "LastName");
            return View();
        }

        // POST: loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "loanID,Active,LoanOnly,customerID,propertyID,SalePrice,DownPayment,ContractDate,ClosingDate,LoanAmount,InterestRate,Period,Payment,FirstPaymentDate,ValueToCalc,LoanNotes,DateCreated,UserCreated,DateModified,UserModified")] loan loan)
        {
            if (ModelState.IsValid)
            {
                db.loans.Add(loan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loan);
        }

        // GET: loans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loan loan = await db.loans.FindAsync(id);

            ViewBag.customerIDSelectList = new SelectList(db.customers, "customerID", "LastName");

            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "loanID,Active,LoanOnly,customerID,propertyID,SalePrice,DownPayment,ContractDate,ClosingDate,LoanAmount,InterestRate,Period,Payment,FirstPaymentDate,ValueToCalc,LoanNotes,DateCreated,UserCreated,DateModified,UserModified")] loan loan)
        {
            if (ModelState.IsValid)
            {

                db.Entry(loan).State = EntityState.Modified;

                // loan.Period = loan.addTwo(loan.Period);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }



            return View(loan);
        }

        // GET: loans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loan loan = await db.loans.FindAsync(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            loan loan = await db.loans.FindAsync(id);
            db.loans.Remove(loan);
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
