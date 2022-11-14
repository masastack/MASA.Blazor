using Android.App;
using Android.Content;
using AndroidX.Core.Hardware.Fingerprint;
using AndroidX.Core.OS;

namespace Masa.Blazor.Maui.Plugin.Biometrics
{
    // All the code in this file is only included on Android.
    public static partial class MasaMauiFingerprintService
    {
        private static FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(Android.App.Application.Context);

        public static void FingerPrintAuthentication()
        {
            fingerprintManager.Authenticate(new CryptoObjectHelper().BuildCryptoObject(), 0, new CancellationSignal(), new MasaMauiAuthCallback(), null);
        }
        /// <summary>
        /// Check eligibility
        /// </summary>
        /// <returns>error message</returns>
        public static async Task<string> CheckingEligibility()
        {
            // 1、Check if your hardware supports it
            if (!fingerprintManager.IsHardwareDetected)
            {
                return "IsHardwareDetected";
            }

            // 2、Check if the user is using a screen lock
            // KeyguardManager: Lock screen management class
            var keyguardManager = Android.App.Application.Context.GetSystemService(Context.KeyguardService) as KeyguardManager;
            if (!keyguardManager.IsKeyguardSecure)
            {
                return "The device does not have a screen lock set";
            }

            // 3、Check if at least one fingerprint is registered
            if (!fingerprintManager.HasEnrolledFingerprints)
            {
                return "The device does not have a fingerprint set, please set at least one fingerprint";
            }

            var granted = await CheckAndRequestFingerprintPermission();
            if (!granted)
            {
                return "Permissions not granted";
            }
            return string.Empty;
        }

        /// <summary>
        /// Permission check
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> CheckAndRequestFingerprintPermission()
        {
            var status = await Permissions.CheckStatusAsync<AndroidFingerprintPermissions>();

            if (status == PermissionStatus.Granted)
                return true;

            status = await Permissions.RequestAsync<AndroidFingerprintPermissions>();

            if (status == PermissionStatus.Granted)
                return true;
            return false;
        }

        /// <summary>
        /// Permissions required for fingerprints
        /// </summary>
        private class AndroidFingerprintPermissions : Permissions.BasePlatformPermission
        {
            public override (string androidPermission, bool isRuntime)[] RequiredPermissions =>
                new List<(string androidPermission, bool isRuntime)>
                {
                    (global::Android.Manifest.Permission.UseFingerprint, true),
                }.ToArray();
        }
    }
}