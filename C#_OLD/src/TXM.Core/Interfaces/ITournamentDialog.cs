namespace TXM.Core
{
    public interface ITournamentDialog
    {
        Tournament GetTournament();
        bool GetDialogResult();
        bool IsChanged();
        void ShowDialog();
        void SetIO(IO io);
        void SetTournament(Tournament tournament);
        void SetGameSystemIsChangeable(bool isGametypeChangeable);
    }
}
