namespace GoodPass.Services;

/// <summary>
/// GoodPass密码生成服务
/// </summary>
public static class GoodPassPWGService
{
    /// <summary>
    /// 生成不含特殊字符的随机密码
    /// </summary>
    /// <param name="length">生成密码长度</param>
    public static string RandomPasswordNormal(int length)
    {
        var random = new Random();
        var password = "";
        for (var i = 0; i < length; i++)
        {
            var temp = random.Next(0, 3);
            switch (temp)
            {
                case 0:
                    password += (char)random.Next(48, 58);
                    break;
                case 1:
                    password += (char)random.Next(65, 91);
                    break;
                case 2:
                    password += (char)random.Next(97, 123);
                    break;
            }
        }
        return password;
    }

    /// <summary>
    /// 生成含特殊字符的随机密码
    /// </summary>
    public static string RandomPasswordSpec(int length)
    {
        var random = new Random();
        var password = "";
        for (var i = 0; i < length; i++)
        {
            var temp = random.Next(0, 4);
            switch (temp)
            {
                case 0:
                    password += (char)random.Next(48, 58);
                    break;
                case 1:
                    password += (char)random.Next(65, 91);
                    break;
                case 2:
                    password += (char)random.Next(97, 123);
                    break;
                case 3:
                    password += (char)random.Next(33, 48);
                    break;
            }
        }
        return password;
    }

    /// <summary>
    /// 生成GoodPass风格密码
    /// </summary>
    public static string GPstylePassword(string platformName, string accountName)
    {
        var random = new Random();
        //对平台名进行大小写处理
        var PNLength = platformName.Length;
        int temp; char upcaseTemp;
        var platn = platformName;
        if (PNLength <= 5)
        {
            temp = random.Next(0, PNLength);
            //将platn上temp位置的字母变为大写
            upcaseTemp = platn[temp];
            if ((int)upcaseTemp >= 97)
            {
                upcaseTemp = (char)(upcaseTemp - 32);
            }
            platn = platn.Remove(temp, 1);
            platn = platn.Insert(temp, upcaseTemp.ToString());
        }
        else
        {
            for (var i = 0; i < 2; i++)
            {
                temp = random.Next(0, PNLength);
                //将platn上temp位置的字母变为大写
                upcaseTemp = platn[temp];
                if ((int)upcaseTemp >= 97)
                {
                    upcaseTemp = (char)(upcaseTemp - 32);
                }
                platn = platn.Remove(temp, 1);
                platn = platn.Insert(temp, upcaseTemp.ToString());
            }
        }
        //处理账号名
        var accn = "";
        if (accountName.StartsWith("@"))
        {
            accn = accountName[..4];
        }
        else
        {
            accn = "@";
            accn += accountName[..3];
        }
        //处理时间戳补强串
        var time = DateTime.Now;
        var timePatch1 = (char)(48 + time.Month + time.Minute);
        var timePatch2 = (char)(48 + time.Hour + time.Day);
        var timePatch = timePatch1.ToString() + timePatch2.ToString();
        //整合
        var gpPassword = platn + accn + timePatch;
        return gpPassword;
    }
}
