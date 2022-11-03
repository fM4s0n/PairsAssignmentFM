using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PairsAssignmentFM.Forms
{
    public partial class FrmAbout : Form
    {
        readonly string imgPath = $"{Directory.GetCurrentDirectory()}\\Images\\";
        public FrmAbout()
        {
            InitializeComponent();

            this.CenterToParent();

            string aboutText = "Description:\n\n" +
                               "This pairs game was developed by Freddie Mason for Assignment 1 of 'Programming Fundamentals' at Sheffield Hallam University";

            TxtAboutText.Text = aboutText;
            PicBxPairsLogo.ImageLocation = $"{imgPath}PairsLogo.png";
        }

        /// <summary>
        /// Close the about screen
        /// </summary>
        private void BtnCloseAbout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
