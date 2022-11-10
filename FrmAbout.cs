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
                               "This pairs game was developed by Freddie Mason for Assignment 1 of 'Programming Fundamentals' at Sheffield Hallam University.\n\n" +
                               "This application shows the use of the following: PictureBoxes, Textboxes, UserControls, TableLayoutPanels and Timers. In addition, it utilises arrays, local & global variables and other programming techniques.";

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
