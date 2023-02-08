using System.Security.Cryptography;

namespace GoodPass.Services;

/// <summary>
/// Provide SHA256 and GPHES hash service
/// </summary>
public static class GoodPassSHAServices
{
    /// <summary>
    /// get SHA256 hash of the InputString
    /// </summary>
    public static string getSHA256(string InputString)
    {
        try
        {
            SHA256 SHA256temp = SHA256.Create();
            var bytValue = System.Text.Encoding.UTF8.GetBytes(InputString);
            var bytHash = SHA256temp.ComputeHash(bytValue);
            SHA256temp.Clear();
            string sHash = "", sTemp = "";
            for (var counter = 0; counter < bytHash.Count(); counter++)
            {
                long i = bytHash[counter] / 16;
                if (i > 9)
                {
                    sTemp = ((char)(i - 10 + 0x41)).ToString();
                }
                else
                {
                    sTemp = ((char)(i + 0x30)).ToString();
                }
                i = bytHash[counter] % 16;
                if (i > 9)
                {
                    sTemp += ((char)(i - 10 + 0x41)).ToString();
                }
                else
                {
                    sTemp += ((char)(i + 0x30)).ToString();
                }
                sHash += sTemp;
            }
            return sHash.ToUpper();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Get GoodPassHash value of the input string
    /// </summary>
    public static string getGPHES(string InputString)
    {
        try
        {
            var saltbase = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            var mkbase = new int[InputString.Length];
            for (var i = 0; i < InputString.Length; i++)
            {
                if (InputString[i] <= 'z' && InputString[i] >= 'a')
                    mkbase[i] = InputString[i] - 'a';
                else if (InputString[i] <= 'Z' && InputString[i] >= 'A')
                    mkbase[i] = InputString[i] - 'A';
                else if (InputString[i] <= '9' && InputString[i] >= '0')
                    mkbase[i] = InputString[i] - '0';
                else
                    mkbase[i] = 0;
            }
            int addcon = InputString.Length / 8; var j = 0; var mkbc = 0;
            for (var i = 0; i < 8; i++)
            {
                j = 0;
                for (; j < addcon; j++)
                {
                    saltbase[i] += mkbase[mkbc];
                    mkbc++;
                }
                //Convert to readable string
                if (saltbase[i] <= 32)
                {
                    saltbase[i] += 32;
                }
                while (saltbase[i] >= 127)
                {
                    saltbase[i] -= 32;
                }
            }
            var saltchar = new char[8];
            for (var i = 0; i < 8; i++)
            {
                saltchar[i] = Convert.ToChar(saltbase[i]);
            }
            var salt = new string(saltchar);
            var salthead = salt[..4];
            var salttail = salt[4..];
            var SaltedString = salthead + InputString + salttail;
            var GPHESValue = getSHA256(SaltedString);
            return GPHESValue;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}