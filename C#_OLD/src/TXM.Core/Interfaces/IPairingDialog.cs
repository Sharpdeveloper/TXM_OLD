using System.Collections.Generic;

namespace TXM.Core
{
    public interface IPairingDialog
    {
        void SetPairings(List<Pairing> pairings);
        void SetParticipants(List<Player> participants);
        void ShowDialog();
        List<Pairing> GetPairings();
        bool GetDialogResult();
    }
}
