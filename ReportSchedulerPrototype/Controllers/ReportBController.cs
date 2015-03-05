using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportSchedulerPrototype.Controllers
{
    public class ReportBController : Controller
    {
        public class ReportBViewModel
        {
            public DmModels.DmReportSchedulerFilter ReportSchedulerFilter;
            public DmModels.DmReportBFilter ReportFilter;
        }

        public ActionResult Index()
        {
            ReportBViewModel model = new ReportBViewModel()
            {
                ReportSchedulerFilter = new DmModels.DmReportSchedulerFilter()
                {
                    DayOfWeek = 1,
                    Repeat = false
                },
                ReportFilter = new DmModels.DmReportBFilter()
                {
                    Name = "Some Great report B filter Value",
                    DayOfMonth = 1
                }
            };

            return View(model);
        }
    }
}