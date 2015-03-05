// Template NOTE:
// Here we establish a click handler on the generic "Save" button of the report scheduler's shared partial.
// We take the base URL path from an hidden form variable, so that each report can define its own URL, as
// long as the same "actions" are supported from all reports as far as the report scheduler partial is concerned.
$(function ()
{
    var reportTypeName = $("#ReportTypeName").val();
    $("#ReportSchedulerSave").on("click", function () { ReportSchedulerSave_OnClick(reportTypeName); });
});

// Template NOTE:
// Where the "save" button of the report scheduler partial is clicked, we end up here and will build the
// appropriate "full URL" from the report's base URL and the desired action we wish to call (here "Save").
// The following code assumes that all report-specific filters are contained inside a <div> with an ID of
// "ReportFilters". This should be considered a "well-known name" as there is no point in changing it per
// report. A post will then be sent to the appropriate controller action and the successful or failed 
// response will be handled appropriately.
// In this template, we assume that error responses return a "DmMvcErrorResult" JSON structure that
// contains all model state errors (validations) as well as a single "ErrorMessage" field that can be
// used if we don't want to act on specific model errors.
// Successful calls are assumed to return nothing.
function ReportSchedulerSave_OnClick(reportTypeName)
{
    // Here we would POST to the appropriate report scheduling controller's command handler
    var reportFilters = $("#ReportFilters :input").serialize();
    var reportSchedulerFilters = $("#ReportSchedulerFilters :input").serialize();

    // We construct the post URL from the report type name, which results in a unique path such as
    // this example for 'ReportA' : "/ReportScheduler/ReportASave". This way of constructing the path
    // is dependent upon the location of the scheduler controller and the action names it defines.
    var saveActionUrl = "/ReportScheduler/" + reportTypeName + "Save";

    var answer = $.ajax(saveActionUrl,
        {
            type: "post",
            data: reportSchedulerFilters + "&" + reportFilters,
            dataType: "json",
            error: function (errorResponse) { ReportSchedulerSave_OnSaveError(errorResponse.responseJSON); },
            success: function (successResponse) { ReportSchedulerSave_OnSaveSuccess(successResponse); }
        });
}

function ReportSchedulerSave_OnSaveSuccess(successResponse)
{
    // Here we would give user feedback as to the success of the operation. If required, we could
    // implement a mechanism to call back a report-specific handler.
    alert("Scheduler data save success response message: " + successResponse);
}

function ReportSchedulerSave_OnSaveError(modelErrors) {
    // Here we would give user feedback regarding the failed operation. The response
    // gives us both a simple error message (first one) or the complete model state describing
    // individual field error. If required, we could implement a mechanism to call back a 
    // report-specific handler.
    alert("Scheduler data save error message: " + modelErrors.ErrorMessage);
}
