using Masa.Blazor.Maui.Plugin.Biometrics;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Maui.Plugin.BiometricsSample.Pages
{
    public partial class Index
    {
        [Inject]
        private IPopupService PopupService { get; set; }
        private async Task Fingerprint()
        {
            var checkingEligibilityErrorMessage = await MasaMauiFingerprintService.CheckingEligibility();
            if (string.IsNullOrEmpty(checkingEligibilityErrorMessage))
            {
                await HandledValidationAsync();
                MasaMauiFingerprintService.FingerPrintAuthentication();
            }
            else
            {
                await PopupService.ToastErrorAsync(checkingEligibilityErrorMessage);
            }
        }

        private async Task HandledValidationAsync()
        {
            // Cancel your subscription first to prevent duplicate subscriptions
            MessagingCenter.Unsubscribe<MasaMauiAuthCallback, string>(this, "Validation");
            MessagingCenter.Subscribe<MasaMauiAuthCallback, string>(this, "Validation", (sender, arg) =>
            {
                PopupService.ToastInfoAsync(arg);
            });
        }
    }
}
