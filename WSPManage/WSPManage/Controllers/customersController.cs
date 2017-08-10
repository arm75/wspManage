using System;
// using System.Collections.Generic;
// using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
// using System.Web;
using System.Web.Mvc;
using WSPManage.Models;
// using PagedList;
using PagedList.EntityFramework;

namespace WSPManage.Controllers
{
    public class customersController : Controller
    {
        private WSPManageContext db = new WSPManageContext();

        // GET: customers
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstname_desc" : "firstname";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var customers = from s in db.customers
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "lastname_desc":
                    customers = customers.OrderByDescending(s => s.LastName);
                    break;
                case "firstname":
                    customers = customers.OrderBy(s => s.FirstName);
                    break;
                case "firstname_desc":
                    customers = customers.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    customers = customers.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(await customers.ToPagedListAsync(pageNumber, pageSize));

            //return View(await customers.ToListAsync());


        }

        // GET: customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = await db.customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: customers/Create
        public ActionResult Create()
        {
            ViewBag.statesDropdownList = statesDropdownList.statesList;

            return View();
        }

        // POST: customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "customerID,Active,FirstName,MiddleName,LastName,SSN,EIN,ContractName,AlternateName,MailingAddress,MailingCity,MailingState,MailingZipcode,HomePhoneNumber,WorkPhoneNumber,CellPhoneNumber,AlternateNumber,Notes,DateCreated,UserCreated,DateModified,UserModified")] customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Active = true;
                db.customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            customer customer = await db.customers.FindAsync(id);

            ViewBag.statesDropdownList = statesDropdownList.statesList;

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "customerID,Active,FirstName,MiddleName,LastName,SSN,EIN,ContractName,AlternateName,MailingAddress,MailingCity,MailingState,MailingZipcode,HomePhoneNumber,WorkPhoneNumber,CellPhoneNumber,AlternateNumber,Notes,DateCreated,UserCreated,DateModified,UserModified")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = await db.customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            customer customer = await db.customers.FindAsync(id);
            db.customers.Remove(customer);
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
