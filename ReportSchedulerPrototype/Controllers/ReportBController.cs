using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Resources;
using ReportSchedulerPrototype.Resources;

namespace ReportSchedulerPrototype.Controllers
{
    public class ReportBController : Controller
    {
        public class ReportBViewModel
        {
            public DmModels.DmReportSchedulerFilter ReportSchedulerFilter;
            public DmModels.DmReportBFilter ReportFilter;
            public ReportFilterDisplayList FilterDisplayList;
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
                },
            };

#if true
            // Example using the "Example filter class":
            model.FilterDisplayList = new ReportFilterDisplayList(new ExampleFilterClass() { PhoneNo = 5143334444, City = "Montreal", Province = "Quebec" });
#else
            // Example how we would pass a real report filter:
            model.FilterDisplayList = new ReportFilterDisplayList(model.ReportFilter);
#endif

            return View(model);
        }


        #region Example of extracting filter values for display

        /// <summary>
        /// This is simply an example of a report filter and how we would apply the 'ReportFilterDisplayName'
        /// attribute to each property so they would display automatically.
        /// </summary>
        public class ExampleFilterClass
        {
            [ReportFilterDisplayName(typeof(DmReportBFilterResources), "ExampleFilterProp1")]
            public long PhoneNo { get; set; }

            [ReportFilterDisplayName(typeof(DmReportBFilterResources), "ExampleFilterProp2")]
            public string City { get; set; }

            [ReportFilterDisplayName(false)]
            public string Province { get; set; }
        }

        /// <summary>
        /// This class must be applied to every filter fields to give hints about how a particular field
        /// should be displayed to the user.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
        public class ReportFilterDisplayNameAttribute : System.Attribute
        {
            public bool AutoDisplay { get; private set; }
            public string DisplayName { get; private set; }

            /// <summary>
            /// Use this constructor for specifying which resource file and item to use as the display name by the
            /// automatic display builder.
            /// </summary>
            /// <param name="displayNameResourceType"></param>
            /// <param name="displayNameResourceName"></param>
            /// <param name="autoDisplay"></param>
            public ReportFilterDisplayNameAttribute(Type displayNameResourceType, string displayNameResourceName, bool autoDisplay = true)
            {
                ResourceManager _displayResourceManager = new ResourceManager(displayNameResourceType);

                DisplayName = _displayResourceManager.GetString(displayNameResourceName);
                AutoDisplay = autoDisplay;
            }

            /// <summary>
            ///  Use this constructor to set a direct display name or prevent a field from being displayed to the user in the
            ///  automatic display builder.
            /// </summary>
            /// <param name="displayName">Name to use for display.</param>
            /// <param name="autoDisplay">True: Display in auto-formatted lists.</param>
            public ReportFilterDisplayNameAttribute(bool autoDisplay, string displayName = null)
            {
                DisplayName = displayName;
                AutoDisplay = autoDisplay;
            }
        }

        /// <summary>
        /// This class represents a user-displayable list of all the public filter properties for the filter
        /// object passed as a parameter. The actual fields' names can be translated through resource files.
        /// </summary>
        public class ReportFilterDisplayList : List<System.Collections.Generic.KeyValuePair<string,string>>
        {
            public ReportFilterDisplayList(object filter)
            {
                Type filterType = filter.GetType();

                PropertyInfo[] filterProps = filterType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo pi in filterProps)
                {
                    string propertyName = pi.Name;

                    // If we have a display attribute, use it to fetch the display name instead of using property name
                    ReportFilterDisplayNameAttribute filterDisplayNameAttr = pi.GetCustomAttribute<ReportFilterDisplayNameAttribute>();
                    if (filterDisplayNameAttr != null)
                    {
                        if (!filterDisplayNameAttr.AutoDisplay) continue;
                        propertyName = filterDisplayNameAttr.DisplayName;
                    }

                    object filterPropValue = pi.GetValue(filter);
                    string propertyValue = (filterPropValue != null) ? filterPropValue.ToString() : String.Empty;

                    this.Add(new KeyValuePair<string, string>(propertyName, propertyValue));
                }
            }
        }

        #endregion
    }
}