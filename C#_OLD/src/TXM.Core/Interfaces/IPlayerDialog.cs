namespace TXM.Core
{
    public interface IPlayerDialog
    {
        Player GetPlayer();
        bool GetDialogResult();
        bool IsChanged();
        void ShowDialog();
        void SetButtonOKText(string text);
        void SetTitle(string text);
        void SetPlayer(Player player);
    }
}
