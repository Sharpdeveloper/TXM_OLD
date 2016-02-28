using System;

using TXM.Core;

namespace TXM
{
    public partial class TournamentDialog : Gtk.Dialog, ITournamentDialog
    {
        public bool OK { get; set; }

        private string tournamentName = "";
        private int maxPoints = 100;
        private bool cut = false;
        private int cutTo = 0;

        public string TournamentName { get { return tournamentName; } set { entryTName.Text = value; } }

        public int MaxPoints { get { return maxPoints; } set { spinbuttonPoints.Value = value; } }

        public bool Cut { get { return cut; } set { radiobuttonNoCut.Active = value; } }

        public int CutTo
        {
            get
            {
                return cutTo;
            }
            set
            {
                if (value == 4)
                    radiobuttonTop4.Active = true;
                else if (value == 8)
                    radiobuttonTop8.Active = true;
                else if (value == 16)
                    radiobuttonTop16.Active = true;
                else if (value == 32)
                    radiobuttonTop32.Active = true;
                else if (value == 64)
                    radiobuttonTop64.Active = true;
                else
                    radiobuttonNoCut.Active = true;
            }
        }

        public Language DisplayedLanguage { get; set; }

        public bool NewTournament { get; set; }

        public TournamentDialog()
        {
            this.Build();
        }


        protected void Cancel_Click(object sender, EventArgs e)
        {
            OK = false;
            this.Destroy();
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            OK = true;
            tournamentName = entryTName.Text;
            maxPoints = (int)spinbuttonPoints.Value;
            cut = !radiobuttonNoCut.Active;
            if (radiobuttonTop4.Active)
                cutTo = 4;
            else if (radiobuttonTop8.Active)
                cutTo = 8;
            else if (radiobuttonTop16.Active)
                cutTo = 16;
            else if (radiobuttonTop32.Active)
                cutTo = 32;
            else if (radiobuttonTop64.Active)
                cutTo = 64;
            else
                cutTo = 0;
            this.Destroy();
        }

        public void ShowDialog()
        {
            SetLanguage();
            this.Run();
        }

        private void SetLanguage()
        {
            if (NewTournament)
                Title = DisplayedLanguage.GetTranslation(StaticLanguage.NewTournament);
            else
                Title = DisplayedLanguage.GetTranslation(StaticLanguage.TournamentSettings);
            radiobuttonNoCut.Label = DisplayedLanguage.GetTranslation(StaticLanguage.NoCut);
            radiobuttonTop4.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 4";
            radiobuttonTop8.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 8";
            radiobuttonTop16.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 16";
            radiobuttonTop32.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 32";
            radiobuttonTop64.Label = DisplayedLanguage.GetTranslation(StaticLanguage.TOP) + " 64";
            labelTName.Text = DisplayedLanguage.GetTranslation(StaticLanguage.TournamentName);
            labelCut.Text = DisplayedLanguage.GetTranslation(StaticLanguage.Cut) + "?";
            labelPoints.Text = DisplayedLanguage.GetTranslation(StaticLanguage.MaxSquadpoints);
            buttonOk.Label = DisplayedLanguage.GetTranslation(StaticLanguage.OK);
            buttonCancel.Label = DisplayedLanguage.GetTranslation(StaticLanguage.Cancel);
        }
    }
}

