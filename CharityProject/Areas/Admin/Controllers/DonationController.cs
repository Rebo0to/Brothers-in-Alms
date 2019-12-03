using CharityProject.Database;
using CharityProject.Entities.DomainClasses;
using CharityProject.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CharityProject.Areas.Admin.Controllers
{

    [Authorize(Roles = "admin")]
    public class DonationController : Controller
    {
        private IGenericRepository<Donation> repository;
        private CharityDBContext db;
        

        //Constructor
        public DonationController(IGenericRepository<Donation> repository, CharityDBContext db)
        {
            this.repository = repository;
            this.db = db;
        }


        // GET: Admin/Donation
        public async Task<ActionResult> Index()
        {
            var donations = repository.GetAll();
            return View(await donations);
        }

        // GET: Admin/Donation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = await repository.GetById(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // GET: Admin/Donation/Create
        public ActionResult Create()
        {
            ViewBag.PartnerId = new SelectList(db.Partners, "PartnerId", "Name");
            ViewBag.PlanId = new SelectList(db.Plans, "PlanId", "Category");
            return View();
        }

        // POST: Admin/Donation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DonationId,Title,Description,Price,PlanId,PartnerId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(donation);
                await repository.SaveAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PartnerId = new SelectList(db.Partners, "PartnerId", "Name", donation.PartnerId);
            ViewBag.PlanId = new SelectList(db.Plans, "PlanId", "Category", donation.PlanId);
            return View(donation);
        }

        // GET: Admin/Donation/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = await repository.GetById(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartnerId = new SelectList(db.Partners, "PartnerId", "Name", donation.PartnerId);
            ViewBag.PlanId = new SelectList(db.Plans, "PlanId", "Category", donation.PlanId);
            return View(donation);
        }

        // POST: Admin/Donation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DonationId,Title,Description,Price,PlanId,PartnerId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                repository.Update(donation);
                await repository.SaveAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PartnerId = new SelectList(db.Partners, "PartnerId", "Name", donation.PartnerId);
            ViewBag.PlanId = new SelectList(db.Plans, "PlanId", "Category", donation.PlanId);
            return View(donation);
        }

        // GET: Admin/Donation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = await repository.GetById(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Admin/Donation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Donation donation = await repository.GetById(id);
            await repository.DeleteAsync(donation);
            await repository.SaveAsync();
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
