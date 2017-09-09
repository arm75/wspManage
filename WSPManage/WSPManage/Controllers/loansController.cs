using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WSPManage.Models;

namespace WSPManage.Controllers
{
    public class loansController : Controller
    {
        private WSPManageContext db = new WSPManageContext();

        // GET: loans
        public async Task<ActionResult> Index()
        {
            // original code
            // return View(await db.loans.ToListAsync());
            
            List<loan> loansList = await db.loans.OrderBy(p => p.loanID).ToListAsync();

            // set the Property NAME using the ID stored in the loan, for THIS instance of the list. it is NOT STORED.
            foreach (var listEntry in loansList)
            {
                property thisProperty = db.properties.Find(listEntry.propertyID);
                customer thisCustomer = db.customers.Find(listEntry.customerID);

                listEntry.propertyIDName = thisProperty != null ? thisProperty.PhysicalAddress : listEntry.propertyID.ToString();
                listEntry.customerIDName = thisCustomer != null ? thisCustomer.FullName : listEntry.customerID.ToString();

            }

            return View(loansList);
        }

        // GET: loans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loan loan = await db.loans.FindAsync(id);
   
            property thisProperty = db.properties.Find(loan.propertyID);
            customer thisCustomer = db.customers.Find(loan.customerID);

            ViewBag.propertyIDName = thisProperty != null ? thisProperty.PhysicalAddress : ViewBag.propertyID.ToString();
            ViewBag.customerIDName = thisCustomer != null ? thisCustomer.FullName : ViewBag.customerID.ToString();
            
            double MonthlyInterestAsDouble = Convert.ToDouble(loan.InterestRate) / 12;
            var OnePlusI = 1 + MonthlyInterestAsDouble;
            double DiscountRate = ((Math.Pow((OnePlusI), (loan.Period)) - 1) / ( MonthlyInterestAsDouble * (Math.Pow((OnePlusI), (loan.Period)))));
            decimal DiscountRateAsDecimal = Convert.ToDecimal(DiscountRate);
            ViewBag.DiscountRate = DiscountRateAsDecimal;
            ViewBag.MonthlyPayment = Math.Round((loan.LoanAmount / DiscountRateAsDecimal), 2);
            

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
            ViewBag.propertyIDSelectList = new SelectList(db.properties, "propertyID", "PhysicalAddress").OrderBy(p => p.Text);
            ViewBag.customerIDSelectList = new SelectList(db.customers, "customerID", "FullNameLastFirst").OrderBy(p => p.Text);
            return View();
        }

        // POST: loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "loanID,Active,LoanOnly,customerID,propertyID,customerIDName,propertyIDName,SalePrice,DownPayment,ContractDate,ClosingDate,LoanAmount,InterestRate,Period,Payment,FirstPaymentDate,ValueToCalc,LoanNotes,ActionInProgress,ActionInProgressCounty,TenDayDate,NoticeOfPub,FirstPubDate,Bankruptcy,SaleDate,AgreeToCont,NewSaleDate,Judgement,TwentyDayDate,UdrpSentOut,CourtDate,AgreeToVacate,WritDate,EvictionDate,DateCreated,UserCreated,DateModified,UserModified")] loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.Active = true;
                loan.Payment = loan.calculateMonthlyLoanPayment(loan.LoanAmount, loan.InterestRate, loan.Period);

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

            ViewBag.propertyIDSelectList = new SelectList(db.properties, "propertyID", "PhysicalAddress").OrderBy(p => p.Text);
            ViewBag.customerIDSelectList = new SelectList(db.customers, "customerID", "FullNameLastFirst").OrderBy(p => p.Text);

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
        public async Task<ActionResult> Edit([Bind(Include = "loanID,Active,LoanOnly,customerID,propertyID,customerIDName,propertyIDName,SalePrice,DownPayment,ContractDate,ClosingDate,LoanAmount,InterestRate,Period,Payment,FirstPaymentDate,ValueToCalc,LoanNotes,ActionInProgress,ActionInProgressCounty,TenDayDate,NoticeOfPub,FirstPubDate,Bankruptcy,SaleDate,AgreeToCont,NewSaleDate,Judgement,TwentyDayDate,UdrpSentOut,CourtDate,AgreeToVacate,WritDate,EvictionDate,DateCreated,UserCreated,DateModified,UserModified")] loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.Payment = loan.calculateMonthlyLoanPayment(loan.LoanAmount, loan.InterestRate, loan.Period);

                db.Entry(loan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = loan.loanID });
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
