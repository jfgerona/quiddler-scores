using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    internal class Player : IPlayer
    {
        //Members
        private Deck deck;
        private List<Card> playerCards = new List<Card>();
        private int totalPoints = 0;
        StringBuilder playerString = new StringBuilder();

        public int CardCount { get { return playerCards.Count; } }

        public int TotalPoints { get { return totalPoints; } }

        //Discard method
        public bool Discard(string card)
        {
            foreach(var c in playerCards)
            {
                if (card.Equals(c.letter))
                {
                    deck.topDiscard = c;
                    playerCards.Remove(c);
                    return true;
                }
            }
            return false;
        }// end method

        //Draw Card method
        public string DrawCard()
        {
            if(deck.CardCount == 0)
            {
                throw new InvalidOperationException();
            }
            else
            {
                Card c = deck.drawTopCard();
                playerCards.Add(c);
                playerString.Append(c.letter + " ");
                return c.letter;
            }
        }// end method

        //PickupToDiscard method
        public string PickupTopDiscard()
        {
            Card c = deck.getTopDiscard();
            playerCards.Add(c);
            playerString.Append(c.letter + " ");
            return c.letter;
        }// end method

        //Play word method
        public int PlayWord(string candidate)
        {
            int point = TestWord(candidate);
            if(point > 0)
            {
                string[] letterArray = candidate.Split(" ");
                foreach(string s in letterArray)
                {
                    Discard(s);
                    totalPoints += getPoint(s);
                }
            }
            return point;
        }// end method

        //Test word method
        public int TestWord(string candidate)
        {
            int point = 0;
            if(playerCards.Count == 0)
            {
                return 0;
            }
            string[] letterArray = candidate.Split(" ");
            string pString = playerString.ToString();
            StringBuilder word = new StringBuilder();
            foreach (string s in letterArray)
            {
                if (pString.Contains(s))
                {
                    point += getPoint(s);
                }
                else
                {
                    return 0;
                }
                word.Append(s);
            }
            if (!deck.checker.CheckSpelling(word.ToString().ToLower()))
            {
                return 0;
            }
            else
            {
                return point;
            }
        }// end method

        //Constructor
        public Player(Deck d)
        {
            deck = d;
        }

        //Public Method
        public override string ToString()
        {
            playerString = new StringBuilder();
            foreach (var c in playerCards)
            {
                playerString.Append(c.letter + " ");
            }
            return "[ " + playerString.ToString() + "]";
        }// end method

        private int getPoint(string s)
        {
            switch (s)
            {
                case "a" or "e" or "i" or "o":
                    return 2;
                case "l" or "s" or "t":
                    return 3;
                case "u" or "y":
                    return 4;
                case "d" or "m" or "n" or "r":
                    return 5;
                case "f" or "g" or "p":
                    return 6;
                case "h" or "er" or "in":
                    return 7;
                case "b" or "c" or "k":
                    return 8;
                case "qu" or "th":
                    return 9;
                case "w" or "cl":
                    return 10;
                case "v":
                    return 11;
                case "x":
                    return 12;
                case "j":
                    return 13;
                case "z":
                    return 14;
                case "q":
                    return 15;
                default:
                    throw new Exception("Error");
            }
        }// end method

    }
}
