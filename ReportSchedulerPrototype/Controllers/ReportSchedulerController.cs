using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ReportSchedulerPrototype.DmModels;
using ReportSchedulerPrototype.Models;

namespace ReportSchedulerPrototype.Controllers
{
    public class ReportSchedulerController : Controller
    {
        // We have one "Save" method per report type simply to let MVC's normal binding and validation work.
        // There are ways to change this to a single method but it won't be implemented this time due to time
        // constraints.
        public JsonResult ReportASave(DmReportSchedulerFilter reportSchedulerFilter, DmReportAFilter reportFilter)
        {
            return Save(reportSchedulerFilter, reportFilter);
        }

        public JsonResult ReportBSave(DmReportSchedulerFilter reportSchedulerFilter, DmReportBFilter reportFilter)
        {
            return Save(reportSchedulerFilter, reportFilter);
        }

        /// <summary>
        /// This helper method does the actual call to the backend. It receives a 'DmReportFilterBase' which
        /// will be serialized on the wire as the real type that it is (whatever derived filter type, such as
        /// 'DmReportAFilter' or 'DmReportBFilter') and received at the other end as such, to be serialized in
        /// the scheduling database.
        /// </summary>
        /// <param name="reportSchedulerFilter">The scheduling-specific parameters, such as recurrence type and
        /// days of the week to execute the report.</param>
        /// <param name="reportFilter">The report-specific filters, such as "user ID" or "User group".</param>
        /// <returns></returns>
        private JsonResult Save(DmReportSchedulerFilter reportSchedulerFilter, DmReportFilterBase reportFilter)
        {
            if (ModelState.IsValid)
            {
                //NOTE: Here we would call the backend through the model as usual
                //reportSchedulerModel.Save(reportSchedulerFilter, reportFilter);

                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json("Report schedule created");
            }
            else
            {
                DmMvcErrorResult mvcErrorResult = new DmMvcErrorResult(ModelState);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(mvcErrorResult);
            }
        }
    }
}
