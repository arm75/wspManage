using System;
// using System.Collections.Generic;
// using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
// using System.Web;
using System.Web.Mvc;
// using System.Web.UI.WebControls.Expressions;
using WSPManage.Models;
// using PagedList;
using PagedList.EntityFramework;

namespace WSPManage.Controllers
{
    public class propertyController : Controller
    {
        private WSPManageContext db = new WSPManageContext();

        // GET: customers - INDEX ACTION
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            // pull in the values for sort ordering and sort parameter (sort by what? and in what order?)
            // pulls values from the query string
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PhysicalAddressSortParm = String.IsNullOrEmpty(sortOrder) ? "physicaladdress_desc" : "";
            ViewBag.PhysicalCitySortParm = sortOrder == "physicalcity" ? "physicalcity_desc" : "physicalcity";
            ViewBag.PhysicalStateSortParm = sortOrder == "physicalstate" ? "physicalstate_desc" : "physicalstate";
            ViewBag.PhysicalZipcodeSortParm = sortOrder == "physicalzipcode" ? "physicalzipcode_desc" : "physicalzipcode";

            // pull in the value of searchString from 
            // the search box string. gets values from the form.
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // pass the search string to the view so the box can re-populate
            ViewBag.CurrentFilter = searchString;

            // go ahead and create the data set
            var properties = from s in db.properties
                select s;

            // remove all data from the data set,
            // that does not contain the searchString
            if (!String.IsNullOrEmpty(searchString))
            {
                properties = properties.Where(s => s.PhysicalAddress.Contains(searchString)
                                                 || s.PhysicalCity.Contains(searchString) 
                                                 || s.PhysicalState.Contains(searchString)
                                                   || s.PhysicalZipcode.Contains(searchString));
            }

            // re-order the data set to the sortOrder
            // and sort parameter specifications.
            // the DEFAULT case, at the bottom, is order by LastName, ascending.
            switch (sortOrder)
            {
                case "physicaladdress_desc":
                    properties = properties.OrderByDescending(s => s.PhysicalAddress);
                    break;
                case "physicalcity":
                    properties = properties.OrderBy(s => s.PhysicalCity);
                    break;
                case "physicalcity_desc":
                    properties = properties.OrderByDescending(s => s.PhysicalCity);
                    break;
                case "physicalstate":
                    properties = properties.OrderBy(s => s.PhysicalState);
                    break;
                case "physicalstate_desc":
                    properties = properties.OrderByDescending(s => s.PhysicalState);
                    break;
                case "physicalzipcode":
                    properties = properties.OrderBy(s => s.PhysicalZipcode);
                    break;
                case "physicalzipcode_desc":
                    properties = properties.OrderByDescending(s => s.PhysicalZipcode);
                    break;
                default:
                    properties = properties.OrderBy(s => s.PhysicalAddress);
                    break;
            }

            // null-coalescing operator. It returns the left-hand operand
            // if the operand is not null; otherwise it returns the
            // right hand operand. SO, if page HAS a value, give it to pageNumber,
            // if page DOESN'T have a value, set pageNumber to 1.
            int pageSize = 30;
            int pageNumber = (page ?? 1);

            // and finally, give the data set to the view.
            return View(await properties.ToPagedListAsync(pageNumber, pageSize));

            // ORIGINAL return to the view
            //return View(await properties.ToListAsync());
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
            ViewBag.businessEntitiesDropdownList = new SelectList(db.businessEntities, "businessID", "businessName");
            ViewBag.statesDropdownList = statesDropdownList.statesList;
            ViewBag.physicalStreetDirDropdownList = physicalStreetDirDropdownList.physicalStreetDirList;
            
            return View();
        }

        // POST: properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "propertyID,Active,PhysicalStreetNumber,PhysicalStreetDir,PhysicalStreetName,PhysicalAddress,PhysicalUnit,PhysicalCity,PhysicalState,PhysicalZipcode,PhysicalCounty,OriginalCost,AcquireDate,MarketValue,RentalUnit,PropertySource,LegallyAvailable,PhysicallyAvailable,WSPOwned,InsuranceRequired,InsuranceCarrier,InsurancePolicy,InsurancePolicyExpiration,WSPLiabilityDateAdded,Lender,LoanNumber,WSPBalanceOwed,TaxIDNumber,LastYearPaid,DateCreated,UserCreated,DateModified,UserModified")] property property)
        {
            if (ModelState.IsValid)
            {
                property.Active = true;
                property.PhysicalAddress = property.PhysicalStreetNumber + " " + property.PhysicalStreetDir + " " + property.PhysicalStreetName + " Unit:" + property.PhysicalUnit;
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
                        
            ViewBag.businessEntitiesDropdownList = new SelectList(db.businessEntities, "businessID", "businessName");
            ViewBag.statesDropdownList = statesDropdownList.statesList;
            ViewBag.physicalStreetDirDropdownList = physicalStreetDirDropdownList.physicalStreetDirList;
            
            // The FIRST way I built the select list, by creating a new instance of a model property.
            //property.businessentitySelectList = new SelectList(db.businessEntities, "businessID", "businessName");

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
        public async Task<ActionResult> Edit([Bind(Include = "propertyID,Active,PhysicalStreetNumber,PhysicalStreetDir,PhysicalStreetName,PhysicalAddress,PhysicalUnit,PhysicalCity,PhysicalState,PhysicalZipcode,PhysicalCounty,OriginalCost,AcquireDate,MarketValue,RentalUnit,PropertySource,LegallyAvailable,PhysicallyAvailable,WSPOwned,InsuranceRequired,InsuranceCarrier,InsurancePolicy,InsurancePolicyExpiration,WSPLiabilityDateAdded,Lender,LoanNumber,WSPBalanceOwed,TaxIDNumber,LastYearPaid,DateCreated,UserCreated,DateModified,UserModified")] property property)
        {
            if (ModelState.IsValid)
            {
                property.PhysicalAddress = property.PhysicalStreetNumber + " " + property.PhysicalStreetDir + " " + property.PhysicalStreetName + " Unit:" + property.PhysicalUnit;
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