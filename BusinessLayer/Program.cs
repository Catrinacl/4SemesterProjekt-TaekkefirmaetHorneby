 public class ProjektController : Controller
    {
        public Action Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(FormCollection formCollection)
        {
            ViewBag.ProjektID = formCollection["projektID"];
            ViewBag.KundeID = formCollection["kundeID"];
            ViewBag.MedarbejderList = formCollection["medarbejderList"];
            ViewBag.Adresse = formCollection["adresse"];
            ViewBag.Status = formCollection["status"];
            ViewBag.ForventetVarighedTimer = formCollection["forventetvarighedtimer"];
            ViewBag.KvadratmeterAfTag = formCollection["kvadratmeteraftag"];
            return View();
        }
    }