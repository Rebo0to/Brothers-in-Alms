using CharityProject.Database;
using System.Linq;
using System.Web.Mvc;

namespace CharityProject.Controllers
{
    public class MapController : Controller
    {
        private CharityDBContext db;

        //Constructor
        public MapController(CharityDBContext context)
        {
            db = context;
        }

        public JsonResult GetLocations()
        {

            var pointers = from partner in db.Partners
                        select new { partner.Name, 
                                     partner.Country, 
                                     partner.Description,
                                     partner.Latitude, 
                                     partner.Longitude,
                                     partner.ImageUrl
                        };

            return Json(pointers, JsonRequestBehavior.AllowGet);

        }
    }
}