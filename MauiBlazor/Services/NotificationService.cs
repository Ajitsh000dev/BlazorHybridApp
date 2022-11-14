using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedUI.Notifications;

namespace MauiBlazor.Services
{
    internal class NotificationService : INotificationService
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

        [Inject]
        public IJSRuntime _jsRuntime { get; set; }
        /// <inheritdoc/>
        public PermissionType PermissionStatus { get; private set; }
        public NotificationService(IJSRuntime jsRuntime)
        {

            _jsRuntime= jsRuntime;  
        }

        /// <inheritdoc/>
        public async ValueTask<bool> IsSupportedByBrowserAsync()
        {
            try
            {
                //var module = await _moduleTask.Value;
                //return await module.InvokeAsync<bool>("isSupported");
                return await _jsRuntime.InvokeAsync<bool>("isSupported");


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <inheritdoc/>
        public async ValueTask<PermissionType> RequestPermissionAsync()
        {
            try
            {
                //var module = await _moduleTask.Value;
                //string permission = await module.InvokeAsync<string>("requestPermission");
                string permission =  await _jsRuntime.InvokeAsync<string>("requestPermission");
                if (permission.Equals("granted", StringComparison.InvariantCultureIgnoreCase))
                    PermissionStatus = PermissionType.Granted;

                if (permission.Equals("denied", StringComparison.InvariantCultureIgnoreCase))
                    PermissionStatus = PermissionType.Denied;
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return PermissionStatus;
        }

        /// <inheritdoc/>
        public async ValueTask CreateAsync(string title, NotificationOptions options)
        {
            //var module = await _moduleTask.Value;
            //await module.InvokeVoidAsync("create", title, options);
            await _jsRuntime.InvokeVoidAsync("create", title, options);
        }

        /// <inheritdoc/>
        public async ValueTask CreateAsync(string title, string body, string icon = null)
        {
            var module = await _moduleTask.Value;

            NotificationOptions options = new NotificationOptions
            {
                Body = body,
                Icon = icon,
            };

            await module.InvokeVoidAsync("create", title, options);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
