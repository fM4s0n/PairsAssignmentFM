using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PairsAssignmentFM.Controls
{
    public partial class CtrPlayerPanel : UserControl
    {
        public CtrPlayerPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Reset the player panels to pre-game state
        /// </summary>
        public void ResetPlayerPanel(bool keepNames)
        {
            if(!keepNames)
            {
                this.TxtPlayerName.Text = "";
                this.TxtPlayerName.ReadOnly = false;
            }

            this.LblPairsFoundNum.Text = "";
            this.PicBxToPlay.Hide();
        }
    }
}
