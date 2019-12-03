using CharityProject.Entities.DomainClasses;
using CharityProject.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CharityProject.Controllers
{
    public class DonationController : Controller
    {
      
        private IGenericRepository<Donation> repository;

        
        //Constructor
        public DonationController(IGenericRepository<Donation> repository)
        {
            this.repository = repository;        
        }


        // GET: Donation
        public async Task<ActionResult> Index()
        {           
            var donations = repository.GetAll();
            return View(await donations);
        }

        // GET: Donation/Details/5

        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation =await repository.GetById(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

           
    }
}
