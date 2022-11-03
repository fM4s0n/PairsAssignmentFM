using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsAssignmentFM.Classes
{
    /// <summary>
    /// This class contains the methods required to generate the necessary number of cards for the relevent grid size
    /// and subsequently, shuffle the deck before returning the deck to the game board 
    /// </summary>
    public class GenerateCards
    {
        /// <summary>
        /// Add a full pack of cards to the deck
        /// </summary>
        /// <param name="deck">List containing the deck</param>
        /// <returns>Deck with an additonal full pack added on</returns>
        public List<int> GenerateFullPack(List<int> deck)
        {
            for (int i = 1; i <= 52; i++)
            {
                deck.Add(i);
                deck.Add(i);
            }
            return deck;
        }

        /// <summary>
        /// Add a part pack of cards to the deck to fill remaining spaces
        /// </summary>
        /// <param name="deck">List containing the deck</param>
        /// <param name="cardsRemaining">How many cards that need adding to the deck</param>
        /// <returns>Deck of correct length</returns>
        public List<int> GeneratePartialPack(List<int> deck, int cardsRemaining)
        {
            for (int i = 1; i <= cardsRemaining / 2; i++)
            {
                deck.Add(i);
                deck.Add(i);
            }
            return deck;
        }

        /// <summary>
        /// Randomly re-order the list containig the deck
        /// </summary>
        /// <param name="deck">Deck that needs shuffling</param>
        /// <returns>Randomly shuffled deck</returns>
        public List<int> ShuffleDeck(List<int> deck)
        {
            Random random = new Random();
            var shuffled = deck.OrderBy(_ => random.Next()).ToList();

            return shuffled;
        }
    }
}
