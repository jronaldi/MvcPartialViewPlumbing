using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportSchedulerPrototype.Controllers
{
    public class ReportAController : Controller
    {
        public class ReportAViewModel
        {
            public DmModels.DmReportSchedulerFilter ReportSchedulerFilter;
            public DmModels.DmReportAFilter ReportFilter;
        }

        public ActionResult Index()
        {
            ReportAViewModel model = new ReportAViewModel()
            {
                ReportSchedulerFilter = new DmModels.DmReportSchedulerFilter()
                {
                    DayOfWeek = 1,
                    Repeat = false
                },
                ReportFilter = new DmModels.DmReportAFilter()
                {
                    Name = "Some Great report A filter Value",
                    UserAge = 10
                }
            };

            return View(model);
        }
    }
}