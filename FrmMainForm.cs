using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PairsAssignmentFM.Controls;
using System.IO;
using PairsAssignmentFM.Forms;
using PairsAssignmentFM.Classes;
using MyDialogs;
using Newtonsoft.Json;

namespace PairsAssignmentFM
{
    /// <summary>
    /// This is the main form where the game is played and is the first form to launch
    /// </summary>
    public partial class FrmMainForm : Form
    {
        #region Global Variables
        GameData game = new GameData()
        {
            CurrentPlayer = true,
            RowColCount = 6,
            TotalPairsWon = 0,
            CardsSelected = 0,
            CardNums = new List<int>(),
            CardImageLocations = new List<string>()
        };

        readonly string imgPath = $"{Directory.GetCurrentDirectory()}\\Images\\";

        bool gameInProgress = false;

        CtrPlayerPanel pnlPlayer1;
        CtrPlayerPanel pnlPlayer2;

        CtrGameBoard gb;

        Timer fiveSecTimerP1 = new Timer() { Interval = 5000, Enabled = false };
        Timer fiveSecTimerP2 = new Timer() { Interval = 5000, Enabled = false };

        int card1Num, card2Num;
        Point card1Loc, card2Loc;
        #endregion

        #region Initial Set-up
        public FrmMainForm()
        {
            InitializeComponent();
            this.CenterToScreen();

            this.BackColor = ColorTranslator.FromHtml("#5AF");

            PicBxLargeLogo.ImageLocation = $"{imgPath}LargeLogo.png";

            InitGamePanels();
        }

        /// <summary>
        /// New game set up
        /// </summary>
        /// <param name="isNewGame">Is this a new game or a loaded game</param>
        private void StartGameSetUp(bool isNewGame)
        {
            //Hide large logo when game starts
            PicBxLargeLogo.Hide();

            //Add the gameboard control to the panel
            PnlGameBoard.Controls.Add(gb);

            gameInProgress = true;

            pnlPlayer1.TxtPlayerName.ReadOnly = true;
            pnlPlayer2.TxtPlayerName.ReadOnly = true;

            //If starting a new game
            if (isNewGame)
                gb.GenerateGameBoard(game.RowColCount);
            //If loading a game
            else
            {
                gb.GenerateGameBoard(game.RowColCount, game.CardNums, game.CardImageLocations);

                pnlPlayer1.TxtPlayerName.Text = game.P1Name;
                pnlPlayer2.TxtPlayerName.Text = game.P2Name;

                pnlPlayer1.LblPairsFoundNum.Text = game.P1Score;
                pnlPlayer2.LblPairsFoundNum.Text = game.P2Score;
            }
        }

        /// <summary>Set up the panels for a new game</summary>
        private void InitGamePanels()
        {
            //Gameboard
            gb = new CtrGameBoard { Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top };

            //Player panels
            pnlPlayer1 = new CtrPlayerPanel() { Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top };
            pnlPlayer2 = new CtrPlayerPanel() { Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top };

            //Add the controls to the panels
            PnlPlayer1.Controls.Add(pnlPlayer1);
            PnlPlayer2.Controls.Add(pnlPlayer2);

            pnlPlayer1.LblPlayerNum.Text = "Player 1";
            pnlPlayer2.LblPlayerNum.Text = "Player 2";

            pnlPlayer1.PicBxToPlay.ImageLocation = $"{imgPath}/YourTurn.png";
            pnlPlayer2.PicBxToPlay.ImageLocation = $"{imgPath}/YourTurn.png";

            pnlPlayer1.PicBxToPlay.Hide();
            pnlPlayer2.PicBxToPlay.Hide();

            //Subsribe to the OnCardClick event
            gb.OnCardClick += CardClicked;
        }

        /// <summary>Check player names have been entered</summary>
        private void PlayerNameCheck()
        {
            string p1Name, p2Name;

            //if both names needed
            if (pnlPlayer1.TxtPlayerName.Text.Length == 0 && pnlPlayer2.TxtPlayerName.Text.Length == 0)
            {
                p1Name = My_Dialogs.InputBox("Pairs", "Please enter player 1 name:");

                //Keep asking for a name until they enter at least 1 char
                while (p1Name == "")
                    p1Name = My_Dialogs.InputBox("Pairs", "Please enter player 1 name:");

                pnlPlayer1.TxtPlayerName.Text = p1Name;

                p2Name = My_Dialogs.InputBox("Pairs", "Please enter player 2 name:");

                while (p2Name == "")
                    p2Name = My_Dialogs.InputBox("Pairs", "Please enter player 2 name:");

                pnlPlayer2.TxtPlayerName.Text = p2Name;
            }
            else
            {
                //If only p1 name needed
                if (pnlPlayer1.TxtPlayerName.Text.Length == 0)
                {
                    p1Name = My_Dialogs.InputBox("Pairs", "Please enter player 1 name:");

                    while (p1Name == "")
                        p1Name = My_Dialogs.InputBox("Pairs", "Please enter player 1 name:");

                    pnlPlayer1.TxtPlayerName.Text = p1Name;
                }
                else
                {
                    //if only p2 name needed 
                    if (pnlPlayer2.TxtPlayerName.Text.Length == 0)
                    {
                        p2Name = My_Dialogs.InputBox("Pairs", "Please enter player 2 name:");

                        while (p2Name == "")
                            p2Name = My_Dialogs.InputBox("Pairs", "Please enter player 2 name:");

                        pnlPlayer2.TxtPlayerName.Text = p2Name;
                    }
                    //All checks pass
                    else
                        return;
                }
            }
        }
        #endregion

        #region Game Logic
        /// <summary>Change current player to next player</summary>
        public void ChangeCurrentPlayer(object sender, EventArgs e)
        {
            fiveSecTimerP1.Enabled = false;
            fiveSecTimerP2.Enabled = false;

            game.CardsSelected = 0;
            gb.isPaused = false;
            card1Loc = new Point(0, 0);
            card2Loc = new Point(0, 0);

            //If currentPlayer is p1
            if (game.CurrentPlayer)
            {
                game.CurrentPlayer = false;
                pnlPlayer1.PicBxToPlay.Hide();
                pnlPlayer2.PicBxToPlay.Show();

                pnlPlayer1.PicBxCard1.ImageLocation = null;
                pnlPlayer1.PicBxCard2.ImageLocation = null;
            }
            //If currentPlayer is p2
            else
            {
                game.CurrentPlayer = true;
                pnlPlayer1.PicBxToPlay.Show();
                pnlPlayer2.PicBxToPlay.Hide();

                pnlPlayer2.PicBxCard1.ImageLocation = null;
                pnlPlayer2.PicBxCard2.ImageLocation = null;
            }
        }

        /// <summary>
        /// Handle the click of a card
        /// </summary>
        /// <param name="sender">Card that was clicked</param>
        /// <param name="e">Event</param>
        /// <param name="cardNum">Card number to set card selected image</param>
        protected void CardClicked(object sender, EventArgs e, int cardNum, Point cardLocation) 
        {
            //Player 1
            if (game.CurrentPlayer)
            {
                //1st card selected
                if (game.CardsSelected == 0)
                {
                    card1Loc = cardLocation;
                    card1Num = cardNum;

                    pnlPlayer1.PicBxCard1.ImageLocation = $"{imgPath}{cardNum}.png";
                    game.CardsSelected = 1;
                }
                //2nd card selected
                else
                {
                    card2Loc = cardLocation;
                    card2Num = cardNum;

                    //Check to see if card in same location was clicked
                    if (card1Loc == card2Loc)
                        return;

                    pnlPlayer1.PicBxCard2.ImageLocation = $"{imgPath}{cardNum}.png";

                    //Pause input for 5secs, then change the player
                    gb.isPaused = true;
                    fiveSecTimerP1 = new Timer() { Interval = 5000, Enabled = true };

                    if (CheckIfCardsMatch())
                        CheckIfGameFinished();
                    else                   
                        fiveSecTimerP1.Tick += FlipCardsBackToRed;

                    fiveSecTimerP1.Tick += ChangeCurrentPlayer;
                }                    
            }
            //Player 2
            else
            {
                if (game.CardsSelected == 0)
                {
                    card1Loc = cardLocation;
                    card1Num = cardNum;

                    pnlPlayer2.PicBxCard1.ImageLocation = $"{imgPath}{cardNum}.png";
                    game.CardsSelected = 1;
                }
                else
                {
                    card2Loc = cardLocation;
                    card2Num = cardNum;

                    if (card1Loc == card2Loc)
                        return;

                    pnlPlayer2.PicBxCard2.ImageLocation = $"{imgPath}{cardNum}.png";                    

                    gb.isPaused = true;

                    fiveSecTimerP2 = new Timer() { Interval = 5000, Enabled = true };

                    if (CheckIfCardsMatch())                    
                        CheckIfGameFinished();
                    else                    
                        fiveSecTimerP2.Tick += FlipCardsBackToRed;

                    fiveSecTimerP2.Tick += ChangeCurrentPlayer;
                }
            }                
        }

        /// <summary>
        /// Check if the cards selected are a match
        /// </summary>
        /// <returns>True if found a match, false if not</returns>
        private bool CheckIfCardsMatch()
        {
            //player 1
            if(game.CurrentPlayer)
            {
                if (card1Num == card2Num)
                {
                    //Increment pairs found value
                    int i = int.Parse(pnlPlayer1.LblPairsFoundNum.Text);
                    i++;
                    pnlPlayer1.LblPairsFoundNum.Text = i.ToString();
                    fiveSecTimerP1.Tick += ChangeCardsToBlue;
                    game.TotalPairsWon++;
                    return true;
                }
                else                
                    return false;                
            }
            //player 2
            else
            {
                if (card1Num == card2Num)
                {
                    //Increment pairs found value
                    int i = int.Parse(pnlPlayer2.LblPairsFoundNum.Text);
                    i++;
                    pnlPlayer2.LblPairsFoundNum.Text = i.ToString();
                    fiveSecTimerP2.Tick += ChangeCardsToBlue;
                    game.TotalPairsWon++;
                    return true;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Change matched cards to blue
        /// </summary>
        /// <param name="cardNum1">Number of 1st card selected</param>
        /// <param name="cardNum2">Number of 2nd card selected</param>
        private void ChangeCardsToBlue(object sender, EventArgs e)
        {
            foreach (PictureBox pb in gb.TlpGameBoard.Controls)
            {
                if (pb.Name == $"pb{card1Num}" || pb.Name == $"pb{card2Num}")
                    pb.ImageLocation = $"{imgPath}Blue.png";
            }
        }

        /// <summary>Flip the cards back to red if not a match</summary>
        private void FlipCardsBackToRed(object sender, EventArgs e)
        {
            foreach (PictureBox pb in gb.TlpGameBoard.Controls)
            {
                if (pb.ImageLocation != $"{imgPath}Blue.png" && pb.ImageLocation != $"{imgPath}Red.png")
                {
                    pb.ImageLocation = $"{imgPath}Red.png";
                }
            }
        }

        /// <summary>
        /// Check if game is finished
        /// </summary>
        private void CheckIfGameFinished()
        {
            //If totalPairsWon is the same as total number of pairs on the board, the game is finished
            if (game.TotalPairsWon == (game.RowColCount * game.RowColCount) / 2)
                GameFinished();
            else
                return;
        }

        /// <summary>Display a message stating who won the game</summary>
        private void GameFinished()
        {
            int p1Score = int.Parse(pnlPlayer1.LblPairsFoundNum.Text);
            int p2Score = int.Parse(pnlPlayer2.LblPairsFoundNum.Text);

            //p1 wins
            if (p1Score > p2Score)
            {
                if (MessageBox.Show($"{pnlPlayer1.TxtPlayerName.Text} Wins!\nNew game?", "Pairs", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    ResetGame(false);
                else
                    return;
            }
            else
            {
                //P2 wins
                if (p2Score > p1Score)
                {
                    if (MessageBox.Show($"{pnlPlayer2.TxtPlayerName.Text} Wins!\nNew game?", "Pairs", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        ResetGame(false);
                    else
                        return;
                }
                //Draw
                else
                {
                    if (MessageBox.Show($"It's a draw!\nNew game?", "Pairs", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        ResetGame(false);
                    else
                        return;
                }
            }
        }

        /// <summary>Reset the game to starting point</summary>
        public void ResetGame(bool retreivingGame)
        {
            gameInProgress = false;
            bool keepNames = false;

            if(!retreivingGame)
            {
                game.CurrentPlayer = true;

                if (MessageBox.Show("Do you wish to keep the same player names?", "Pairs", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    keepNames = true;
            }
            else
                keepNames = true;
            
            //Reset the player panels
            pnlPlayer1.ResetPlayerPanel(keepNames);
            pnlPlayer2.ResetPlayerPanel(keepNames);

            //Reset the timers
            fiveSecTimerP1.Enabled = false;
            fiveSecTimerP2.Enabled = false;

            //Reset the gamebard
            gb.ResetGameBoard();

            string p1Name, p2Name;

            //If not keeping names, input new player names
            if(!keepNames)
            {
                p1Name = My_Dialogs.InputBox("Pairs", "Enter player 1's name:");
                while (p1Name == "")
                    p1Name = My_Dialogs.InputBox("Enter player 1's name:");

                p2Name = My_Dialogs.InputBox("Pairs","Enter player 2's name:");
                while (p2Name == "")
                    p2Name = My_Dialogs.InputBox("Pairs", "Enter player 2's name:");

                pnlPlayer1.TxtPlayerName.Text = p1Name;
                pnlPlayer2.TxtPlayerName.Text = p2Name;
            }

            //Start the new game
            if (retreivingGame)
                StartGameSetUp(false);
            else
                StartGameSetUp(true);

            pnlPlayer1.PicBxToPlay.Show();            
        }
        #endregion

        #region Toolbar Button methods
        /// <summary>Exit button</summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Allow user to save first
            if (MessageBox.Show("Do you wish to save your progress?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SaveGame();
            else
                Application.Exit();
        }

        /// <summary>Start a new game</summary>
        private void TsmiNewGame_Click(object sender, EventArgs e)
        {
            //If a game is in progress give oportunity to save first
            if (gameInProgress)
            {
                if (MessageBox.Show("Do you wish to save your progress?", "New game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    SaveGame();
                             
                    ResetGame(false);                
            }
            else
            {
                PlayerNameCheck();
                StartGameSetUp(true);
                pnlPlayer1.PicBxToPlay.Show();  
            }
        }

        /// <summary>Save the game</summary>
        private void SaveGame()
        {
            //Don't allow user to save while game is frozen
            if (gb.isPaused)
            {
                MessageBox.Show("Cannot save game while gameboard is frozen", "Pairs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Don't allow user to save if a game hasn't been started
            if(!gameInProgress)
            {
                MessageBox.Show("Please start a game before saving", "Pairs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (PictureBox pb in gb.TlpGameBoard.Controls)
            {
                int cardNum = int.Parse(pb.Name.Substring(2));
                game.CardNums.Add(cardNum);
                game.CardImageLocations.Add(pb.ImageLocation);
            }

            game.P1Name = pnlPlayer1.TxtPlayerName.Text.ToString();
            game.P2Name = pnlPlayer2.TxtPlayerName.Text.ToString();
            game.P1Score = pnlPlayer1.LblPairsFoundNum.Text.ToString();
            game.P2Score = pnlPlayer2.LblPairsFoundNum.Text.ToString();

            game.SaveGame();
        }

        /// <summary>Save button in tool strip</summary>
        private void TsmiSaveGame_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        /// <summary>Retreive a previous game</summary>
        private void TsmiLoadGame_Click(object sender, EventArgs e)
        {
            //Allow user to save progress first
            if(gameInProgress)
            {
                if(MessageBox.Show("Do you wish to save the game first?", "Pairs", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    SaveGame();                                               
            }

            string json = game.LoadGame();
            //User cancelled load
            if (json == "0")
                return;
            else
            {
                //StreamReader failed
                if (json == "1")
                    MessageBox.Show("Error, loading failed - incorrect file format. check the file you are opening is a saved game and in .txt file format.","Pairs",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //All checks pass, continue to load the game
                else
                {
                    try
                    {
                        game = JsonConvert.DeserializeObject<GameData>(json);
                    }
                    catch (JsonReaderException)
                    {
                        MessageBox.Show("Error, loading failed - file unbale to be read. Please check the file is a saved game.", "Pairs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (gameInProgress)
                        ResetGame(true);
                    else
                        StartGameSetUp(false);
                }
            }
        }

        /// <summary>Launch 'about' info</summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.Show();
        }

        /// <summary>Set grid size to 6x6</summary>
        private void TsmiSize6x6_Click(object sender, EventArgs e)
        {
            game.RowColCount = 6;
            TsmiSize10x10.Checked = false;
            TsmiSize16x16.Checked = false;
        }

        /// <summary>Set grid size to 10x10</summary>
        private void TsmiSize10x10_Click(object sender, EventArgs e)
        {
            game.RowColCount = 10;
            TsmiSize6x6.Checked = false;
            TsmiSize16x16.Checked = false;
        }

        /// <summary>set grid size to 16x16</summary>
        private void TsmiSize16x16_Click(object sender, EventArgs e)
        {
            game.RowColCount = 16;
            TsmiSize6x6.Checked = false;
            TsmiSize10x10.Checked = false;
        }
        #endregion
    }
}
