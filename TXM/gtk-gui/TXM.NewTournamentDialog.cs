
// This file has been generated by the GUI designer. Do not modify.
namespace TXM
{
	public partial class NewTournamentDialog
	{
		private global::Gtk.Table table1;
		
		private global::Gtk.Entry entryTName;
		
		private global::Gtk.Label labelCut;
		
		private global::Gtk.Label labelPoints;
		
		private global::Gtk.Label labelTName;
		
		private global::Gtk.SpinButton spinbuttonPoints;
		
		private global::Gtk.Table table3;
		
		private global::Gtk.RadioButton radiobuttonNoCut;
		
		private global::Gtk.RadioButton radiobuttonTop16;
		
		private global::Gtk.RadioButton radiobuttonTop32;
		
		private global::Gtk.RadioButton radiobuttonTop4;
		
		private global::Gtk.RadioButton radiobuttonTop64;
		
		private global::Gtk.RadioButton radiobuttonTop8;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget TXM.NewTournamentDialog
			this.Name = "TXM.NewTournamentDialog";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child TXM.NewTournamentDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(3)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entryTName = new global::Gtk.Entry ();
			this.entryTName.CanFocus = true;
			this.entryTName.Name = "entryTName";
			this.entryTName.IsEditable = true;
			this.entryTName.InvisibleChar = '●';
			this.table1.Add (this.entryTName);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryTName]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCut = new global::Gtk.Label ();
			this.labelCut.Name = "labelCut";
			this.labelCut.LabelProp = global::Mono.Unix.Catalog.GetString ("2. Cut?");
			this.table1.Add (this.labelCut);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelCut]));
			w3.TopAttach = ((uint)(1));
			w3.BottomAttach = ((uint)(2));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelPoints = new global::Gtk.Label ();
			this.labelPoints.Name = "labelPoints";
			this.labelPoints.LabelProp = global::Mono.Unix.Catalog.GetString ("3. Max Squadpoints:");
			this.table1.Add (this.labelPoints);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelPoints]));
			w4.TopAttach = ((uint)(2));
			w4.BottomAttach = ((uint)(3));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelTName = new global::Gtk.Label ();
			this.labelTName.Name = "labelTName";
			this.labelTName.LabelProp = global::Mono.Unix.Catalog.GetString ("1. Tournament name:");
			this.table1.Add (this.labelTName);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.labelTName]));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbuttonPoints = new global::Gtk.SpinButton (0, 100, 1);
			this.spinbuttonPoints.CanFocus = true;
			this.spinbuttonPoints.Name = "spinbuttonPoints";
			this.spinbuttonPoints.Adjustment.PageIncrement = 10;
			this.spinbuttonPoints.ClimbRate = 1;
			this.spinbuttonPoints.Numeric = true;
			this.table1.Add (this.spinbuttonPoints);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.spinbuttonPoints]));
			w6.TopAttach = ((uint)(2));
			w6.BottomAttach = ((uint)(3));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.table3 = new global::Gtk.Table (((uint)(3)), ((uint)(2)), false);
			this.table3.Name = "table3";
			this.table3.RowSpacing = ((uint)(6));
			this.table3.ColumnSpacing = ((uint)(6));
			// Container child table3.Gtk.Table+TableChild
			this.radiobuttonNoCut = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("No Cut"));
			this.radiobuttonNoCut.CanFocus = true;
			this.radiobuttonNoCut.Name = "radiobuttonNoCut";
			this.radiobuttonNoCut.DrawIndicator = true;
			this.radiobuttonNoCut.UseUnderline = true;
			this.radiobuttonNoCut.Group = new global::GLib.SList (global::System.IntPtr.Zero);
			this.table3.Add (this.radiobuttonNoCut);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table3 [this.radiobuttonNoCut]));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.radiobuttonTop16 = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Top 16"));
			this.radiobuttonTop16.CanFocus = true;
			this.radiobuttonTop16.Name = "radiobuttonTop16";
			this.radiobuttonTop16.DrawIndicator = true;
			this.radiobuttonTop16.UseUnderline = true;
			this.radiobuttonTop16.Group = this.radiobuttonNoCut.Group;
			this.table3.Add (this.radiobuttonTop16);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table3 [this.radiobuttonTop16]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.radiobuttonTop32 = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Top 32"));
			this.radiobuttonTop32.CanFocus = true;
			this.radiobuttonTop32.Name = "radiobuttonTop32";
			this.radiobuttonTop32.DrawIndicator = true;
			this.radiobuttonTop32.UseUnderline = true;
			this.radiobuttonTop32.Group = this.radiobuttonNoCut.Group;
			this.table3.Add (this.radiobuttonTop32);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table3 [this.radiobuttonTop32]));
			w9.TopAttach = ((uint)(2));
			w9.BottomAttach = ((uint)(3));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.radiobuttonTop4 = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Top 4"));
			this.radiobuttonTop4.CanFocus = true;
			this.radiobuttonTop4.Name = "radiobuttonTop4";
			this.radiobuttonTop4.DrawIndicator = true;
			this.radiobuttonTop4.UseUnderline = true;
			this.radiobuttonTop4.Group = this.radiobuttonNoCut.Group;
			this.table3.Add (this.radiobuttonTop4);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table3 [this.radiobuttonTop4]));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.radiobuttonTop64 = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Top 64"));
			this.radiobuttonTop64.CanFocus = true;
			this.radiobuttonTop64.Name = "radiobuttonTop64";
			this.radiobuttonTop64.DrawIndicator = true;
			this.radiobuttonTop64.UseUnderline = true;
			this.radiobuttonTop64.Group = this.radiobuttonNoCut.Group;
			this.table3.Add (this.radiobuttonTop64);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table3 [this.radiobuttonTop64]));
			w11.TopAttach = ((uint)(2));
			w11.BottomAttach = ((uint)(3));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.radiobuttonTop8 = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Top 8"));
			this.radiobuttonTop8.CanFocus = true;
			this.radiobuttonTop8.Name = "radiobuttonTop8";
			this.radiobuttonTop8.DrawIndicator = true;
			this.radiobuttonTop8.UseUnderline = true;
			this.radiobuttonTop8.Group = this.radiobuttonNoCut.Group;
			this.table3.Add (this.radiobuttonTop8);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table3 [this.radiobuttonTop8]));
			w12.TopAttach = ((uint)(1));
			w12.BottomAttach = ((uint)(2));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			this.table1.Add (this.table3);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1 [this.table3]));
			w13.TopAttach = ((uint)(1));
			w13.BottomAttach = ((uint)(2));
			w13.LeftAttach = ((uint)(1));
			w13.RightAttach = ((uint)(2));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			w1.Add (this.table1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(w1 [this.table1]));
			w14.Position = 0;
			w14.Expand = false;
			w14.Fill = false;
			// Internal child TXM.NewTournamentDialog.ActionArea
			global::Gtk.HButtonBox w15 = this.ActionArea;
			w15.Name = "dialog1_ActionArea";
			w15.Spacing = 10;
			w15.BorderWidth = ((uint)(5));
			w15.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w16 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w15 [this.buttonCancel]));
			w16.Expand = false;
			w16.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w17 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w15 [this.buttonOk]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 305;
			this.DefaultHeight = 197;
			this.Show ();
			this.buttonCancel.Clicked += new global::System.EventHandler (this.Cancel_Click);
			this.buttonOk.Clicked += new global::System.EventHandler (this.OK_Click);
		}
	}
}
