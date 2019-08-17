using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWE1R.Racer;
using SWE1R.Util;

namespace SWE1RSE
{
    public partial class Editor : Form
    {
        private LoadState loaded = 0; // 0=none, 1=profile, 2=gamesave
        private string loaded_filename_gamesave, loaded_filename_profile;

        public Editor()
        {
            InitializeComponent();
            basepanel.MouseEnter += (o, e) => basepanel.Focus();
            sections = new AccordionLayout(basepanel);
            gamesave = new Save.GameSave();
            ProcessLoadState(loaded);
        }

        private enum LoadState
        {
            None = 0,
            Profile = 1,
            GameSave = 2
        }

        private void ProcessLoadState(LoadState ls)
        {
            loaded = ls;
            menu_save.Enabled = ls != LoadState.None;
            menu_saveAs.Enabled = ls != LoadState.None;
        }

        private void TgfddatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlg_inGame.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportGameSave(dlg_inGame.FileName);
                    loaded_filename_gamesave = dlg_inGame.FileName;
                    loaded_filename_profile = "";
                    ProcessLoadState(LoadState.GameSave);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void Menu_openProfile_Click(object sender, EventArgs e)
        {
            if (dlg_inProfile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportProfileSave(dlg_inProfile.FileName);
                    loaded_filename_profile = dlg_inProfile.FileName;
                    loaded_filename_gamesave = "";
                    ProcessLoadState(LoadState.Profile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (loaded == LoadState.GameSave)
                {
                    dlg_outGame.FileName = Path.GetFileName(loaded_filename_gamesave);
                    if (dlg_outGame.ShowDialog() == DialogResult.OK)
                        ExportGameSave(dlg_outGame.FileName);
                }
                else if (loaded == LoadState.Profile)
                {
                    dlg_outProfile.FileName = string.Format(@"{0}.{1}", profile.Player, Save.ProfileSave.Extension);
                    if (dlg_outProfile.ShowDialog() == DialogResult.OK)
                        ExportProfileSave(dlg_outProfile.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void TgfddatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //eventually remove confirmation messages in lieu of passive visual indicators of unsaved data
            try
            {
                if (loaded == LoadState.GameSave)
                {
                    ExportGameSave(loaded_filename_gamesave);
                    MessageBox.Show(string.Format("Saved {0}", Path.GetFileName(loaded_filename_gamesave)));
                }
                else if (loaded == LoadState.Profile)
                {
                    ExportProfileSave(string.Format(@"{0}\{1}.{2}", Path.GetDirectoryName(loaded_filename_profile), profile.Player, Save.ProfileSave.Extension));
                    MessageBox.Show(string.Format("Saved {0}.{1}", profile.Player, Save.ProfileSave.Extension));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
