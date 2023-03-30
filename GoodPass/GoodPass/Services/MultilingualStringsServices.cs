using GoodPass.Strings;

namespace GoodPass.Services;

public class MultilingualStringsServices
{
    private readonly UIStrings UIStrings_zh_CN;

    private readonly UIStrings UIStrings_en_US;

    public MultilingualStringsServices()
    {
        UIStrings_zh_CN = new UIStrings("账号",
                                        "账号名已复制！",
                                        "密码",
                                        "密码已复制！",
                                        "平台网址",
                                        "最后修改时间",
                                        "编辑数据",
                                        "删除数据",
                                        "复制账号名",
                                        "复制密码",
                                        "显示密码",
                                        "点击访问平台网址",
                                        "请阅读并同意《用户协议》和《隐私政策》",
                                        "警告",
                                        "出错了",
                                        "提示",
                                        "您需要同意《用户协议》和《隐私政策》后方可使用本软件！",
                                        "已启用Microsoft Passport",
                                        "未启用Microsoft Passport",
                                        "已启用AES加密数据",
                                        "未启用AES加密数据",
                                        "主密码正确",
                                        "主密码错误");

        UIStrings_en_US = new UIStrings("Account",
                                        "AccountName has copied!",
                                        "Password",
                                        "Password has copied!",
                                        "Platform Url",
                                        "Last modified",
                                        "Edit Data",
                                        "Delete Data",
                                        "Copy AccountName",
                                        "Copy Password",
                                        "Reveal Password",
                                        "Click to visit the platform URL",
                                        "Please read and agree to the \"User Agreement\" and \"Privacy Policy\"",
                                        "Warning",
                                        "Error",
                                        "Info",
                                        "You need to agree to the User Agreement and Privacy Policy before you can use the software!",
                                        "Microsoft Passport is enabled",
                                        "Microsoft Passport is not enabled",
                                        "AES encryption is enabled",
                                        "AES encryption is not enabled",
                                        "The master password is correct",
                                        "The master password is incorrect");
    }

    public UIStrings Getzh_CN() => UIStrings_zh_CN;

    public UIStrings Geten_US() => UIStrings_en_US;
}