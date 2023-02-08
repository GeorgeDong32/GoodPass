namespace GoodPass.Strings;

/// <summary>
/// UI控件字符串类，为无法使用resw的控件提供多语言支持
/// </summary>
public class UIStrings
{
    //TODO: 添加控件字符串位置
    public readonly string ListDetailsDetailControl_AccountNameTitleText;

    public readonly string ListDetailsDetailControl_CopiedTipforAcconutNameCopyButtonTitle;

    public readonly string ListDetailsDetailControl_PasswordTitleText;

    public readonly string ListDetailsDetailControl_CopiedTipforPasswordCopyButtonTitle;

    public readonly string ListDetailsDetailControl_PlatformUrlTitleText;

    public readonly string ListDetailsDetailControl_LastmodifiedTitleText;

    public readonly string EditButtonTipText;

    public readonly string DeleteButtonTipText;

    public readonly string AccountNameCopyButtonTipText;

    public readonly string PasswordCopyButtonTipText;

    public readonly string PasswordRevealButtonTipText;

    public readonly string PlatformUrlButtonTipText;

    public readonly string OOBEAgreementsDialogTitle;

    public readonly string WarningDialogTitle;

    public readonly string ErrorDialogTitle;

    public readonly string InfoDialogTitle;

    public readonly string AgreementNotArgeeContent;

    /// <summary>
    /// UIStrings构造函数
    /// </summary>
    public UIStrings(string listDetailsDetailControl_AccountNameTitleText,
                     string listDetailsDetailControl_CopiedTipforAcconutNameCopyButtonTitle,
                     string listDetailsDetailControl_PasswordTitleText,
                     string listDetailsDetailControl_CopiedTipforPasswordCopyButtonTitle,
                     string listDetailsDetailControl_PlatformUrlTitleText,
                     string listDetailsDetailControl_LastmodifiedTitleText,
                     string editButtonTipText,
                     string deleteButtonTipText,
                     string accountNameCopyButtonTipText,
                     string passwordCopyButtonTipText,
                     string passwordRevealButtonTipText,
                     string platformUrlButtonTipText,
                     string oobeAgreementsDialogTitle,
                     string warningDialogTitle,
                     string errorDialogTitle,
                     string infoDialogTitle,
                     string agreementNotArgeeContent)
    {
        ListDetailsDetailControl_AccountNameTitleText = listDetailsDetailControl_AccountNameTitleText;
        ListDetailsDetailControl_CopiedTipforAcconutNameCopyButtonTitle = listDetailsDetailControl_CopiedTipforAcconutNameCopyButtonTitle;
        ListDetailsDetailControl_PasswordTitleText = listDetailsDetailControl_PasswordTitleText;
        ListDetailsDetailControl_CopiedTipforPasswordCopyButtonTitle = listDetailsDetailControl_CopiedTipforPasswordCopyButtonTitle;
        ListDetailsDetailControl_PlatformUrlTitleText = listDetailsDetailControl_PlatformUrlTitleText;
        ListDetailsDetailControl_LastmodifiedTitleText = listDetailsDetailControl_LastmodifiedTitleText;
        EditButtonTipText = editButtonTipText;
        DeleteButtonTipText = deleteButtonTipText;
        AccountNameCopyButtonTipText = accountNameCopyButtonTipText;
        PasswordCopyButtonTipText = passwordCopyButtonTipText;
        PasswordRevealButtonTipText = passwordRevealButtonTipText;
        PlatformUrlButtonTipText = platformUrlButtonTipText;
        OOBEAgreementsDialogTitle = oobeAgreementsDialogTitle;
        WarningDialogTitle = warningDialogTitle;
        ErrorDialogTitle = errorDialogTitle;
        InfoDialogTitle = infoDialogTitle;
        AgreementNotArgeeContent = agreementNotArgeeContent;
    }
}