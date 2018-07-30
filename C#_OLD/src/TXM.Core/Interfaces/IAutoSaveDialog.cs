using System.Collections.Generic;

namespace TXM.Core
{
    public interface IAutoSaveDialog
    {
        void Init(List<AutosaveFile> files);
        void ShowDialog();
        bool GetDialogReturn();
        string GetFileName();
    }
}
