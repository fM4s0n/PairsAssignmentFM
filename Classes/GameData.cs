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
        #region Global Variables
        readonly string savesPath = Directory.GetCurrentDirectory();
        #endregion

        #region properties   
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
        /// <summary>Saves the current state of the game</summary>
        public void SaveGame()
        {
            //Create a new SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog() { InitialDirectory = savesPath, DefaultExt = "txt", Filter = "Text File (*.txt) | *.txt", AddExtension = true };
 
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //write the save file in the selected user location as a text file
                using (StreamWriter sw = File.CreateText(saveFileDialog.FileName))
                {
                    //serialise the game object as a json
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(sw, this);
                }
                MessageBox.Show("Game saved successfully.", "Pairs", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //If the user cancel's out of the dilaog
            else
            {
                MessageBox.Show("Saving cancelled", "Pairs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>Loads a previously saved game</summary>
        public string LoadGame()
        {
            string fileName;
            OpenFileDialog openFileDialog = new OpenFileDialog() { InitialDirectory = savesPath, Filter = "Text File (*.txt) | *.txt" };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                try
                {
                    using (StreamReader sr = new StreamReader(fileName))
                        return sr.ReadToEnd();
                }
                //Catch for if they load a non .txt file
                catch (IOException)
                {
                    return "1";
                }                            
            }
            else
            {
                MessageBox.Show("Loading cancelled", "Pairs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "0";
            }
        }
        #endregion
    }
}
