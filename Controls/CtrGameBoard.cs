using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PairsAssignmentFM.Classes;

namespace PairsAssignmentFM.Controls
{
    public partial class CtrGameBoard : UserControl
    {
        #region Global Variables
        //File path for images
        readonly string imgPath = $"{Directory.GetCurrentDirectory()}\\Images\\";
        //Array that represents the gameboard, holding picBoxs for all card images
        PictureBox[,] picBoxArray;
        //Timer
        Timer initialTimer;
        int initTimerInterval;

        //Bool to stop click event for cards being reached
        //true = not clickable, false = clickable
        public bool isPaused;

        //Card click event 
        public delegate void CardClick(object obj, EventArgs e, int cardNum, Point cardLocation);
        public event CardClick OnCardClick;
        #endregion

        #region Gamboard Set-up
        public CtrGameBoard()
        {
            isPaused = false;
            InitializeComponent();            
        }

        /// <summary>
        /// Create the game board and fill with cards
        /// </summary>
        /// <param name="rowColCount">Number of rows and columns</param>
        public void GenerateGameBoard(int rowColCount)
        {
            switch (rowColCount)
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

            isPaused = true;
            //Only allow user input after 10secs
            initialTimer = new Timer() { Interval = initTimerInterval, Enabled = true };
            initialTimer.Tick += (_, __) => isPaused = false;

            //Create the array to store the gameboard data
            picBoxArray = new PictureBox[rowColCount, rowColCount];

            //Add rows and columns to the gameboard
            AddRowAndCols(rowColCount);
            
            //Loop through array adding the picture boxes to each cell
            AddCardsToArray(rowColCount);

            //Add the pictureBoxs to the TLP
            foreach (PictureBox pb in picBoxArray)
                TlpGameBoard.Controls.Add(pb);

            //Add correcttion padding to the tlp
            TlpGameBoard.Padding = GetCorrectionPadding(TlpGameBoard, 1);

            //Set all cards to red after 10 seconds
            initialTimer.Tick += SetAllCardsRed;            
        }

        /// <summary>
        /// Overload of Generate gameboard. This is for loading a game
        /// </summary>
        /// <param name="rowColCount">Number of rows and cols</param>
        /// <param name="cardNums">List of all the card numbers in use in the loaded game</param>
        /// <param name="cardImgLocations">list of all the card locations in the loaded game</param>
        public void GenerateGameBoard(int rowColCount, List<int> cardNums, List<string> cardImgLocations)
        {
            switch (rowColCount)
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

            isPaused = true;
            //Only allow user input after 10secs
            initialTimer = new Timer() { Interval = initTimerInterval, Enabled = true };
            initialTimer.Tick += (_, __) => isPaused = false;

            //Create the array to store the gameboard data
            picBoxArray = new PictureBox[rowColCount, rowColCount];

            //Add rows and columns to the gameboard
            AddRowAndCols(rowColCount);

            //Load cards from saved game into the array
            LoadCardsIntoArray(rowColCount, cardNums, cardImgLocations);

            //Add the pictureBoxs to the TLP
            foreach (PictureBox pb in picBoxArray)
                TlpGameBoard.Controls.Add(pb);

            //Set the correction padding
            TlpGameBoard.Padding = GetCorrectionPadding(TlpGameBoard, 1);

            //show cards for 10secs that havn't been won
            SetAvailableCardsFaceUp();

            //Set cards to red that havn't been won
            initialTimer.Tick += SetAvailableCardsRed;
        }

        /// <summary>
        /// Add rows and colums to the tlp
        /// </summary>
        /// <param name="rowColCount">no. of rows and columns each</param>
        private void AddRowAndCols(int rowColCount)
        {
            //set row & col count
            TlpGameBoard.RowCount = rowColCount;
            TlpGameBoard.ColumnCount = rowColCount;

            //Calc % each row and col should be
            float rowColPercentage = 100F/rowColCount;
                
            //Clear all current row & col styles as these are still set to the default
            TlpGameBoard.ColumnStyles.Clear();
            TlpGameBoard.RowStyles.Clear();
                
            //Add new row & col styles
            for (int i = 0; i < rowColCount; i++)
            {
                TlpGameBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, rowColPercentage));
                TlpGameBoard.RowStyles.Add(new RowStyle(SizeType.Percent, rowColPercentage));
            }
        }

        /// <summary>
        /// Add required no. of cards to the array randomly
        /// </summary>
        /// <param name="rowsAndCols">no. of rows and columns each</param>
        private void AddCardsToArray(int rowsAndCols)
        {
            GenerateCards generateCards = new GenerateCards();

            int row = rowsAndCols;
            int col = rowsAndCols;

            List<int> deck = new List<int>();
            int cardsRemaining = rowsAndCols * rowsAndCols;

            //keep adding full packs until the cards required is less than a full deck
            while (cardsRemaining > 104)
            {
                deck = generateCards.GenerateFullPack(deck);
                cardsRemaining -= 104;
            }

            //Add remaining cards to the deck
            deck = generateCards.GeneratePartialPack(deck, cardsRemaining);

            //Shuffle the deck
            deck = generateCards.ShuffleDeck(deck);

            //Add cards from deck to the array
            for (int r = 0; r < rowsAndCols; r++)
            {
                for (int c = 0; c < rowsAndCols; c++)
                {
                    int cardNum = deck[0];
                    picBoxArray[r, c] = new PictureBox()
                    {
                        Name = $"pb{cardNum}",
                        ImageLocation = $"{imgPath}{cardNum}.png",
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Fill
                    };
                    //Click event handling
                    picBoxArray[r, c].Click += CardClicked;

                    //remove top one from deck so next card can be added
                    deck.RemoveAt(0);
                }
            }            
        }

        /// <summary>
        /// Load card info stored in lists into the array
        /// </summary>
        private void LoadCardsIntoArray(int rowsAndCols, List<int> cardNums, List<string> cardImgLocations)
        {
            for (int r = 0; r < rowsAndCols; r++)
            {
                for (int c = 0; c < rowsAndCols; c++ )
                {
                    int cardNum = cardNums[0];
                    string cardImgLocation = cardImgLocations[0];

                    picBoxArray[r, c] = new PictureBox()
                    {
                        Name = $"pb{cardNum}",
                        ImageLocation = cardImgLocation,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Fill
                    };
                    picBoxArray[r,c].Click += CardClicked;

                    cardNums.RemoveAt(0);
                    cardImgLocations.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Set all cards to red for the begining of the game
        /// unless cards are blue as this means they have been won in a retreived game
        /// </summary>
        private void SetAllCardsRed(object sender, EventArgs e)
        {
            foreach (PictureBox pb in picBoxArray)
            { 
                if (pb.ImageLocation != $"{imgPath}Blue.png")
                    pb.ImageLocation = $"{imgPath}Red.png";
            }
            initialTimer.Enabled = false;
        }

        /// <summary>
        /// When loading a game, set cards to red which have not been won
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetAvailableCardsRed(object sender, EventArgs e)
        {
            foreach (PictureBox pb in picBoxArray)
            {
                if (pb.ImageLocation != $"{imgPath}Blue.png")                
                    pb.ImageLocation = $"{imgPath}Red.png";                
            }
            initialTimer.Enabled = false;
        }

        /// <summary>
        /// When loading a game, set all cards that have not been won face up for 10secs
        /// </summary>
        private void SetAvailableCardsFaceUp()
        {
            foreach (PictureBox pb in picBoxArray)
            {
                int cardNum = int.Parse(pb.Name.Substring(2));

                if (pb.ImageLocation == $"{imgPath}Red.png")
                    pb.ImageLocation = $"{imgPath}{cardNum}.png";
            }
        }

        /// <summary>
        /// Calcualtes paddinng for the tlp to correct for odd sized rows & cols
        /// </summary>
        /// <param name="tlp">Table layout panel</param>
        /// <param name="minPadding"></param>
        /// <returns>Padding for tlp</returns>
        Padding GetCorrectionPadding(TableLayoutPanel tlp, int minPad)
        {
            Rectangle netRect = tlp.ClientRectangle;
            netRect.Inflate(-minPad, -minPad);

            int w = netRect.Width / tlp.ColumnCount;
            int h = netRect.Height / tlp.RowCount;

            int deltaX = (netRect.Width - w * tlp.ColumnCount) / 2;
            int deltaY = (netRect.Height - h * tlp.RowCount) / 2;

            int OddX = (netRect.Width - w * tlp.ColumnCount) % 2;
            int OddY = (netRect.Height - h * tlp.RowCount) % 2;

            return new Padding (minPad + deltaX, minPad + deltaY, minPad + deltaX + OddX, minPad + deltaY + OddY);
        }

        /// <summary>
        /// Reset the gameboard
        /// </summary>
        public void ResetGameBoard()
        {
            TlpGameBoard.Controls.Clear();

            int rowsAndCols = Convert.ToInt32(Math.Sqrt(picBoxArray.Length));

            for (int r = 0; r < rowsAndCols; r++)
            {
                for (int c = 0; c < rowsAndCols; c++)
                {
                    picBoxArray[r, c] = null;
                }
            }
        }
        #endregion

        #region Event handling
        /// <summary>
        /// CardClicked event method
        /// </summary>
        /// <param name="sender">Picture box which was clicked</param>
        /// <param name="e"></param>
        private void CardClicked(object sender, EventArgs e)
        {
            //stop the clickevent if isPaused is true
            if (isPaused)
                return;

            //Cast sender to picture box
            PictureBox pb = sender as PictureBox;
            Point location = pb.Location;

            //If card clicked is blue, dont do anything as it has been won
            if (pb.ImageLocation == $"{imgPath}Blue.png")
                return;

            //get card number from name of the picBox
            int cardNum = int.Parse(pb.Name.Substring(2));

            //If card is currently red show the face of the card
            if (pb.ImageLocation == $"{imgPath}Red.png")
                pb.ImageLocation = $"{imgPath}{cardNum}.png";

            //Invoke the OnCardClick event to continue game logic on MainForm
            OnCardClick?.Invoke(this, e, cardNum, location);
        }
        #endregion
    }
}
