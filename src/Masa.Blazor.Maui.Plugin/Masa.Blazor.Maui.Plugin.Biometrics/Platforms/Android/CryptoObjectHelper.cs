using Android.Hardware.Biometrics;
using Android.Security.Keystore;
using AndroidX.Core.Hardware.Fingerprint;
using Java.Security;
using Javax.Crypto;

namespace Masa.Blazor.Maui.Plugin.Biometrics
{
    public class CryptoObjectHelper
    {
        // Key-value names that need to be unique within the app
        static readonly string KEY_NAME = "com.masa-maui-blazor.android.sample.fingerprint_authentication_key";

        // Write dead without changing it
        static readonly string KEYSTORE_NAME = "AndroidKeyStore";

        // Encryption algorithm parameters do not need to be changed
        static readonly string KEY_ALGORITHM = KeyProperties.KeyAlgorithmAes;
        static readonly string BLOCK_MODE = KeyProperties.BlockModeCbc;
        static readonly string ENCRYPTION_PADDING = KeyProperties.EncryptionPaddingPkcs7;
        static readonly string TRANSFORMATION = KEY_ALGORITHM + "/" +
                                                BLOCK_MODE + "/" +
                                                ENCRYPTION_PADDING;
        readonly KeyStore _keystore;

        public CryptoObjectHelper()
        {
            _keystore = KeyStore.GetInstance(KEYSTORE_NAME);
            _keystore.Load(null);
        }

        public FingerprintManagerCompat.CryptoObject BuildCryptoObject()
        {
            var cipher = CreateCipher();
            return new FingerprintManagerCompat.CryptoObject(cipher);
        }

        Cipher CreateCipher(bool retry = true)
        {
            var key = GetKey();
            var cipher = Cipher.GetInstance(TRANSFORMATION);
            try
            {
                cipher.Init(CipherMode.EncryptMode, key);
            }
            catch (KeyPermanentlyInvalidatedException e)
            {
                _keystore.DeleteEntry(KEY_NAME);
                if (retry)
                {
                    CreateCipher(false);
                }
                else
                {
                    throw new Exception("Could not create the cipher for fingerprint authentication.", e);
                }
            }
            return cipher;
        }

        IKey GetKey()
        {
            IKey secretKey;
            if (!_keystore.IsKeyEntry(KEY_NAME))
            {
                CreateKey();
            }

            secretKey = _keystore.GetKey(KEY_NAME, null);
            return secretKey;
        }

        void CreateKey()
        {
            var keyGen = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, KEYSTORE_NAME);
            var keyGenSpec =
                new KeyGenParameterSpec.Builder(KEY_NAME, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                    .SetBlockModes(BLOCK_MODE)
                    .SetEncryptionPaddings(ENCRYPTION_PADDING)
                    .SetUserAuthenticationRequired(true)
                    .Build();
            keyGen.Init(keyGenSpec);
            keyGen.GenerateKey();
        }
    }
}
