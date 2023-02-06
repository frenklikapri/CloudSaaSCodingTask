using Microsoft.JSInterop;

namespace CloudSaaSCodingTask.Web.Helpers
{
    public static class JSExtensions
    {
        public static async Task ShowModalWithIdAsync(this IJSRuntime jS, string modalId, bool noCloseWithClick = false)
        {
            var module = await jS.InvokeAsync<IJSObjectReference>("import",
                "./js/main.js");
            await module.InvokeAsync<string>("showModalWithId", modalId, noCloseWithClick);
        }

        public static async Task HideModalWithIdAsync(this IJSRuntime jS, string modalId)
        {
            var module = await jS.InvokeAsync<IJSObjectReference>("import",
                "./js/main.js");
            await module.InvokeAsync<string>("hideModalWithId", modalId);
        }

        /// <summary>
        /// Shows an error toast. Uses JS
        /// </summary>
        /// <param name="jS"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static async Task ShowErrorAsync(this IJSRuntime jS, string message, string title = "Error")
        {
            var module = await jS.InvokeAsync<IJSObjectReference>("import",
                "./toastr/toastrFunctions.js");
            await module.InvokeAsync<string>("showError", message, title);
        }

        /// <summary>
        /// Shows a success toast
        /// </summary>
        /// <param name="jS"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static async Task ShowSuccessAsync(this IJSRuntime jS, string message, string title = "Success")
        {
            var module = await jS.InvokeAsync<IJSObjectReference>("import",
                "./toastr/toastrFunctions.js");
            await module.InvokeAsync<string>("showSuccess", message, title);
        }
    }
}
