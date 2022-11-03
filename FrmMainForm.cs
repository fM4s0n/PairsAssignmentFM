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
    public partial class FrmMainForm : Form
    {
        #region Global Variables
        //New instance of GameData Class so game can be saved/loaded
        GameData game = new GameData()
        {
            CurrentPlayer = true,
            RowColCount = 6,
            TotalPairsWon = 0,
            CardsSelected = 0,
            CardNums = new List<int>(),
            CardImageLocations = new List<string>()
        };

        //Directory path
        readonly string imgPath = $"{Directory.GetCurrentDirectory()}\\Images\\";

        //track if a game is in progress
        bool gameInProgress = false;

        //Player panels
        CtrPlayerPanel pnlPlayer1;
        CtrPlayerPanel pnlPlayer2;

        //Gamboard
        CtrGameBoard gb;
        //Timers
        Timer timerInitialDisplay, fiveSecTimer;

        int initTimerInterval;

        Point card1Loc, card2Loc;
        #endregion

        public FrmMainForm()
        {
            InitializeComponent();

            //Make the form launch in the centre of the screen
            this.CenterToScreen();

            //Fill out game panels with controls
            InitGamePanels();

            //Set background colour of the form to the same colour as the logo
            this.BackColor = ColorTranslator.FromHtml("#5AF");

            PicBxLargeLogo.ImageLocation = $"{imgPath}LargeLogo.png";
        }

        #region Initial Set-up methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isNewGame">Is this a new game or a loaded game</param>
        private void StartGameSetUp(bool isNewGame)
        {
            //Hide large logo when game starts
            PicBxLargeLogo.Hide();

            //Add the gameboard control to the panel
            PnlGameBoard.Controls.Add(gb);

            gameInProgress = true;

            //Set the default grid size to 6x6
            TsmiSize6x6.Checked = true;

            pnlPlayer1.TxtPlayerName.ReadOnly = true;
            pnlPlayer2.TxtPlayerName.ReadOnly = true;

            //Change timer interval based on grid size
            switch (game.RowColCount)
            {
                case 6:
                    initTimerInterval = 5000;
                    break;
                case 10:
                    initTimerInterval = 10000;
                    break;
                case 16:
                    initTimerInterval = 15000;
                    break;
            }

            //If were a starting a new game
            if (isNewGame)
                gb.GenerateGameBoard(game.RowColCount);
            //If loading a game, need to call overloaded version
            else
            {
                gb.GenerateGameBoard(game.RowColCount, game.CardNums, game.CardImageLocations);

                pnlPlayer1.TxtPlayerName.Text = game.P1Name;
                pnlPlayer2.TxtPlayerName.Text = game.P2Name;

                pnlPlayer1.LblPairsFoundNum.Text = game.P1Score;
                pnlPlayer2.LblPairsFoundNum.Text = game.P2Score;
            }
        }

        private void InitGamePanels()
        {
            //Gameboard
            gb = new CtrGameBoard { Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top };

            //Player panels
            pnlPlayer1 = new CtrPlayerPanel();
            pnlPlayer2 = new CtrPlayerPanel();

            //Add the controls to the panels
            PnlPlayer1.Controls.Add(pnlPlayer1);
            PnlPlayer2.Controls.Add(pnlPlayer2);

            //Label the controls
            pnlPlayer1.LblPlayerNum.Text = "Player 1";
            pnlPlayer2.LblPlayerNum.Text = "Player 2";

            //Set next to play images
            pnlPlayer1.PicBxToPlay.ImageLocation = $"{imgPath}/YourTurn.png";
            pnlPlayer2.PicBxToPlay.ImageLocation = $"{imgPath}/YourTurn.png";

            //Set both to plays to hidden until game starts
            pnlPlayer1.PicBxToPlay.Hide();
            pnlPlayer2.PicBxToPlay.Hide();

            //Subsribe to the OnCardClick event
            gb.OnCardClick += CardClicked;
        }

        /// <summary>
        /// Checks if both player names have been entered and are not the same
        /// </summary>
        /// <returns>True if all checks pass, false if any checks fail</returns>
        private bool PlayerNameCheck()
        {
            //if both names are empty
            if (pnlPlayer1.TxtPlayerName.Text.Length == 0 && pnlPlayer2.TxtPlayerName.Text.Length == 0)
            {
                MessageBox.Show("Please enter player names");
                pnlPlayer1.TxtPlayerName.BackColor = Color.Yellow;
                pnlPlayer2.TxtPlayerName.BackColor = Color.Yellow;
                return false;
            }
            else
            {
                //if p1 name is empty
                if (pnlPlayer1.TxtPlayerName.Text.Length == 0)
                {
                    MessageBox.Show("Please enter name for Player 1");
                    pnlPlayer1.TxtPlayerName.BackColor = Color.Yellow;
                    return false;
                }
                else
                {
                    pnlPlayer1.TxtPlayerName.BackColor = Color.White;
                    //if p2 name is empty
                    if (pnlPlayer2.TxtPlayerName.Text.Length == 0)
                    {
                        MessageBox.Show("Please enter name for Player 2");
                        pnlPlayer2.TxtPlayerName.BackColor = Color.Yellow;
                        return false;
                    }
                    else
                    {
                        //if both player names match
                        if (pnlPlayer1.TxtPlayerName.Text == pnlPlayer2.TxtPlayerName.Text)
                        {
                            MessageBox.Show("Please ensure player names are different");
                            pnlPlayer1.TxtPlayerName.BackColor = Color.Yellow;
                            pnlPlayer2.TxtPlayerName.BackColor = Color.Yellow;
                            return false;
                        }
                        //All checks pass
                        else
                        {
                            pnlPlayer2.TxtPlayerName.BackColor = Color.White;
                            pnlPlayer1.TxtPlayerName.ReadOnly = true;
                            pnlPlayer2.TxtPlayerName.ReadOnly = true;
                            return true;
                        }
                    }
                }
            }
        }
        #endregion

        #region Game Logic
        /// <summary>
        /// Change current player to next player and amend controls to reflect this
        /// </summary>
        public void ChangeCurrentPlayer(object sender, EventArgs e)
        {
            game.CardsSelected = 0;
            timerInitialDisplay.Enabled = false;
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
                //This is the 1st card the player has selected for their turn
                if (game.CardsSelected == 0)
                {
                    card1Loc = cardLocation;

                    pnlPlayer1.PicBxCard1.ImageLocation = $"{imgPath}{cardNum}.png";
                    game.CardsSelected = 1;
                }
                //This is the 2nd card the player has selected for their turn
                else
                {
                    //Check for if they have clicked the same card as the first so they must click the pair of the card selected, not the same exact card
                    card2Loc = cardLocation;
                    if (card1Loc == card2Loc)
                        return;

                    pnlPlayer1.PicBxCard2.ImageLocation = $"{imgPath}{cardNum}.png";

                    //Pause input for 5secs, then change the player
                    gb.isPaused = true;
                    timerInitialDisplay = new Timer() { Interval = initTimerInterval, Enabled = true };
                    timerInitialDisplay.Tick += ChangeCurrentPlayer;

                    //If there is a match, check if the game has been won
                    if (CheckIfCardsMatch())
                        CheckIfGameFinished();
                    else
                    {
                        fiveSecTimer = new Timer() { Interval = 5000, Enabled = true };
                        fiveSecTimer.Tick += FlipCardsBackToRed;
                    }
                }                    
            }
            //Player 2
            else
            {
                if (game.CardsSelected == 0)
                {
                    card1Loc = cardLocation;

                    pnlPlayer2.PicBxCard1.ImageLocation = $"{imgPath}{cardNum}.png";
                    game.CardsSelected = 1;
                }
                else
                {
                    card2Loc = cardLocation;
                    if (card1Loc == card2Loc)
                        return;

                    pnlPlayer2.PicBxCard2.ImageLocation = $"{imgPath}{cardNum}.png";
                    game.CardsSelected = 2;

                    gb.isPaused = true;
                    timerInitialDisplay = new Timer() { Interval = 5000, Enabled = true };
                    timerInitialDisplay.Tick += ChangeCurrentPlayer;
                    
                    //If the cards match, check if the game is finished
                    if (CheckIfCardsMatch())                    
                        CheckIfGameFinished();
                    //If not, set the cards back to red again
                    else
                    {
                        fiveSecTimer = new Timer() { Interval = 5000, Enabled = true };
                        fiveSecTimer.Tick += FlipCardsBackToRed;
                    }                                   
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
                //Remove path
                string c1NoPath = pnlPlayer1.PicBxCard1.ImageLocation.Substring(104);
                string c2NoPath = pnlPlayer1.PicBxCard2.ImageLocation.Substring(104);

                //remove file type so just left with card no.
                int cardNum1 = int.Parse(c1NoPath.Substring(0, c1NoPath.Length - 4));
                int cardNum2 = int.Parse(c2NoPath.Substring(0, c2NoPath.Length - 4));

                if (cardNum1 == cardNum2)
                {
                    //Increment pairs found value
                    int i = int.Parse(pnlPlayer1.LblPairsFoundNum.Text);
                    i++;
                    pnlPlayer1.LblPairsFoundNum.Text = i.ToString();
                    ChangeCardsToBlue(cardNum1, cardNum2);
                    game.TotalPairsWon++;
                    return true;
                }
                else                
                    return false;                
            }
            //player 2
            else
            {
                //Remove path
                string c1NoPath = pnlPlayer2.PicBxCard1.ImageLocation.Substring(104);
                string c2NoPath = pnlPlayer2.PicBxCard2.ImageLocation.Substring(104);

                //remove file type so just left with card no.
                int cardNum1 = int.Parse(c1NoPath.Substring(0, c1NoPath.Length - 4));
                int cardNum2 = int.Parse(c2NoPath.Substring(0, c2NoPath.Length - 4));

                if (cardNum1 == cardNum2)
                {
                    //Increment pairs found value
                    int i = int.Parse(pnlPlayer2.LblPairsFoundNum.Text);
                    i++;
                    pnlPlayer2.LblPairsFoundNum.Text = i.ToString();
                    ChangeCardsToBlue(cardNum1, cardNum2);
                    game.TotalPairsWon++;
                    return true;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Chnage matched cards to blue
        /// </summary>
        /// <param name="cardNum1">Number of 1st card selected</param>
        /// <param name="cardNum2">Number of 2nd card selected</param>
        private void ChangeCardsToBlue(int cardNum1, int cardNum2)
        {
            foreach(PictureBox pb in gb.TlpGameBoard.Controls)
            {
                if (pb.Name == $"pb{cardNum1}" || pb.Name == $"pb{cardNum2}")
                    pb.ImageLocation = $"{imgPath}Blue.png";
            }
        }

        /// <summary>
        /// Flip the cards back to red if not a match
        /// </summary>
        private void FlipCardsBackToRed(object sender, EventArgs e)
        {
            fiveSecTimer.Stop();

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

        /// <summary>
        /// Display a message stating who won the game
        /// </summary>
        private void GameFinished()
        {
            int p1Score = int.Parse(pnlPlayer1.LblPairsFoundNum.Text);
            int p2Score = int.Parse(pnlPlayer2.LblPairsFoundNum.Text);

            //p1 wins
            if (p1Score > p2Score)
            {
                if (MessageBox.Show($"{pnlPlayer1.TxtPlayerName.Text} Wins!\nNew game?", "Game complete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    ResetGame();
                else
                    return;
            }
            else
            {
                //P2 wins
                if (p2Score > p1Score)
                {
                    if (MessageBox.Show($"{pnlPlayer2.TxtPlayerName.Text} Wins!\nNew game?", "Game complete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        ResetGame();
                    else
                        return;
                }
                //Draw
                else
                {
                    if (MessageBox.Show($"It's a draw!\nNew game?", "Game complete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        ResetGame();
                    else
                        return;
                }
            }
        }

        /// <summary>
        /// Reset the game to starting point
        /// </summary>
        public void ResetGame()
        {
            gameInProgress = false;
            game.CurrentPlayer = true;
            bool keepNames = false;

            //Reset the Player Panels
            if (MessageBox.Show("Do you wish to keep the same player names?", "New Game", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                keepNames = true;
            
            //Reset the player panels
            pnlPlayer1.ResetPlayerPanel(keepNames);
            pnlPlayer2.ResetPlayerPanel(keepNames);

            //Reset the timers
            if(timerInitialDisplay != null)
                timerInitialDisplay.Enabled = false;
            if (fiveSecTimer != null)
                fiveSecTimer.Enabled = false;

            //Reset the gamebard
            gb.ResetGameBoard();

            string p1Name, p2Name;

            //If not keeping names, input new player names
            if(!keepNames)
            {
                p1Name = My_Dialogs.InputBox("Enter player 1's name:");
                while (p1Name == "")
                    p1Name = My_Dialogs.InputBox("Enter player 1's name:");

                p2Name = My_Dialogs.InputBox("Enter player 2's name:");
                while (p2Name == "")
                    p2Name = My_Dialogs.InputBox("Enter player 2's name:");

                pnlPlayer1.TxtPlayerName.Text = p1Name;
                pnlPlayer2.TxtPlayerName.Text = p2Name;
            }

            //Start the new game         
            StartGameSetUp(true);
            pnlPlayer1.PicBxToPlay.Show();
            
        }
        #endregion

        #region Toolbar Button methods
        /// <summary>
        /// Exit button
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If user clicks OK, exit the program, else do nothing
            if (MessageBox.Show("Do you wish to save your progress?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                SaveGame();
            else
                Application.Exit();
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        private void TsmiNewGame_Click(object sender, EventArgs e)
        {
            //If a game is in progress give oportunity to save first
            if (gameInProgress)
            {
                if (MessageBox.Show("Do you wish to save your progress?", "New game", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    SaveGame();
                             
                    ResetGame();                
            }
            //else start a new game
            else
            {
                //If PlayerNameCheck returns false, do nothing else continue with the game
                if (!PlayerNameCheck())
                    return;
                else
                {
                    StartGameSetUp(true);
                    pnlPlayer1.PicBxToPlay.Show();
                }
            }
        }

        /// <summary>
        /// Save the game
        /// </summary>
        private void SaveGame()
        {
            if (gb.isPaused)
            {
                MessageBox.Show("Cannot save game while gameboard is frozen");
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

        /// <summary>
        /// Save button in tool strip
        /// </summary>
        private void TsmiSaveGame_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        /// <summary>
        /// Retreive a previous game
        /// </summary>
        private void TsmiLoadGame_Click(object sender, EventArgs e)
        {
            string json = game.LoadGame();

            game = JsonConvert.DeserializeObject<GameData>(json);
            StartGameSetUp(false);
        }

        /// <summary>
        /// Launch 'about' info
        /// </summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.Show();
        }

        /// <summary>
        /// set grid size to 6x6
        /// </summary>
        private void TsmiSize6x6_Click(object sender, EventArgs e)
        {
            game.RowColCount = 6;
            TsmiSize10x10.Checked = false;
            TsmiSize16x16.Checked = false;
        }

        /// <summary>
        /// set grid size to 10x10
        /// </summary>
        private void TsmiSize10x10_Click(object sender, EventArgs e)
        {
            game.RowColCount = 10;
            TsmiSize6x6.Checked = false;
            TsmiSize16x16.Checked = false;
        }

        /// <summary>
        /// set grid size to 16x16
        /// </summary>
        private void TsmiSize16x16_Click(object sender, EventArgs e)
        {
            game.RowColCount = 16;
            TsmiSize6x6.Checked = false;
            TsmiSize10x10.Checked = false;
        }
        #endregion
    }
}
