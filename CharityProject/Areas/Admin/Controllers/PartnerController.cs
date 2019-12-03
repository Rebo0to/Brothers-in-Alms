using CharityProject.Database;
using CharityProject.Entities.DomainClasses;
using CharityProject.Services;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CharityProject.Areas.Admin.Controllers
{

    [Authorize(Roles = "admin")]
    public class PartnerController : Controller
    {
        private IGenericRepository<Partner> repository;
        private CharityDBContext db;


        //Constructor
        public PartnerController(CharityDBContext db, IGenericRepository<Partner> repository)
        {
            this.db = db;
            this.repository = repository;
        }


        // GET: Admin/Partner
        public async Task<ActionResult> Index()
        {
            var partners = db.Partners.Include(p => p.Donations).Include(p => p.Subscriptions);
            return View(await partners.ToListAsync());
        }

        // GET: Admin/Partner/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Partner partner = await repository.GetById(id);
            if (partner == null)
                return HttpNotFound();

            return View(partner);
        }

        // GET: Admin/Partner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Partner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PartnerId,Name,Address,Country,Description,ImageUrl,Longitude,Latitude")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(partner);
                await repository.SaveAsync();
                return RedirectToAction("Index");
            }

            return View(partner);
        }

        // GET: Admin/Partner/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partner partner = await repository.GetById(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // POST: Admin/Partner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PartnerId,Name,Address,Country,Description,ImageUrl,Longitude,Latitude")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                repository.Update(partner);
                await repository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(partner);
        }

        // GET: Admin/Partner/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Partner partner = await repository.GetById(id);

            if (partner == null)
                return HttpNotFound();

            return View(partner);
        }

        // POST: Admin/Partner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Partner partner = await repository.GetById(id);
            await repository.DeleteAsync(partner);
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
