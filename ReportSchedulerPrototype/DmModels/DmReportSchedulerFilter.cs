using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReportSchedulerPrototype.DmModels
{
    public class DmReportSchedulerFilter
    {
        [Required]
        [Display(Name = "Day Of Week (1=Monday)")]
        [Range(1, 7)]
        public int DayOfWeek { get; set; }

        [Required]
        [Display(Name = "Repeat")]
        public bool Repeat { get; set; }
    }
}