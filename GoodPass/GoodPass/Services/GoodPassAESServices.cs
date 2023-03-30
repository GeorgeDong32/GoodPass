using System.Security.Cryptography;
using GoodPass.Helpers;
using Windows.Storage;

namespace GoodPass.Services;

/// <summary>
/// 提供AES加密服务
/// </summary>
public static class GoodPassAESServices
{
    public static string EncryptToBase64(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("EncryptToBase64: plainText is null");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("EncryptToBase64: Key is null");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("EncryptToBase64: IV is null");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        return Convert.ToBase64String(encrypted);
    }

    public static string DecryptFromBase64(string cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("DecryptFromBase64: cipherText is null");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("DecryptFromBase64: Key is null");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("DecryptFromBase64: IV is null");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext;

        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        return plaintext;
    }

    /// <summary>
    /// 生成AES初始化向量
    /// </summary>
    public static byte[] GenerateIV(string masterKey)
    {
        var salt = System.Text.Encoding.Default.GetBytes("GoodPass");
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(masterKey, salt);
        return rfc2898DeriveBytes.GetBytes(16);
    }

    public static byte[] GetLocalIV(string masterKey)
    {
        if (RuntimeHelper.IsMSIX)
        {
            string localIVStr;
            byte[] localIV;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("IVBase64", out var obj))
            {
                localIVStr = (string)obj;
                if (localIVStr == String.Empty || localIVStr == null)
                {
                    localIV = GenerateIV(masterKey);
                    SetLocalIV(localIV);
                }
                else
                {
                    localIV = Convert.FromBase64String(localIVStr);
                }
            }
            else
            {
                localIV = GenerateIV(masterKey);
                SetLocalIV(localIV);
            }
            return localIV;
        }
        else
        {
            throw new GPRuntimeException("GetLocalIV: Not in MSIX");
        }
    }

    public static async void SetLocalIV(byte[] IV)
    {
        if (RuntimeHelper.IsMSIX)
        {
            if (IV == null)
                throw new ArgumentNullException();
            else
            {
                ApplicationData.Current.LocalSettings.Values["IVBase64"] = Convert.ToBase64String(IV);
                await Task.CompletedTask;
            }
        }
        else
        {
            throw new GPRuntimeException("GetLocalIV: Not in MSIX");
        }
    }

    public static byte[] GenerateKey(string password, byte[] salt)
    {
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt);
        return rfc2898DeriveBytes.GetBytes(32);
    }
}