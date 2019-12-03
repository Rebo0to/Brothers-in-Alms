using CharityProject.Entities.DomainClasses;
using CharityProject.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CharityProject.Controllers
{
    public class SubscriptionController : Controller
    {

        private IGenericRepository<Subscription> repository;


        //Constructor
        public SubscriptionController(IGenericRepository<Subscription> repository)
        {
            this.repository = repository;
        }


        // GET: Subscription
        public async Task<ActionResult> Index()
        {          
            return View(await repository.GetAll());
        }

        // GET: Subscription/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = await repository.GetById(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }  
    }
}
