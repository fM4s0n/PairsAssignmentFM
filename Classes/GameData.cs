using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft;
using Newtonsoft.Json;

namespace PairsAssignmentFM.Classes
{
    /// <summary>
    /// This class stores info on the current game allowing it to be saved and retreived in the future
    /// </summary>
    public class GameData
    {
        readonly string savesPath = $"{Directory.GetCurrentDirectory()}\\Saves";

        #region properties
        /// <summary>
        /// Game save data
        /// </summary>      
        public bool CurrentPlayer { get; set; }
        public int RowColCount { get; set; }
        public string P1Score { get; set; }
        public string P2Score { get; set; }
        public string P1Name { get; set; }
        public string P2Name { get; set; }
        public int TotalPairsWon { get; set; }
        public int CardsSelected { get; set; }
        public List<int> CardNums { get; set; }
        public List<string> CardImageLocations { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Saves the current state of the game
        /// </summary>
        public void SaveGame()
        {
            string fileName;
            //Create a new SaveFileDialog to allow user to select save file location
            SaveFileDialog saveFileDialog = new SaveFileDialog() { InitialDirectory = savesPath, DefaultExt = "txt", Filter = "Text File (*.txt) | *.txt", AddExtension = true };

            //If the user doesn't cancel out of the dialog 
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //save the file path
                fileName = saveFileDialog.FileName;

                //write the save file in the selected user location as a text file
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    //serialise the game object as a json
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(sw, this);
                }
            }
            //If the user cancel's out of the dilaog
            else
            {
                MessageBox.Show("Saving operation cancelled", "Save Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// Loads a previously saved game
        /// </summary>
        public string LoadGame()
        {
            string fileName, json;
            OpenFileDialog openFileDialog = new OpenFileDialog() { InitialDirectory = savesPath };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                
                using (StreamReader sr = new StreamReader(fileName))                
                    json = sr.ReadToEnd();
                
                return json;
            }
            else
            {
                MessageBox.Show("Loading operation cancelled", "Load Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "0";
            }
        }
        #endregion
    }
}
