using log4net;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class BaseController : Controller
    {
        protected static readonly ILog Log = log4net.LogManager.GetLogger(typeof(BaseController));
    }
}