using CharityProject.Database;
using CharityProject.Entities.DomainClasses;
using CharityProject.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CharityProject.Areas.Admin.Controllers
{

    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private IGenericRepository<Product> repository;
        private CharityDBContext db;


        //Constructor
        public ProductController(IGenericRepository<Product> repository, CharityDBContext db)
        {
            this.repository = repository;
            this.db = db;
        }


        // GET: Admin/Product
        public async Task<ActionResult> Index()
        {
            return View(await repository.GetAll());
        }

        // GET: Admin/Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = await repository.GetById(id);
           
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price,Size")] Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(product);
                await repository.SaveAsync();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = await repository.GetById(id);
           
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price,Size")] Product product)
        {
            if (ModelState.IsValid)
            {
               repository.Update(product);
               await repository.SaveAsync();
               return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = await repository.GetById(id);
           
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await repository.GetById(id);
            await repository.DeleteAsync(product);
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
