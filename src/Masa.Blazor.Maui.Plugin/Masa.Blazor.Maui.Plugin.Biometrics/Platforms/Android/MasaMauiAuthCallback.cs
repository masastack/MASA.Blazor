using AndroidX.Core.Hardware.Fingerprint;

namespace Masa.Blazor.Maui.Plugin.Biometrics
{
    public class MasaMauiAuthCallback : FingerprintManagerCompat.AuthenticationCallback
    {
        //  Write whatever you want, but keep it unique within the app
        byte[] SECRET_BYTES = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        
        public override void OnAuthenticationSucceeded(FingerprintManagerCompat.AuthenticationResult result)
        {
            if (result.CryptoObject.Cipher != null) 
            {
                var doFinalResult = result.CryptoObject.Cipher.DoFinal(SECRET_BYTES);

                if (doFinalResult.Any())
                {
                    MessagingCenter.Send<MasaMauiAuthCallback,string>(this, "Validation", "Validation succeeded");

                }
            }
            else
            {
                // Not using Cipher?
                // Our example here uses Cipher, leaving aside the exclusions for now
            }
        }

        public override void OnAuthenticationFailed()
        {
            // Notifies the user that authentication failed
            MessagingCenter.Send<MasaMauiAuthCallback, string>(this, "Validation", "Validation failed");
        }
    }
}
