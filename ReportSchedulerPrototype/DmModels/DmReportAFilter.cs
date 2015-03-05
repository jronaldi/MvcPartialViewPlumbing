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
    public class DmReportAFilter : DmReportFilterBase
    {
        // Always use resources for validation messages
        [Required(ErrorMessageResourceType = typeof(DmReportAFilterResources), ErrorMessageResourceName = "UserAgeRequired")]
        [Display(Name = "User Age")]
        // Always use resources for validation messages
        [Range(18, 100, ErrorMessageResourceType = typeof(DmReportAFilterResources), ErrorMessageResourceName = "UserAgeOutOfRange")]
        public int UserAge { get; set; }
    }
}