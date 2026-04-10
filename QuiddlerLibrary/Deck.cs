using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;

namespace QuiddlerLibrary
{
    public class Deck : IDeck
    {
        //Members
        private List<Card> Cards = null;
        private int cardsPerPlayer = 0;
        internal Card topDiscard = null;
        internal Application checker = new Application();
        private bool disposedValue;

        public string About => "Test client for: Quiddler (TM) Library, © 2022 Justine Franz Gerona and Kimberley Akit";

        public int CardCount { get { return Cards.Count; } }

        public int CardsPerPlayer {
            get { return cardsPerPlayer; }
            set
            {
                if(value < 3 || value > 10)
                {
                    throw new ArgumentOutOfRangeException("Error: Only 3-10 cards per player");
                }
                else
                {
                    cardsPerPlayer = value;
                }
            }
        }

        public string TopDiscard
        {
            get
            {
                if(topDiscard == null)
                {
                    topDiscard = drawTopCard();
                }
                else
                {
                    return topDiscard.letter;
                }
                return topDiscard.letter;
            }
        }

        //new player method
        public IPlayer NewPlayer()
        {
            Player p = new Player(this);
            for (int i = 0; i < cardsPerPlayer; i++)
            {
                p.DrawCard();
            }
            return p;
        }//end method
        

        //Constructor
        public Deck()
        {
            Cards = new List<Card>();
            makeDeck();
        }

        //void method for output deck
        public void outputDeck()
        {
            Console.WriteLine($"a({countCards("a")})\tb({countCards("b")})\tc({countCards("c")})\td({countCards("d")})\te({countCards("e")})\tf({countCards("f")})\tg({countCards("g")})\t" +
                $"h({countCards("h")})\ti({countCards("i")})\tj({countCards("j")})\tk({countCards("k")})\tl({countCards("l")})");
            Console.WriteLine($"m({countCards("m")})\tn({countCards("n")})\to({countCards("o")})\tp({countCards("p")})\tq({countCards("q")})\tr({countCards("r")})\ts({countCards("s")})\t" +
                $"t({countCards("t")})\tu({countCards("u")})\tv({countCards("v")})\tw({countCards("w")})\tx({countCards("x")})");
            Console.WriteLine($"y({countCards("y")})\tz({countCards("z")})\tcl({countCards("cl")})\ter({countCards("er")})\tin({countCards("in")})\tqu({countCards("qu")})\tth({countCards("th")})\t\n");
        }//end method

        //Private Helper method 
        private void makeDeck()
        {
            List<Card> dList = new List<Card>(); //double letter list
            //Adding Cards to Deck
            for (int i = 0; i < 2; i++)
            {
                Cards.Add(new Card("b"));
                Cards.Add(new Card("c"));
                Cards.Add(new Card("f"));
                Cards.Add(new Card("h"));
                Cards.Add(new Card("j"));
                Cards.Add(new Card("k"));
                Cards.Add(new Card("m"));
                Cards.Add(new Card("p"));
                Cards.Add(new Card("q"));
                Cards.Add(new Card("v"));
                Cards.Add(new Card("w"));
                Cards.Add(new Card("x"));
                Cards.Add(new Card("z"));
                Cards.Add(new Card("cl"));
                Cards.Add(new Card("er"));
                Cards.Add(new Card("in"));
                Cards.Add(new Card("qu"));
                Cards.Add(new Card("th"));
            }
            for (int i = 0; i < 4; i++)
            {
                Cards.Add(new Card("d"));
                Cards.Add(new Card("g"));
                Cards.Add(new Card("l"));
                Cards.Add(new Card("s"));
                Cards.Add(new Card("y"));
            }
            for (int i = 0; i < 6; i++)
            {
                Cards.Add(new Card("n"));
                Cards.Add(new Card("r"));
                Cards.Add(new Card("t"));
                Cards.Add(new Card("u"));
            }
            for (int i = 0; i < 8; i++)
            {
                Cards.Add(new Card("i"));
                Cards.Add(new Card("o"));
            }
            for (int i = 0; i < 10; i++)
            {
                Cards.Add(new Card("a"));
            }
            for (int i = 0; i < 12; i++)
            {
                Cards.Add(new Card("e"));
            }
            //Shuffling Cards
            Random rng = new Random();
            Cards = Cards.OrderBy(c => rng.Next()).ToList();
        }//end method

        //count cards method 
        private int countCards(string l)
        {
            int count = 0;
            foreach(var c in Cards)
            {
                if (c.letter.Equals(l))
                {
                    count++;
                }
            }
            return count;
        }//end method

        
        internal Card drawTopCard()
        {
            Card topCard = Cards[0];
            Cards.RemoveAt(0);
            return topCard;
        }
        internal Card getTopDiscard()
        {
            Card c = topDiscard;
            topDiscard = null;
            return c;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    checker.Quit();
                }               
                disposedValue = true;
            }
        }
        //dispode method 
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }//end method    
        //toString method
        public override string ToString()
        {
            return 
                $"a({countCards("a")})\tb({countCards("b")})\tc({countCards("c")})\td({countCards("d")})\te({countCards("e")})\tf({countCards("f")})\tg({countCards("g")})\t" +
                            $"h({countCards("h")})\ti({countCards("i")})\tj({countCards("j")})\tk({countCards("k")})\tl({countCards("l")})\n" +
                $"m({countCards("m")})\tn({countCards("n")})\to({countCards("o")})\tp({countCards("p")})\tq({countCards("q")})\tr({countCards("r")})\ts({countCards("s")})\t" +
                            $"t({countCards("t")})\tu({countCards("u")})\tv({countCards("v")})\tw({countCards("w")})\tx({countCards("x")})\n" + 
                $"y({countCards("y")})\tz({countCards("z")})\tcl({countCards("cl")})\ter({countCards("er")})\tin({countCards("in")})\tqu({countCards("qu")})\tth({countCards("th")})\t\n";
        }//end method
    }//end class
}
