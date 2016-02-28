using System;

using TXM.Core;

namespace TXM
{
    public partial class PlayerDialog : Gtk.Dialog, IPlayerDialog
    {
        private string nickname = "";
        private string lastname = "";
        private string forename = "";
        private int tablenr = 0;
        private string team = "";
        private string city = "";
        private bool disqualified = false;
        private bool present = false;
        private string faction = "";
        private bool wonbye = false;
        private string t3id = "";
        private bool paid = false;
        private bool squadlistgiven = false;

        public bool OK { get; set; }

        public string LastName { get { return lastname; } set { entryLastName.Text = value; } }

        public string ForeName { get { return forename; } set { entryForeName.Text = value; } }

        public string NickName { get { return nickname; } set { entryNickName.Text = value; } }

        public int TableNr { get { return tablenr; } set { spinbuttonTableNr.Value = value; } }

        public string Team { get { return team; } set { entryTeam.Text = value; } }

        public string City { get { return city; } set { entryCity.Text = value; } }

        public bool Disqualified { get { return disqualified; } set { checkbuttonDisqualified.Active = value; } }

        public bool Present { get { return present; } set { checkbuttonPresent.Active = value; } }

        public string Faction
        {
            get{ return faction; }
            set
            {
                for (int i = 0; i < comboboxFaction.Children.Length; i++)
                {
                    comboboxFaction.Active = i;
                    if (comboboxFaction.ActiveText == value)
                        break;
                }
            }
        }

        public bool WonBye { get { return wonbye; } set { checkbuttonWonBye.Active = value; } }

        public string T3ID { get { return t3id; } set { entryT3ID.Text = value; } }

        public bool Paid { get { return paid; } set { checkbuttonPaid.Active = value; } }

        public bool SquadlistGiven { get { return squadlistgiven; } set { checkbuttonSquadListGiven.Active = value; } }

        public bool NewPlayer { get; set; }

        public Language DisplayedLanguage { get; set; }

        public PlayerDialog()
        {
            this.Build();
        }

        public void Delete()
        {
            this.Destroy();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            OK = false;
            this.Destroy();
        }

        protected void OK_Click(object sender, EventArgs e)
        {
            OK = true;
            nickname = entryNickName.Text;
            lastname = entryLastName.Text;
            forename = entryForeName.Text;
            tablenr = (int)spinbuttonTableNr.Value;
            team = entryTeam.Text;
            city = entryCity.Text;
            present = checkbuttonPresent.Active;
            disqualified = checkbuttonDisqualified.Active;
            faction = comboboxFaction.ActiveText;
            wonbye = checkbuttonWonBye.Active;
            t3id = entryT3ID.Text;
            paid = checkbuttonPaid.Active;
            squadlistgiven = checkbuttonSquadListGiven.Active;
            this.Destroy();
        }

        public void ShowDialog()
        {
            SetLanguage();
            this.comboboxFaction.Active = 0;
            this.Run();
        }

        public void AddFaction(string faction)
        {
            comboboxFaction.AppendText(faction);
        }

        private void SetLanguage()
        {
            if (NewPlayer)
                Title = DisplayedLanguage.GetTranslation(StaticLanguage.NewPlayer);
            else
                Title = DisplayedLanguage.GetTranslation(StaticLanguage.EditPlayer);
            labelForeName.Text = "* " + DisplayedLanguage.GetTranslation(StaticLanguage.Forename);
            labelNickname.Text = "* " + DisplayedLanguage.GetTranslation(StaticLanguage.Nickname);
            labelLastName.Text = "* " + DisplayedLanguage.GetTranslation(StaticLanguage.Lastname);
            labelCity.Text = DisplayedLanguage.GetTranslation(StaticLanguage.City);
            labelFaction.Text = DisplayedLanguage.GetTranslation(StaticLanguage.Faction);
            labelInfoName.Text = "* " + DisplayedLanguage.GetTranslation(StaticLanguage.InfoName1) + "\n" + DisplayedLanguage.GetTranslation(StaticLanguage.InfoName2);
            labelInfoTableNr.Text = "* " + DisplayedLanguage.GetTranslation(StaticLanguage.InfoTNo1) + "\n" + DisplayedLanguage.GetTranslation(StaticLanguage.InfoTNo2);
            labelT3ID.Text = DisplayedLanguage.GetTranslation(StaticLanguage.T3ID);
            labelTabAdvanced.Text = DisplayedLanguage.GetTranslation(StaticLanguage.Advanced);
            labelTabBasic.Text = DisplayedLanguage.GetTranslation(StaticLanguage.Basic);
            labelTableNr.Text = "* " + DisplayedLanguage.GetTranslation(StaticLanguage.TableNrShort);
            labelTeam.Text = DisplayedLanguage.GetTranslation(StaticLanguage.Team);
            checkbuttonDisqualified.Label = DisplayedLanguage.GetTranslation(StaticLanguage.Disqualified);
            checkbuttonPaid.Label = DisplayedLanguage.GetTranslation(StaticLanguage.Paid);
            checkbuttonPresent.Label = DisplayedLanguage.GetTranslation(StaticLanguage.IsPresent);
            checkbuttonSquadListGiven.Label = DisplayedLanguage.GetTranslation(StaticLanguage.SquadListGiven);
            checkbuttonWonBye.Label = DisplayedLanguage.GetTranslation(StaticLanguage.WonBye);
            buttonOk.Label = DisplayedLanguage.GetTranslation(StaticLanguage.OK);
            buttonCancel.Label = DisplayedLanguage.GetTranslation(StaticLanguage.Cancel);
        }
    }
}

