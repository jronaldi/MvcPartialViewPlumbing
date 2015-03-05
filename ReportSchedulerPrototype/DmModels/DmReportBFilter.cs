using ReportSchedulerPrototype.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReportSchedulerPrototype.DmModels
{
    // This class would probably come from the "Anomalous.Model" module as defined by the backend's
    // supported filter.
    public class DmReportBFilter : DmReportFilterBase
    {
        // Always use resources for validation messages
        [Required(ErrorMessageResourceType = typeof(DmReportBFilterResources), ErrorMessageResourceName = "DayOfMonthRequired")]
        [Display(Name = "Day Of Month (1-31)")]
        // Always use resources for validation messages
        [Range(1, 31, ErrorMessageResourceType = typeof(DmReportBFilterResources), ErrorMessageResourceName = "DayOfMonthOutOfRange")]
        public int DayOfMonth { get; set; }
    }
}
