namespace TXM.Core
{
    public interface ITimerWindow
    {
        void Show();
        void SetTimer(TournamentTimer t);
        bool SetImage(System.Uri path);
        void SetLabelColor(bool white);
        void SetTextSize(double size);
        void Quit();
    }
}
