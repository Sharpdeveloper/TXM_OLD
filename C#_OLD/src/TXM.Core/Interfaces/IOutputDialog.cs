namespace TXM.Core
{
    public interface IOutputDialog
    {
        bool GetDialogResult();
        void ShowDialog();
        bool IsTableOutput();
        bool IsPairingOutput();
        bool IsResultOutput();
    }
}
