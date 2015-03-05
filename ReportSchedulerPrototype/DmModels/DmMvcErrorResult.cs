using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportSchedulerPrototype.DmModels
{
    public class DmMvcErrorResult
    {
        public string _errorMessage = null;
        public string ErrorMessage 
        {
            get {
                if (_errorMessage != null) return _errorMessage;
                else
                {
                    if (ModelState.Count == 0)
                        return String.Empty;
                    else
                        return ModelState.First().Value;
                }
            }

            set { _errorMessage = value; }
        }

        public Dictionary<string, string> ModelState { get; set; }

        public DmMvcErrorResult(ModelStateDictionary modelState)
        {
            var modelErrors = from x in modelState where x.Value.Errors.Count > 0 select new KeyValuePair<string, ModelError>(x.Key, x.Value.Errors.FirstOrDefault());
            ModelState = new Dictionary<string, string>(modelErrors.Count());
            foreach (var item in modelErrors)
            {
                ModelState.Add(item.Key, item.Value.ErrorMessage);
            }
        }
    }
}