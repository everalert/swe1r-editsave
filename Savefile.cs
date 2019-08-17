using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1R.Racer;
using SWE1R.Util;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace SWE1RSE
{
    partial class Editor
    {
        private Save.GameSave gamesave;
        private Save.ProfileSave profile;
        private Color _lCol = Color.FromArgb(0x60, 0x00, 0x00), _lColHov = Color.Red;

        private void ImportGameSave(string filename)
        {
            //ss_label.Text = "Loading...";

            gamesave.Import(filename);
            sections.Reset();
            string[] pName = new string[6] { "General", "Profile 1", "Profile 2", "Profile 3", "Profile 4", "Times" };
            Panel[] p = new Panel[pName.Length];
            for (int i = 0; i < pName.Length; i++)
                sections.AddSection(pName[i], out p[i]);

            SetupSection(p[0], gamesave.GeneralSettings);
            for (int i = 0; i < 4; i++)
                SetupSection(p[i + 1], gamesave.Profiles[i]);
            SetupSection(p[5], gamesave.Times);

            sections.ToggleSection(0, true);
            sections.ToggleSection(1, true);
            sections.ToggleSection(5, true);

            //ss_label.Text = "";
        }

        private void ExportGameSave(string filename)
        {
            gamesave.Export(filename);
        }

        private void ImportProfileSave(string filename)
        {
            profile = new Save.ProfileSave(filename);
            sections.Reset();
            Panel p;
            sections.AddSection("Profile", out p);

            SetupSection(p, profile);
            sections.ToggleAllSections(true);
        }

        private void ExportProfileSave(string filename)
        {
            profile.Export(filename);
        }

        private void SetupSection(Panel p, Save.GeneralSettings gs)
        {
            //sfx vol, music vol, pods, tracks
            int pad = 8, padMid = 4, h = 20, wT = 96, wI = 160, wI2 = 32, hIoff = -2;
            string[] cols = new string[2] { "SFX Volume", "Music Volume" };
            Label l1;
            for (int i = 0; i < cols.Length; i++)
            {
                l1 = new Label();
                l1.Text = cols[i];
                l1.Location = new Point(pad, pad + (h + padMid) * i);
                l1.Size = new Size(wT, h);
                l1.Padding = new Padding(0);
                l1.Margin = new Padding(0);
                l1.AutoSize = true;
                p.Controls.Add(l1);
            }

            Label lSfx = new Label();
            lSfx = new Label();
            lSfx.Text = Helper.FloatToByte(gs.VolSFX).ToString();
            lSfx.Location = new Point(pad + wT + wI + padMid, pad + (h + padMid) * 0);
            lSfx.Size = new Size(wI2, h);
            lSfx.Padding = new Padding(0);
            lSfx.Margin = new Padding(0);
            lSfx.AutoSize = true;
            p.Controls.Add(lSfx);
            TrackBar tbSfx = new TrackBar();
            tbSfx.Maximum = byte.MaxValue;
            tbSfx.Minimum = byte.MinValue;
            tbSfx.Value = Helper.FloatToByte(gs.VolSFX);
            tbSfx.TickStyle = TickStyle.None;
            tbSfx.AutoSize = false;
            tbSfx.Location = new Point(pad + wT, pad + hIoff + (h + padMid) * 0);
            tbSfx.Size = new Size(wI, h);
            tbSfx.ValueChanged += (o, e) => UpdateVolSfx(o, lSfx, gs);
            p.Controls.Add(tbSfx);

            Label lMus = new Label();
            lMus = new Label();
            lMus.Text = Helper.FloatToByte(gs.VolMusic).ToString();
            lMus.Location = new Point(pad + wT + wI + padMid, pad + (h + padMid) * 1);
            lMus.Size = new Size(wI2, h);
            lMus.Padding = new Padding(0);
            lMus.Margin = new Padding(0);
            lMus.AutoSize = true;
            p.Controls.Add(lMus);
            TrackBar tbMus = new TrackBar();
            tbMus.Maximum = byte.MaxValue;
            tbMus.Minimum = byte.MinValue;
            tbMus.Value = Helper.FloatToByte(gs.VolMusic);
            tbMus.TickStyle = TickStyle.None;
            tbMus.AutoSize = false;
            tbMus.Location = new Point(pad + wT, pad + hIoff + (h + padMid) * 1);
            tbMus.Size = new Size(wI, h);
            tbMus.ValueChanged += (o, e) => UpdateVolMusic(o, lMus, gs);
            p.Controls.Add(tbMus);

            int clbW = (basepanel.Width - pad * 3 - 32) / 2, clbH = 110, clbBtnW = clbW/4;

            Label lVcl = new Label();
            lVcl = new Label();
            lVcl.Text = "Free Play Vehicles";
            lVcl.Location = new Point(pad + (clbW + pad) * 0, pad + (h + padMid) * 2);
            lVcl.Size = new Size(wI2, h);
            lVcl.Padding = new Padding(0);
            lVcl.Margin = new Padding(0);
            lVcl.AutoSize = true;
            p.Controls.Add(lVcl);
            CheckedListBox clbVcl = new CheckedListBox();
            clbVcl.IntegralHeight = false;
            clbVcl.Location = new Point(lVcl.Location.X, lVcl.Location.Y + lVcl.Size.Height + padMid);
            clbVcl.Size = new Size(clbW, clbH);
            clbVcl.CheckOnClick = true;
            Array vclFlagList = Enum.GetValues(typeof(Save.VehicleUnlockFlags));
            foreach (Save.VehicleUnlockFlags flag in vclFlagList)
                if (gs.AvailableVehicles.HasFlag(flag))
                    clbVcl.Items.Add(flag, true);
                else
                    clbVcl.Items.Add(flag, false);
            clbVcl.ItemCheck += (o, e) => UpdateFreePlayVehicles(o, gs);
            p.Controls.Add(clbVcl);
            Button btnVnone = new Button();
            btnVnone.Text = "None";
            btnVnone.Size = new Size(clbBtnW, h);
            btnVnone.Location = new Point(clbVcl.Location.X, clbVcl.Location.Y + clbVcl.Size.Height + padMid);
            btnVnone.Click += (o, e) => SelectAll(clbVcl, false);
            p.Controls.Add(btnVnone);
            Button btnVall = new Button();
            btnVall.Text = "All";
            btnVall.Size = new Size(clbBtnW, h);
            btnVall.Location = new Point(btnVnone.Location.X + btnVnone.Size.Width + padMid, clbVcl.Location.Y + clbVcl.Size.Height + padMid);
            btnVall.Click += (o, e) => SelectAll(clbVcl, true);
            p.Controls.Add(btnVall);

            Label lTrk = new Label();
            lTrk = new Label();
            lTrk.Text = "Free Play Tracks";
            lTrk.Location = new Point(pad + (clbW + pad) * 1, pad + (h + padMid) * 2);
            lTrk.Size = new Size(wI2, h);
            lTrk.Padding = new Padding(0);
            lTrk.Margin = new Padding(0);
            lTrk.AutoSize = true;
            p.Controls.Add(lTrk);
            CheckedListBox clbTrk = new CheckedListBox();
            clbTrk.IntegralHeight = false;
            clbTrk.Location = new Point(lTrk.Location.X, lTrk.Location.Y + lTrk.Size.Height + padMid);
            clbTrk.Size = new Size(clbW, clbH);
            clbTrk.CheckOnClick = true;
            Array trkFlagList = Enum.GetValues(typeof(Save.TrackUnlockFlags));
            foreach (Save.TrackUnlockFlags flag in trkFlagList)
                if (gs.AvailableTracks.HasFlag(flag) && !flag.ToString().EndsWith("Done"))
                    clbTrk.Items.Add(flag, true);
                else if (!flag.ToString().EndsWith("Done"))
                    clbTrk.Items.Add(flag, false);
            clbTrk.ItemCheck += (o, e) => UpdateFreePlayTracks(o, gs);
            p.Controls.Add(clbTrk);
            Button btnTnone = new Button();
            btnTnone.Text = "None";
            btnTnone.Size = new Size(clbBtnW, h);
            btnTnone.Location = new Point(clbTrk.Location.X, clbTrk.Location.Y + clbTrk.Size.Height + padMid);
            btnTnone.Click += (o, e) => SelectAll(clbTrk, false);
            p.Controls.Add(btnTnone);
            Button btnTall = new Button();
            btnTall.Text = "All";
            btnTall.Size = new Size(clbBtnW, h);
            btnTall.Location = new Point(btnTnone.Location.X + btnTnone.Size.Width + padMid, clbTrk.Location.Y + clbTrk.Size.Height + padMid);
            btnTall.Click += (o, e) => SelectAll(clbTrk, true);
            p.Controls.Add(btnTall);
        }

        private void SelectAll(CheckedListBox clb, bool selected)
        {
            for (int i = 0; i < clb.Items.Count; i++)
                clb.SetItemChecked(i, selected);
        }

        private void UpdateVolSfx(object o, Label l, Save.GeneralSettings gs)
        {
            TrackBar tb = (TrackBar)o;
            float vol = Helper.ByteToFloat((byte)tb.Value);
            l.Text = tb.Value.ToString();
            gs.VolSFX = vol;
        }

        private void UpdateVolMusic(object o, Label l, Save.GeneralSettings gs)
        {
            TrackBar tb = (TrackBar)o;
            float vol = Helper.ByteToFloat((byte)tb.Value);
            l.Text = tb.Value.ToString();
            gs.VolMusic = vol;
        }

        private void UpdateFreePlayVehicles(object o, Save.GeneralSettings gs)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                CheckedListBox v = (CheckedListBox)o;
                Save.VehicleUnlockFlags unlock = 0;
                foreach (Save.VehicleUnlockFlags vehicle in v.CheckedItems)
                    unlock |= vehicle;
                gs.AvailableVehicles = unlock;
            });
        }

        private void UpdateFreePlayTracks(object o, Save.GeneralSettings gs)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                CheckedListBox v = (CheckedListBox)o;
                Save.TrackUnlockFlags unlock = 0;
                foreach (Save.TrackUnlockFlags track in v.CheckedItems)
                    unlock |= track;
                gs.AvailableTracks = unlock;
            });
        }

        private void SetupSection(Panel p, Save.ProfileSave pfl)
        {
            while (p.Controls.Count > 0)
                p.Controls.RemoveAt(0);

            // MISCELLANEOUS

            int pad = 8, padMid = 4, h = 20, wT = 96, wI = 180, wI2 = 32, hIoff = -2, vIoff = -2, mgn = 2;
            string[] rows = new string[4] { "Name", "Truguts", "Pit Droids", "Selected Vehicle" };
            Label l1;
            for (int i = 0; i < rows.Length; i++)
            {
                l1 = new Label();
                l1.Text = rows[i];
                l1.Location = new Point(pad, pad + (h + padMid) * i);
                l1.Size = new Size(wT, h);
                l1.Padding = new Padding(mgn);
                l1.Margin = new Padding(0);
                l1.AutoSize = true;
                p.Controls.Add(l1);
            }

            TextBox tb1 = new TextBox();
            tb1 = new TextBox();
            tb1.MaxLength = Save.MaxNameLength;
            tb1.Text = pfl.Player;
            tb1.Location = new Point(pad + wT + hIoff, pad + (h + padMid) * 0 + vIoff);
            tb1.Size = new Size(wI, h);
            tb1.TextChanged += (o, e) => UpdateName(tb1.Text, pfl);
            p.Controls.Add(tb1);

            NumericUpDown numM = new NumericUpDown();
            numM.Maximum = int.MaxValue;
            numM.Minimum = int.MinValue;
            numM.Value = pfl.Truguts;
            numM.Increment = 1000;
            numM.Location = new Point(pad + wT + hIoff, pad + (h + padMid) * 1 + vIoff);
            numM.Size = new Size(wI/2, h);
            numM.ValueChanged += (o, e) => pfl.Truguts = (int)numM.Value;
            p.Controls.Add(numM);
            Button btnMmin = new Button();
            btnMmin.Text = "Min";
            btnMmin.Location = new Point(numM.Location.X + numM.Size.Width + padMid, numM.Location.Y);
            btnMmin.Size = new Size((wI - numM.Size.Width - padMid * 2) / 2, h);
            btnMmin.Click += (o, e) => numM.Value = numM.Minimum;
            p.Controls.Add(btnMmin);
            Button btnMmax = new Button();
            btnMmax.Text = "Max";
            btnMmax.Location = new Point(btnMmin.Location.X + btnMmin.Size.Width + padMid, btnMmin.Location.Y);
            btnMmax.Size = new Size((wI - numM.Size.Width - padMid * 2) / 2, h);
            btnMmax.Click += (o, e) => numM.Value = numM.Maximum;
            p.Controls.Add(btnMmax);

            NumericUpDown numP = new NumericUpDown();
            numP.Maximum = 7;
            numP.Minimum = 1;
            numP.Value = pfl.PitDroids;
            numP.Increment = 1;
            numP.Location = new Point(pad + wT + hIoff, pad + (h + padMid) * 2 + vIoff);
            numP.Size = new Size(wI/2, h);
            numP.ValueChanged += (o, e) => pfl.PitDroids = (byte)numP.Value;
            p.Controls.Add(numP);
            Button btnPmin = new Button();
            btnPmin.Text = "Min";
            btnPmin.Location = new Point(numP.Location.X + numP.Size.Width + padMid, numP.Location.Y);
            btnPmin.Size = new Size((wI - numP.Size.Width - padMid * 2) / 2, h);
            btnPmin.Click += (o, e) => numP.Value = numP.Minimum;
            p.Controls.Add(btnPmin);
            Button btnPmax = new Button();
            btnPmax.Text = "Max";
            btnPmax.Location = new Point(btnPmin.Location.X + btnPmin.Size.Width + padMid, btnPmin.Location.Y);
            btnPmax.Size = new Size((wI - numP.Size.Width - padMid * 2) / 2, h);
            btnPmax.Click += (o, e) => numP.Value = numP.Maximum;
            p.Controls.Add(btnPmax);

            ComboBox cbx = new ComboBox();
            cbx.Location = new Point(pad + wT + hIoff, pad + (h + padMid) * 3 + vIoff);
            cbx.Size = new Size(wI, h);
            cbx.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx.BindingContext = new BindingContext();
            cbx.DataSource = new BindingSource(Value.Vehicle.Name, null);
            cbx.DisplayMember = "VALUE";
            cbx.ValueMember = "KEY";
            cbx.SelectedValue = (byte)pfl.SelectedVehicle;
            cbx.SelectedIndexChanged += (o, e) => pfl.SelectedVehicle = (Save.Vehicle)cbx.SelectedValue;
            p.Controls.Add(cbx);


            int clbVclW = (basepanel.Width - pad * 3 - 32) / 5 * 2, pTrkW = (basepanel.Width - pad * 3 - 32) - clbVclW, clbH = 210, clbBtnW = 48, pBtnW = 48, vclY = cbx.Location.Y + cbx.Size.Height + pad * 2, trkY = vclY;

            // VEHICLES

            Label lVcl = new Label();
            lVcl = new Label();
            lVcl.Text = "Vehicles";
            lVcl.Location = new Point(pad, vclY);
            lVcl.Size = new Size(wI2, h);
            lVcl.Padding = new Padding(0);
            lVcl.Margin = new Padding(0);
            lVcl.AutoSize = true;
            p.Controls.Add(lVcl);
            CheckedListBox clbVcl = new CheckedListBox();
            clbVcl.IntegralHeight = false;
            clbVcl.Location = new Point(lVcl.Location.X, lVcl.Location.Y + lVcl.Size.Height + padMid);
            clbVcl.Size = new Size(clbVclW, clbH);
            clbVcl.CheckOnClick = true;
            Array vclFlagList = Enum.GetValues(typeof(Save.VehicleUnlockFlags));
            foreach (Save.VehicleUnlockFlags flag in vclFlagList)
                if (pfl.AvailableVehicles.HasFlag(flag))
                    clbVcl.Items.Add(flag, true);
                else
                    clbVcl.Items.Add(flag, false);
            clbVcl.ItemCheck += (o, e) => UpdateVehicles(o, pfl);
            p.Controls.Add(clbVcl);
            Button btnVnone = new Button();
            btnVnone.Text = "None";
            btnVnone.Size = new Size(clbBtnW, h);
            btnVnone.Location = new Point(clbVcl.Location.X, clbVcl.Location.Y + clbVcl.Size.Height + padMid);
            btnVnone.Click += (o, e) => SelectAll(clbVcl, false);
            p.Controls.Add(btnVnone);
            Button btnVall = new Button();
            btnVall.Text = "All";
            btnVall.Size = new Size(clbBtnW, h);
            btnVall.Location = new Point(btnVnone.Location.X + btnVnone.Size.Width + padMid, clbVcl.Location.Y + clbVcl.Size.Height + padMid);
            btnVall.Click += (o, e) => SelectAll(clbVcl, true);
            p.Controls.Add(btnVall);

            // TRACKS

            Label lTrk = new Label();
            lTrk = new Label();
            lTrk.Text = "Tracks";
            lTrk.Location = new Point(lVcl.Location.X + clbVclW + pad, trkY);
            lTrk.Size = new Size(wI2, h);
            lTrk.Padding = new Padding(0);
            lTrk.Margin = new Padding(0);
            lTrk.AutoSize = true;
            p.Controls.Add(lTrk);
            Panel pTrk = new Panel();
            pTrk.Location = new Point(lTrk.Location.X, lTrk.Location.Y + lTrk.Size.Height + padMid);
            pTrk.Size = new Size(pTrkW, clbH);
            pTrk.AutoScroll = true;
            pTrk.BorderStyle = BorderStyle.FixedSingle;
            pTrk.BackColor = Color.White;
            p.Controls.Add(pTrk);
            Array trkFlagList = Enum.GetValues(typeof(Save.TrackUnlockFlags));
            CheckBox[] cbTrk = new CheckBox[trkFlagList.Length];
            ComboBox[] cbxTrk = new ComboBox[trkFlagList.Length];
            int trkPlW = 64;
            for (int i = 0; i < trkFlagList.Length; i++)
            {
                //eventually expand events to reconcile available/placing combinations to be realistic? or just keep it totally free control? option for both?

                Enum flag = (Enum)trkFlagList.GetValue(i);
                Save.TrackUnlockFlags flagReal = (Save.TrackUnlockFlags)trkFlagList.GetValue(i);
                bool isRace = !flag.ToString().EndsWith("Done");

                cbTrk[i] = new CheckBox();
                CheckBox check = cbTrk[i], checkPrev = cbTrk[Math.Max(0, i - 1)];
                check.Text = trkFlagList.GetValue(i).ToString();
                check.Checked = pfl.AvailableTracks.HasFlag(flag);
                check.Enabled = isRace;
                check.AutoSize = true;
                check.Padding = new Padding(4);
                check.Margin = new Padding(0);
                check.Location = new Point(0, (h + padMid) * i);
                if (!isRace)
                    checkPrev.CheckedChanged += (o, e) => check.Checked = checkPrev.Checked;
                else
                    check.CheckedChanged += (o, e) => UpdateTracks(pTrk, pfl);
                pTrk.Controls.Add(check);

                if (isRace)
                {
                    cbxTrk[i] = new ComboBox();
                    ComboBox combo = cbxTrk[i], comboPrev = cbxTrk[Math.Max(0, i - 1)];
                    combo.Location = new Point(pTrk.Size.Width - 20 - trkPlW, check.Location.Y + 1);
                    combo.Size = new Size(trkPlW, h);
                    combo.DropDownStyle = ComboBoxStyle.DropDownList;
                    combo.BindingContext = new BindingContext();
                    combo.DataSource = new BindingSource(Save.TrackPlacingText, null);
                    combo.DisplayMember = "VALUE";
                    combo.ValueMember = "KEY";
                    combo.SelectedValue = pfl.GetPlacement(flagReal);
                    combo.SelectedIndexChanged += (o, e) => pfl.SetPlacement(flagReal, (Save.TrackPlacing)combo.SelectedValue);
                    pTrk.Controls.Add(combo);
                }
            }
            List<Control> cbList = pTrk.Controls.OfType<CheckBox>().Where(cb => cb.Enabled).Cast<Control>().ToList();
            Button btnTnone = new Button();
            btnTnone.Text = "None";
            btnTnone.Size = new Size(pBtnW, h);
            btnTnone.Location = new Point(pTrk.Location.X, pTrk.Location.Y + pTrk.Size.Height + padMid);
            btnTnone.Click += (o, e) => { foreach (CheckBox cb in cbList) { cb.Checked = false; } };
            p.Controls.Add(btnTnone);
            Button btnTall = new Button();
            btnTall.Text = "All";
            btnTall.Size = new Size(pBtnW, h);
            btnTall.Location = new Point(btnTnone.Location.X + btnTnone.Size.Width + padMid, pTrk.Location.Y + pTrk.Size.Height + padMid);
            btnTall.Click += (o, e) => { foreach (CheckBox cb in cbList) { cb.Checked = true; } };
            p.Controls.Add(btnTall);


            int upBaseY = btnVnone.Location.Y + btnVnone.Size.Height + pad * 2;

            // UPGRADES

            string[] lUpColT = new string[2] { "Upgrade", "Health" };
            ComboBox[] cbxUp = new ComboBox[pfl.Upgrades.Length];
            TrackBar[] tbUpHp = new TrackBar[pfl.Upgrades.Length];
            Label[] lUp = new Label[pfl.Upgrades.Length], lUpHp = new Label[pfl.Upgrades.Length], lUpCol = new Label[lUpColT.Length];
            int wIup = (basepanel.Width - wT - wI2 - pad * 6) / 2;
            for (int i = 0; i < lUpColT.Length; i++)
            {
                int x = pad + wT + (wIup + padMid) * i;
                int y = upBaseY;
                lUpCol[i] = new Label();
                lUpCol[i].Text = lUpColT[i];
                lUpCol[i].Location = new Point(x, y);
                lUpCol[i].Size = new Size(wI, h);
                lUpCol[i].Padding = new Padding(mgn);
                lUpCol[i].Margin = new Padding(0);
                lUpCol[i].AutoSize = true;
                p.Controls.Add(lUpCol[i]);
            }
            for (int i = 0; i < pfl.Upgrades.Length; i++)
            {
                Save.ProfileSave.Upgrade u = pfl.Upgrades[i];
                int y = upBaseY + (h + padMid) * (i + 1);

                lUp[i] = new Label();
                Label thisLUp = lUp[i];
                lUp[i].Text = Value.Upgrade.Title[(Value.Upgrade.Id)i];
                lUp[i].Location = new Point(pad, y);
                lUp[i].Size = new Size(wT, h);
                lUp[i].Padding = new Padding(mgn);
                lUp[i].Margin = new Padding(0);
                lUp[i].AutoSize = true;
                p.Controls.Add(lUp[i]);

                cbxUp[i] = new ComboBox();
                ComboBox thisCbxUp = cbxUp[i];
                cbxUp[i].Location = new Point(lUp[i].Location.X + wT, y + vIoff);
                cbxUp[i].Size = new Size(wIup, h);
                cbxUp[i].DropDownStyle = ComboBoxStyle.DropDownList;
                cbxUp[i].BindingContext = new BindingContext();
                cbxUp[i].DataSource = new BindingSource(Value.Upgrade.Name[(Value.Upgrade.Id)i], null);
                cbxUp[i].DisplayMember = "VALUE";
                cbxUp[i].ValueMember = "KEY";
                cbxUp[i].SelectedValue = u.Level;
                cbxUp[i].SelectedIndexChanged += (o,e) => u.Level = (byte)thisCbxUp.SelectedValue;
                p.Controls.Add(cbxUp[i]);

                lUpHp[i] = new Label();
                Label thisLUpHp = lUpHp[i];
                tbUpHp[i] = new TrackBar();
                TrackBar thisTbUpHp = tbUpHp[i];
                tbUpHp[i].Location = new Point(cbxUp[i].Location.X + cbxUp[i].Size.Width, y);
                tbUpHp[i].Size = new Size(wIup, h);
                tbUpHp[i].Maximum = byte.MaxValue;
                tbUpHp[i].Minimum = byte.MinValue;
                tbUpHp[i].Value = u.Health;
                tbUpHp[i].TickStyle = TickStyle.None;
                tbUpHp[i].AutoSize = false;
                tbUpHp[i].ValueChanged += (o, e) => UpdateUpgradeHealth(o, thisLUpHp, u);
                p.Controls.Add(tbUpHp[i]);
                lUpHp[i].Location = new Point(tbUpHp[i].Location.X + tbUpHp[i].Size.Width, y);
                lUpHp[i].Size = new Size(wI2, h);
                lUpHp[i].Text = u.Health.ToString();
                lUpHp[i].Padding = new Padding(0);
                lUpHp[i].Margin = new Padding(0);
                lUpHp[i].AutoSize = true;
                p.Controls.Add(lUpHp[i]);
            }

            // IMPORT/EXPORT

            Button lImport = new Button();
            lImport.Text = "Import";
            lImport.Location = new Point(lUp.Last().Location.X, cbxUp.Last().Location.Y + cbxUp.Last().Size.Height + pad * 2);
            lImport.Size = new Size(64, h + mgn * 2);
            lImport.Padding = new Padding(0);
            lImport.Margin = new Padding(0);
            lImport.Click += (o, e) =>
            {
                if (dlg_inProfile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pfl.Import(dlg_inProfile.FileName);
                        SetupSection(p, pfl);
                        sections.Resize();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            };
            p.Controls.Add(lImport);

            Button lExport = new Button();
            lExport.Text = "Export";
            lExport.Location = new Point(lImport.Location.X + lImport.Size.Width + pad, lImport.Location.Y);
            lExport.Size = new Size(lImport.Size.Width, lImport.Size.Height);
            lExport.Padding = new Padding(0);
            lExport.Margin = new Padding(0);
            lExport.Click += (o, e) =>
            {
                try
                {
                    dlg_outProfile.FileName = string.Format(@"{0}.{1}", pfl.Player, Save.ProfileSave.Extension);
                    if (dlg_outProfile.ShowDialog() == DialogResult.OK)
                        pfl.Export(dlg_outProfile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            };
            p.Controls.Add(lExport);
        }

        private void UpdateName(string n, Save.ProfileSave pfl)
        {
            if (Helper.CheckFilenameFormat(n))
            {
                pfl.Player = n;
                ss_label.Text = "";
            }
            else
                ss_label.Text = "Name contains invalid characters";
        }

        private void UpdateTracks(Panel p, Save.ProfileSave pfl)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                List<Control> list = p.Controls.OfType<CheckBox>().ToList<Control>();
                Save.TrackUnlockFlags trk = 0;
                foreach (CheckBox cb in list)
                    trk |= (Save.TrackUnlockFlags)((int)Enum.Parse(typeof(Save.TrackUnlockFlags), cb.Text) * (cb.Checked ? 1 : 0));
                pfl.AvailableTracks = trk;
            });
        }

        private void UpdateVehicles(object o, Save.ProfileSave pfl)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                CheckedListBox v = (CheckedListBox)o;
                Save.VehicleUnlockFlags unlock = 0;
                foreach (Save.VehicleUnlockFlags vehicle in v.CheckedItems)
                    unlock |= vehicle;
                pfl.AvailableVehicles = unlock;
            });
        }

        private void UpdateUpgradeHealth(object o, Label l, Save.ProfileSave.Upgrade u)
        {
            TrackBar tb = (TrackBar)o;
            u.Health = (byte)tb.Value;
            l.Text = u.Health.ToString();
        }

        private void SetupSection(Panel p, Save.RaceTime[] t)
        {
            Label l1;
            int pad = 8, h = 20, wT = 165, wI = (basepanel.Width-pad*2-16-wT)/4, wIpad = 4;
            string[] cols = new string[4] { "3-Lap", "1-Lap", "3-Lap Mir", "1-Lap Mir" };
            for (int i = 0; i < cols.Length; i++)
            {
                l1 = new Label();
                l1.Text = cols[i];
                l1.Location = new Point(pad + wT + wI * i, pad);
                l1.Padding = new Padding(0);
                l1.Margin = new Padding(0);
                l1.AutoSize = true;
                p.Controls.Add(l1);
            }
            for (int i = 0; i < 25; i++)
            {
                Label l = new Label();
                l.Text = Value.Track.Name[(byte)i];
                l.Padding = new Padding(0);
                l.Margin = new Padding(0);
                l.AutoSize = true;
                l.Location = new Point(pad, pad + h + h * i);
                p.Controls.Add(l);
            }
            for (int i = 0; i < t.Length; i++)
            {
                Label l = new Label();
                l.Text = t[i].FullTimeString;
                l.ForeColor = _lCol;
                l.Padding = new Padding(0);
                l.Margin = new Padding(0);
                l.MouseEnter += (o, e) => { l.Cursor = Cursors.Hand; l.ForeColor = _lColHov; };
                l.MouseLeave += (o, e) => { l.Cursor = Cursors.Default; l.ForeColor = _lCol; };
                l.Click += UpdateTime;
                //l.MouseHover += (o, e) => ss_label.Text = "Edit time details...";
                //l.MouseLeave += (o, e) => ss_label.Text = "";
                l.Height = h;
                l.Width = wI - wIpad;
                //l.AutoSize = true;
                l.Location = new Point(pad + wT + wI * ((2 * (t[i].Mirror ? 1 : 0)) + (t[i].TimeType == Save.RaceTimeType.ThreeLap ? 0 : 1)), pad + h + h * (int)t[i].Track);
                p.Controls.Add(l);
                p.Controls.SetChildIndex(l, i); // allow backtracing time data
            }
        }

        private void UpdateTime(object o, EventArgs e)
        {
            Label l = (Label)o;
            int i = sections.GetSection("Times").Controls.GetChildIndex(l);
            //Save.RaceTime t = ;
            using (EditTimeBox dialog = new EditTimeBox(this, gamesave.ImmutableTimes[i]))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (Save.RaceTime.TimeIsValid(dialog.Time))
                        {
                            gamesave.Times[i].Time = dialog.Time;
                            l.Text = gamesave.Times[i].FullTimeString;
                            gamesave.Times[i].Name = dialog.Player;
                            gamesave.Times[i].Vehicle = (Save.Vehicle)dialog.Vehicle;
                        }
                        else
                        {
                            gamesave.Times[i].Time = Save.DefaultTime;
                            l.Text = gamesave.Times[i].FullTimeString;
                            gamesave.Times[i].Name = Save.DefaultName;
                            gamesave.Times[i].Vehicle = (Save.Vehicle)Value.Track.Favorite[(byte)gamesave.Times[i].Track];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
    }

    class EditTimeBox : Form
    {
        private Save.RaceTime _time;
        private int _bdrW = 16, _bdrH = 32;
        private int _p = 16, _pV = 2, _pH = 0, _lH = 24, _lW = 64, _tbH = 20, _tbW = 128, _btnH = 24, _btnW = 48, _xOff = -8;
        private string _title = "Edit Time";
        private Font _fTrack = new Font(DefaultFont.FontFamily, DefaultFont.Size, FontStyle.Bold);



        private Label _errorLabel;
        private Button _savebutton;

        public byte Vehicle;
        public string Player;
        public float Time;

        public EditTimeBox(Form p, Save.RaceTime t)
        {
            _time = t;
            Time = t.Time;
            Vehicle = Save.RaceTime.TimeIsValid(t.Time) ? (byte)t.Vehicle : Value.Track.Favorite[(byte)t.Track];
            Player = Save.RaceTime.TimeIsValid(t.Time) ? t.Name : "";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(_p * 2 + _lW + _tbW + _bdrW, (int)(_p * 2.5) + Math.Max(_lH, _tbH) + _lH * 4 + _btnH+_bdrH);
            this.Text = _title;
            this.Icon = p.Icon;

            string[] labels = new string[6] { t.TrackName, string.Format(@"{0}{1}", t.TimeType == Save.RaceTimeType.ThreeLap ? "3-Lap" : "1-Lap", t.Mirror ? ", Mirrored" : ""), "Time", "Vehicle", "Player", "" };
            for (int i = 0; i < labels.Length; i++)
            {
                Label l = new Label();
                if (i < 2)
                    l.Font = _fTrack;
                if (i == labels.Length - 1)
                {
                    l.ForeColor = Color.Red;
                    _errorLabel = l;
                    l.Location = new Point(_p, this.Height - (int)(_p * 1.75) - _lH - _bdrH / 2);
                }
                else
                    l.Location = new Point(_p, _p + _lH * i);
                l.Text = labels[i];
                l.Padding = new Padding(_pH, _pV, _pH, _pV);
                l.Margin = new Padding(0);
                l.AutoSize = true;
                this.Controls.Add(l);
            }


            Button ok = new Button();
            ok.Size = new Size(_btnW, _btnH);
            ok.Location = new Point(this.Width - ok.Width - _p - _bdrW, (int)(_p * 1.5) + Math.Max(_lH, _tbH) + _lH * 4);
            ok.DialogResult = DialogResult.OK;
            ok.Text = "Save";
            this.Controls.Add(ok);
            _savebutton = ok;

            TextBox tb1 = new TextBox();
            tb1.MaxLength = 13; // 00:00.0000000
            tb1.Text = Save.RaceTime.TimeIsValid(t.Time) ? t.FullTimeString : "";
            tb1.Location = new Point(_p + _lW + _xOff, _p + _lH * 2);
            tb1.Size = new Size(_tbW, _tbH);
            tb1.TextChanged += UpdateTime;
            tb1.KeyDown += EnterSave;
            this.Controls.Add(tb1);

            ComboBox cbx = new ComboBox();
            cbx.Location = new Point(_p + _lW + _xOff, _p + _lH * 3);
            cbx.Size = new Size(_tbW, _tbH);
            cbx.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx.BindingContext = new BindingContext();
            cbx.DataSource = new BindingSource(Value.Vehicle.Name, null);
            cbx.DisplayMember = "VALUE";
            cbx.ValueMember = "KEY";
            cbx.SelectedValue = Vehicle;
            cbx.SelectedIndexChanged += UpdateVehicle;
            cbx.KeyDown += EnterSave;
            this.Controls.Add(cbx);

            TextBox tb2 = new TextBox();
            tb2 = new TextBox();
            tb2.MaxLength = Save.MaxNameLength;
            tb2.Text = Player;
            tb2.Location = new Point(_p + _lW + _xOff, _p + _lH * 4);
            tb2.Size = new Size(_tbW, _tbH);
            tb2.TextChanged += UpdateName;
            tb2.KeyDown += EnterSave;
            this.Controls.Add(tb2);
        }

        private void EnterSave(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                _savebutton.PerformClick();
            }
        }

        private void UpdateTime(object o, EventArgs e)
        {
            TextBox tb = (TextBox)o;
            if (Helper.CheckDurationFormat(tb.Text))
            {
                Time = Helper.TimeStringToSeconds(tb.Text);
                _errorLabel.Text = "";
            }
            else
                _errorLabel.Text = "Time Format: 59:59.5959595";
        }

        private void UpdateVehicle(object o, EventArgs e)
        {
            ComboBox cbx = (ComboBox)o;
            Vehicle = (byte)cbx.SelectedValue;
        }

        private void UpdateName(object o, EventArgs e)
        {
            TextBox tb = (TextBox)o;
            if (Helper.CheckFilenameFormat(tb.Text))
            {
                Player = tb.Text;
                _errorLabel.Text = "";
            }
            else
                _errorLabel.Text = "Name contains invalid characters";
        }
    }
}
