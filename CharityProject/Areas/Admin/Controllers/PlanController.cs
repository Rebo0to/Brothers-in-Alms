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
    public class PlanController : Controller
    {
        private IGenericRepository<Plan> repository;
        private CharityDBContext db;


        //Constructor
        public PlanController(CharityDBContext db, IGenericRepository<Plan> repository)
        {
            this.repository = repository;
            this.db = db;
        }

        // GET: Admin/Plan
        public async Task<ActionResult> Index()
        {
            var plans = db.Plans.Include(p => p.Donations).Include(p => p.Subscriptions);

            return View(await plans.ToListAsync());
        }

        // GET: Admin/Plan/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Plan plan = await repository.GetById(id);

            if (plan == null)
                return HttpNotFound();

            return View(plan);
        }

        // GET: Admin/Plan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Plan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PlanId,Category")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(plan);
                await repository.SaveAsync();
                return RedirectToAction("Index");
            }

            return View(plan);
        }

        // GET: Admin/Plan/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Plan plan = await repository.GetById(id);

            if (plan == null)
                return HttpNotFound();

            return View(plan);
        }

        // POST: Admin/Plan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PlanId,Category")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                repository.Update(plan);
                await repository.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(plan);
        }

        // GET: Admin/Plan/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Plan plan = await repository.GetById(id);
            
            if (plan == null)
                return HttpNotFound();

            return View(plan);
        }

        // POST: Admin/Plan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Plan plan = await repository.GetById(id);
            await repository.DeleteAsync(plan);
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
