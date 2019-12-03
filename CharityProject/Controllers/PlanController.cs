using CharityProject.Entities.DomainClasses;
using CharityProject.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CharityProject.Controllers
{
    public class PlanController : Controller
    {
      
        private IGenericRepository<Plan> db;


        //Constructor
        public PlanController(IGenericRepository<Plan> repository)
        {
            this.db = repository;
        }

        // GET: Plan
        public async Task<ActionResult> Index()
        {
            return View(await db.GetAll());
        }

        // GET: Plan/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = await db.GetById(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }
    }
}
