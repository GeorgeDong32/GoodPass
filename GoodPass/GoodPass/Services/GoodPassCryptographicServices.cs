namespace GoodPass.Services;

/// <summary>
/// 提供GPSES服务，用于对数据进行加解密
/// </summary>
public class GoodPassCryptographicServices
{
    /// <summary>
    /// 加密基数组
    /// </summary>
    private int[] CryptBase
    {
        get; set;
    }

    /// <summary>
    /// 数字字符index数组
    /// </summary>
    private int[] NumPos
    {
        get; set;
    }

    /// <summary>
    /// 特殊字符index数组
    /// </summary>
    private int[] SpecPos
    {
        get; set;
    }

    /// <summary>
    /// GoodPassCryptographicServices构造函数
    /// </summary>
    public GoodPassCryptographicServices()
    {
        if (App.MKBase == null)
            throw new ArgumentNullException("GoodPassCryptographicServices(): App.MKBase is null");
        CryptBase = App.MKBase;
        NumPos = new int[41];
        SpecPos = new int[41];
        NumPos[0] = 0; SpecPos[0] = 0;
    }

    /// <summary>
    /// (测试用)GoodPassCryptographicServices构造函数
    /// </summary>
    /// <param name="cryptBase">加密基</param>
    public GoodPassCryptographicServices(int[] cryptBase)
    {
        CryptBase = cryptBase;
        NumPos = new int[41];
        SpecPos = new int[41];
        NumPos[0] = 0; SpecPos[0] = 0;
    }

    /// <summary>
    /// 解密输入的字符串
    /// </summary>
    /// <param name="input">待解密的字符串</param>
    /// <returns>解密后字符串</returns>
    /// <exception cref="ArgumentNullException">空输入异常</exception>
    public string DecryptStr(string input)
    {
        if (App.MKBase == null)
            throw new ArgumentNullException("DecryptStr: App.MKBase is null");
        return DecryptStr(input, App.MKBase);
    }

    /// <summary>
    /// (基础的)解密输入的字符串
    /// </summary>
    /// <param name="input">待解密的字符串</param>
    /// <param name="cryptBase">加密基</param>
    /// <returns>解密结果</returns>
    /// <exception cref="ArgumentNullException">输入字符串为空</exception>
    public string DecryptStr(string input, int[] cryptBase)
    {
        if (input == null || input == string.Empty)
        {
            throw new ArgumentNullException("DecryptStr: input is null or empty");
        }
        CryptBase = cryptBase;
        var decStr = "";
        var baseStr = "";
        //清理数组
        Array.Fill(NumPos, -1);
        Array.Fill(SpecPos, -1);
        //找数字位置
        NumPos[0] = (int)input[0] - 'A';
        for (var i = 1; i <= NumPos[0]; i++)
        {
            switch (i % 2)
            {
                case 0:
                    NumPos[i] = (int)input[i] - 97;
                    break;
                case 1:
                    NumPos[i] = (int)input[i] - 65;
                    break;
            }
        }
        //找特殊字符位置
        var retemp = input.ToCharArray();
        Array.Reverse(retemp);
        var reinput = new string(retemp);
        SpecPos[0] = (int)reinput[0] - 'A';
        for (var i = 1; i <= SpecPos[0]; i++)
        {
            SpecPos[i] = (int)reinput[i] - 65;
        }
        var baseLength = input.Length - NumPos[0] - SpecPos[0] - 2;
        baseStr = input.Substring(NumPos[0] + 1, baseLength);

        for (var i = 0; i < baseLength; i++)
        {
            if (Array.FindIndex(NumPos, 1, p => p == i) != -1)//判断是否为数字
            {
                switch (i % 2)
                {
                    case 0:
                        decStr += (char)(baseStr[i] - 'e' - CryptBase[i] + '0');
                        break;
                    case 1:
                        decStr += (char)(baseStr[i] - 'O' - CryptBase[i] + '0');
                        break;
                }
            }
            else if (Array.FindIndex(SpecPos, 1, p => p == i) != -1)
            {
                decStr += (char)(baseStr[i] - CryptBase[i]);
            }
            else
            {
                var temp = baseStr[i] - CryptBase[i];
                if (temp >= 65 && temp <= 90)
                {
                    decStr += (char)(temp + 32);
                }
                else
                {
                    decStr += (char)(temp - 32);
                }
            }
        }
        return decStr;
    }

    /// <summary>
    /// 加密输入的字符串
    /// </summary>
    /// <param name="input">待加密字符串</param>
    /// <returns>加密后字符串</returns>
    public string EncryptStr(string input)//Todo：测试char-int是否按要求转换
    {
        if (App.MKBase == null)
            throw new ArgumentNullException("EncryptStr: App.MKBase is null");
        return EncryptStr(input, App.MKBase);
    }

    /// <summary>
    /// (基础的)加密输入字符串
    /// </summary>
    /// <param name="input">待加密的字符串</param>
    /// <param name="cryptBase">加密基</param>
    /// <returns>加密结果</returns>
    public string EncryptStr(string input, int[] cryptBase)//Todo：测试char-int是否按要求转换
    {
        CryptBase = cryptBase;
        if (input == null || input == string.Empty)
        {
            throw new ArgumentNullException("EncryptStr: input is null or empty");
        }
        //清理NumPos和SpecPos
        Array.Clear(NumPos);
        Array.Clear(SpecPos);
        //找数字位置
        var Strlength = input.Length;
        var npCount = 1;
        var specCount = 1;
        var output = "";
        for (var i = 0; i < Strlength; i++)
        {
            if ((int)input[i] >= 48 && (int)input[i] <= 57)
            {
                NumPos[npCount] = i;
                npCount++;
                NumPos[0]++;
            }
        }
        //全串加密
        for (var i = 0; i < Strlength; i++)
        {
            var temp = (int)input[i];
            //数字加密
            if (temp >= 48 && temp <= 57)
            {
                if (i % 2 == 0)
                {
                    temp = 'e' + temp - '0' + CryptBase[i];
                }
                else
                {
                    temp = 'O' + temp - '0' + CryptBase[i];
                }

                output += (char)temp;
            }
            //大写字母加密
            else if (temp >= 65 && temp <= 90)
            {
                temp += 32;
                output += (char)(temp + CryptBase[i]);
            }
            //小写字母加密
            else if (temp >= 97 && temp <= 122)
            {
                temp -= 32;
                output += (char)(temp + CryptBase[i]);
            }
            //特殊字符
            else
            {
                output += (char)(temp + CryptBase[i]);
                SpecPos[specCount] = i;
                specCount++;
                SpecPos[0]++;
            }
        }
        //生成指示串头
        var head = "";
        head += (char)(NumPos[0] + 'A');
        for (var i = 1; i <= NumPos[0]; i++)
        {
            switch (i % 2)
            {
                case 0:
                    head += (char)(NumPos[i] + 97);
                    break;
                case 1:
                    head += (char)(NumPos[i] + 65);
                    break;
            }
        }
        //生成指示串尾
        var tail = "";
        for (var i = 1; i <= SpecPos[0]; i++)
        {
            tail += (char)(SpecPos[i] + 65);
        }
        tail += (char)(SpecPos[0] + 65);
        output = head + output + tail;
        return output;
    }
}