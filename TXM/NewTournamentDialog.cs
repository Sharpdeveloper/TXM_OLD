using System;

using TXM.Core;

namespace TXM
{
    public partial class NewTournamentDialog : Gtk.Dialog, INewTournamentDialog
    {
        public bool OK { get; set; }

        public string TournamentName { get; set; }

        public int MaxPoints { get; set; }

        public bool Cut { get; set; }

        public int CutTo { get; set; }

        public Language DisplayedLanguage { get; set; }

        private bool newT;

        public NewTournamentDialog(Tournament tournament = null)
        {
            this.Build();

            spinbuttonPoints.Value = 100;
            newT = true;

            if (tournament != null)
            {
                newT = false;
                spinbuttonPoints.Value = tournament.MaxSquadPoints;
                entryTName.Text = tournament.Name;
                Cut = tournament.Cut;
                CutTo = tournament.CutTo;
                if (CutTo == 4)
                    radiobuttonTop4.Active = true;
                else if (CutTo == 8)
                    radiobuttonTop8.Active = true;
                else if (CutTo == 16)
                    radiobuttonTop16.Active = true;
                else if (CutTo == 32)
                    radiobuttonTop32.Active = true;
                else if (CutTo == 64)
                    radiobuttonTop64.Active = true;
                else
                    radiobuttonNoCut.Active = true;
            }
        }


        protected void Cancel_Click(object sender, EventArgs e)
        {
            OK = false;
            this.Destroy();
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            OK = true;
            TournamentName = entryTName.Text;
            MaxPoints = (int)spinbuttonPoints.Value;
            Cut = !radiobuttonNoCut.Active;
            if (radiobuttonTop4.Active)
                CutTo = 4;
            else if (radiobuttonTop8.Active)
                CutTo = 8;
            else if (radiobuttonTop16.Active)
                CutTo = 16;
            else if (radiobuttonTop32.Active)
                CutTo = 32;
            else if (radiobuttonTop64.Active)
                CutTo = 64;
            else
                CutTo = 0;
            this.Destroy();
        }

        public void ShowDialog()
        {
            SetLanguage();
            this.Run();
        }

        private void SetLanguage()
        {
            if (newT)
                Title = DisplayedLanguage.GetTranslation(StaticLanguage.NewTournament);
            else
                Title = DisplayedLanguage.GetTranslation(StaticLanguage.TournamentSettings);
            radiobuttonNoCut.Label = DisplayedLanguage.GetTranslation(StaticLanguage.NoCut);
            radiobuttonTop4.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 4";
            radiobuttonTop8.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 8";
            radiobuttonTop16.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 16";
            radiobuttonTop32.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 32";
            radiobuttonTop64.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 64";
            labelTName.Text = "1. " + DisplayedLanguage.GetTranslation(StaticLanguage.TournamentName) + ":";
            labelCut.Text = "2. " + DisplayedLanguage.GetTranslation(StaticLanguage.Cut) + "?";
            labelPoints.Text = "3. " + DisplayedLanguage.GetTranslation(StaticLanguage.MaxSquadpoints) + ":";
            buttonOk.Label = DisplayedLanguage.GetTranslation(StaticLanguage.OK);
            buttonCancel.Label = DisplayedLanguage.GetTranslation(StaticLanguage.Cancel);
        }
    }
}

