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
    public class loanPaymentsController : Controller
    {
        private WSPManageContext db = new WSPManageContext();

        // GET: loanPayments
        public async Task<ActionResult> Index()
        {
            return View(await db.loanPayments.ToListAsync());
        }

        // GET: loanPayments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loanPayment loanPayment = await db.loanPayments.FindAsync(id);
            if (loanPayment == null)
            {
                return HttpNotFound();
            }
            return View(loanPayment);
        }

        // GET: loanPayments/Create
        public ActionResult Create()
        {
            List<loan> validLoansList = db.loans.Where(p => p.Active).OrderBy(p => p.loanID).ToList();
            
            ViewBag.loanIDSelectList = new SelectList(validLoansList, "loanID", "loanID").OrderBy(p => p.Text);

            ViewBag.loanPaymentTypeDropdownList = loanPaymentTypeDropdownList.loanPaymentTypeList;

            return View();
        }

        // POST: loanPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "loanPaymentID,customerID,propertyID,loanID,PaymentType,Payor,PaymentAmount,PaymentDate,PostedDate,DepositDate,PaymentMethod,CheckNumber,PaymentDescription,PaymentNotes,DateCreated,UserCreated,DateModified,UserModified")] loanPayment loanPayment)
        {
            if (ModelState.IsValid)
            {
                db.loanPayments.Add(loanPayment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loanPayment);
        }

        // GET: loanPayments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loanPayment loanPayment = await db.loanPayments.FindAsync(id);
            if (loanPayment == null)
            {
                return HttpNotFound();
            }
            return View(loanPayment);
        }

        // POST: loanPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "loanPaymentID,customerID,propertyID,loanID,PaymentType,Payor,PaymentAmount,PaymentDate,PostedDate,DepositDate,PaymentMethod,CheckNumber,PaymentDescription,PaymentNotes,DateCreated,UserCreated,DateModified,UserModified")] loanPayment loanPayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanPayment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(loanPayment);
        }

        // GET: loanPayments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loanPayment loanPayment = await db.loanPayments.FindAsync(id);
            if (loanPayment == null)
            {
                return HttpNotFound();
            }
            return View(loanPayment);
        }

        // POST: loanPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            loanPayment loanPayment = await db.loanPayments.FindAsync(id);
            db.loanPayments.Remove(loanPayment);
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
